from fastapi import FastAPI
from pydantic import BaseModel
from agents.code_reviewer.graph import reviewer_graph

app = FastAPI(title="Synapse AI Agents", version="1.0.0")

class ReviewRequest(BaseModel):
    pr_url: str

class ReviewResponse(BaseModel):
    pr_url: str
    report: str

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
    return ReviewResponse(pr_url=request.pr_url, report=result["report"])