function performAjaxRequest(form, url, successCallback) {
    var formData = form.serialize();
    $.ajax({
        type: 'POST',
        url: url,
        data: formData,
        success: function (res) {
            successCallback(res);
        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Hata',
                text: 'Bir hata oluştu.'
            });
        }
    });
}

$(document).ready(function () {
    $('#Enter').on('click', function (event) {
        event.preventDefault();
        var form = $('#EnteringForm');
        performAjaxRequest(form, '/LogIn/Entering', function (res) {
            if (res == 0) {
                window.location.href = '/LogIn/SignIn';
            } else if (res == 1) {
                window.location.href = '/Home/Index';
            }
        });
    });

    $('#Register').on('click', function (event) {
        event.preventDefault();
        var form = $('#RegisteringForm');
        performAjaxRequest(form, '/LogIn/Registering', function (res) {
            if (res > 0) {
                window.location.href = '/LogIn/SignIn';
            }
        });
    });
});

