import json
from sqlalchemy import select
# 모든 'from .'에서 점(.) 제거
from db_config import engine, SessionLocal, Base
from models import SubunitGroup, Problem
from wait_for_db import check_db_connection # DB 연결 확인

def load_data_from_jsonl(session, filepath: str):
    print("데이터 로딩 시작...")
    
    subunit_group_cache = {}
    existing_groups = session.scalars(select(SubunitGroup)).all()
    for group in existing_groups:
        subunit_group_cache[group.name] = group.id
    print(f"기존 소단원 그룹 {len(subunit_group_cache)}개 캐시됨.")

    problem_count = 0
    with open(filepath, 'r', encoding='utf-8') as f:
        for line in f:
            try:
                data = json.loads(line.strip())
            except json.JSONDecodeError:
                print(f"잘못된 형식의 라인 스킵: {line}")
                continue

            subunit_title = data.get('subunit_title')
            if not subunit_title:
                print(f"'subunit_title'이 없는 문제 ({data.get('id')}) 스킵")
                continue
                
            subunit_group_id = subunit_group_cache.get(subunit_title)
            
            if subunit_group_id is None:
                new_group = SubunitGroup(name=subunit_title)
                session.add(new_group)
                session.flush()
                subunit_group_id = new_group.id
                subunit_group_cache[subunit_title] = subunit_group_id
                print(f"새 소단원 그룹 생성: '{subunit_title}' (ID: {subunit_group_id})")

            existing_problem = session.get(Problem, data['id'])
            if existing_problem:
                continue

            # 이 부분이 INSERT 문을 생성합니다.
            new_problem = Problem(
                id=data['id'],
                textbody=data['textbody'],
                options=data['options'],
                answer=data['answer'],
                level=data['level'], # API 예시와 jsonl을 따라 문자열로 저장
                detail=data['detail'],
                subunit_group_id=subunit_group_id
            )
            session.add(new_problem)
            problem_count += 1
            
            if problem_count % 1000 == 0:
                session.commit()
                print(f"{problem_count}개 문제 커밋...")

    session.commit()
    print(f"총 {problem_count}개의 새 문제를 성공적으로 로드 및 커밋했습니다.")

def main():
    print("DB 연결 대기...")
    check_db_connection() # DB가 준비되었는지 확인
    
    # 이 부분이 CREATE TABLE 문을 실행합니다.
    print("테이블 생성 시도 (이미 존재하면 스킵)...")
    Base.metadata.create_all(bind=engine)
    print("테이블 생성 완료.")

    session = SessionLocal()
    try:
        # docker-compose에서 마운트한 경로
        jsonl_file_path = "your_problems.jsonl" 
        load_data_from_jsonl(session, jsonl_file_path)
    except FileNotFoundError:
        print(f"오류: '{jsonl_file_path}' 파일을 찾을 수 없습니다.")
        print("docker-compose.yml의 volumes 설정을 확인하세요.")
    except Exception as e:
        print(f"오류 발생: {e}")
        session.rollback()
    finally:
        session.close()

if __name__ == "__main__":
    main()