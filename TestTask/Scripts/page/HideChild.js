var onFolderClick = function (e) {
    console.log($(e.target).attr("data-hide"));
    if ($(e.target).attr("data-hide") == "true") {
        $(e.target).parent().children().next().show()
        $(e.target).attr("data-hide", 'false')
        return;
    }
    $(e.target).attr("data-hide", 'true')
    $(e.target).parent().children().next().hide()
}
var init2 = function () {
    $('img').on('click', onFolderClick);
}
$(function () {
    init2();
})