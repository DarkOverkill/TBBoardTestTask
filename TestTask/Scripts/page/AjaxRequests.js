var addFolder = function (e) {

    var data = {
        Name: $("#folderName").val(),
        Level: $(this).attr("data-folder-level"),
        parentId: $(this).attr("data-folder-parent")
    }
    if (data.Name == "") {
        data.Name = "New Folder" + (($(this).attr("id")).replace(/[^0-9]/gim, ''));
    }
    console.log(JSON.stringify(data));
    $.ajax({
        url: "default.aspx/AddChild",
        type: "post",    
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function () {
        console.log("ajax-done");
        location.reload();
    }).fail(function () {
        console.log("ajax-fail");
    })
}

var uploadFile = function (e) {
    if ($("#fileToUpload").val() == "") {
        alert("Nothing to upload!!!");
        return;
    }
    //var data = {
    //    parentFolderId: (($(this).attr("id")).replace(/[^0-9]/gim, '')),
    //    fileName: $("#fileToUpload").val().split(/(\\|\/)/g).pop(),
    //    file: $("#fileToUpload")
    //}
    var data = new FormData();
    var file = $("#fileToUpload").get(0).files;
    if (file.length > 0) {
        data.append("UploadFile", file[0]);
    }
    data.append("Folder", ($(this).attr("id")).replace(/[^0-9]/gim, ''));

    $.ajax({
        url: "/api/fileupload/uploadfile",
        type: "post",
        data: data,
        contentType: false,
        processData: false
    }).done(function (responseData, textStatus) {
        console.log("ajax-done" + responseData.Value);
        //location.reload();
    }).fail(function () {
        console.log("ajax-fail");
    })
    //console.log(JSON.stringify(data));
    /*
    $.ajax({
        url: "default.aspx/LoadFile",
        method: "post",
        data: data,//JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function () {
        console.log("ajax-done");
        //location.reload();
    }).fail(function () {
        console.log("ajax-fail");
    })
    */
}
var init = function () {
    $(".send").on("click", addFolder);
    $(".loadFile").on("click", uploadFile);
}

$(function () {
    init();
})