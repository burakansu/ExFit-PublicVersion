$(document).ready(function () {
    function performAjaxRequest(url, successCallback) {
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'html',
            success: function (data) {
                $("#ExFit_Body").html(data);
                if (successCallback) {
                    successCallback();
                }
            },
            error: function () {
                alert("AJAX  - Veri Okunamadı.");
            }
        });
    }

    $.ajax({
        url: '@Url.Action("_LastTasks","Home")',
        success: function () {
            $("#importdiv").html(data);
        }
    });

    $("#AllActivitiesToday").click(function () {
        performAjaxRequest('@Url.Action("AllActivitiesToday","Personals")');
    });

    $("#AddPersonal").click(function () {
        performAjaxRequest('@Url.Action("AddPersonal","Personals")');
    });

    $("#CheckInMember").click(function () {
        performAjaxRequest('@Url.Action("CheckInMember","Members")');
    });

    $("#PIndex").click(function () {
        performAjaxRequest('@Url.Action("Index","Personals")');
    });

    $("#PassiveMembers").click(function () {
        performAjaxRequest('@Url.Action("AllPassivedMembers","Members")');
    });

    $("#AllMembers").click(function () {
        performAjaxRequest('@Url.Action("AllMembers","Members")');
    });

    $("#ExcersizeRoom").click(function () {
        performAjaxRequest('@Url.Action("ExcersizeRoom","ExcersizeRoom")');
    });

    $("#DietRoom").click(function () {
        performAjaxRequest('@Url.Action("DietRoom","DietRoom")');
    });

    $("#Economy").click(function () {
        performAjaxRequest('@Url.Action("Economy","AnalyzeRoom")');
    });

    $("#Package").click(function () {
        performAjaxRequest('@Url.Action("Index","Packages")');
    });
});
