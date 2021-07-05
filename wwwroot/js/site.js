// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("[id^='Add_']").click(function () {
    var prodId = $(this).attr("id").split("_")[1];
    console.log(prodId);
    var prodCountElement = document.getElementById("productCount_" + prodId);
    var count = +prodCountElement.innerHTML;
    prodCountElement.innerHTML = count + 1;
    Add(prodId);
});
$("[id^='Delete_']").click(function () {
    var prodId = $(this).attr("id").split("_")[1];
    console.log(prodId);
    var prodCountElement = document.getElementById("productCount_" + prodId);
    var count = +prodCountElement.innerHTML;
    if (count > 0) {
        prodCountElement.innerHTML = count - 1;
    }
    Delete(prodId);
});
function Delete(id) {
    console.log("Start delete");
    $.get(location.origin + "/Cart/Delete?id=" + id);
}
function Add(id) {
    console.log("Start add");
    $.get(location.origin + "/Cart/Add?id=" + id);
}
