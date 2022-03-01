function showList(e) {
    var $gridCont = $(".grid-container");
    e.preventDefault();
    $gridCont.hasClass("list-view")
      ? $gridCont.removeClass("list-view")
      : $gridCont.addClass("list-view");
  }
  function gridList(e) {
    var $gridCont = $(".grid-container");
    e.preventDefault();
    $gridCont.removeClass("list-view");
  }
  
  $(document).on("click", ".hover-slide-down", gridList);
  $(document).on("click", ".hover-slide-up", showList);
  