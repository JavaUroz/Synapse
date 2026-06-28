from typing import TypedDict, Annotated
from langgraph.graph import StateGraph, END
from langchain_anthropic import ChatAnthropic
from langchain_core.messages import HumanMessage
import os
from dotenv import load_dotenv

load_dotenv()

# --- State ---
class ReviewState(TypedDict):
    pr_url: str
    diff: str
    analysis: str
    report: str

# --- Model ---
model = ChatAnthropic(
    model="claude-haiku-4-5",
    api_key=os.getenv("ANTHROPIC_API_KEY")
)

# --- Nodes ---
def fetch_pr(state: ReviewState) -> ReviewState:
    """Simula la obtención del diff de un PR (mock por ahora)."""
    mock_diff = """
    diff --git a/src/Service.cs b/src/Service.cs
    + public class UserService {
    +     public void ProcessUser(User user) {
    +         // validate
    +         if (user.Email == null) throw new Exception("Invalid");
    +         // save
    +         var db = new DatabaseContext();
    +         db.Users.Add(user);
    +         db.SaveChanges();
    +         // send email
    +         var smtp = new SmtpClient();
    +         smtp.Send(user.Email, "Welcome!");
    +     }
    + }
    """
    return {**state, "diff": mock_diff}


def analyze_code(state: ReviewState) -> ReviewState:
    """Claude analiza el diff buscando violaciones SOLID."""
    prompt = f"""You are a senior software engineer reviewing a pull request.
    
Analyze the following code diff and identify:
1. SOLID principle violations
2. Design pattern opportunities  
3. Specific improvement suggestions

Code diff:
{state['diff']}

Be specific and actionable in your feedback."""

    response = model.invoke([HumanMessage(content=prompt)])
    return {**state, "analysis": response.content}


def format_report(state: ReviewState) -> ReviewState:
    """Formatea el análisis como un reporte estructurado."""
    prompt = f"""Format the following code review analysis as a structured report with:
- **Summary**: One line overview
- **Issues Found**: Numbered list of problems
- **Suggestions**: Specific code improvements
- **Score**: Overall code quality score (1-10)

Analysis:
{state['analysis']}"""

    response = model.invoke([HumanMessage(content=prompt)])
    return {**state, "report": response.content}


# --- Graph ---
def build_graph():
    graph = StateGraph(ReviewState)
    graph.add_node("fetch_pr", fetch_pr)
    graph.add_node("analyze_code", analyze_code)
    graph.add_node("format_report", format_report)
    graph.set_entry_point("fetch_pr")
    graph.add_edge("fetch_pr", "analyze_code")
    graph.add_edge("analyze_code", "format_report")
    graph.add_edge("format_report", END)

    return graph.compile()


reviewer_graph = build_graph()