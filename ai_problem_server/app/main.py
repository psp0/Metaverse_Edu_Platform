from fastapi import FastAPI, Depends, Query
from pydantic import BaseModel, ConfigDict
from sqlalchemy import select, func
from sqlalchemy.orm import Session
from typing import List, Dict

# 로컬 파일에서 임포트 (절대 경로)
from db_config import get_db
from models import Problem

# --- 1. Pydantic 응답 모델 정의 ---
# API 명세서의 응답 형식 [cite: 10-16]
class ProblemResponse(BaseModel):
    # Pydantic v2 스타일로 orm_mode 설정
    model_config = ConfigDict(from_attributes=True) 

    id: str
    textbody: str
    options: str
    answer: str
    level: str # API 예시가 "2"이므로 str

# --- 2. FastAPI 앱 ---
app = FastAPI(title="문제 은행 API 서버")

@app.get("/")
def read_root():
    return {"message": "문제 은행 API 서버입니다. /docs 에서 API 문서를 확인하세요."}

# --- 3. API 엔드포인트 구현 ---

# 1. 문제 재고 확인 API
@app.get(
    "/problems/inventory", 
    response_model=Dict[str, int],
    summary="문제 재고 확인 API"
)
async def get_problems_inventory(
    # [수정됨] Spring이 보내는 'subUnitGroupId' (U 대문자)로 변수명 변경
    subUnitGroupId: int = Query(..., description="소단원 그룹 ID"), 
    db: Session = Depends(get_db)
):
    """
    특정 소단원 그룹(subUnitGroupId)의 레벨별 문제 개수(재고)를 확인합니다.
    응답은 레벨을 키로, 개수를 값으로 갖는 JSON 객체입니다.
    """
    query = (
        select(Problem.level, func.count(Problem.id))
        # [수정됨] 쿼리에서도 변경된 변수명(subUnitGroupId) 사용
        .where(Problem.subunit_group_id == subUnitGroupId) 
        .group_by(Problem.level)
    )
    results = db.execute(query).all() # [("2", 150), ("3", 200)]
    
    # API 명세서 형식으로 변환 [cite: 40-43]
    inventory = {level: count for level, count in results}
    return inventory

# 2. 문제 목록 조회 API
@app.get(
    "/problems", 
    response_model=List[ProblemResponse],
    summary="문제 목록 조회 API"
)
async def get_problems_list(
    # [수정됨] Spring이 보내는 'subUnitGroupId' (U 대문자)로 변수명 변경
    subUnitGroupId: int = Query(..., description="소단원 그룹 ID"),
    level: int = Query(..., description="문제 난이도 레벨"),
    count: int = Query(..., description="요청할 문제 개수"),
    db: Session = Depends(get_db)
):
    """
    특정 소단원 그룹, 레벨, 개수에 맞는 문제 목록을 반환합니다.
    (매번 랜덤으로 섞어서 반환합니다.)
    """
    # API는 level을 정수(int)로 받지만, DB에는 문자열(str)로 저장했으므로 변환
    level_str = str(level)
    
    query = (
        select(Problem)
        .where(
            # [수정됨] 쿼리에서도 변경된 변수명(subUnitGroupId) 사용
            Problem.subunit_group_id == subUnitGroupId, 
            Problem.level == level_str
        )
        .order_by(func.random())  # 랜덤 섞기
        .limit(count)
    )
    
    problems = db.scalars(query).all()
    return problems
