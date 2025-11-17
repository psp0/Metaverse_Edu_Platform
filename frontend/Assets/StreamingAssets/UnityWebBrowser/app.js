
// ===== app.js (fixed: KaTeX-safe rendering, custom quiz fixes, review mode improvements) =====

// -------------------- 전역 상태 --------------------
let fullUnitData = {}; 
let navHistory = []; 
let currentSubUnitGroupId = null; 
let oldProficiency = 0; 
let reviewResults = []; 

let selectedLecture = null;
let conceptQuizzes = [];
let customQuizzes = []; // previously missing global
let quizIdx = 0;
let customIdx = 0;
let pickedOptionId = null;
let customAnswers = {};
let accessToken = null;

// [KaTeX 렌더링 헬퍼 함수]
function renderMath(text) {
  if (text === null || text === undefined) return "";
  let processedText = String(text);

  // Normalize common escapes and newlines
  processedText = processedText.replace(/\\\\/g, "\\");
  processedText = processedText.replace(/\\n/g, '<br>');

  // If already contains $ delimiters or \( \) or \[ \], don't auto-wrap
  const hasDelimiters = /(\$[^$]+\$|\\\([^)]*\\\)|\\\[[^\]]*\\\])/m.test(processedText);
  // If it looks like plain variable-power patterns like x^{2} convert to inline math if no delimiters
  if (!hasDelimiters) {
    // Wrap common math fragments with $...$ if it contains ^, _, \frac, \sqrt or backslash-Latex patterns
    if (/[\\^_]|\\frac|\\sqrt|{|}/.test(processedText)) {
      processedText = '$' + processedText + '$';
    }
  }

  // If KaTeX auto-render function is not available, return escaped HTML (remove $)
  if (typeof renderMathInElement !== 'function') {
    return processedText.replace(/\$/g, '');
  }

  const tempDiv = document.createElement('div');
  tempDiv.innerHTML = processedText;

  try {
    renderMathInElement(tempDiv, {
      delimiters: [
        {left: "$$", right: "$$", display: true},
        {left: "$", right: "$", display: false},
        {left: "\\(", right: "\\)", display: false},
        {left: "\\[", right: "\\]", display: true}
      ],
      throwOnError: false,
      errorColor: '#cc0000'
    });
  } catch (error) {
    console.warn("KaTeX 렌더링 오류:", error, "원본:", text);
  }
  return tempDiv.innerHTML;
}

// -------------------- 환경 설정 --------------------
const API_BASE = "http://localhost/api";
const SUBJECT_MAP = { KOR: 3, ENG: 2, MATH: 1 };

// -------------------- Unity → JS (토큰 전달) --------------------
function setAuthToken(token) {
  console.log("✅ Token received from Unity:", token);
  accessToken = token;
  localStorage.setItem("accessToken", token);
}
window.setAuthToken = setAuthToken;

// 🔹 아래처럼 “수동 토큰”을 직접 세팅
accessToken = "";

// ✅ 브라우저 새로고침해도 유지되게
localStorage.setItem("accessToken", accessToken);

function getToken() {
  return accessToken || localStorage.getItem("accessToken") || "";
}
// -------------------- 공통 Fetch --------------------
async function apiFetch(path, options = {}) {
  const token = getToken();
  const headers = {
    Accept: "application/json",
    ...(options.body && { "Content-Type": "application/json" }),
    ...(token ? { Authorization: `Bearer ${token}` } : {}),
  };
  const res = await fetch(`${API_BASE}${path}`, { ...options, headers, cache: 'no-store' });
  const text = await res.text();
  if (!res.ok) {
    console.error(`❌ API Error ${res.status}: ${path}`);
    throw new Error(text || `API ${res.status}`);
  }
  return text ? JSON.parse(text) : null;
}

