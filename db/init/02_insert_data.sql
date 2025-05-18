USE mep_db;

-- Insert into chapter table
INSERT INTO `unit` (`unit_id`, `chapter_no`, `title`, `description`) VALUES
(201, 201, '다항식', '다항식의 기본 연산과 인수정리를 통해 다항식 구조를 이해한다.'),
(202, 202, '방정식과 부등식', '복소수를 포함한 다양한 방정식과 부등식의 풀이 방법을 학습한다.'),
(203, 203, '도형과 방정식', '좌표평면에서 도형을 방정식으로 표현하고 위치 관계를 분석한다.'),
(204, 204, '집합과 명제', '집합 연산과 논리적 명제의 구조를 이해한다.'),
(205, 205, '함수와 그래프', '함수 개념과 그래프 해석 능력을 기른다.'),
(206, 206, '경우의 수', '사건 발생 경우를 수학적으로 계산하는 방법을 익힌다.'),
(207, 207, '지수와 로그 함수', '지수와 로그의 개념 및 그래프를 이해하고 방정식을 푼다.'),
(208, 208, '삼각함수', '삼각함수의 그래프와 주기·변환을 활용한다.'),
(209, 209, '수열', '수열의 규칙성과 합을 분석하고 증명한다.'),
(210, 210, '함수의 극한과 연속', '함수의 극한 값을 구하고 연속성을 탐구한다.'),
(211, 211, '미분', '도함수를 이용해 함수의 변화를 해석한다.'),
(212, 212, '적분', '적분의 개념과 정적분을 활용한다.'),
(213, 213, '경우의 수(확통)', '확률 계산을 위한 경우의 수 원리를 학습한다.'),
(214, 214, '확률', '확률의 개념과 조건부확률, 베이즈 정리를 이해한다.'),
(215, 215, '통계', '자료 분석과 통계적 추정을 수행한다.'),
(216, 216, '이차곡선', '원·타원·쌍곡선·포물선의 방정식과 성질을 탐구한다.'),
(217, 217, '평면벡터', '평면벡터 연산과 내적을 통해 도형 문제를 해결한다.'),
(218, 218, '공간도형과 공간좌표', '3차원 공간의 벡터와 도형을 분석한다.');

-- Sub_unit table INSERT statements
INSERT INTO `sub_unit` (`sub_unit_id`, `unit_id`, `sub_chapter_no`, `sub_title`, `description`) VALUES
(20101, 201, 1, '다항식의 연산', '다항식의 덧셈·뺄셈·곱셈·나눗셈 규칙을 학습한다.'),
(20102, 201, 2, '나머지정리·인수정리', '다항식을 다른 다항식으로 나눌 때의 나머지와 인수를 찾는다.'),
(20103, 201, 3, '인수분해·항등식', '다항식을 인수분해하고 항등식을 활용하여 식을 변형한다.'),
(20201, 202, 1, '복소수와 이차방정식', '복소수 개념과 이차방정식의 근을 구한다.'),
(20202, 202, 2, '여러 가지 방정식', '분수·무리·삼차 등 여러 유형의 방정식을 해결한다.'),
(20203, 202, 3, '부등식의 해석·연립부등식', '부등식 영역 해석 및 연립부등식 풀이를 익힌다.'),
(20301, 203, 1, '직선의 방정식·거리·기울기', '직선의 방정식, 두 점 사이 거리, 기울기를 다룬다.'),
(20302, 203, 2, '원의 방정식·이동', '원의 표준형 방정식과 평행 이동을 학습한다.'),
(20303, 203, 3, '도형과 직선·원의 위치 관계', '도형과 직선 및 원 사이의 위치 관계를 판별한다.'),
(20401, 204, 1, '집합의 표현과 연산', '집합을 기호로 표현하고 합·교·차 연산을 수행한다.'),
(20402, 204, 2, '명제·조건·역·대우', '명제의 참·거짓과 조건·역·대우를 파악한다.'),
(20403, 204, 3, '필요충분조건', '필요조건과 충분조건을 구분하고 판별한다.'),
(20501, 205, 1, '함수의 뜻·표현', '함수의 정의와 식·표·그래프 표현을 익힌다.'),
(20502, 205, 2, '여러 가지 함수', '절댓값 함수 등 다양한 함수의 특징을 학습한다.'),
(20503, 205, 3, '합성함수·역함수', '합성함수와 역함수의 정의와 성질을 이해한다.'),
(20601, 206, 1, '순열·조합 기본법칙', '합·곱의 법칙과 순열·조합을 적용한다.'),
(20602, 206, 2, '중복순열·중복조합', '반복 허용 순열·조합을 계산한다.'),
(20603, 206, 3, '이항정리·파스칼삼각형', '이항정리를 전개하고 파스칼 삼각형을 활용한다.'),
(20701, 207, 1, '지수법칙·지수방정식·지수그래프', '지수법칙과 그래프, 지수방정식 풀이를 다룬다.'),
(20702, 207, 2, '로그의 정의·성질·로그방정식·로그그래프', '로그의 기본 성질과 그래프, 로그방정식을 학습한다.'),
(20801, 208, 1, '삼각비의 뜻과 그래프', '삼각비의 정의와 그래프를 이해한다.'),
(20802, 208, 2, '삼각함수 y=sin x, cos x, tan x', '기본 삼각함수의 특징을 익힌다.'),
(20803, 208, 3, '삼각함수의 활용(주기, 변환, 합성)', '삼각함수의 주기성, 그래프 변환, 합성을 다룬다.'),
(20901, 209, 1, '등차·등비수열', '등차·등비수열의 일반항과 합을 구한다.'),
(20902, 209, 2, '시그마 계산 기법', '시그마 표기법을 이용해 수열의 합을 계산한다.'),
(20903, 209, 3, '귀납적 정의(점화식)와 계차 수열', '점화식과 계차 수열을 분석한다.'),
(21001, 210, 1, '극한의 정의·성과', '극한의 기본 정의와 성질을 학습한다.'),
(21002, 210, 2, '무한대/제로 극한·좌우극한', '무한·0 극한 및 좌우극한을 다룬다.'),
(21003, 210, 3, '함수의 연속·점근선', '연속성 판정과 점근선을 이해한다.'),
(21101, 211, 1, '미분계수와 도함수', '미분계수 개념과 도함수 계산을 익힌다.'),
(21102, 211, 2, '여러 함수의 미분법(합·곱·체인룰)', '합, 곱, 체인룰을 활용한 미분법을 학습한다.'),
(21103, 211, 3, '접선·증가·감소·극값·평균값 정리', '도함수를 이용해 그래프의 형태를 분석한다.'),
(21201, 212, 1, '부정적분·정적분의 뜻', '적분의 기본 개념과 표기를 다룬다.'),
(21202, 212, 2, '기본 정리·치환·부분적분', '적분 기본정리와 치환·부분적분 기법을 학습한다.'),
(21203, 212, 3, '정적분의 활용(넓이·부피)', '정적분으로 넓이·부피를 구한다.'),
(21301, 213, 1, '합·곱의 법칙', '경우의 수 기본 원칙을 적용한다.'),
(21302, 213, 2, '순열·조합·이항정리 재복습', '순열·조합 및 이항 전개를 복습한다.'),
(21303, 213, 3, '중복·원순열·분할수', '중복 순열, 원순열, 분할수를 학습한다.'),
(21401, 214, 1, '확률의 뜻·고전적 정의', '확률의 기본 정의와 성질을 학습한다.'),
(21402, 214, 2, '조건부확률·베이즈정리', '조건부확률과 베이즈 정리를 적용한다.'),
(21403, 214, 3, '독립·종속 사건', '독립·종속 사건과 전환 법칙을 다룬다.'),
(21501, 215, 1, '자료의 정리(평균·분산)', '기초 통계량을 계산하고 해석한다.'),
(21502, 215, 2, '표본·모집단·표본분포', '표본분포와 중심극한정리를 학습한다.'),
(21503, 215, 3, '통계적 추정(신뢰구간)', '신뢰구간을 이용해 모수를 추정한다.'),
(21601, 216, 1, '원·타원의 표준형·이동', '원과 타원의 방정식을 표준형과 이동형으로 학습한다.'),
(21602, 216, 2, '쌍곡선·포물선의 성질', '쌍곡선과 포물선의 정의와 특징을 이해한다.'),
(21603, 216, 3, '이차곡선 간 위치관계', '이차곡선의 교점과 응용 문제를 해결한다.'),
(21701, 217, 1, '벡터의 정의·합·차·실수배', '벡터의 기본 개념과 연산을 학습한다.'),
(21702, 217, 2, '내적·벡터의 성분과 성립조건', '벡터 내적과 성분을 이용한 조건을 다룬다.'),
(21703, 217, 3, '평면도형 벡터 풀이', '삼각형·사각형을 벡터로 풀이한다.'),
(21801, 218, 1, '공간벡터·평행·수직 조건', '공간벡터의 연산과 평행·수직 조건을 학습한다.'),
(21802, 218, 2, '직선·평면의 방정식과 교점', '공간에서 직선·평면 방정식을 다룬다.'),
(21803, 218, 3, '공간도형의 거리·각·부피 계산', '공간도형의 거리, 각도, 부피를 계산한다.');


INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20101, '다음 중 다항식 (3x^2 + 2x - 5) + (x^2 - 4x + 1)의 올바른 결과는?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20101_1234567890', 'A', '4x^2 - 2x - 6', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20101_1234567890', 'B', '4x^2 - 2x - 4', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20101_1234567890', 'C', '2x^2 - 2x - 4', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20101_1234567890', 'D', '4x^2 + 6x - 4', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20101_1234567890', '동일한 차수끼리 항을 더하면 3x^2 + x^2 = 4x^2, 2x - 4x = -2x, -5 + 1 = -4가 되어 최종 결과는 4x^2 - 2x - 4이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20102, '다항식 f(x) = 2x^3 + 3x^2 - 5x + 6을 x - 2로 나눌 때의 나머지는?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20102_1234567891', 'A', '0', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20102_1234567891', 'B', '24', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20102_1234567891', 'C', '-4', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20102_1234567891', 'D', '10', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20102_1234567891', '나머지정리에 따라 다항식 f(x)를 x - 2로 나눌 때의 나머지는 f(2)이다. 계산하면 f(2) = 2×8 + 3×4 - 5×2 + 6 = 24가 되어 정답은 24이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20103, '다항식 x^2 + 6x + 9를 인수분해한 결과로 옳은 것은?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20103_1234567892', 'A', '(x + 3)(x + 3)', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20103_1234567892', 'B', '(x + 1)(x + 9)', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20103_1234567892', 'C', '(x + 2)(x + 4)', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20103_1234567892', 'D', '(x - 3)(x - 3)', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20103_1234567892', '다항식 x^2 + 6x + 9는 완전제곱식이며, (x + 3)^2 = (x + 3)(x + 3)로 인수분해된다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20201, '이차방정식 x^2 + 4x + 5 = 0의 근은?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20201_1234567893', 'A', '-2 ± i', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20201_1234567893', 'B', '-2 ± √5', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20201_1234567893', 'C', '-4 ± i', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20201_1234567893', 'D', '-2 ± √-5', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20201_1234567893', '근의 공식을 사용하면 x = (-4 ± √(16 - 20))/2 = (-4 ± √-4)/2 = (-4 ± 2i)/2 = -2 ± i이므로 정답은 -2 ± i이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20202, '무리방정식 √(x + 1) = x - 1의 해는?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20202_1234567894', 'A', '3', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20202_1234567894', 'B', '4', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20202_1234567894', 'C', '2', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20202_1234567894', 'D', '해가 없다', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20202_1234567894', '양변을 제곱하면 x + 1 = (x - 1)^2가 되고, 정리하면 x = 0 또는 x = 3이다. 그러나 x = 0은 원래 식에 대입 시 성립하지 않으므로, 유일한 해는 x = 3이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20203, '다음 연립부등식의 해는?  x - 2 > 1  그리고  2x + 3 < 9');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20203_1234567895', 'A', 'x > 3', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20203_1234567895', 'B', 'x < 3', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20203_1234567895', 'C', '1 < x < 3', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20203_1234567895', 'D', '해가 없다', TRUE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20203_1234567895', 'x - 2 > 1는 x > 3, 2x + 3 < 9는 x < 3이므로, 두 조건을 동시에 만족하는 x는 존재하지 않는다. 따라서 해가 없다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20301, '두 점 A(1, 2), B(4, 8)을 지나는 직선의 기울기는?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20301_1234567896', 'A', '3', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20301_1234567896', 'B', '2', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20301_1234567896', 'C', '2/3', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20301_1234567896', 'D', '3/2', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20301_1234567896', '기울기 공식 m = (y₂ - y₁)/(x₂ - x₁)를 사용하여 (8 - 2)/(4 - 1) = 6/3 = 2가 되어 정답은 2이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20302, '다음 중 중심이 (2, -3), 반지름의 길이가 5인 원의 방정식으로 옳은 것은?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20302_1234567897', 'A', '(x - 2)^2 + (y + 3)^2 = 25', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20302_1234567897', 'B', '(x + 2)^2 + (y - 3)^2 = 25', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20302_1234567897', 'C', '(x - 2)^2 + (y + 3)^2 = 5', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20302_1234567897', 'D', '(x - 2)^2 + (y - 3)^2 = 25', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20302_1234567897', '원의 표준형 방정식은 (x - a)^2 + (y - b)^2 = r^2이다. 중심이 (2, -3), 반지름이 5이므로 식은 (x - 2)^2 + (y + 3)^2 = 25이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20303, '다음 중 직선 y = 2x + 1과 원 (x - 1)^2 + (y - 2)^2 = 4의 위치 관계로 옳은 것은?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20303_1234567898', 'A', '서로 만나지 않는다', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20303_1234567898', 'B', '한 점에서 만난다', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20303_1234567898', 'C', '두 점에서 만난다', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20303_1234567898', 'D', '직선은 원의 중심을 지난다', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20303_1234567898', '직선 y = 2x + 1과 원의 중심 (1,2) 사이의 거리는 반지름보다 작지만 실제로 대입해 보면 한 점에서 접하게 된다. 따라서 위치 관계는 ''한 점에서 만난다''이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20401, '집합 A = {1, 2, 3, 4, 5}, B = {3, 4, 5, 6, 7}일 때, A ∩ B는?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20401_1234567890', 'A', '{1, 2}', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20401_1234567890', 'B', '{3, 4, 5}', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20401_1234567890', 'C', '{1, 2, 6, 7}', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20401_1234567890', 'D', '{1, 2, 3, 6, 7}', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20401_1234567890', '집합 A와 B의 공통 원소는 3, 4, 5이므로, A ∩ B는 {3, 4, 5}입니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20402, '다음 명제의 대우를 고르세요: "x가 짝수이면, x²는 짝수이다."');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20402_1234567891', 'A', 'x가 짝수가 아니면, x²는 짝수가 아니다.', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20402_1234567891', 'B', 'x²가 짝수이면, x는 짝수이다.', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20402_1234567891', 'C', 'x²가 짝수가 아니면, x는 짝수가 아니다.', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20402_1234567891', 'D', 'x가 짝수이고, x²도 짝수이다.', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20402_1234567891', '대우는 "조건의 결과를 부정하여 조건으로, 조건을 부정하여 결과로" 만드는 것입니다. 따라서 "x가 짝수이면 x²는 짝수이다"의 대우는 "x²가 짝수가 아니면 x는 짝수가 아니다"입니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20403, '다음 명제에서 "x가 3의 배수이다"는 "x는 6의 배수이다"의 어떤 조건인가?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20403_1234567892', 'A', '필요조건', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20403_1234567892', 'B', '충분조건', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20403_1234567892', 'C', '필요충분조건', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20403_1234567892', 'D', '조건이 아니다', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20403_1234567892', '"x가 6의 배수이면 x는 3의 배수이다"는 참이지만, 그 역인 "x가 3의 배수이면 x는 6의 배수이다"는 거짓이므로, "x가 3의 배수이다"는 "x는 6의 배수이다"의 필요조건입니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20501, '다음 중 함수가 아닌 것은 무엇인가?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20501_1234567893', 'A', 'y = 2x + 1', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20501_1234567893', 'B', 'y = x^2', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20501_1234567893', 'C', 'x = y^2', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20501_1234567893', 'D', '(1,2), (2,3), (3,4)로 구성된 관계', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20501_1234567893', '함수는 한 입력값에 하나의 출력값만을 대응시켜야 합니다. C의 식 x = y^2에서는 하나의 x값에 대해 두 개의 y값이 가능하므로 함수가 아닙니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20502, '다음 중 함수 y = |x - 2|의 그래프에 대한 설명으로 옳은 것은?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20502_1234567894', 'A', '그래프는 (0,0)을 꼭짓점으로 갖는다.', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20502_1234567894', 'B', '그래프는 x축에 평행한 직선이다.', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20502_1234567894', 'C', '그래프는 (2,0)을 꼭짓점으로 하는 V자 형태이다.', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20502_1234567894', 'D', '그래프는 원점을 지나고, 대칭축은 y축이다.', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20502_1234567894', '함수 y = |x - 2|는 x = 2에서 꼭짓점을 가지며, V자형 그래프입니다. 대칭축은 x = 2입니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20503, '함수 f(x) = 2x + 3의 역함수 f⁻¹(x)는 무엇인가?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20503_1234567895', 'A', 'f⁻¹(x) = (x + 3) / 2', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20503_1234567895', 'B', 'f⁻¹(x) = 2x - 3', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20503_1234567895', 'C', 'f⁻¹(x) = (x - 3) / 2', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20503_1234567895', 'D', 'f⁻¹(x) = x / 2 + 3', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20503_1234567895', '역함수를 구하기 위해 f(x) = 2x + 3을 y = 2x + 3으로 놓고 x에 대해 풀면 x = (y - 3)/2이므로, 역함수는 f⁻¹(x) = (x - 3)/2입니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20601, '5명의 학생 중에서 3명을 서로 다른 순서로 줄 세우는 방법의 수는?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20601_1234567896', 'A', '10', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20601_1234567896', 'B', '20', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20601_1234567896', 'C', '60', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20601_1234567896', 'D', '120', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20601_1234567896', '줄을 세우는 문제는 순서를 고려하는 순열입니다. 5명 중 3명을 순서 있게 뽑는 경우의 수는 P(5,3) = 5 × 4 × 3 = 60입니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20602, '4종류의 색 중에서 같은 색을 여러 번 사용할 수 있을 때, 3개의 구슬을 나란히 배열하는 방법의 수는?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20602_1234567897', 'A', '12', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20602_1234567897', 'B', '24', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20602_1234567897', 'C', '64', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20602_1234567897', 'D', '256', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20602_1234567897', '같은 색을 반복해서 사용할 수 있는 경우는 중복순열입니다. 가능한 색이 4가지이고, 3개의 구슬을 나란히 배열하므로 4^3 = 64가지 경우가 있습니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20603, '이항정리를 이용하여 (x + 2)^4을 전개할 때, x^2의 계수는 얼마인가?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20603_1716000000', 'A', '12', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20603_1716000000', 'B', '24', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20603_1716000000', 'C', '36', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20603_1716000000', 'D', '16', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20603_1716000000', '(x + 2)^4의 전개에서 x^2 항은 이항정리에 따라 \\binom{4}{2} x^2 \\cdot 2^2 = 6 \\cdot 4 = 24이므로, 계수는 24입니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20701, '다음 지수방정식의 해는? 2^{x+1} = 16');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20701_1716000000', 'A', '2', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20701_1716000000', 'B', '3', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20701_1716000000', 'C', '4', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20701_1716000000', 'D', '5', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20701_1716000000', '우변의 16을 밑이 같은 2의 거듭제곱인 2^4로 바꾸면, 2^{x+1} = 2^4가 됩니다. 지수끼리 비교하면 x + 1 = 4, 따라서 x = 3입니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20702, '다음 로그식의 값을 구하시오: log_2{8}');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20702_1716000000', 'A', '2', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20702_1716000000', 'B', '3', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20702_1716000000', 'C', '4', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20702_1716000000', 'D', '3/2', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20702_1716000000', 'log_2{8}은 x = 3을 의미하는데, 이는 2^3 = 8이므로 정답은 3입니다. 로그의 정의를 바탕으로 풀 수 있습니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20801, 'sin 30°의 값은 얼마인가?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20801_1716000000', 'A', '√3/2', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20801_1716000000', 'B', '1/2', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20801_1716000000', 'C', '1/√2', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20801_1716000000', 'D', '√2/2', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20801_1716000000', 'sin 30°는 기본 삼각비 중 하나로 값은 1/2입니다. 이는 단위원상에서 30° 위치의 y좌표이거나, 빗변이 1인 직각삼각형에서의 대변 비율로도 유도할 수 있습니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20802, '함수 y = sin x의 주기는 얼마인가?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20802_1716000000', 'A', 'π', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20802_1716000000', 'B', '2π', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20802_1716000000', 'C', 'π/2', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20802_1716000000', 'D', '4π', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20802_1716000000', '사인 함수 y = sin x는 2π마다 값을 반복하므로 주기는 2π입니다. 이는 그래프나 정의에서 쉽게 확인할 수 있습니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20803, '함수 y = sin(2x)의 주기는 얼마인가?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20803_1716000000', 'A', '2π', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20803_1716000000', 'B', 'π', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20803_1716000000', 'C', 'π/2', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20803_1716000000', 'D', '2π/3', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20803_1716000000', '함수 y = sin(2x)는 주기 공식 2π/|b|에서 b=2를 대입해 주기가 π임을 알 수 있습니다. 따라서 정답은 π입니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20901, '초항이 3이고 공차가 2인 등차수열의 10번째 항의 값은?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20901_1716000000', 'A', '19', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20901_1716000000', 'B', '20', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20901_1716000000', 'C', '21', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20901_1716000000', 'D', '23', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20901_1716000000', '등차수열의 일반항 공식 aₙ = a + (n - 1)d를 이용하면 10번째 항은 3 + 9×2 = 21입니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20902, '다음 시그마 값을 계산하시오: ∑_{k=1}^{5} (2k + 1)');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20902_1716000000', 'A', '30', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20902_1716000000', 'B', '33', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20902_1716000000', 'C', '35', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20902_1716000000', 'D', '37', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20902_1716000000', 'k = 1부터 5까지 대입하면 각 항은 3, 5, 7, 9, 11이며 이들을 모두 더하면 35입니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (20903, '다음과 같이 정의된 수열 aₙ의 네 번째 항 a₄의 값을 구하시오: a₁ = 2, aₙ₊₁ = aₙ + 3');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20903_1716000000', 'A', '9', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20903_1716000000', 'B', '10', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20903_1716000000', 'C', '11', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_20903_1716000000', 'D', '12', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_20903_1716000000', '점화식이 aₙ₊₁ = aₙ + 3이고 a₁ = 2이므로 a₂ = 5, a₃ = 8, a₄ = 11입니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21001, '다음 극한을 계산하시오: limₓ→2 (3x² - 4x + 1)');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21001_1716000000', 'A', '3', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21001_1716000000', 'B', '4', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21001_1716000000', 'C', '5', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21001_1716000000', 'D', '6', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21001_1716000000', '다항함수의 극한은 연속이므로, x에 2를 대입하여 계산하면 3(2)² - 4(2) + 1 = 12 - 8 + 1 = 5입니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21002, '다음 극한의 값을 구하시오: limₓ→0⁺ (1/x)');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21002_1716000000', 'A', '0', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21002_1716000000', 'B', '-∞', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21002_1716000000', 'C', '+∞', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21002_1716000000', 'D', '극한은 존재하지 않는다', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21002_1716000000', 'x가 0보다 큰 쪽에서 0으로 가까워지면 1/x는 매우 큰 양의 수가 되어 +∞로 발산합니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21003, '함수 f(x) = { x² - 1 (x ≠ 2), 3 (x = 2) }는 x = 2에서 연속인가?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21003_1716000000', 'A', '연속이다', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21003_1716000000', 'B', '불연속이며, 제거 가능 불연속이다', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21003_1716000000', 'C', '불연속이며, 점근 불연속이다', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21003_1716000000', 'D', '불연속이며, 점프 불연속이다', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21003_1716000000', '극한값과 함수값이 모두 3으로 같으므로, f(x)는 x = 2에서 연속이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21101, '함수 f(x) = x² + 2x의 x = 1에서의 미분계수를 구하시오.');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21101_1716000000', 'A', '2', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21101_1716000000', 'B', '3', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21101_1716000000', 'C', '4', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21101_1716000000', 'D', '5', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21101_1716000000', '도함수 f''(x) = 2x + 2를 x = 1에 대입하면 2(1) + 2 = 4로, 미분계수는 4입니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21102, '다음 함수의 도함수를 구하시오: f(x) = (x² + 1)·sin x');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21102_1716000000', 'A', 'f''(x) = 2x·sin x + (x² + 1)·cos x', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21102_1716000000', 'B', 'f''(x) = 2x·cos x + (x² + 1)·sin x', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21102_1716000000', 'C', 'f''(x) = (2x + cos x)·sin x', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21102_1716000000', 'D', 'f''(x) = (2x + sin x)·cos x', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21102_1716000000', '곱의 미분법 (uv)'' = u''v + uv''에 따라, u = x² + 1, v = sin x 이므로 f''(x) = 2x·sin x + (x² + 1)·cos x 가 됩니다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21103, '함수 f(x) = x³ - 3x²의 극값의 개수는?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21103_1716000000', 'A', '극값이 없다', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21103_1716000000', 'B', '극값이 1개 있다', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21103_1716000000', 'C', '극값이 2개 있다', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21103_1716000000', 'D', '극값이 3개 있다', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21103_1716000000', '도함수 f''(x) = 3x(x - 2)는 x = 0, 2에서 0이 되고, 부호 변화가 있으므로 극대와 극소가 각각 존재하여 극값은 총 2개이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21201, '다음 중 함수 f(x) = 3x²의 부정적분으로 옳은 것은?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21201_1716000000', 'A', 'x³ + C', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21201_1716000000', 'B', '3x³ + C', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21201_1716000000', 'C', 'x² + C', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21201_1716000000', 'D', '3⁄2·x² + C', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21201_1716000000', '3x²의 부정적분은 x³ + C이다. 이는 거듭제곱 함수의 부정적분 공식 ∫xⁿ dx = xⁿ⁺¹⁄(n+1) + C를 적용해 계산한 결과이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21202, '치환 적분을 이용하여 ∫2x·cos(x²) dx를 계산한 결과는?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21202_1716000000', 'A', 'cos(x²) + C', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21202_1716000000', 'B', 'sin(x²) + C', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21202_1716000000', 'C', '2sin(x) + C', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21202_1716000000', 'D', '½·sin(x²) + C', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21202_1716000000', 'u = x²로 치환하면 du = 2x dx가 되어 ∫2x·cos(x²) dx는 ∫cos(u) du = sin(u) + C가 되며, u = x²를 다시 대입하면 sin(x²) + C이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21203, '함수 f(x) = x²에 대해 구간 [0, 2]에서의 평균값은 얼마인가?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21203_1716000000', 'A', '8⁄3', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21203_1716000000', 'B', '2', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21203_1716000000', 'C', '4⁄3', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21203_1716000000', 'D', '2⁄3', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21203_1716000000', '구간 [0, 2]에서 함수 x²의 평균값은 (1⁄2) × ∫₀² x² dx = (1⁄2) × (8⁄3) = 4⁄3이다. 평균값 공식을 적용하면 된다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21301, '5개의 다른 책 중에서 2권을 순서 있게 고르는 방법의 수는?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21301_1716000000', 'A', '10', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21301_1716000000', 'B', '20', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21301_1716000000', 'C', '25', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21301_1716000000', 'D', '5', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21301_1716000000', '순서 있게 2권을 고르는 경우는 순열로 계산하며, 5P2 = 5 × 4 = 20이다. 조합과 혼동하지 않도록 주의해야 한다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21302, '이항정리를 이용하여 (x + 2)^3을 전개할 때, x²의 계수는 얼마인가?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21302_1716000000', 'A', '3', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21302_1716000000', 'B', '4', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21302_1716000000', 'C', '6', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21302_1716000000', 'D', '12', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21302_1716000000', '이항정리 (x + 2)^3에서 x² 항은 k = 1일 때 생성되며, 이때 항은 3 × x² × 2 = 6x²이므로 계수는 6이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21303, '4명을 원형 테이블에 앉히는 서로 다른 방법의 수는?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21303_1716000000', 'A', '12', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21303_1716000000', 'B', '6', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21303_1716000000', 'C', '24', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21303_1716000000', 'D', '4', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21303_1716000000', '원형 순열에서는 회전을 고려하여 중복을 제거하므로, 4명을 원형에 배치하는 경우는 (4-1)! = 3! = 6가지이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21401, '1부터 6까지의 숫자가 적힌 주사위를 한 번 던질 때, 2 또는 4가 나올 확률은?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21401_1716000000', 'A', '1/2', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21401_1716000000', 'B', '1/3', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21401_1716000000', 'C', '1/4', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21401_1716000000', 'D', '2/3', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21401_1716000000', '전체 경우의 수는 6이고, 2 또는 4가 나올 경우는 2가지이므로 확률은 2/6 = 1/3이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21402, '한 공장에서 생산된 제품 중 A기계에서 만든 것이 60%, B기계에서 만든 것이 40%이다. A기계의 불량률은 2%, B기계의 불량률은 5%이다. 임의로 뽑은 제품이 불량일 때, 그것이 B기계에서 만들어졌을 확률은?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21402_1716000000', 'A', '3/8', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21402_1716000000', 'B', '1/2', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21402_1716000000', 'C', '5/8', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21402_1716000000', 'D', '1/4', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21402_1716000000', '베이즈 정리에 따라, P(B|불량) = P(불량|B) * P(B) / P(불량) = (0.05*0.4)/(0.032) = 5/8.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21403, '어떤 두 사건 A와 B에 대하여 P(A) = 0.4, P(B) = 0.5, P(A ∩ B) = 0.2일 때, 두 사건 A와 B는 독립인가?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21403_1716000000', 'A', '독립이다', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21403_1716000000', 'B', '종속이다', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21403_1716000000', 'C', '확률 정보가 부족하여 판단할 수 없다', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21403_1716000000', 'D', '두 사건은 서로 배반이다', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21403_1716000000', '독립 사건은 P(A ∩ B) = P(A) × P(B)일 때 성립한다. 여기서 0.4 × 0.5 = 0.2 = P(A ∩ B)이므로, 두 사건은 독립이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21501, '다음은 어느 반 학생 5명의 수학 시험 점수이다: 70, 80, 90, 100, 110. 이 자료의 표준편차는 얼마인가?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21501_1716000000', 'A', '20', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21501_1716000000', 'B', '10√2', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21501_1716000000', 'C', '5√2', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21501_1716000000', 'D', '√2000', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21501_1716000000', '평균은 90이고, 각 편차의 제곱 평균은 200이므로, 표준편차는 √200 = 10√2이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21502, '어느 모집단의 평균이 60, 표준편차가 12일 때, 크기가 36인 표본을 여러 번 추출하여 표본평균의 분포를 만들었다. 이 표본평균의 표준편차(표준오차)는 얼마인가?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21502_1716000000', 'A', '12', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21502_1716000000', 'B', '6', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21502_1716000000', 'C', '2', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21502_1716000000', 'D', '12/36', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21502_1716000000', '표본평균의 표준편차(표준오차)는 모집단의 표준편차를 표본크기의 제곱근으로 나눈 값이다. 따라서 12 ÷ √36 = 2가 된다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21503, '어느 제품의 무게를 임의로 뽑은 100개의 표본으로 조사한 결과, 평균이 500g, 표준편차가 20g이었다. 이 표본을 바탕으로 95% 신뢰수준에서 모평균의 신뢰구간을 구하면? (단, Z_{0.025} = 1.96)');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21503_1716000000', 'A', '(495.00, 505.00)', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21503_1716000000', 'B', '(498.00, 502.00)', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21503_1716000000', 'C', '(494.12, 505.88)', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21503_1716000000', 'D', '(496.08, 503.92)', TRUE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21503_1716000000', '신뢰구간은 표본평균 ± Z값 × 표준오차로 계산한다. 표준오차는 2이므로, 500 ± 3.92 → (496.08, 503.92)가 정답이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21601, '다음 중 타원 \\( \\frac{(x-2)^2}{9} + \\frac{(y+1)^2}{4} = 1 \\)의 중심 좌표와 장축의 길이로 옳은 것은?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21601_1716000000', 'A', '중심: (–2, 1), 장축 길이: 6', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21601_1716000000', 'B', '중심: (2, –1), 장축 길이: 6', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21601_1716000000', 'C', '중심: (2, –1), 장축 길이: 4', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21601_1716000000', 'D', '중심: (–2, –1), 장축 길이: 3', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21601_1716000000', '표준형 타원 \\( \\frac{(x-h)^2}{a^2} + \\frac{(y-k)^2}{b^2} = 1 \\)에서 중심은 (h, k)이고 장축 길이는 2a이다. \\( h=2, k=-1, a=3 \\)이므로 정답은 중심 (2, –1), 장축 길이 6이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21602, '다음 중 포물선 \\( y^2 = 8x \\)의 초점 좌표로 옳은 것은?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21602_1716000000', 'A', '(–2, 0)', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21602_1716000000', 'B', '(2, 0)', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21602_1716000000', 'C', '(0, 2)', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21602_1716000000', 'D', '(0, –2)', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21602_1716000000', '포물선 \\( y^2 = 4px \\)의 초점은 \\( (p, 0) \\)이다. 주어진 식 \\( y^2 = 8x \\)에서 \\( 4p = 8 \\)이므로 \\( p = 2 \\), 따라서 초점은 (2, 0)이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21603, '다음 두 곡선 \\( y = x^2 \\)과 \\( y = 2x + 3 \\)의 교점의 x좌표를 모두 고르면?');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21603_1716000000', 'A', 'x = -1, 3', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21603_1716000000', 'B', 'x = 1, 3', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21603_1716000000', 'C', 'x = -3, 1', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21603_1716000000', 'D', 'x = -1, -3', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21603_1716000000', '두 함수 \\( y = x^2 \\)와 \\( y = 2x + 3 \\)의 교점을 구하려면 \\( x^2 = 2x + 3 \\)을 풀어야 한다. 정리하면 \\( x^2 - 2x - 3 = 0 \\)이 되고, 인수분해하여 \\( (x - 3)(x + 1) = 0 \\)에서 x = -1, 3이다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21701, '다음 벡터 \\( \\vec{a} = (2, -1) \\), \\( \\vec{b} = (-3, 4) \\)에 대해 \\( 2\\vec{a} + \\vec{b} \\)의 값을 구하시오.');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21701_1716000000', 'A', '(5, 3)', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21701_1716000000', 'B', '(1, 2)', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21701_1716000000', 'C', '(-1, -6)', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21701_1716000000', 'D', '(2, 1)', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21701_1716000000', '주어진 벡터 \\( \\vec{a} = (2, -1) \\), \\( \\vec{b} = (-3, 4) \\)에서 \\( 2\\vec{a} = (4, -2) \\)이고, 이를 \\( \\vec{b} \\)와 더하면 \\( (4 + (-3), -2 + 4) = (1, 2) \\)가 된다.');

INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21702, '벡터 \\( \\vec{a} = (3, 4) \\), \\( \\vec{b} = (x, 2) \\)가 서로 수직일 때, \\( x \\)의 값을 구하시오.');
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21702_1716000000', 'A', '-\\frac{8}{3}', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21702_1716000000', 'B', '-\\frac{3}{8}', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21702_1716000000', 'C', '\\frac{8}{3}', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21702_1716000000', 'D', '-4', FALSE);
INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21702_1716000000', '두 벡터가 수직이면 내적이 0이어야 하므로, \\( 3x + 8 = 0 \\rightarrow x = -\\frac{8}{3} \\)이다.');
INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21703, '삼각형 \\( ABC \\)에서 점 \\( D \\)는 변 \\( BC \\)의 중점이다. 벡터 \\( \\vec{AB} = \\vec{b} \\), \\( \\vec{AC} = \\vec{c} \\)일 때, 벡터 \\( \\vec{AD} \\)를 \\( \\vec{b}, \\vec{c} \\)로 나타낸 것은?');

INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21703_1716000000', 'A', '\\vec{b} + \\vec{c}', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21703_1716000000', 'B', '\\frac{1}{2} \\vec{b} - \\frac{1}{2} \\vec{c}', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21703_1716000000', 'C', '\\frac{1}{2} \\vec{b} + \\frac{1}{2} \\vec{c}', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21703_1716000000', 'D', '\\vec{b} - \\vec{c}', FALSE);

INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21703_1716000000', '중점 공식을 이용하면 \\( \\vec{BD} = \\frac{1}{2}(\\vec{c} - \\vec{b}) \\)이고, \\( \\vec{AD} = \\vec{AB} + \\vec{BD} = \\vec{b} + \\frac{1}{2}(\\vec{c} - \\vec{b}) = \\frac{1}{2} \\vec{b} + \\frac{1}{2} \\vec{c} \\)이다.');
INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21801, '공간벡터 \\( \\vec{a} = (2, -1, 3) \\), \\( \\vec{b} = (x, 2, -6) \\)가 서로 수직일 때, \\( x \\)의 값을 구하시오.');

INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21801_1716000000', 'A', '8', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21801_1716000000', 'B', '10', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21801_1716000000', 'C', '12', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21801_1716000000', 'D', '14', FALSE);

INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21801_1716000000', '벡터의 내적이 0이면 수직이다. \\( \\vec{a} \\cdot \\vec{b} = 2x - 2 - 18 = 0 \\)이므로 \\( x = 10 \\)이다.');
INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21802, '직선 \\( \\vec{r}(t) = (1, 2, -1) + t(2, -1, 3) \\)과 평면 \\( 2x - y + z = 5 \\)가 만나는 점의 좌표를 구하시오.');

INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21802_1716000000', 'A', '(3, 1, 2)', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21802_1716000000', 'B', '(2, 1.5, 1)', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21802_1716000000', 'C', '(2.5, 1.25, 1.25)', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21802_1716000000', 'D', '(1.5, 1.75, 0.5)', FALSE);

INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21802_1716000000', '직선의 각 성분을 평면 방정식에 대입하여 \\( t = \\frac{3}{4} \\)을 구하고, 이를 통해 교점 좌표 \\( (2.5, 1.25, 1.25) \\)를 계산한다.');
INSERT INTO `quiz` (`sub_unit_id`, `content`) VALUES (21803, '점 \\( A(1, 2, 3) \\)에서 직선 \\( \\vec{r}(t) = (4, 0, -1) + t(2, 1, 2) \\)까지의 거리를 구하시오.');

INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21803_1716000000', 'A', '\\( \\frac{\\sqrt{221}}{3} \\)', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21803_1716000000', 'B', '\\( \\frac{\\sqrt{245}}{3} \\)', TRUE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21803_1716000000', 'C', '\\( \\frac{\\sqrt{170}}{5} \\)', FALSE);
INSERT INTO `quiz_option` (`quiz_id`, `label`, `option_text`, `is_answer`) VALUES ('quiz_for_subunit_21803_1716000000', 'D', '\\( \\frac{\\sqrt{261}}{2} \\)', FALSE);

INSERT INTO `explanation` (`quiz_id`, `commentary`) VALUES ('quiz_for_subunit_21803_1716000000', '점과 직선 사이의 거리는, 점에서 직선 위 한 점까지의 벡터와 직선 방향벡터의 외적의 크기를 방향벡터의 크기로 나누어 계산하며, 이 문제에서는 \\( \\frac{\\sqrt{245}}{3} \\)이다.');

-- Insert into user table
INSERT INTO `user` (`user_no`, `user_id`, `created_at`, `updated_at`) VALUES
(1, 'user1', '2023-03-15 07:41:09', '2024-09-20 02:13:51'),
(2, 'user2', '2023-03-15 08:26:32', '2024-09-20 01:56:16'),
(3, 'user3', '2023-03-15 09:11:55', '2024-09-20 01:38:41'),
(4, 'user4', '2023-03-15 09:57:18', '2024-09-20 01:21:06'),
(5, 'user5', '2023-03-15 10:42:41', '2024-09-20 01:03:31');
(6, 'user6', '2023-03-15 11:28:04', '2024-09-20 00:45:56'),
(7, 'user7', '2023-03-15 12:13:27', '2024-09-20 00:28:21'),
(8, 'user8', '2023-03-15 12:58:50', '2024-09-20 00:10:46'),
(9, 'user9', '2023-03-15 13:44:13', '2024-09-19 23:53:11'),
(10, 'user10', '2023-03-15 14:29:36', '2024-09-19 23:35:36');
(11, 'user11', '2023-03-15 15:14:59', '2024-09-19 23:18:01'),
(12, 'user12', '2023-03-15 16:00:22', '2024-09-19 23:00:26'),
(13, 'user13', '2023-03-15 16:45:45', '2024-09-19 22:42:51'),
(14, 'user14', '2023-03-15 17:31:08', '2024-09-19 22:25:16'),
(15, 'user15', '2023-03-15 18:16:31', '2024-09-19 22:07:41'),
(16, 'user16', '2023-03-15 19:01:54', '2024-09-19 21:50:06'),
(17, 'user17', '2023-03-15 19:47:17', '2024-09-19 21:32:31'),
(18, 'user18', '2023-03-15 20:32:40', '2024-09-19 21:14:56'),
(19, 'user19', '2023-03-15 21:17:03', '2024-09-19 20:57:21'),
(20, 'user20', '2023-03-15 22:02:26', '2024-09-19 20:39:46');
(21, 'user21', '2023-03-18 22:47:49', '2024-09-19 20:22:11'),
(22, 'user22', '2023-03-18 23:33:12', '2024-09-19 20:04:36'),
(23, 'user23', '2023-03-19 00:18:35', '2024-09-19 19:46:01'),
(24, 'user24', '2023-03-19 01:03:58', '2024-09-19 19:28:26'),
(25, 'user25', '2023-03-19 01:49:21', '2024-09-19 19:10:51'),
(26, 'user26', '2023-03-19 02:34:44', '2024-09-23 18:53:16'),
(27, 'user27', '2023-03-19 03:20:07', '2024-09-23 18:35:41'),
(28, 'user28', '2023-03-19 04:05:30', '2024-09-23 18:18:06'),
(29, 'user29', '2023-03-19 04:50:53', '2024-09-23 18:00:31'),
(30, 'user30', '2023-03-19 05:36:16', '2024-09-23 17:42:56');
(31, 'user31', '2023-03-19 06:21:39', '2024-09-23 17:25:21'),
(32, 'user32', '2023-03-19 07:06:02', '2024-09-23 17:07:46'),
(33, 'user33', '2023-03-19 07:51:25', '2024-09-23 16:50:11'),
(34, 'user34', '2023-03-19 08:36:48', '2024-09-23 16:32:36'),
(35, 'user35', '2023-03-19 09:22:11', '2024-09-23 16:15:01'),
(36, 'user36', '2023-03-19 10:07:34', '2024-09-23 15:57:26'),
(37, 'user37', '2023-03-19 10:52:57', '2024-09-23 15:39:51'),
(38, 'user38', '2023-03-19 11:38:20', '2024-09-23 15:22:16'),
(39, 'user39', '2023-03-19 12:23:43', '2024-09-23 15:04:41'),
(40, 'user40', '2023-03-19 13:09:06', '2024-09-23 14:47:06');
(41, 'user41', '2023-03-19 13:54:29', '2024-09-23 14:29:31'),
(42, 'user42', '2023-03-19 14:39:52', '2024-09-23 14:11:56'),
(43, 'user43', '2023-03-19 15:25:15', '2024-09-23 13:54:21'),
(44, 'user44', '2023-03-19 16:10:38', '2024-09-23 13:36:46'),
(45, 'user45', '2023-03-19 16:55:01', '2024-09-23 13:19:11'),
(46, 'user46', '2023-03-19 17:40:24', '2024-09-23 13:01:36'),
(47, 'user47', '2023-03-19 18:25:47', '2024-09-23 12:44:01'),
(48, 'user48', '2023-03-19 19:11:10', '2024-09-23 12:26:26'),
(49, 'user49', '2023-03-19 19:56:33', '2024-09-23 12:08:51'),
(50, 'user50', '2023-04-19 20:41:56', '2024-10-04 11:51:16');
(51, 'user51', '2023-04-19 21:27:19', '2024-10-04 11:33:41'),
(52, 'user52', '2023-04-20 22:12:42', '2024-10-05 11:16:06'),
(53, 'user53', '2023-04-19 22:58:05', '2024-10-06 10:58:31'),
(54, 'user54', '2023-04-19 23:43:28', '2024-10-06 10:40:56'),
(55, 'user55', '2023-04-20 00:28:51', '2024-10-21 10:23:21'),
(56, 'user56', '2023-04-20 01:14:14', '2024-10-06 10:05:46'),
(57, 'user57', '2023-04-20 01:59:37', '2024-10-06 09:48:11'),
(58, 'user58', '2023-04-20 02:44:00', '2024-10-07 09:30:36'),
(59, 'user59', '2023-04-20 03:29:23', '2024-10-12 09:13:01'),
(60, 'user60', '2023-04-20 04:14:46', '2024-10-06 08:55:26');
(61, 'user61', '2023-04-20 05:00:09', '2024-10-07 08:37:51'),
(62, 'user62', '2023-04-20 05:45:32', '2024-10-07 08:20:16'),
(63, 'user63', '2023-04-20 06:30:55', '2024-10-30 08:02:41'),
(64, 'user64', '2023-04-20 07:16:18', '2024-10-07 07:45:06'),
(65, 'user65', '2023-04-20 08:01:41', '2024-10-07 07:27:31'),
(66, 'user66', '2023-04-10 08:47:04', '2024-10-07 07:09:56'),
(67, 'user67', '2023-04-20 09:32:27', '2024-10-17 06:52:21'),
(68, 'user68', '2023-04-26 10:17:50', '2024-10-07 06:34:46'),
(69, 'user69', '2023-04-20 11:03:13', '2024-10-07 06:17:11'),
(70, 'user70', '2023-04-23 11:48:36', '2024-10-25 05:59:36');
(71, 'user71', '2023-04-20 12:33:59', '2024-10-07 05:41:01'),
(72, 'user72', '2023-04-20 13:19:22', '2024-10-07 05:23:26'),
(73, 'user73', '2023-04-20 14:04:45', '2024-10-30 05:05:51'),
(74, 'user74', '2023-04-20 14:50:08', '2024-10-07 04:48:16'),
(75, 'user75', '2023-04-20 15:35:31', '2024-10-16 04:30:41'),
(76, 'user76', '2023-04-20 16:20:54', '2024-10-07 04:13:06'),
(77, 'user77', '2023-04-20 17:06:17', '2024-10-07 03:55:31'),
(78, 'user78', '2023-04-20 17:51:40', '2024-10-29 03:37:56'),
(79, 'user79', '2023-04-20 18:36:03', '2024-10-07 03:20:21'),
(80, 'user80', '2023-04-20 19:21:26', '2024-10-07 03:02:46');
(81, 'user81', '2023-04-20 20:06:49', '2024-10-07 02:45:11'),
(82, 'user82', '2023-04-20 20:52:12', '2024-10-07 02:27:36'),
(83, 'user83', '2023-04-20 21:37:35', '2024-10-07 02:10:01'),
(84, 'user84', '2023-04-20 22:22:58', '2024-10-12 01:52:26'),
(85, 'user85', '2023-04-20 23:08:21', '2024-10-07 01:34:51'),
(86, 'user86', '2023-04-21 23:53:44', '2024-10-07 01:17:16'),
(87, 'user87', '2023-04-21 00:39:07', '2024-10-07 00:59:41'),
(88, 'user88', '2023-04-21 01:24:30', '2024-10-07 00:42:06'),
(89, 'user89', '2023-04-24 02:09:53', '2024-10-07 00:24:31'),
(90, 'user90', '2023-04-29 02:55:16', '2024-10-24 00:06:56');
(91, 'user91', '2023-05-01 03:40:39', '2024-10-07 23:49:21'),
(92, 'user92', '2023-05-01 04:25:02', '2024-10-07 23:31:46'),
(93, 'user93', '2023-05-01 05:10:25', '2024-10-07 23:14:11'),
(94, 'user94', '2023-05-01 05:55:48', '2024-10-07 22:56:36'),
(95, 'user95', '2023-05-01 06:41:11', '2024-10-27 22:39:01'),
(96, 'user96', '2023-05-01 07:26:34', '2024-10-07 22:21:26'),
(97, 'user97', '2023-05-01 08:11:57', '2024-11-07 22:03:51'),
(98, 'user98', '2023-05-01 08:57:20', '2024-10-07 21:46:16'),
(99, 'user99', '2023-05-01 09:42:43', '2024-10-07 21:28:41'),
(100, 'user100', '2023-05-01 10:28:06', '2024-10-07 21:11:06');


