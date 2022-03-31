/*******************************
* ACCORDION WITH TOGGLE ICONS IN PLAYLISTS/ABOUT PAGES
*******************************/
function toggleIcon(e) {
    $(e.target)
        .prev('.panel-heading')
        .find(".more-less")
        .toggleClass('glyphicon-plus glyphicon-minus');
}
$('.panel-group').on('hidden.bs.collapse', toggleIcon);
$('.panel-group').on('shown.bs.collapse', toggleIcon);


//QRCode  - turn byte array from db into png image
//document.getElementById("ItemPreview").src = "data:image/png;base64," + yourByteArrayAsBase64;

