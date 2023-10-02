$(document).ready(function () {
    loadView('Prints', '/AnalyzeRoom/List');

    $('#Save').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('AnalyzeForm', 'AnalyzeRoom', 'Save', 'Prints', '/AnalyzeRoom/List');
    });
    $(document).on('click', '.delete-button', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/AnalyzeRoom/Delete', id, 'Prints', '/AnalyzeRoom/List');
    });
});