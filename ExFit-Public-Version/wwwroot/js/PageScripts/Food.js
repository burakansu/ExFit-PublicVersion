$(document).ready(function () {
    loadView('FoodList1', `/DietRoom/FoodList/${DietId}?day=1`);
    $('#Save1').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('MainForm1', 'Food', 'Save', 'FoodList1', `/DietRoom/FoodList/${DietId}?day=1`);
    });
    $('#Save2').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('MainForm2', 'Food', 'Save', 'FoodList2', `/DietRoom/FoodList/${DietId}?day=2`);
    });
    $('#Save3').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('MainForm3', 'Food', 'Save', 'FoodList3', `/DietRoom/FoodList/${DietId}?day=3`);
    });
    $('#Save4').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('MainForm4', 'Food', 'Save', 'FoodList4', `/DietRoom/FoodList/${DietId}?day=4`);
    });
    $('#Save5').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('MainForm5', 'Food', 'Save', 'FoodList5', `/DietRoom/FoodList/${DietId}?day=5`);
    });
    $('#Save6').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('MainForm6', 'Food', 'Save', 'FoodList6', `/DietRoom/FoodList/${DietId}?day=6`);
    });
    $('#Save7').on('click', function (event) {
        event.preventDefault();
        sendPostRequest('MainForm7', 'Food', 'Save', 'FoodList7', `/DietRoom/FoodList/${DietId}?day=7`);
    });



    $('.day1').on('click', function (event) {
        event.preventDefault();
        loadView('FoodList1', `/DietRoom/FoodList/${DietId}?day=1`);    });
    $('.day2').on('click', function (event) {
        event.preventDefault();
        loadView('FoodList2', `/DietRoom/FoodList/${DietId}?day=2`);    });
    $('.day3').on('click', function (event) {
        event.preventDefault();
        loadView('FoodList3', `/DietRoom/FoodList/${DietId}?day=3`);    });
    $('.day4').on('click', function (event) {
        event.preventDefault();
        loadView('FoodList4', `/DietRoom/FoodList/${DietId}?day=4`);    });
    $('.day5').on('click', function (event) {
        event.preventDefault();
        loadView('FoodList5', `/DietRoom/FoodList/${DietId}?day=5`);    });
    $('.day6').on('click', function (event) {
        event.preventDefault();
        loadView('FoodList6', `/DietRoom/FoodList/${DietId}?day=6`);    });
    $('.day7').on('click', function (event) {
        event.preventDefault();
        loadView('FoodList7', `/DietRoom/FoodList/${DietId}?day=7`);    });



    $(document).on('click', '.delete-button1', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Food/Delete', id, 'FoodList1', `/DietRoom/FoodList/${DietId}?day=1`);
    });
    $(document).on('click', '.delete-button2', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Food/Delete', id, 'FoodList2', `/DietRoom/FoodList/${DietId}?day=2`);
    });
    $(document).on('click', '.delete-button3', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Food/Delete', id, 'FoodList3', `/DietRoom/FoodList/${DietId}?day=3`);
    });
    $(document).on('click', '.delete-button4', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Food/Delete', id, 'FoodList4', `/DietRoom/FoodList/${DietId}?day=5`);
    });
    $(document).on('click', '.delete-button5', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Food/Delete', id, 'FoodList5', `/DietRoom/FoodList/${DietId}?day=6`);
    });
    $(document).on('click', '.delete-button6', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Food/Delete', id, 'FoodList6', `/DietRoom/FoodList/${DietId}?day=6`);
    });
    $(document).on('click', '.delete-button7', function (event) {
        event.preventDefault();
        debugger;
        var id = $(this).data("id");
        deleteItem('/Food/Delete', id, 'FoodList7', `/DietRoom/FoodList/${DietId}?day=7`);
    });
});