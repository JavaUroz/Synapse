from motor.motor_asyncio import AsyncIOMotorClient
import os
from dotenv import load_dotenv

load_dotenv()

MONGO_URL = os.getenv("MONGO_URL", "mongodb://synapse_admin:AmulioEneas_22!@localhost:27017")

client = AsyncIOMotorClient(MONGO_URL)
db = client["synapse"]
reviews_collection = db["code_reviews"]