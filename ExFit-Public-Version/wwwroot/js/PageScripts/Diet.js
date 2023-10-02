$(document).ready(function () {
    loadView('Prints', '/DietRoom/List');

    $('#Save').on('click', function (event) {
        event.preventDefault();
        $("#exampleModal2").css("display", "none");
        $(".modal-backdrop.fade.show").remove();
        sendPostRequest('DietForm', 'DietRoom', 'Save', 'Prints', '/DietRoom/List');
    });
    $(document).on('click', '.delete-button', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/DietRoom/Delete', id, 'Prints', '/DietRoom/List');
    });
});