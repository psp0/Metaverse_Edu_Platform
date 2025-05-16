# Quiz Generator

## Setup

1. Create a virtual environment
```bash
python3 -m venv venv
source venv/bin/activate
```

2. Install dependencies
```bash
pip install -r requirements.txt
```

3. Use the main project's `.env` file
   - The quiz generator uses environment variables from the project's root `.env` file
   - Ensure the following variables are set in the main `.env`:
     * `OPENAI_API_KEY`
     * `MYSQL_DATABASE`
     * `MYSQL_USER`
     * `MYSQL_PASSWORD`

## Running the Quiz Generator
```bash
python src/generate_quiz.py
```

## Prerequisites
- Python 3.8+
- MySQL Database
- OpenAI API Key
