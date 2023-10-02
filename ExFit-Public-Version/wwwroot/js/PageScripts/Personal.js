$(document).ready(function () {
    $(document).on('click', '#btnPersonEdit', function () {
        var id = $(this).attr("data-id");
        $("#divEditPage").load("/Personals/GetUser?id=" + id, function () {
        });
    });

    $('#Save').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('RegistryingForm', 'Personals', 'Registrying', null, null);
    });
});