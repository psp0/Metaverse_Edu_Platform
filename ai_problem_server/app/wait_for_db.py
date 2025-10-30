import time
from sqlalchemy import create_engine, text
from sqlalchemy.exc import OperationalError
from tenacity import retry, stop_after_delay, wait_fixed
from db_config import DATABASE_URL

print("Waiting for database connection...")

@retry(stop=stop_after_delay(60), wait=wait_fixed(1))
def check_db_connection():
    try:
        engine = create_engine(DATABASE_URL)
        with engine.connect() as conn:
            conn.execute(text("SELECT 1"))
        print("Database connection successful!")
    except OperationalError as e:
        print(f"Database connection failed: {e}. Retrying...")
        raise

if __name__ == "__main__":
    check_db_connection()
