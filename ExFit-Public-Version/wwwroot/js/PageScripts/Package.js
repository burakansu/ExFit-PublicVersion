$(document).ready(function () {
    loadView('Prints', '/Package/List');

    $('#Save').on('click', function (event) {
        event.preventDefault();
        $("#exampleModal2").css("display", "none");
        $(".modal-backdrop.fade.show").remove();
        sendPostRequest('PackageForm', 'Package', 'Save', 'Prints', '/Package/List');
    });
    $(document).on('click', '.delete-button', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Package/Delete', id, 'Prints', '/Package/List');
    });
});