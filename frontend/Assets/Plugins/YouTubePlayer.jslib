mergeInto(LibraryManager.library, {
  YT_GetCurrentTime: function () {
    if (window.youtubePlayer && window.youtubePlayer.getCurrentTime) {
      return window.youtubePlayer.getCurrentTime();
    }
    return 0;
  }
});
