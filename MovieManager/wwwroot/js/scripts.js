/*REMOVE MOVIE BUTTON*/
function removeMovieLeft1(btnstate) {
    var eDiv = document.getElementById("LeftMovie1");
    eDiv.parentNode.removeChild(eDiv);
}

/*******************************
* ACCORDION WITH TOGGLE ICONS IN ABOUT PAGE
*******************************/
function toggleIcon(e) {
    $(e.target)
        .prev('.panel-heading')
        .find(".more-less")
        .toggleClass('glyphicon-plus glyphicon-minus');
}
$('.panel-group').on('hidden.bs.collapse', toggleIcon);
$('.panel-group').on('shown.bs.collapse', toggleIcon);