-- Insert into user_auth table
INSERT INTO `user_auth` (`user_no`, `password_hash`, `pw_changed`) VALUES
(1, '$2b$10$q3PI.xK.O3EQusR999sple/GxvGu8pIlDLSNYXq3IUYPKwmgthJzO', '2025-01-09 14:52:00'),
(2, '$2b$10$2dhJ1LBFHti.9Vw6kfj63OKORgaMqe52Ow/9wWyh73cKsDYRWe6PK', '2025-03-19 04:16:00'),
(3, '$2b$10$iPjIv5NzXSzH8UZWPQ1x0euGIDq9RATSO54X6Xn5l82d1rIy9ctmu', '2025-04-01 15:37:00'),
(4, '$2b$10$7e3326pTDX3Y0y5kWfS2Gu5lkHUaBcyYvGxTBASn/E2khZCb86wtC', '2025-02-03 23:21:00'),
(5, '$2b$10$yEWhvC.U0/Ws8Hv4enmeouq4tdfYF1632ow0tJDdyUhVlJtf1EnnS', '2025-01-28 11:55:00'),
(6, '$2b$10$xS2zbfWJJfVCrTqtZQ5ape2wvMf48/QPBSVmJnH05dU76.AT1QBz.', '2025-02-14 14:38:00'),
(7, '$2b$10$r406oC7JPw5/co5GbNl2ce0C6SYnZqsuL9/wRh.hh6KEYQ1XRXfCi', '2025-04-03 07:42:00'),
(8, '$2b$10$HWyI2B6nDa91sIPOsojWkOjuhR3PkZZCV6mS5TACixGuXPUthZsmu', '2025-03-31 00:02:00'),
(9, '$2b$10$jXZ9boMd3KPRxeNScli9s.XehNS4MbHuLa/xf9.w/sYTPOVj.EwCC', '2025-01-05 00:03:00'),
(10, '$2b$10$7Flsc3FoyXu0bukUFz0.iumLEO8gP5NRNFNzSenN2cO1Lx2dwzWeG', '2025-01-16 14:53:00'),
(11, '$2b$10$m2t0et4irHhUw4iZdoTAju8AJzsr8k6/N/w9ca5llZbRCaoUlp63.', '2025-01-27 16:01:00'),
(12, '$2b$10$iXdWqEL3JTpx4jyprbFgYO/VHDwe9x/FbfuxrrERNElQRkHE499DW', '2025-03-22 22:47:00'),
(13, '$2b$10$/S89NrBmKpqmeVF8xd01i.v0mSwzxzCjDNDnpxanzhE1Osd802jOW', '2025-02-03 18:05:00'),
(14, '$2b$10$EI/22bQhGWeKokKOO4n2e.f7xVW202bMS3Ii.Ml6QHPe1ai4OOm/.', '2025-01-03 18:15:00'),
(15, '$2b$10$/maStR4jWjKJr3YnPj39fObNtCAZV6oJW6F9OotoRORCw99BiC...', '2025-01-28 05:47:00'),
(16, '$2b$10$MRRFZRB5qBa8yLJzJUSoeO6SlRD9y4AU4cpNPRoK0kEjHwPXFuHmy', '2025-02-15 21:54:00'),
(17, '$2b$10$/GMJXZAbvlbkJV/SxCc.au4csKqjnuG6kjHd/WBIH5dkFB9SpfVxa', '2025-02-10 12:55:00'),
(18, '$2b$10$XZyipZtpXfo7N12SU14twen2Jl8S98EN4PODCnFLG4i/FfhLcf/hy', '2025-02-04 04:11:00'),
(19, '$2b$10$e5Q8B9W5FaIFIB/uZgf8U.P0xsyUvFvwNrVkl4kwmJfYlBSNq..Mq', '2025-01-26 13:45:00'),
(20, '$2b$10$d9/UEZDRpdDR6nAxBpsqJOlnZYA.e.PjX4JAbTC4Zhk2sitJq6XFy', '2025-03-13 09:15:00'),
(21, '$2b$10$1XZk0d4s1/3ZENr5lFw5V.4xa68fOs9bOphwv88V2oBtMm/QcgJEW', '2025-01-31 05:12:00'),
(22, '$2b$10$cysQ.5iXR0IcBLpAeuOgBe3DQKIE1YjTMY6EkOI5h1.GVLFGokLJC', '2025-03-04 23:03:00'),
(23, '$2b$10$IUXOez76GkU5G1sW4C7GB.GZxvhon9ywqEh1pbpbhsWVYtp3SPApG', '2025-04-05 07:47:00'),
(24, '$2b$10$a02A4/BDePz7QpbrYb6ObeqMSoYbAOB0F7L2cklYy0SRiyS2N3..y', '2025-03-08 02:57:00'),
(25, '$2b$10$RxVAHdzYFX4.3MkOhCp3UOHoHomatBELhTmjFjgpJHku68MqjSOM.', '2025-02-15 02:44:00'),
(26, '$2b$10$Tb47tovVUj8GMO81HXKKHuRxILaOYAc2NV3u1alVgeIixz4ughYcG', '2025-03-11 11:48:00'),
(27, '$2b$10$x9Mhh.a2AXn9XftOdkBpfOL9cF5TP/aMaAXEg83dY4P34sTt3IgJO', '2025-02-14 21:01:00'),
(28, '$2b$10$kSRk4Dlu.FVRYlVUqp1n9eXnM3LmaydmgyepV4Zf6VwoF3MF67hY6', '2025-03-09 16:52:00'),
(29, '$2b$10$/mGk6z75MBy.fYANmiZXUu3kZ25EndBq1gIYx9UZ/MCH5KgS6oIBq', '2025-02-22 11:17:00'),
(30, '$2b$10$dkl36ULANdwGZ2oOafP5BuThX/iYVqew9AXoJQfBBJK5NekoJo.IG', '2025-01-10 00:20:00'),
(31, '$2b$10$5SbhI.ax4rljJhzsHgu4fOVw1kW8RDHlTa66fA11SD7CLV5uhrJM2', '2025-03-21 10:09:00'),
(32, '$2b$10$8Gmm64ipZg059bFSUnBVfO0hCe1XUb3SpbpyR0igcGIeeNV2rT7xK', '2025-03-11 10:05:00'),
(33, '$2b$10$3z8.0nfK6Ceki3ZE2x2e3.jGMByxYyLgmmZpVFITPQwF.iUbNYpJ6', '2025-01-29 02:16:00'),
(34, '$2b$10$tE0scgOlngQKahD/2Gv0gO7cXmbeAvDbGwPEZ3azPlMJQW/RqvGOG', '2025-04-10 23:30:00'),
(35, '$2b$10$JLxTIkwB.XEBla0F0Lm8BeaZ.iovTuHJ1sVCNWdMdDXaYF6mWUaL2', '2025-01-17 06:41:00'),
(36, '$2b$10$2lwve0cBf9.ImlgICL9uXuyQhqLPxNm3clhhVTSYE/Byc5m/gyqcy', '2025-03-16 00:21:00'),
(37, '$2b$10$aJlmaXY9LnDa8T1MGuzDY.4OBogp9gOf50L03ysF9dISNgn5fKrnu', '2025-03-23 01:50:00'),
(38, '$2b$10$0J6tRkbnK51UW1JPMUlL5u3OxSCvhm1lpD1tqKuO8Jm/4bu0ZIBri', '2025-02-01 19:26:00'),
(39, '$2b$10$St/OT6KTpn6R7LROgm55ROQhUlJ6lVP43Cfizu6wSBNXAJpWolVlq', '2025-03-29 07:41:00'),
(40, '$2b$10$d1zU/LYMfLJZ45SE7sPbeePbGLr5Pc5sIliatJNohQU6A6w/21wu2', '2025-03-25 13:06:00'),
(41, '$2b$10$063Oag/h9HWoJh3u5fMtSOZrkhbpORKAbP4U6/rRMMClLtjaayoYS', '2025-01-04 07:11:00'),
(42, '$2b$10$Fd1gudF0Yos7qhq/vJyDW.S9QaRQxTvd56h/EPgOhRiDIp5BqfP0e', '2025-04-01 21:12:00'),
(43, '$2b$10$FC0GJWWqBtAeV9LVauQXlOnpMkTmaRMPlUDCUBowwsKgX6oCzMjRC', '2025-02-16 14:18:00'),
(44, '$2b$10$76c5PL5bkJW5kDVm.Znkw.bbjRV6Xs/.oiq/ZtHn91nGKrj5h7goq', '2025-01-12 18:14:00'),
(45, '$2b$10$s675KfPcYJFigD3QRtB6bOeVJmA.CW668fIUzFaR/UyTXNmK84fHG', '2025-04-08 02:18:00'),
(46, '$2b$10$ntuTOAlk8rzqAaClVzWBNe5a04NGQ8a8nezPRplEu/vGfDlq460Fi', '2025-02-23 02:13:00'),
(47, '$2b$10$/k0/kLqjH/D1fuShYlBU4O8dDnclgS4bTYyXkaoscw82PSbeptGHC', '2025-01-31 06:33:00'),
(48, '$2b$10$zc2BIbogAOFaauIRmHzXHugx5MVWK1p/5uiwA6Wp.ijqZUM8hKN0a', '2025-01-23 00:27:00'),
(49, '$2b$10$SdxHF.x5lhna5rElEwEJbOLeQcP.WyVGaDo.Rxfwk.nmto4wET8oa', '2025-03-02 03:46:00'),
(50, '$2b$10$3A/KtUsVX9PvsnxSljWkZOjouQ2dZfxNZaM3GQW8ZLbARGG0ouCo2', '2025-02-23 11:51:00'),
(51, '$2b$10$AqN6mB1KMSfl71oJVYa4K.621/ZrK/EVpnaLH/ZG9Pft0Wfn/qc6q', '2025-01-27 03:37:00'),
(52, '$2b$10$zz1I9TeKeajiKehdXYVrce7uzNmz1dq4RmzFuAbF.zJOtSQyreNie', '2025-03-27 08:05:00'),
(53, '$2b$10$8R60pFyy3QbDR3LlptUdhefZxDWRD9zlew1YLeMV5.AAbOl34yXrO', '2025-03-30 11:48:00'),
(54, '$2b$10$d1mzGtTDAMJCe/EA0pTNyufUJ8tOzgd/K4n9zfiAp.XCfIP7GurJq', '2025-03-17 17:03:00'),
(55, '$2b$10$bRd3PfNqJU1KEVUhOFqzCewTNBbsVyaLdErNaQEEBTERwQuVKQ0WW', '2025-02-24 22:24:00'),
(56, '$2b$10$qmWj/oNiqhUmdgvuCDshhOx0V5er.uu66CgImTS5Pg3g//aZW9cPm', '2025-03-11 12:05:00'),
(57, '$2b$10$cCJJ6.dlQfdvNn/iEWlLpe85DzK1c8W26YhHE6ge8MIhHqfbgTWDy', '2025-01-21 20:47:00'),
(58, '$2b$10$tCucN/fYElKdSoPo8y6N7..kOR3.2pzW/dIfEHOPfNHSWCQy36tY6', '2025-02-25 14:18:00'),
(59, '$2b$10$yEPEwtcpT0yWT8KbOcqn/umTakLnhGUqipB0jo/Mqj7UMw.KJq8qe', '2025-02-18 21:00:00'),
(60, '$2b$10$wkGrVqeV/kV2fn6zLrQPtes7gxQk43byz0JPujycAzxnreoVwivxS', '2025-02-16 02:48:00'),
(61, '$2b$10$a/WBC6H0.x2.x/4t34KBCe3wCVwAP.qXdwj5nM8sCzIJXGw0XPEPu', '2025-01-06 08:39:00'),
(62, '$2b$10$CiymWAd6wB7Znwz9P8YXR.OVK5XiGN8HNQGMI0Ltc6iVk.fZenUtC', '2025-03-20 10:46:00'),
(63, '$2b$10$s84px7V5uJ6byDgYZxh0u.K76M/LV/kQ8ksAeD7gA3sSyABUutDWK', '2025-03-10 08:42:00'),
(64, '$2b$10$EiXBUpRTM7Hh5PaOIvm/2.LZACAkCiifa7ItrAbccdnIBs8FJXKJW', '2025-01-05 03:10:00'),
(65, '$2b$10$lfd9PcZgabhIDJta1bk7wurx9EWRZr/cEzlBIlMSItpNK9VoETVWe', '2025-02-27 17:04:00'),
(66, '$2b$10$gt0L/BVF53y47SYrYkexweeHw8TdGUAcgQMIbwhBjuvwI4Efpi5Ky', '2025-01-27 07:54:00'),
(67, '$2b$10$lop./KlOS7mYZxCISzzaq.m5wAzgWDN3HCKqlrGhTF.yxZMtP2kxG', '2025-03-07 07:15:00'),
(68, '$2b$10$tYh0dmiuywty9zkwxaLG4u4TknRSov/VcFe8rtxNV7WYs.WO2tg9K', '2025-04-01 08:42:00'),
(69, '$2b$10$scibzy5SVEFFMyCj5DCkieFyaeuRVNrm9FFZ.6imsGERHlY5ehwQu', '2025-02-07 10:43:00'),
(70, '$2b$10$6k49rIW.x2yw62Nkc3F9YOt2ids3gWG3fBh0WqvwulFbIyu.JpBAa', '2025-02-21 01:50:00'),
(71, '$2b$10$BoZWo1axjcpolHqIiZ/igeCkQyxxXS4EBGMGRapkfe0XWQDZQkL.S', '2025-01-28 15:15:00'),
(72, '$2b$10$ybFwxFdkuZ5SpE1FWk4tSOKeCwHuEtadr4/54QtOF3tPYaAlAYQbe', '2025-01-04 04:20:00'),
(73, '$2b$10$ovCoZc8C9SdO0XtcOnxfEenfc8U4qmQoeE0W4WzYjOcRSHmVmzQcC', '2025-04-05 14:39:00'),
(74, '$2b$10$lEOIgb9Uk4rSvdgrDawXSOF7a73fkgzJMqPFV8p7YO2HGPeWXdhem', '2025-04-10 21:11:00'),
(75, '$2b$10$s9HiFNZ8on9wbxlIrx/fPOrmXqW1So1pr65wP.1J.nIm.ipQLkIj.', '2025-01-21 17:48:00'),
(76, '$2b$10$MrO8wW3zZMS6YRmCD574meqTz8xaqOJBstFmp/JIiK/JUzfxEsgqK', '2025-02-23 07:56:00'),
(77, '$2b$10$IbuxmGV641UqOGM.4PBznOjf5o.E9.u8ExV6n8ypLFkxG4VFrqeNS', '2025-03-23 23:03:00'),
(78, '$2b$10$b95NSP6e7mInuBUVMJJPouSoy.A9WcOnOukxw1VLK6BAX5UfZfheW', '2025-03-02 00:49:00'),
(79, '$2b$10$bMA3HkuwOnYB2BWWrok41.qWkd4Xcnwi8XN5F/DQoBFhjJQQFcFGW', '2025-01-15 11:09:00'),
(80, '$2b$10$/4jnOMQJTFYmsenS2YXxb.1IrxYy4htFwKUMQ.FyeTuRaR0pK81wq', '2025-03-11 04:17:00'),
(81, '$2b$10$XvcnMqxGK6sALSodHGeS9uTSNB6hRJ8Xtd3bx2ifQD5o1cFNVljMC', '2025-01-11 05:32:00'),
(82, '$2b$10$MTh90T7DpiwUqEBP.xWxCuJaMifZ0EOP2ElOrQ5syJ8iLPLBkGy52', '2025-03-14 08:05:00'),
(83, '$2b$10$wcgxNEsEzJ7JsGxvc/v2OO5.fusnn.XYdIQpEkXjwVH0zEcNnBfE.', '2025-03-08 16:14:00'),
(84, '$2b$10$xTpYavHOlLJPuwzQshK6uu3mtBgyFEWmZ8DzJe9Pp9e/BS4uQoCLW', '2025-01-24 21:08:00'),
(85, '$2b$10$RIWCsjtXvTVzeLIJ0zhGIe781jZjrBsaW.AfmbaqCufEzwGb3KE.G', '2025-04-03 02:58:00'),
(86, '$2b$10$0OaPAlUPtwO7t5Roh5TEhuTjL.FEMTwzOWCV6lhFDyZB1PV2BuQ2u', '2025-02-20 02:41:00'),
(87, '$2b$10$gACqwbUQL6j./u9iQKasNOkk6PtHERBdreYqs5r6zXF/.OyjXAfe6', '2025-03-16 03:57:00'),
(88, '$2b$10$/roWr9MX2ZkM8VJ3ZPUzPu6E4aFd19Y9En52vwVcFCI3VWQ42Kz1y', '2025-02-05 13:30:00'),
(89, '$2b$10$ZvpBiLWmyAJwu3P3TYj2WegZPFiGV0wHPo3XhQoGD3gp7T4.sawF2', '2025-02-22 10:16:00'),
(90, '$2b$10$AbxcGGbG9w5cDDmqSUii5eZRxoYxgDtdkv/DMnjJV3vWE38nwezpO', '2025-03-18 12:36:00'),
(91, '$2b$10$aw2tO8hOJz1ySn.7IdViQuvYAfPNvxPRS0.iWkBgxZPtFuhG09Qfm', '2025-03-25 00:29:00'),
(92, '$2b$10$pRMW6OA9X1JKGQcAEllFIOmEvZxrKKIF.3ugfv3r7wl3B/zIGvRY2', '2025-01-30 10:41:00'),
(93, '$2b$10$11JSphw3IbMeqMTFdTzafOQ/lF0NZBxpyEGSziUf2MnyTX7EPQ8WC', '2025-03-11 06:36:00'),
(94, '$2b$10$6.WeZCgd/z3efa6bUUQTne3FiAtk1CWEW3DG3u7.eI7OUvje/4C32', '2025-02-11 18:41:00'),
(95, '$2b$10$lHiULUA9avaJmJP3sbHoMO5awhV4tnsE69vcViWynmhSLkZ57CFrW', '2025-01-04 20:34:00'),
(96, '$2b$10$5e9G1U1tLoRHBNtJQNbcHuON67kaD078QNS3DwvJstpbVA6.AmtL.', '2025-04-03 16:39:00'),
(97, '$2b$10$JbsviWOQxjZpTRKazPzI3O7niNjKSp87Wl3mXTKGmxxqMzmz7Fz02', '2025-02-15 14:40:00'),
(98, '$2b$10$iC6jQzWaLCy84bI0kWc3MOUovoLX5SS1scGNouL3Dc1C6AnhBe7n6', '2025-02-15 20:42:00'),
(99, '$2b$10$ZKEg9IpBwWribS5G2KV/ge/HKFl.bwOTT6t.dvrIkUXTBtaywWCtW', '2025-02-16 20:44:00'),
(100, '$2b$10$9oNhbqqZM5xArj1gK5qNOe3lu01KhDeMgvs6Hct/GiU3SD7M0H99a', '2025-03-12 09:23:00');

