$(document).ready(function () {
    loadView('Prints', '/ExcersizeRoom/List');

    $('#Save').on('click', function (event) {
        event.preventDefault();
        $("#exampleModal2").css("display", "none");
        $(".modal-backdrop.fade.show").remove();
        sendPostRequest('ExcersizeForm', 'ExcersizeRoom', 'Save', 'Prints', '/ExcersizeRoom/List');
    });
    $(document).on('click', '.delete-button', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/ExcersizeRoom/Delete', id, 'Prints', '/ExcersizeRoom/List');
    });
});