// -------------------- 🎨 UI/네비게이션 --------------------
function showPanel(name) {
  ["panel-index", "panel-video", "panel-quiz", "panel-customquiz", "panel-results-modal"].forEach((id) => {
    document.getElementById(id).classList.add("hidden");
  });
  if (name !== 'results-modal') {
    document.getElementById("panel-" + name).classList.remove("hidden");
  }

  const exitBtn = document.querySelector('header .exitBtn');
  const titleEl = document.getElementById('subjectTitle');

  if (name === 'index') {
    // "목록" 화면: navHistory에 따라 '뒤로' 버튼 (goBack) 또는 '숨김' 처리
    updateNavUI(); 
  } else if (name === 'results-modal') {
    // "결과 모달" 화면: '뒤로' 버튼 숨김
    document.getElementById("panel-results-modal").classList.remove("hidden");
    exitBtn.classList.add('hidden'); 
  } else {
    // "퀴즈", "영상" 등 화면:
    const currentLevel = navHistory[navHistory.length - 1];
    if (currentLevel) {
        titleEl.textContent = currentLevel.title;
    }
    exitBtn.textContent = '← 뒤로';
    exitBtn.classList.remove('hidden');

    // ★★★ 핵심: 'goBackAndRefresh' 함수를 호출하도록 변경 ★★★
    exitBtn.onclick = goBackAndRefresh; 
  }
}

function updateNavUI() {
  const exitBtn = document.querySelector('header .exitBtn');
  const titleEl = document.getElementById('subjectTitle');

  if (navHistory.length > 1) { 
    const currentLevel = navHistory[navHistory.length - 1];
    titleEl.textContent = currentLevel.title;
    exitBtn.textContent = '← 뒤로';
    exitBtn.classList.remove('hidden');
    exitBtn.onclick = goBack; 
  } else if (navHistory.length === 1) { 
    const currentLevel = navHistory[0];
    titleEl.textContent = currentLevel.title;
    exitBtn.classList.add('hidden'); 
  } else {
    titleEl.textContent = '강좌 로딩 중...';
    exitBtn.classList.add('hidden');
  }
}

function goBack() {
  if (navHistory.length <= 1) return; 
  
  navHistory.pop(); // 현재 레벨(e.g., 'subs')을 제거
  
  const prevLevel = navHistory[navHistory.length - 1]; // 돌아갈 레벨(e.g., 'groups')

  let freshData;
  if (prevLevel.type === 'units') {
    freshData = fullUnitData['MATH'];
    renderUnits(freshData);
  } 
  else if (prevLevel.type === 'groups') {
    for (const unit of fullUnitData['MATH']) {
      if (unit.subUnitGroups.some(g => g.subUnitGroupId === prevLevel.data[0]?.subUnitGroupId)) {
        freshData = unit.subUnitGroups;
        break;
      }
    }
    renderSubUnitGroups(freshData || prevLevel.data);
  } 
  else if (prevLevel.type === 'subs') {
    for (const unit of fullUnitData['MATH']) {
      for (const group of unit.subUnitGroups) {
        if (group.subUnits.some(s => s.subUnitId === prevLevel.data[0]?.subUnitId)) {
          freshData = group.subUnits;
          break;
        }
      }
      if (freshData) break;
    }
    renderSubUnits(freshData || prevLevel.data);
  }
  
  updateNavUI();

  // ★★★★★★★★★★★★★★★★★★★★★★★
  // 이 코드가 문제를 해결합니다.
  // (숨겨진 목록을 갱신한 뒤, 그 목록을 보여줍니다)
  showPanel('index');
  // ★★★★★★★★★★★★★★★★★★★★★★★
}
// [신규 함수] 데이터를 새로고침한 뒤, 이전 화면으로 돌아갑니다.
// [신규 함수] 데이터를 새로고침한 뒤, "이전" 화면이 아닌 "현재" 목록으로 돌아갑니다.
async function goBackAndRefresh() {
  console.log("Exiting panel and refreshing current list...");

  // 1. 모든 데이터를 백그라운드에서 새로고침
  await refreshAllData('MATH');

  // 2. 돌아가야 할 "현재" 레벨(e.g., 'subs')을 가져옵니다.
  //    (navHistory.pop()을 하지 않습니다!)
  const currentLevel = navHistory[navHistory.length - 1];
  
  if (!currentLevel) {
    goHome(); // 비상시 goHome 호출
    return;
  }

  // 3. 갱신된 fullUnitData에서 "현재" 레벨에 맞는 데이터를 다시 찾습니다.
  let freshData;
  if (currentLevel.type === 'units') {
    freshData = fullUnitData['MATH'];
    renderUnits(freshData);
  } 
  else if (currentLevel.type === 'groups') {
    for (const unit of fullUnitData['MATH']) {
      // currentLevel.data[0]는 낡은 데이터일 수 있으므로, ID로 비교
      if (unit.subUnitGroups.some(g => g.subUnitGroupId === currentLevel.data[0]?.subUnitGroupId)) {
        freshData = unit.subUnitGroups;
        break;
      }
    }
    renderSubUnitGroups(freshData || currentLevel.data);
  } 
  else if (currentLevel.type === 'subs') {
    for (const unit of fullUnitData['MATH']) {
      for (const group of unit.subUnitGroups) {
        if (group.subUnits.some(s => s.subUnitId === currentLevel.data[0]?.subUnitId)) {
          freshData = group.subUnits;
          break;
        }
      }
      if (freshData) break;
    }
    renderSubUnits(freshData || currentLevel.data);
  }
  
  // 4. 헤더 제목 등을 업데이트
  updateNavUI();

  // 5. 갱신된 내용이 적용된 'index' 패널을 보여줍니다.
  showPanel('index');
}

