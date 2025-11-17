mergeInto(LibraryManager.library, {
  /**
   * YT_Show(url, x, y, w, h)
   * - url: UTF8 string (YouTube embed URL 권장)
   * - x,y,w,h: 화면 좌상단 기준 CSS 픽셀 좌표/크기
   *   (Unity 스크린 좌표에서 y는 상단 기준으로 뒤집어 전달해야 정확)
   */
  YT_Show: function(urlPtr, x, y, w, h) {
    const url = UTF8ToString(urlPtr);

    // 호스트 div 생성(1회)
    let host = document.getElementById('unity-yt-host');
    if (!host) {
      host = document.createElement('div');
      host.id = 'unity-yt-host';
      host.style.position = 'absolute';
      host.style.pointerEvents = 'auto'; // 클릭 통과
      host.style.zIndex = '10000';       // Unity 캔버스 위로
      host.style.background = 'transparent';
      document.body.appendChild(host);

      const iframe = document.createElement('iframe');
      iframe.id = 'unity-yt-iframe';
      iframe.frameBorder = '0';
      iframe.allow = 'accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share';
      iframe.allowFullscreen = true;
      iframe.style.width = '100%';
      iframe.style.height = '100%';
      iframe.style.display = 'block';
      iframe.style.background = 'black';
      host.appendChild(iframe);
    }

    // 위치/크기 적용
    host.style.left   = String(x) + 'px';
    host.style.top    = String(y) + 'px';
    host.style.width  = String(w) + 'px';
    host.style.height = String(h) + 'px';
    host.style.display = 'block';

    // URL 적용: "__unity_no_change__"면 src 유지
    const iframe = document.getElementById('unity-yt-iframe');
    if (url && url !== '__unity_no_change__') {
      // 자동재생 정책 우회: embed URL에 autoplay=1과 playsinline=1 권장
      iframe.src = url;
    }
  },

  /**
   * YT_Move(x, y, w, h)
   * - 위치/크기만 갱신(재생 중단 없이)
   */
  YT_Move: function(x, y, w, h) {
    const host = document.getElementById('unity-yt-host');
    if (!host) return;
    host.style.left   = String(x) + 'px';
    host.style.top    = String(y) + 'px';
    host.style.width  = String(w) + 'px';
    host.style.height = String(h) + 'px';
    host.style.display = 'block';
  },

  /**
   * YT_Hide()
   * - iframe 가리기(중단은 안 함)
   */
  YT_Hide: function() {
    const host = document.getElementById('unity-yt-host');
    if (host) host.style.display = 'none';
  },

  /**
   * YT_Dispose()
   * - 완전히 제거(필요 시 메모리 정리)
   */
  YT_Dispose: function() {
    const host = document.getElementById('unity-yt-host');
    if (host && host.parentNode) {
      host.parentNode.removeChild(host);
    }
  }
});
