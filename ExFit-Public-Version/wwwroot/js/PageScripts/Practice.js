$(document).ready(function () {
    loadView('PracticeList1', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=1`);
    $('#Save1').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('MainForm1', 'Practice', 'Save', 'PracticeList1', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=1`);
    });
    $('#Save2').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('MainForm2', 'Practice', 'Save', 'PracticeList2', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=2`);
    });
    $('#Save3').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('MainForm3', 'Practice', 'Save', 'PracticeList3', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=3`);
    });
    $('#Save4').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('MainForm4', 'Practice', 'Save', 'PracticeList4', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=4`);
    });
    $('#Save5').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('MainForm5', 'Practice', 'Save', 'PracticeList5', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=5`);
    });
    $('#Save6').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('MainForm6', 'Practice', 'Save', 'PracticeList6', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=6`);
    });
    $('#Save7').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('MainForm7', 'Practice', 'Save', 'PracticeList7', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=7`);
    });



    $('.day1').on('click', function (event) {
        event.preventDefault();
        loadView('PracticeList1', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=1`);
    });
    $('.day2').on('click', function (event) {
        event.preventDefault();
        loadView('PracticeList2', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=2`);
    });
    $('.day3').on('click', function (event) {
        event.preventDefault();
        loadView('PracticeList3', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=3`);
    });
    $('.day4').on('click', function (event) {
        event.preventDefault();
        loadView('PracticeList4', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=4`);
    });
    $('.day5').on('click', function (event) {
        event.preventDefault();
        loadView('PracticeList5', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=5`);
    });
    $('.day6').on('click', function (event) {
        event.preventDefault();
        loadView('PracticeList6', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=6`);
    });
    $('.day7').on('click', function (event) {
        event.preventDefault();
        loadView('PracticeList7', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=7`);
    });



    $(document).on('click', '.delete-button1', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Practice/Delete', id, 'PracticeList1', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=1`);
    });
    $(document).on('click', '.delete-button2', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Practice/Delete', id, 'PracticeList2', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=2`);
    });
    $(document).on('click', '.delete-button3', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Practice/Delete', id, 'PracticeList3', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=3`);
    });
    $(document).on('click', '.delete-button4', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Practice/Delete', id, 'PracticeList4', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=5`);
    });
    $(document).on('click', '.delete-button5', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Practice/Delete', id, 'PracticeList5', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=6`);
    });
    $(document).on('click', '.delete-button6', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Practice/Delete', id, 'PracticeList6', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=6`);
    });
    $(document).on('click', '.delete-button7', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Practice/Delete', id, 'PracticeList7', `/ExcersizeRoom/PracticeList/${ExcersizeId}?day=7`);
    });
});