-- Insert into user_profile table
INSERT INTO `user_profile` (`user_no`, `email`, `phone`, `name`, `nickname`, `birth_date`, `gender`, `updated_at`) VALUES
(1, 'kim.minho@student.kr', '010-1234-5678', '김민호', '민호스터디', '2006-03-15', 'M', '2024-05-01'),
(2, 'park.jiyeon@student.kr', '010-2345-6789', '박지연', '지연이얌', '2006-07-22', 'F', '2024-04-28'),
(3, 'lee.junho@student.kr', '010-3456-7890', '이준호', '준호쌤', '2007-01-30', 'M', '2024-05-03'),
(4, 'choi.soyeon@student.kr', '010-4567-8901', '최소연', '소연공부중', '2007-05-17', 'F', '2024-04-29'),
(5, 'jung.minseo@student.kr', '010-5678-9012', '정민서', '민서의하루', '2008-09-03', 'F', '2024-05-10'),
(6, 'kang.jihoon@student.kr', '010-6789-0123', '강지훈', '지훈수학왕', '2008-11-28', 'M', '2024-05-02'),
(7, 'yoon.jiwoo@student.kr', '010-7890-1234', '윤지우', '지우공부방', '2009-02-14', 'F', '2024-05-07'),
(8, 'han.donghyun@student.kr', '010-8901-2345', '한동현', '동현코딩', '2009-04-19', 'M', '2024-05-05'),
(9, 'kim.yeji@student.kr', '010-9012-3456', '김예지', '예지잉글리시', '2010-06-25', 'F', '2024-05-09'),
(10, 'park.minwoo@student.kr', '010-0123-4567', '박민우', '민우과학자', '2010-08-11', 'M', '2024-05-03'),
(11, 'seo.jimin@student.kr', '010-1122-3344', '서지민', '지민책벌레', '2011-03-07', 'F', '2024-05-06'),
(12, 'jo.hyunjin@student.kr', '010-2233-4455', '조현진', '현진수학자', '2011-05-29', 'M', '2024-05-08'),
(13, 'shin.yuna@student.kr', '010-3344-5566', '신유나', '유나열공중', '2012-01-13', 'F', '2024-05-01'),
(14, 'lee.jaehyun@student.kr', '010-4455-6677', '이재현', '재현스터디', '2012-07-20', 'M', '2024-05-04'),
(15, 'kwon.jisoo@student.kr', '010-5566-7788', '권지수', '지수의공부', '2013-09-05', 'F', '2024-05-02'),
(16, 'hwang.minjun@student.kr', '010-6677-8899', '황민준', '민준영어왕', '2013-11-17', 'M', '2024-05-07'),
(17, 'kim.hajin@student.kr', '010-7788-9900', '김하진', '하진수학왕', '2006-04-22', 'F', '2024-05-03'),
(18, 'park.seojun@student.kr', '010-8899-0011', '박서준', '서준국어왕', '2006-08-14', 'M', '2024-05-10'),
(19, 'jang.dayeon@student.kr', '010-9900-1122', '장다연', '다연공주', '2007-02-28', 'F', '2024-05-06'),
(20, 'choi.junseo@student.kr', '010-0011-2233', '최준서', '준서과학자', '2007-10-09', 'M', '2024-05-08'),
(21, 'kim.soomin@student.kr', '010-1234-0001', '김수민', '수민스마트', '2008-06-18', 'F', '2024-05-01'),
(22, 'lee.taehyun@student.kr', '010-2345-0002', '이태현', '태현뇌섹남', '2008-12-05', 'M', '2024-05-05'),
(23, 'park.yerin@student.kr', '010-3456-0003', '박예린', '예린열공', '2009-03-29', 'F', '2024-05-07'),
(24, 'jung.jiho@student.kr', '010-4567-0004', '정지호', '지호매스터', '2009-09-12', 'M', '2024-05-03'),
(25, 'yoo.jiwon@student.kr', '010-5678-0005', '유지원', '지원공부중', '2010-01-27', 'F', '2024-05-09'),
(26, 'song.minho@student.kr', '010-6789-0006', '송민호', '민호영어왕', '2010-11-03', 'M', '2024-05-04'),
(27, 'yang.soeun@student.kr', '010-7890-0007', '양소은', '소은이의꿈', '2011-04-15', 'F', '2024-05-02'),
(28, 'han.jihyuk@student.kr', '010-8901-0008', '한지혁', '지혁스터디', '2011-08-22', 'M', '2024-05-08'),
(29, 'moon.jiyoon@student.kr', '010-9012-0009', '문지윤', '지윤이얌', '2012-02-07', 'F', '2024-05-06'),
(30, 'kim.dongmin@student.kr', '010-0123-0010', '김동민', '동민코딩맨', '2012-10-19', 'M', '2024-05-01'),
(31, 'ahn.sujin@student.kr', '010-1122-0011', '안수진', '수진영어', '2013-03-04', 'F', '2024-05-07'),
(32, 'park.sunwoo@student.kr', '010-2233-0012', '박선우', '선우책읽기', '2013-12-26', 'M', '2024-05-03'),
(33, 'kim.hyunji@student.kr', '010-3344-0013', '김현지', '현지수학왕', '2006-05-08', 'F', '2024-05-09'),
(34, 'lee.seunghoon@student.kr', '010-4455-0014', '이승훈', '승훈공부중', '2006-09-16', 'M', '2024-05-02'),
(35, 'shin.minji@student.kr', '010-5566-0015', '신민지', '민지열공', '2007-03-21', 'F', '2024-05-04'),
(36, 'kang.minjae@student.kr', '010-6677-0016', '강민재', '민재영어', '2007-11-30', 'M', '2024-05-06'),
(37, 'hong.seoyeon@student.kr', '010-7788-0017', '홍서연', '서연과학자', '2008-04-23', 'F', '2024-05-08'),
(38, 'lim.jaemin@student.kr', '010-8899-0018', '임재민', '재민수학왕', '2008-10-11', 'M', '2024-05-10'),
(39, 'park.sohee@student.kr', '010-9900-0019', '박소희', '소희영어왕', '2009-05-25', 'F', '2024-05-01'),
(40, 'choi.yoonho@student.kr', '010-0011-0020', '최윤호', '윤호코딩', '2009-12-08', 'M', '2024-05-05'),
(41, 'na.jiyoung@student.kr', '010-1234-0021', '나지영', '지영스마트', '2010-03-14', 'F', '2024-05-03'),
(42, 'kim.taeho@student.kr', '010-2345-0022', '김태호', '태호공부방', '2010-07-27', 'M', '2024-05-07'),
(43, 'jeong.eunbi@student.kr', '010-3456-0023', '정은비', '은비열공중', '2011-02-09', 'F', '2024-05-04'),
(44, 'lee.junhyeok@student.kr', '010-4567-0024', '이준혁', '준혁영재', '2011-06-18', 'M', '2024-05-09'),
(45, 'oh.yerim@student.kr', '010-5678-0025', '오예림', '예림수학', '2012-04-01', 'F', '2024-05-02'),
(46, 'han.jisung@student.kr', '010-6789-0026', '한지성', '지성이얌', '2012-08-30', 'M', '2024-05-08'),
(47, 'baek.sohyun@student.kr', '010-7890-0027', '백소현', '소현영어', '2013-01-12', 'F', '2024-05-06'),
(48, 'seo.minjun@student.kr', '010-8901-0028', '서민준', '민준의하루', '2013-05-07', 'M', '2024-05-01'),
(49, 'kim.jieun@student.kr', '010-9012-0029', '김지은', '지은공부중', '2006-06-29', 'F', '2024-05-05'),
(50, 'park.hyungsoo@student.kr', '010-0123-0030', '박형수', '형수수학자', '2006-10-14', 'M', '2024-05-03'),
(51, 'choi.dayoung@student.kr', '010-1122-0031', '최다영', '다영열공', '2007-04-26', 'F', '2024-05-07'),
(52, 'kim.jiwon@student.kr', '010-2233-0032', '김지원', '지원영어왕', '2007-08-13', 'M', '2024-05-09'),
(53, 'lee.yoonji@student.kr', '010-3344-0033', '이윤지', '윤지공주', '2008-01-04', 'F', '2024-05-02'),
(54, 'song.hyunjin@student.kr', '010-4455-0034', '송현진', '현진스마트', '2008-05-19', 'M', '2024-05-06'),
(55, 'jung.soyoon@student.kr', '010-5566-0035', '정소윤', '소윤공부중', '2009-07-03', 'F', '2024-05-04'),
(56, 'hwang.jinho@student.kr', '010-6677-0036', '황진호', '진호과학자', '2009-11-22', 'M', '2024-05-08'),
(57, 'kim.subin@student.kr', '010-7788-0037', '김수빈', '수빈의꿈', '2010-02-16', 'F', '2024-05-01'),
(58, 'yoon.dongwoo@student.kr', '010-8899-0038', '윤동우', '동우코딩', '2010-09-30', 'M', '2024-05-05'),
(59, 'jang.hyejin@student.kr', '010-9900-0039', '장혜진', '혜진공부방', '2011-01-13', 'F', '2024-05-03'),
(60, 'park.jiwoo@student.kr', '010-0011-0040', '박지우', '지우수학왕', '2011-07-24', 'M', '2024-05-10'),
(61, 'shin.heejin@student.kr', '010-1234-0041', '신희진', '희진열공', '2012-03-08', 'F', '2024-05-07'),
(62, 'lee.minseok@student.kr', '010-2345-0042', '이민석', '민석영어', '2012-11-15', 'M', '2024-05-02'),
(63, 'kang.yoonju@student.kr', '010-3456-0043', '강윤주', '윤주공부중', '2013-04-18', 'F', '2024-05-09'),
(64, 'kim.jihoon@student.kr', '010-4567-0044', '김지훈', '지훈코딩맨', '2013-08-23', 'M', '2024-05-04'),
(65, 'han.chaewon@student.kr', '010-5678-0045', '한채원', '채원이얌', '2006-02-11', 'F', '2024-05-06'),
(66, 'jin.minhyuk@student.kr', '010-6789-0046', '진민혁', '민혁수학왕', '2006-12-19', 'M', '2024-05-08'),
(67, 'park.jimin@student.kr', '010-7890-0047', '박지민', '지민공주', '2007-06-27', 'F', '2024-05-01'),
(68, 'choi.jaeyong@student.kr', '010-8901-0048', '최재용', '재용스터디', '2007-09-08', 'M', '2024-05-03'),
(69, 'kim.seohyun@student.kr', '010-9012-0049', '김서현', '서현열공중', '2008-02-22', 'F', '2024-05-05'),
(70, 'lee.jungmin@student.kr', '010-0123-0050', '이정민', '정민영어왕', '2008-08-04', 'M', '2024-05-07'),
(71, 'ko.jieun@student.kr', '010-1122-0051', '고지은', '지은책벌레', '2009-01-17', 'F', '2024-05-09'),
(72, 'park.sungjae@student.kr', '010-2233-0052', '박성재', '성재코딩', '2009-06-20', 'M', '2024-05-02'),
(73, 'shin.yeeun@student.kr', '010-3344-0053', '신예은', '예은공부방', '2010-05-02', 'F', '2024-05-04'),
(74, 'kim.wonjun@student.kr', '010-4455-0054', '김원준', '원준수학왕', '2010-12-11', 'M', '2024-05-06'),
(75, 'jeong.daeun@student.kr', '010-5566-0055', '정다은', '다은의하루', '2011-05-27', 'F', '2024-05-08'),
(76, 'lee.sungwon@student.kr', '010-6677-0056', '이성원', '성원공부중', '2011-09-16', 'M', '2024-05-10'),
(77, 'moon.jiyoung@student.kr', '010-7788-0057', '문지영', '지영영어', '2012-05-13', 'F', '2024-05-01'),
(78, 'kang.joonho@student.kr', '010-8899-0058', '강준호', '준호과학자', '2012-09-27', 'M', '2024-05-05'),
(79, 'kim.yoonah@student.kr', '010-9900-0059', '김윤아', '윤아공주', '2013-02-14', 'F', '2024-05-03'),
(80, 'park.jiho@student.kr', '010-0011-0060', '박지호', '지호코딩맨', '2013-11-03', 'M', '2024-05-07'),
(81, 'yoon.soeun@student.kr', '010-1234-0061', '윤소은', '소은스마트', '2006-03-27', 'F', '2024-05-04'),
(82, 'kim.jaehoon@student.kr', '010-2345-0062', '김재훈', '재훈열공', '2006-11-08', 'M', '2024-05-09'),
(83, 'jo.minji@student.kr', '010-3456-0063', '조민지', '민지공부중', '2007-07-10', 'F', '2024-05-02'),
(84, 'lee.minho@student.kr', '010-4567-0064', '이민호', '민호영어왕', '2007-12-31', 'M', '2024-05-06'),
(85, 'choi.jiyeon@student.kr', '010-5678-0065', '최지연', '지연스터디', '2008-03-19', 'F', '2024-05-08'),
(86, 'song.joonhyuk@student.kr', '010-6789-0066', '송준혁', '준혁수학자', '2008-07-26', 'M', '2024-05-01'),
(87, 'hong.dayeon@student.kr', '010-7890-0067', '홍다연', '다연공부방', '2009-02-09', 'F', '2024-05-05'),
(88, 'park.minjun@student.kr', '010-8901-0068', '박민준', '민준코딩', '2009-10-14', 'M', '2024-05-03'),
(89, 'jang.haeun@student.kr', '010-9012-0069', '장하은', '하은열공중', '2010-04-21', 'F', '2024-05-07'),
(90, 'kim.sungho@student.kr', '010-0123-0070', '김성호', '성호과학자', '2010-10-03', 'M', '2024-05-09'),
(91, 'lee.soomin@student.kr', '010-1122-0071', '이수민', '수민이얌', '2011-03-28', 'F', '2024-05-02'),
(92, 'han.jinwoo@student.kr', '010-2233-0072', '한진우', '진우영어왕', '2011-11-19', 'M', '2024-05-04'),
(93, 'jung.eunseo@student.kr', '010-3344-0073', '정은서', '은서공부방', '2012-02-26', 'F', '2024-05-06'),
(94, 'shin.jaemin@student.kr', '010-4455-0074', '신재민', '재민코딩맨', '2012-06-12', 'M', '2024-05-08'),
(95, 'kim.yejin@student.kr', '010-5566-0075', '김예진', '예진스마트', '2013-07-01', 'F', '2024-05-10'),
(96, 'park.donghyun@student.kr', '010-6677-0076', '박동현', '동현수학왕', '2013-10-28', 'M', '2024-05-01'),
(97, 'choi.jihye@student.kr', '010-7788-0077', '최지혜', '지혜공부중', '2006-06-03', 'F', '2024-05-03'),
(98, 'kim.yoonwoo@student.kr', '010-8899-0078', '김윤우', '윤우과학자', '2006-12-25', 'M', '2024-05-05'),
(99, 'lee.hyunjin@student.kr', '010-9900-0079', '이현진', '현진열공', '2007-05-14', 'F', '2024-05-07'),
(100, 'jung.minsu@student.kr', '010-0011-0080', '정민수', '민수코딩맨', '2007-11-06', 'M', '2024-05-09');

