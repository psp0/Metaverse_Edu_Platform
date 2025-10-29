from sqlalchemy import Column, Integer, String, Text, ForeignKey
from sqlalchemy.orm import Mapped, mapped_column, relationship
from db_config import Base # db_config.py에서 Base 임포트

# 소단원 그룹 테이블
class SubunitGroup(Base):
    __tablename__ = 'subunit_groups'
    id: Mapped[int] = mapped_column(Integer, primary_key=True, autoincrement=True)
    # MySQL은 문자열 길이를 지정하는 것이 좋음
    name: Mapped[str] = mapped_column(String(255), unique=True, index=True)
    
    problems = relationship("Problem", back_populates="subunit_group")

# 문제 테이블
class Problem(Base):
    __tablename__ = 'problems'
    
    id: Mapped[str] = mapped_column(String(50), primary_key=True, index=True) 
    textbody: Mapped[str] = mapped_column(Text) 
    options: Mapped[str] = mapped_column(Text) # 선택지는 길 수 있으므로 Text
    answer: Mapped[str] = mapped_column(String(255)) 
    
    # SyntaxError를 일으켰던 [cite] 태그가 제거되었습니다.
    level: Mapped[str] = mapped_column(String(10), index=True) # "2", "3" 등
    
    detail: Mapped[str] = mapped_column(Text) # jsonl에 있던 detail 필드
    
    subunit_group_id: Mapped[int] = mapped_column(Integer, ForeignKey('subunit_groups.id'), index=True)
    subunit_group = relationship("SubunitGroup", back_populates="problems")
