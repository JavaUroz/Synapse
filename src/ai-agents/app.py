from fastapi import FastAPI
from pydantic import BaseModel
from datetime import datetime, timezone
from agents.code_reviewer.graph import reviewer_graph
from db.mongo import reviews_collection

app = FastAPI(title="Synapse AI Agents", version="1.0.0")

class ReviewRequest(BaseModel):
    pr_url: str

class ReviewResponse(BaseModel):
    pr_url: str
    report: str
    review_id: str

@app.get("/health")
def health():
    return {"status": "ok", "service": "synapse-ai-agents"}

@app.post("/api/agents/code-reviewer", response_model=ReviewResponse)
async def review_code(request: ReviewRequest):
    result = await reviewer_graph.ainvoke({
        "pr_url": request.pr_url,
        "diff": "",
        "analysis": "",
        "report": ""
    })

    # Persistir en MongoDB
    doc = {
        "pr_url": request.pr_url,
        "report": result["report"],
        "diff": result["diff"],
        "created_at": datetime.now(timezone.utc)
    }
    inserted = await reviews_collection.insert_one(doc)

    return ReviewResponse(
        pr_url=request.pr_url,
        report=result["report"],
        review_id=str(inserted.inserted_id)
    )

@app.get("/api/agents/code-reviewer/history")
async def get_history():
    reviews = []
    async for doc in reviews_collection.find().sort("created_at", -1).limit(20):
        reviews.append({
            "id": str(doc["_id"]),
            "pr_url": doc["pr_url"],
            "created_at": doc["created_at"].isoformat(),
            "report_preview": doc["report"][:200] + "..."
        })
    return reviews