-- Insert into user_refresh_token table
INSERT INTO `user_refresh_token` (`user_no`, `refresh_token`, `issued_at`, `expires_at`, `revoked`) VALUES
(1, 'refresh_token_example_string_for_user1_blahblah_very_long', '2025-05-12 14:00:00', '2025-06-11 14:00:00', FALSE),
(2, 'refresh_token_example_string_for_user2_another_long_one', '2025-05-12 14:05:00', '2025-06-11 14:05:00', FALSE),
(3, 'refresh_token_example_string_for_user3_yet_another_token', '2025-05-12 14:10:00', '2025-06-11 14:10:00', FALSE),
(4, 'refresh_token_example_string_for_user4_unique_value_here', '2025-05-12 14:15:00', '2025-06-11 14:15:00', FALSE),
(5, 'refresh_token_example_string_for_user5_secure_and_random', '2025-05-12 14:20:00', '2025-06-11 14:20:00', FALSE);
(6, 'refresh_token_example_string_for_user6_another_unique', '2025-05-12 14:25:00', '2025-06-11 14:25:00', FALSE),
(7, 'refresh_token_example_string_for_user7_different_value', '2025-05-12 14:30:00', '2025-06-11 14:30:00', FALSE),
(8, 'refresh_token_example_string_for_user8_another_random', '2025-05-12 14:35:00', '2025-06-11 14:35:00', FALSE),
(9, 'refresh_token_example_string_for_user9_unique_value', '2025-05-12 14:40:00', '2025-06-11 14:40:00', FALSE),
(10, 'refresh_token_example_string_for_user10_another_secure', '2025-05-12 14:45:00', '2025-06-11 14:45:00', FALSE);
(11, 'refresh_token_example_string_for_user11_unique_value', '2025-05-12 14:50:00', '2025-06-11 14:50:00', FALSE),
(12, 'refresh_token_example_string_for_user12_another_secure', '2025-05-12 14:55:00', '2025-06-11 14:55:00', FALSE),
(13, 'refresh_token_example_string_for_user13_unique_value', '2025-05-12 15:00:00', '2025-06-11 15:00:00', FALSE),
(14, 'refresh_token_example_string_for_user14_another_secure', '2025-05-12 15:05:00', '2025-06-11 15:05:00', FALSE),
(15, 'refresh_token_example_string_for_user15_unique_value', '2025-05-12 15:10:00', '2025-06-11 15:10:00', FALSE);
(16, 'refresh_token_example_string_for_user16_another_secure', '2025-05-12 15:15:00', '2025-06-11 15:15:00', FALSE),
(17, 'refresh_token_example_string_for_user17_unique_value', '2025-05-12 15:20:00', '2025-06-11 15:20:00', FALSE),
(18, 'refresh_token_example_string_for_user18_another_secure', '2025-05-12 15:25:00', '2025-06-11 15:25:00', FALSE),
(19, 'refresh_token_example_string_for_user19_unique_value', '2025-05-12 15:30:00', '2025-06-11 15:30:00', FALSE),
(20, 'refresh_token_example_string_for_user20_another_secure', '2025-05-12 15:35:00', '2025-06-11 15:35:00', FALSE);
(21, 'refresh_token_example_string_for_user21_unique_value', '2025-05-12 15:40:00', '2025-06-11 15:40:00', FALSE),
(22, 'refresh_token_example_string_for_user22_another_secure', '2025-05-12 15:45:00', '2025-06-11 15:45:00', FALSE),
(23, 'refresh_token_example_string_for_user23_unique_value', '2025-05-12 15:50:00', '2025-06-11 15:50:00', FALSE),
(24, 'refresh_token_example_string_for_user24_another_secure', '2025-05-12 15:55:00', '2025-06-11 15:55:00', FALSE),
(25, 'refresh_token_example_string_for_user25_unique_value', '2025-05-12 16:00:00', '2025-06-11 16:00:00', FALSE);
(26, 'refresh_token_example_string_for_user26_another_secure', '2025-05-12 16:05:00', '2025-06-11 16:05:00', FALSE),
(27, 'refresh_token_example_string_for_user27_unique_value', '2025-05-12 16:10:00', '2025-06-11 16:10:00', FALSE),
(28, 'refresh_token_example_string_for_user28_another_secure', '2025-05-12 16:15:00', '2025-06-11 16:15:00', FALSE),
(29, 'refresh_token_example_string_for_user29_unique_value', '2025-05-12 16:20:00', '2025-06-11 16:20:00', FALSE),
(30, 'refresh_token_example_string_for_user30_another_secure', '2025-05-12 16:25:00', '2025-06-11 16:25:00', FALSE);
(31, 'refresh_token_example_string_for_user31_unique_value', '2025-05-12 16:30:00', '2025-06-11 16:30:00', FALSE),
(32, 'refresh_token_example_string_for_user32_another_secure', '2025-05-12 16:35:00', '2025-06-11 16:35:00', FALSE),
(33, 'refresh_token_example_string_for_user33_unique_value', '2025-05-12 16:40:00', '2025-06-11 16:40:00', FALSE),
(34, 'refresh_token_example_string_for_user34_another_secure', '2025-05-12 16:45:00', '2025-06-11 16:45:00', FALSE),
(35, 'refresh_token_example_string_for_user35_unique_value', '2025-05-12 16:50:00', '2025-06-11 16:50:00', FALSE);
(36, 'refresh_token_example_string_for_user36_another_secure', '2025-05-12 16:55:00', '2025-06-11 16:55:00', FALSE),
(37, 'refresh_token_example_string_for_user37_unique_value', '2025-05-12 17:00:00', '2025-06-11 17:00:00', FALSE),
(38, 'refresh_token_example_string_for_user38_another_secure', '2025-05-12 17:05:00', '2025-06-11 17:05:00', FALSE),
(39, 'refresh_token_example_string_for_user39_unique_value', '2025-05-12 17:10:00', '2025-06-11 17:10:00', FALSE),
(40, 'refresh_token_example_string_for_user40_another_secure', '2025-05-12 17:15:00', '2025-06-11 17:15:00', FALSE);
(41, 'refresh_token_example_string_for_user41_unique_value', '2025-05-12 17:20:00', '2025-06-11 17:20:00', FALSE),
(42, 'refresh_token_example_string_for_user42_another_secure', '2025-05-12 17:25:00', '2025-06-11 17:25:00', FALSE),
(43, 'refresh_token_example_string_for_user43_unique_value', '2025-05-12 17:30:00', '2025-06-11 17:30:00', FALSE),
(44, 'refresh_token_example_string_for_user44_another_secure', '2025-05-12 17:35:00', '2025-06-11 17:35:00', FALSE),
(45, 'refresh_token_example_string_for_user45_unique_value', '2025-05-12 17:40:00', '2025-06-11 17:40:00', FALSE);
(46, 'refresh_token_example_string_for_user46_another_secure', '2025-05-12 17:45:00', '2025-06-11 17:45:00', FALSE),
(47, 'refresh_token_example_string_for_user47_unique_value', '2025-05-12 17:50:00', '2025-06-11 17:50:00', FALSE),
(48, 'refresh_token_example_string_for_user48_another_secure', '2025-05-12 17:55:00', '2025-06-11 17:55:00', FALSE),
(49, 'refresh_token_example_string_for_user49_unique_value', '2025-05-12 18:00:00', '2025-06-11 18:00:00', FALSE),
(50, 'refresh_token_example_string_for_user50_another_secure', '2025-05-12 18:05:00', '2025-06-11 18:05:00', FALSE);
(51, 'refresh_token_example_string_for_user51_unique_value', '2025-05-12 18:10:00', '2025-06-11 18:10:00', FALSE),
(52, 'refresh_token_example_string_for_user52_another_secure', '2025-05-12 18:15:00', '2025-06-11 18:15:00', FALSE),
(53, 'refresh_token_example_string_for_user53_unique_value', '2025-05-12 18:20:00', '2025-06-11 18:20:00', FALSE),
(54, 'refresh_token_example_string_for_user54_another_secure', '2025-05-12 18:25:00', '2025-06-11 18:25:00', FALSE),
(55, 'refresh_token_example_string_for_user55_unique_value', '2025-05-12 18:30:00', '2025-06-11 18:30:00', FALSE);
(56, 'refresh_token_example_string_for_user56_another_secure', '2025-05-12 18:35:00', '2025-06-11 18:35:00', FALSE),
(57, 'refresh_token_example_string_for_user57_unique_value', '2025-05-12 18:40:00', '2025-06-11 18:40:00', FALSE),
(58, 'refresh_token_example_string_for_user58_another_secure', '2025-05-12 18:45:00', '2025-06-11 18:45:00', FALSE),
(59, 'refresh_token_example_string_for_user59_unique_value', '2025-05-12 18:50:00', '2025-06-11 18:50:00', FALSE),
(60, 'refresh_token_example_string_for_user60_another_secure', '2025-05-12 18:55:00', '2025-06-11 18:55:00', FALSE);
(61, 'refresh_token_example_string_for_user61_unique_value', '2025-05-12 19:00:00', '2025-06-11 19:00:00', FALSE),
(62, 'refresh_token_example_string_for_user62_another_secure', '2025-05-12 19:05:00', '2025-06-11 19:05:00', FALSE),
(63, 'refresh_token_example_string_for_user63_unique_value', '2025-05-12 19:10:00', '2025-06-11 19:10:00', FALSE),
(64, 'refresh_token_example_string_for_user64_another_secure', '2025-05-12 19:15:00', '2025-06-11 19:15:00', FALSE),
(65, 'refresh_token_example_string_for_user65_unique_value', '2025-05-12 19:20:00', '2025-06-11 19:20:00', FALSE);
(66, 'refresh_token_example_string_for_user66_another_secure', '2025-05-12 19:25:00', '2025-06-11 19:25:00', FALSE),
(67, 'refresh_token_example_string_for_user67_unique_value', '2025-05-12 19:30:00', '2025-06-11 19:30:00', FALSE),
(68, 'refresh_token_example_string_for_user68_another_secure', '2025-05-12 19:35:00', '2025-06-11 19:35:00', FALSE),
(69, 'refresh_token_example_string_for_user69_unique_value', '2025-05-12 19:40:00', '2025-06-11 19:40:00', FALSE),
(70, 'refresh_token_example_string_for_user70_another_secure', '2025-05-12 19:45:00', '2025-06-11 19:45:00', FALSE);
(71, 'refresh_token_example_string_for_user71_unique_value', '2025-05-12 19:50:00', '2025-06-11 19:50:00', FALSE),
(72, 'refresh_token_example_string_for_user72_another_secure', '2025-05-12 19:55:00', '2025-06-11 19:55:00', FALSE),
(73, 'refresh_token_example_string_for_user73_unique_value', '2025-05-12 20:00:00', '2025-06-11 20:00:00', FALSE),
(74, 'refresh_token_example_string_for_user74_another_secure', '2025-05-12 20:05:00', '2025-06-11 20:05:00', FALSE),
(75, 'refresh_token_example_string_for_user75_unique_value', '2025-05-12 20:10:00', '2025-06-11 20:10:00', FALSE);
(76, 'refresh_token_example_string_for_user76_another_secure', '2025-05-12 20:15:00', '2025-06-11 20:15:00', FALSE),
(77, 'refresh_token_example_string_for_user77_unique_value', '2025-05-12 20:20:00', '2025-06-11 20:20:00', FALSE),
(78, 'refresh_token_example_string_for_user78_another_secure', '2025-05-12 20:25:00', '2025-06-11 20:25:00', FALSE),
(79, 'refresh_token_example_string_for_user79_unique_value', '2025-05-12 20:30:00', '2025-06-11 20:30:00', FALSE),
(80, 'refresh_token_example_string_for_user80_another_secure', '2025-05-12 20:35:00', '2025-06-11 20:35:00', FALSE);
(81, 'refresh_token_example_string_for_user81_unique_value', '2025-05-12 20:40:00', '2025-06-11 20:40:00', FALSE),
(82, 'refresh_token_example_string_for_user82_another_secure', '2025-05-12 20:45:00', '2025-06-11 20:45:00', FALSE),
(83, 'refresh_token_example_string_for_user83_unique_value', '2025-05-12 20:50:00', '2025-06-11 20:50:00', FALSE),
(84, 'refresh_token_example_string_for_user84_another_secure', '2025-05-12 20:55:00', '2025-06-11 20:55:00', FALSE),
(85, 'refresh_token_example_string_for_user85_unique_value', '2025-05-12 21:00:00', '2025-06-11 21:00:00', FALSE);
(86, 'refresh_token_example_string_for_user86_another_secure', '2025-05-12 21:05:00', '2025-06-11 21:05:00', FALSE),
(87, 'refresh_token_example_string_for_user87_unique_value', '2025-05-12 21:10:00', '2025-06-11 21:10:00', FALSE),
(88, 'refresh_token_example_string_for_user88_another_secure', '2025-05-12 21:15:00', '2025-06-11 21:15:00', FALSE),
(89, 'refresh_token_example_string_for_user89_unique_value', '2025-05-12 21:20:00', '2025-06-11 21:20:00', FALSE),
(90, 'refresh_token_example_string_for_user90_another_secure', '2025-05-12 21:25:00', '2025-06-11 21:25:00', FALSE);
(91, 'refresh_token_example_string_for_user91_unique_value', '2025-05-12 21:30:00', '2025-06-11 21:30:00', FALSE),
(92, 'refresh_token_example_string_for_user92_another_secure', '2025-05-12 21:35:00', '2025-06-11 21:35:00', FALSE),
(93, 'refresh_token_example_string_for_user93_unique_value', '2025-05-12 21:40:00', '2025-06-11 21:40:00', FALSE),
(94, 'refresh_token_example_string_for_user94_another_secure', '2025-05-12 21:45:00', '2025-06-11 21:45:00', FALSE),
(95, 'refresh_token_example_string_for_user95_unique_value', '2025-05-12 21:50:00', '2025-06-11 21:50:00', FALSE),
(96, 'refresh_token_example_string_for_user96_another_secure', '2025-05-12 21:55:00', '2025-06-11 21:55:00', FALSE),
(97, 'refresh_token_example_string_for_user97_unique_value', '2025-05-12 22:00:00', '2025-06-11 22:00:00', FALSE),
(98, 'refresh_token_example_string_for_user98_another_secure', '2025-05-12 22:05:00', '2025-06-11 22:05:00', FALSE),
(99, 'refresh_token_example_string_for_user99_unique_value', '2025-05-12 22:10:00', '2025-06-11 22:10:00', FALSE),
(100, 'refresh_token_example_string_for_user100_another_secure', '2025-05-12 22:15:00', '2025-06-11 22:15:00', FALSE);

