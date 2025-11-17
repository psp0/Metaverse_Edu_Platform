mergeInto(LibraryManager.library, {
  HTML_CreateIframe: function(idPtr, urlPtr) {
    const id  = UTF8ToString(idPtr);
    const url = UTF8ToString(urlPtr);

    // 유니티 캔버스/부모 탐색
    const canvas = document.querySelector("#unity-canvas") || Module.canvas;
    const parent = canvas.parentElement || document.body;
    if (getComputedStyle(parent).position === 'static') {
      parent.style.position = 'relative'; // 자식 absolute 기준점
    }

    // 기존 있으면 재사용
    let iframe = document.getElementById(id);
    if (!iframe) {
      iframe = document.createElement('iframe');
      iframe.id = id;
      iframe.style.position = 'absolute';
      iframe.style.border = '0';
      iframe.style.zIndex = '9999';
      iframe.style.background = 'white'; // 필요 시 transparent로
      iframe.style.display = 'none';     // 처음엔 숨김
      iframe.allow = 'autoplay; fullscreen; clipboard-read; clipboard-write';
      parent.appendChild(iframe);
    }
    iframe.src = url;

    // 상태 저장
    window.__unity_iframes = window.__unity_iframes || {};
    window.__unity_iframes[id] = window.__unity_iframes[id] || {};
    const S = window.__unity_iframes[id];
    S.parent = parent;
    S.canvas = canvas;
    S.iframe = iframe;

    // 기본값 안전 처리 (구버전 JS 호환)
    S.nx = (S.nx !== undefined && S.nx !== null) ? S.nx : 0;
    S.ny = (S.ny !== undefined && S.ny !== null) ? S.ny : 0;
    S.nw = (S.nw !== undefined && S.nw !== null) ? S.nw : 1;
    S.nh = (S.nh !== undefined && S.nh !== null) ? S.nh : 1;

    // 리사이즈마다 캔버스 기준으로 위치 재계산
    S.onResize = function() {
      const rect = S.canvas.getBoundingClientRect(); // CSS px
      const left   = rect.left + S.nx * rect.width;
      const top    = rect.top  + S.ny * rect.height;
      const width  = S.nw * rect.width;
      const height = S.nh * rect.height;
      Object.assign(S.iframe.style, {
        left: left + 'px',
        top: top + 'px',
        width: width + 'px',
        height: height + 'px'
      });
    };
    window.addEventListener('resize', S.onResize);
    S.onResize();
  },

  HTML_SetIframeRectNormalized: function(idPtr, nx, ny, nw, nh) {
    const id = UTF8ToString(idPtr);
    const map = window.__unity_iframes || {};
    const S = map[id]; if (!S) return;
    S.nx = nx; S.ny = ny; S.nw = nw; S.nh = nh;
    if (S.onResize) S.onResize();
  },

  HTML_SetIframeVisible: function(idPtr, visible) {
    const id = UTF8ToString(idPtr);
    const map = window.__unity_iframes || {};
    const S = map[id]; if (!S) return;
    S.iframe.style.display = visible ? 'block' : 'none';
  },

  HTML_SetIframeURL: function(idPtr, urlPtr) {
    const id  = UTF8ToString(idPtr);
    const url = UTF8ToString(urlPtr);
    const map = window.__unity_iframes || {};
    const S = map[id]; if (!S) return;
    S.iframe.src = url;
  },

  HTML_SetIframeInteractable: function(idPtr, interactable) {
    const id = UTF8ToString(idPtr);
    const map = window.__unity_iframes || {};
    const S = map[id]; if (!S) return;
    S.iframe.style.pointerEvents = interactable ? 'auto' : 'none';
  },

  HTML_DestroyIframe: function(idPtr) {
    const id = UTF8ToString(idPtr);
    const map = window.__unity_iframes || {};
    const S = map[id]; if (!S) return;
    window.removeEventListener('resize', S.onResize);
    S.iframe.remove();
    delete map[id];
  }
});