// [기존 함수] function goHome() { ...
function goHome() {
  showPanel('index');
  navHistory = []; 
  reviewResults = []; 
  fullUnitData = {};
  loadSubject('MATH').catch(console.error); 
}

function escapeHtml(s) {
  return String(s || "").replace(/[&<>"']/g, (m) => ({ "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;" }[m]));
}

// -------------------- 📈 진행도 계산 --------------------
function calculateAverageProgress(units) {
  units.forEach(unit => {
    let totalProgress = 0;
    let groupCount = 0;

    unit.subUnitGroups.forEach(group => {
      let subTotalProgress = 0;
      let subCount = 0;

      group.subUnits.forEach(sub => {
        subTotalProgress += (sub.progressPercentage || 0);
        subCount++;
      });

      group.avgProgress = (subCount > 0) ? (subTotalProgress / subCount) : 0;
      totalProgress += group.avgProgress;
      groupCount++;
    });

    unit.avgProgress = (groupCount > 0) ? (totalProgress / groupCount) : 0;
  });
}

async function refreshAllData(subjectKey = 'MATH') {
  console.log('Refreshing all data...');
  const subjectId = SUBJECT_MAP[subjectKey];
  if (!subjectId) {
    console.error("Invalid subject key:", subjectKey);
    return null;
  }
  try {
    // apiFetch는 cache: 'no-store'가 이미 적용되어 있음
    const data = await apiFetch(`/classroom/subjects/${subjectId}/units`);
    calculateAverageProgress(data);
    fullUnitData[subjectKey] = data; // 새 데이터로 덮어쓰기
    return data;
  } catch (err) {
    console.error("Data refresh failed:", err);
    return null;
  }
}

// -------------------- 📚 과목/단원 로드 --------------------
async function loadSubject(subjectKey = 'MATH') {
  let units;
  // 데이터가 없거나, 리뷰가 끝났으면 새 데이터를 불러옴
  if (!fullUnitData[subjectKey] || reviewResults.length > 0) { 
    units = await refreshAllData(subjectKey); // ★ 신규 함수 호출
    reviewResults = []; // 리뷰 플래그 초기화
  } else {
    units = fullUnitData[subjectKey];
  }

  if (!units) return; // 데이터 로드 실패 시 중단

  navHistory = [{ 
    type: 'units', 
    data: units, 
    title: `${subjectKey} 과목` 
  }];

  renderUnits(units); 
  updateNavUI(); 
}

function renderUnits(units) {
  const list = document.getElementById('list');
  list.innerHTML = ""; 

  if (!units?.length) {
    list.innerHTML = `<div class="card"><div class="info"><div class="title">컨텐츠가 없습니다</div></div></div>`;
    return;
  }

  units.forEach(unit => {
    const div = document.createElement('div');
    div.className = 'card';
    const progress = unit.avgProgress.toFixed(2);

    div.innerHTML = `
      <div class="info">
        <div class="title">${unit.chapterNo}. ${unit.title}</div>
        <div class="progress-display">
          <div class="progress-bar-bg">
            <div class="progress-bar-fill" style="width: ${progress}%;"></div>
          </div>
          <div class="progress">진행도: ${progress}%</div>
        </div>
      </div>
      <div class="btns">
        <button class="btn primary" onclick="event.stopPropagation(); showSubUnitGroups(${unit.unitId}, '${escapeHtml(unit.title)}')">
          학습하기
        </button>
      </div>`;
    div.onclick = () => showSubUnitGroups(unit.unitId, unit.title);
    list.appendChild(div);
  });
}

function showSubUnitGroups(unitId, unitTitle) {
  const subjectKey = 'MATH';
  const unit = fullUnitData[subjectKey].find(u => u.unitId === unitId);
  if (!unit || !unit.subUnitGroups) return;

  navHistory.push({ 
    type: 'groups', 
    data: unit.subUnitGroups, 
    title: unit.title 
  });

  renderSubUnitGroups(unit.subUnitGroups);
  updateNavUI();
}

function renderSubUnitGroups(groups) {
  const list = document.getElementById('list');
  list.innerHTML = ""; 

  groups.forEach(group => {
    const allSubUnitsCompleted = (group.avgProgress >= 100);
    const progress = group.avgProgress.toFixed(2);

    const div = document.createElement('div');
    div.className = 'card';
    div.innerHTML = `
      <div class="info">
        <div class="title">${group.subunitTitle}</div>
        <div class="progress-display">
          <div class="progress-bar-bg">
            <div class="progress-bar-fill" style="width: ${progress}%;"></div>
          </div>
          <div class="progress">개념 진행도: ${progress}%</div>
        </div>
        <div class="progress" style="color: #2bb673; font-weight: bold; margin-top: 10px;">숙달도: ${group.proficiencyPercentage || 0}%</div>
      </div>
      <div class="btns">
        <button class="btn" onclick="event.stopPropagation(); showSubUnits(${group.subUnitGroupId}, '${escapeHtml(group.subunitTitle)}')">
          개념학습
        </button>
        <button 
          class="btn primary ${allSubUnitsCompleted ? '' : 'disabled'}" 
          onclick="${allSubUnitsCompleted ? `event.stopPropagation(); openCustom(${group.subUnitGroupId}, ${group.proficiencyPercentage || 0})` : 'event.preventDefault();'}">
          맞춤문제
        </button>
      </div>`;
    div.onclick = () => showSubUnits(group.subUnitGroupId, group.subunitTitle);
    list.appendChild(div);
  });
}

function showSubUnits(groupId, groupTitle) {
  const subjectKey = 'MATH';
  let foundGroup = null;
  for (const unit of fullUnitData[subjectKey]) {
    foundGroup = unit.subUnitGroups.find(g => g.subUnitGroupId === groupId);
    if (foundGroup) break;
  }
  if (!foundGroup || !foundGroup.subUnits) return;

  navHistory.push({ 
    type: 'subs', 
    data: foundGroup.subUnits, 
    title: groupTitle 
  });

  renderSubUnits(foundGroup.subUnits);
  updateNavUI();
}

function renderSubUnits(subUnits) {
  const list = document.getElementById('list');
  list.innerHTML = ""; 

  subUnits.forEach(sub => {
    const div = document.createElement('div');
    div.className = 'card';
    const lectureProg = (sub.lectureProgress || 0).toFixed(2);
    const quizProg = (sub.quizProgress || 0).toFixed(2);

    div.innerHTML = `
      <div class="info">
        <div class="title">${sub.detail}</div>

        <div class="progress-display">
          <div class="progress-bar-bg">
            <div class="progress-bar-fill" style="width: ${lectureProg}%;"></div>
          </div>
          <div class="progress">강의 진행도: ${lectureProg}%</div>
        </div>

        <div class="progress-display" style="margin-top: 8px;">
          <div class="progress-bar-bg">
            <div class="progress-bar-fill" style="width: ${quizProg}%; background-color: rgb(66, 99, 235);"></div>
          </div>
          <div class="progress">퀴즈 진행도: ${quizProg}%</div>
        </div>

      </div>
      <div class="btns">
        <button class="btn" onclick="openVideo(${sub.subUnitId}, '${escapeHtml(sub.detail)}', '${sub.contentUrl}')">
          개념 강의
        </button>
        <button class="btn primary" onclick="openQuiz(${sub.subUnitId})">
          개념 퀴즈
        </button>
      </div>`;
    list.appendChild(div);
  });
}

// -------------------- 개념공부 (기존과 동일) --------------------
function extractYouTubeId(url) {
  if (!url) return null;
  try {
    const u = new URL(url);
    if (u.hostname.includes("youtu.be")) return u.pathname.slice(1);
    if (u.searchParams.get("v")) return u.searchParams.get("v");
    // fallback: if url itself is id
  } catch (_) {}
  if (/^[a-zA-Z0-9_-]{11}$/.test(url)) return url;
  return null;
}

function openVideo(subUnitId, title, contentUrl) {
  selectedLecture = { id: subUnitId, title, contentUrl };
  const titleEl = document.getElementById("videoTitle");
  if (titleEl) titleEl.innerHTML = renderMath(title);
  const vid = extractYouTubeId(contentUrl);
  const yt = document.getElementById("ytplayer");
  if (yt && vid)
    yt.innerHTML = `<iframe width="640" height="360" src="https://www.youtube.com/embed/${vid}" frameborder="0" allowfullscreen></iframe>`;
  else if (yt)
    yt.innerHTML = `<div style="padding:20px; text-align:center; color:red;">강의 영상(contentUrl)이 없습니다.</div>`;
  showPanel("video");
}

// -------------------- 개념퀴즈 (v22 수정: '이전' 버튼 로직 보강) --------------------
async function openQuiz(subUnitId) {
  try {
    const quizData = await apiFetch(`/quizzes/sub-unit/${subUnitId}/state`);
    conceptQuizzes = quizData?.quizzes || [];
    if (!conceptQuizzes.length) return alert("⚠️ 퀴즈가 없습니다.");
    quizIdx = 0;
    pickedOptionId = null;
    renderQuiz();
    showPanel("quiz");
  } catch (err) {
    alert("퀴즈 불러오기 실패");
    console.error(err);
  }
}

function renderQuiz() {
  const quiz = conceptQuizzes[quizIdx];
  const qEl = document.getElementById("qtext");
  const wrap = document.getElementById("choices");
  const resEl = document.getElementById("quizResult");
  const btn = document.getElementById("submitBtn");
  const prevBtn = document.getElementById("prevBtn"); // [신규] 이전 버튼

  const isAlreadySolved = quiz.userSelectedOptionId != null && quiz.userSelectedOptionId !== 0;

  qEl.innerHTML = renderMath(`(${quizIdx + 1}/${conceptQuizzes.length}) ${quiz.content}`);
  wrap.innerHTML = "";
  resEl.innerHTML = "";

  quiz.options.forEach((opt) => {
    const o = document.createElement("div");
    o.className = "opt";
    o.innerHTML = renderMath(`${opt.label}. ${opt.optionText}`);

    if (isAlreadySolved) {
      o.classList.add("disabled"); 
      if (opt.optionId === quiz.correctOptionId) {
        o.classList.add("correct-answer"); 
      }
      if (opt.optionId === quiz.userSelectedOptionId && !quiz.isCorrect) {
        o.classList.add("user-answer", "incorrect"); 
      }
    } else {
      o.onclick = () => {
        pickedOptionId = opt.optionId;
        renderQuiz(); 
      };
      if (pickedOptionId === opt.optionId) {
        o.classList.add("selected");
      }
    }
    wrap.appendChild(o);
  });

  // render any math inside choices area (defensive)
  try {
    renderMathInElement(wrap, {
      delimiters: [
        {left: "$$", right: "$$", display: true},
        {left: "$", right: "$", display: false},
        {left: "\\(", right: "\\)", display: false},
        {left: "\\[", right: "\\]", display: true}
      ],
      throwOnError: false
    });
  } catch (e) { /* ignore */ }

  // --- [신규] 이전 버튼 로직 ---
  if (quizIdx > 0) {
    prevBtn.classList.remove("hidden");
    prevBtn.onclick = () => {
      quizIdx--;
      pickedOptionId = null; // 이전 문제로 갈 때 선택 초기화
      renderQuiz();
    };
  } else {
    prevBtn.classList.add("hidden"); // 첫 번째 문제일 땐 숨김
  }

  // --- 하단 버튼 및 해설 (수정) ---
  if (isAlreadySolved) {
    const correct = quiz.isCorrect;
    resEl.innerHTML = `
      ${correct ? "✅" : "❌"}<br>
      <span style="color:#555;">${renderMath(quiz.commentary)}</span>
    `;
    resEl.style.color = correct ? "#2bb673" : "#e53935";

    btn.textContent = quizIdx < conceptQuizzes.length - 1 ? "다음" : "끝내기";
    btn.onclick = async () => {
      if (quizIdx < conceptQuizzes.length - 1) {
        quizIdx++;
        pickedOptionId = null; 
        renderQuiz();
      } else {
        await loadSubject('MATH');
        goHome();
      }
    };
  } else {
    btn.textContent = "제출";
    btn.onclick = async () => {
      if (!pickedOptionId) return alert("선택지를 고르세요!");

      const res = await apiFetch("/quizzes/answer", {
        method: "POST",
        body: JSON.stringify({
          quizId: quiz.quizId,
          selectedOptionId: pickedOptionId,
        }),
      });

      quiz.isCorrect = res.correct;
      quiz.commentary = res.commentary;
      quiz.correctOptionId = res.correctOptionId;
      quiz.userSelectedOptionId = pickedOptionId;

      renderQuiz(); 
    };
  }
}

// -------------------- 맞춤문제 (완전 보강) --------------------
async function openCustom(subUnitGroupId, proficiency) {
  try {
    currentSubUnitGroupId = subUnitGroupId; 
    oldProficiency = proficiency || 0; 

    customQuizzes = await apiFetch(`/custom-problems/group/${subUnitGroupId}`);
    if (!customQuizzes?.length) return alert("⚠️ 맞춤문제가 없습니다.");

    customIdx = 0;
    customAnswers = {}; 
    reviewResults = []; 
    renderCustomQuiz(false); 
    showPanel("customquiz");
  } catch (err) {
    alert("맞춤문제 불러오기 실패");
    console.error(err);
  }
}

// 안전한 옵션 파싱: 서버에서 optionsList 혹은 options 문자열로 올 수 있음
function safeParseOptions(q) {
  if (!q) return [];
  if (Array.isArray(q.optionsList)) return q.optionsList;
  if (Array.isArray(q.options)) return q.options;
  // sometimes options are provided as a string that looks like "['$a$','$b$']" - try to convert to JSON
  if (typeof q.options === 'string') {
    try {
      // convert single-quotes to double-quotes (careful)
      const s = q.options.replace(/'/g, '"');
      return JSON.parse(s);
    } catch (e) {
      // fallback: try to split by comma (very defensive)
      const parts = q.options.split(',').map(p => p.trim());
      return parts.map(p => p.replace(/^"(.*)"$/, '$1').replace(/^'(.*)'$/, '$1'));
    }
  }
  return [];
}

function renderCustomQuiz(isReviewMode = false) {
  const quiz = customQuizzes[customIdx];
  const qEl = document.getElementById("cqtext");
  const wrap = document.getElementById("cchoices");
  const cnt = document.getElementById("counter");
  const nextBtn = document.getElementById("nextBtn");
  const prevBtn = document.getElementById("cPrevBtn"); // [신규] 이전 버튼
  const omrWrap = document.getElementById("omrButtons");
  const reviewData = isReviewMode ? reviewResults.find(r => r.problemId === quiz.id) : null;

  const levelEl = document.getElementById("cqlevel");
  if (levelEl) {
    levelEl.textContent = `Level ${quiz.level}`;
  }

  omrWrap.innerHTML = ""; 
  customQuizzes.forEach((q, i) => {
    const omr = document.createElement("button");
    omr.className = "omr-btn";
    omr.textContent = i + 1;
    if (i === customIdx) omr.classList.add('active'); 

    if (isReviewMode) {
      const res = reviewResults.find(r => r.problemId === q.id);
      if (res) omr.classList.add(res.correct ? 'correct' : 'incorrect');
    }

    omr.onclick = () => {
      customIdx = i;
      renderCustomQuiz(isReviewMode); 
    };
    omrWrap.appendChild(omr);
  });

  let qPrefix = isReviewMode && reviewData ? (reviewData.correct ? '✅' : '❌') : '';

  cnt.textContent = `${customIdx + 1} / ${customQuizzes.length}`;

  // 문제 텍스트: 반드시 renderMath 로 감싸 출력
  qEl.innerHTML = `${qPrefix} ${renderMath(quiz.textbody)}`;

  wrap.innerHTML = "";

  const options = safeParseOptions(quiz);
  if (options && options.length > 0) {
    options.forEach((opt, i) => {
      const d = document.createElement("div");
      d.className = "opt";
      // render option text through renderMath
      d.innerHTML = renderMath(`${i + 1}. ${opt}`);

      if (isReviewMode) {
        const userAnswer = reviewData ? reviewData.userAnswer : null;
        const correctAnswer = String(quiz.answer); 

        d.classList.add("disabled"); 

        if (String(i + 1) === correctAnswer) {
          d.classList.add("correct-answer"); 
        }
        if (String(i + 1) === userAnswer && userAnswer !== correctAnswer) {
          d.classList.add("user-answer", "incorrect"); 
        }

      } else {
        const currentAnswer = customAnswers[quiz.id];
        if (currentAnswer === String(i + 1)) {
          d.classList.add("selected");
        }
        d.onclick = () => {
          customAnswers[quiz.id] = String(i + 1); 
          renderCustomQuiz(false); 
        };
      }
      wrap.appendChild(d);
    });
  } else {
    // short answer input
    const input = document.createElement("input");
    input.type = "text";
    input.className = "short-answer";

    if (isReviewMode) {
      input.value = reviewData ? (reviewData.userAnswer || "") : ""; 
      input.disabled = true; 
      wrap.appendChild(input);

      const answerBox = document.createElement('div');
      answerBox.className = 'review-answer-box';
      answerBox.innerHTML = `
        <div>당신의 답: <span class="user">${renderMath(reviewData ? (reviewData.userAnswer || '(입력 안함)') : '(입력 안함)')}</span></div>
        <div>정답: <span class="correct">${renderMath(quiz.answer)}</span></div>
      `;
      wrap.appendChild(answerBox);

    } else {
      input.placeholder = "정답 입력";
      input.value = customAnswers[quiz.id] || ""; 
      input.oninput = () => (customAnswers[quiz.id] = input.value.trim());
      wrap.appendChild(input);
    }
  }

  // --- [신규] 이전 버튼 로직 ---
  if (customIdx > 0) {
    prevBtn.classList.remove("hidden");
    prevBtn.onclick = () => {
      customIdx--;
      renderCustomQuiz(isReviewMode);
    };
  } else {
    prevBtn.classList.add("hidden");
  }

  // --- 하단 버튼 로직 (수정) ---
  if (isReviewMode) {
    nextBtn.textContent = customIdx < customQuizzes.length - 1 ? "다음 문제" : "목록으로";
    nextBtn.onclick = () => {
      if (customIdx < customQuizzes.length - 1) {
        customIdx++;
        renderCustomQuiz(true);
      } else {
        goHome(); 
      }
    };
  } else {
    nextBtn.textContent = customIdx < customQuizzes.length - 1 ? "다음" : "제출";
    nextBtn.onclick = () => {
      if (customIdx < customQuizzes.length - 1) {
        customIdx++;
        renderCustomQuiz(false);
      } else {
        if (confirm(`모든 문제를 제출하시겠습니까? 총 ${customQuizzes.length}문제 중 ${Object.keys(customAnswers).length}문제를 풀었습니다.`)) {
          submitCustomAnswers();
        }
      }
    };
  }

  // Ensure math inside current panel is rendered (renderMathInElement -> specific container)
  try {
    renderMathInElement(document.getElementById('panel-customquiz'), {
      delimiters: [
        {left: "$$", right: "$$", display: true},
        {left: "$", right: "$", display: false},
        {left: "\\(", right: "\\)", display: false},
        {left: "\\[", right: "\\]", display: true}
      ],
      throwOnError: false
    });
  } catch (e) {
    // ignore
  }
}

async function submitCustomAnswers() {
  const answers = Object.entries(customAnswers).map(([pid, ans]) => ({
    problemId: pid,
    userAnswer: ans,
  }));

  if (!currentSubUnitGroupId) {
    alert("오류: 소단원 그룹 ID가 없습니다.");
    return;
  }

  try {
    const res = await apiFetch("/custom-problems/submit", {
      method: "POST",
      body: JSON.stringify({ 
        subUnitGroupId: currentSubUnitGroupId, 
        answers: answers 
      }),
    });

    reviewResults = res.results || []; 
    showResultsModal(reviewResults, res.newProficiencyPercentage);

  } catch (err) {
    alert("제출에 실패했습니다.");
    console.error(err);
  }
}

function showResultsModal(results, newProficiency) {
  const newProf = newProficiency || 0;
  const oldProf = oldProficiency || 0;
  const change = newProf - oldProf;

  const correctCount = results.filter(r => r.correct).length;
  const totalCount = customQuizzes.length;

  document.getElementById('res-stats-score').textContent = `${correctCount} / ${totalCount}`;
  document.getElementById('res-stats-prof').innerHTML = 
    `${oldProf.toFixed(2)}% &rarr; ${newProf.toFixed(2)}% 
     <span style="color: ${change >= 0 ? '#2bb673' : '#e53935'};">
       (${change >= 0 ? '▲' : '▼'}${Math.abs(change).toFixed(2)}%)
     </span>`;

  document.getElementById('btn-review-results').onclick = startReviewMode;
  document.getElementById('btn-back-to-list').onclick = goHome; // goHome() 호출로 변경

showPanel('results-modal');

}

function startReviewMode() {
  customIdx = 0; 
  renderCustomQuiz(true); 
  showPanel('customquiz'); 
}

// -------------------- 초기 실행 --------------------
window.onload = () => {
  document.querySelector('header .logo').onclick = goHome;
  document.querySelector('header .exitBtn').onclick = goBack; 

  document.querySelector("button[onclick=\"loadSubject('ALL')\"]").onclick = (e) => {
    e.preventDefault(); 
    alert('\'전체강좌\'는 현재 지원되지 않습니다. \'수학\'을 선택해주세요.'); 
    loadSubject('MATH');
  };
  document.querySelector("button[onclick=\"loadSubject('KOR')\"]").onclick = (e) => {
    e.preventDefault(); loadSubject('KOR');
  };
  document.querySelector("button[onclick=\"loadSubject('ENG')\"]").onclick = (e) => {
    e.preventDefault(); loadSubject('ENG');
  };
  document.querySelector("button[onclick=\"loadSubject('MATH')\"]").onclick = (e) => {
    e.preventDefault(); loadSubject('MATH');
  };
  document.querySelector("button[onclick=\"loadSubject('RECENT')\"]").onclick = (e) => {
    e.preventDefault(); alert('\'최근학습\'은 현재 지원되지 않습니다.');
  };

  console.log("📄 App Loaded → '수학' 과목 기본 로드");
  goHome(); 
};
