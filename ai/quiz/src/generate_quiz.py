import os
import json
import time
import logging
import random
from dotenv import load_dotenv
import mysql.connector
from openai import OpenAI

# Configure logging
logging.basicConfig(
    level=logging.INFO, 
    format='%(asctime)s - %(levelname)s - %(message)s',
    handlers=[
        logging.StreamHandler(),
        logging.FileHandler('/app/quiz_generator.log')
    ]
)

# Load environment variables from project root
load_dotenv('/app/.env')

# Initialize OpenAI client
try:
    client = OpenAI(api_key=os.getenv('OPENAI_API_KEY'))
except Exception as e:
    logging.error(f'Failed to initialize OpenAI client: {e}')
    client = None

def generate_quiz(sub_unit_id, sub_title, description):
    """
    Generate a quiz for a specific sub-unit using OpenAI API
    """
    prompt = f"""Create a multiple-choice quiz question about {sub_title}. 
    Context: {description}
    
    Requirements:
    - Create a single quiz question
    - Provide 4 answer options (A, B, C, D)
    - Mark the correct answer
    - Write a brief explanation for the correct answer
    
    Output format (JSON):
    {{
        "content": "Quiz question text",
        "options": [
            {{"label": "A", "text": "Option A text", "is_correct": false}},
            {{"label": "B", "text": "Option B text", "is_correct": true}},
            ...
        ],
        "explanation": "Explanation for the correct answer"
    }}
    """
    
    try:
        response = client.chat.completions.create(
            model="gpt-3.5-turbo-0125",
            response_format={"type": "json_object"},
            messages=[
                {"role": "system", "content": "You are a helpful assistant that generates educational math quizzes."},
                {"role": "user", "content": prompt}
            ]
        )
        
        quiz_data = json.loads(response.choices[0].message.content)
        return {
            'sub_unit_id': sub_unit_id,
            'content': quiz_data['content'],
            'options': quiz_data['options'],
            'explanation': quiz_data['explanation']
        }
    except Exception as e:
        print(f"Error generating quiz: {e}")
        return None

def save_quiz_to_db(quiz_data):
    """
    Save generated quiz to the database
    """
    try:
        # Establish database connection
        connection = mysql.connector.connect(
            host=os.getenv('MYSQL_HOST', 'mysql'),
            user=os.getenv('MYSQL_USER', 'mepuser'),
            password=os.getenv('MYSQL_PASSWORD'),
            database=os.getenv('MYSQL_DATABASE', 'mep_db')
        )
        
        cursor = connection.cursor()
        
        # Insert quiz
        quiz_query = "INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (%s, %s)"
        cursor.execute(quiz_query, (quiz_data['sub_unit_id'], quiz_data['content']))
        quiz_id = cursor.lastrowid
        
        # Insert quiz options
        option_query = "INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES (%s, %s, %s, %s)"
        option_values = [
            (quiz_id, option['label'], option['text'], option['is_correct']) 
            for option in quiz_data['options']
        ]
        cursor.executemany(option_query, option_values)
        
        # Insert explanation
        explanation_query = "INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES (%s, %s)"
        cursor.execute(explanation_query, (quiz_id, quiz_data['explanation']))
        
        connection.commit()
        print(f"Quiz saved successfully with ID: {quiz_id}")
        return quiz_id
    
    except mysql.connector.Error as err:
        print(f"Database error: {err}")
        return None
    
    finally:
        if connection.is_connected():
            cursor.close()
            connection.close()

def main():
    # Example sub-units from the given data
    sub_units = [
        {"id": 201, "title": "다항식의 연산", "description": "다항식의 덧셈·뺄셈·곱셈·나눗셈 규칙을 학습한다."},
        {"id": 202, "title": "나머지정리·인수정리", "description": "다항식을 다른 다항식으로 나눌 때의 나머지와 인수를 찾는다."},
        {"id": 203, "title": "인수분해·항등식", "description": "다항식을 인수분해하고 항등식을 활용하여 식을 변형한다."}
    ]
    
    # Generate and save a quiz for each sub-unit
    for unit in sub_units:
        quiz_data = generate_quiz(unit['id'], unit['title'], unit['description'])
        if quiz_data:
            save_quiz_to_db(quiz_data)

if __name__ == "__main__":
    main()
