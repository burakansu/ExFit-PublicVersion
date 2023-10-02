function sendAjaxRequest(method, url, data, successCallback, errorCallback) {
    $.ajax({
        type: method,
        url: url,
        data: data,
        success: successCallback,
        error: errorCallback
    });
}

function loadView(targetdivid, url) {
    sendAjaxRequest('GET', url, null, function (data) {
        $("#" + targetdivid).html(data);
    }, function (error) {
        showErrorAlert();
    });
}

function showErrorAlert() {
    Swal.fire({
        icon: 'error',
        title: 'Hata!',
        text: 'İstek sırasında bir hata oluştu.',
        timer: 1000,
        showConfirmButton: false
    });
}

function showSuccessAlert() {
    Swal.fire({
        icon: 'success',
        title: 'Başarılı!',
        text: 'İstek başarıyla tamamlandı.',
        timer: 1000,
        showConfirmButton: false
    });
}

function sendPostRequest(formName, controllerName, actionName, targetdivid, viewurl) {
    var form = $('#' + formName);
    if (formName == "RegistryingForm") {
        var formData = new FormData($('#' + formName)[0]);
    }
    else {
        var formData = form.serialize();
    }

    sendAjaxRequest('POST', '/' + controllerName + '/' + actionName, formData, function (data) {
        loadView(targetdivid, viewurl);
        showSuccessAlert();
    }, function (error) {
        showErrorAlert();
    });
}

function deleteItem(url, id, targetdivid, viewurl) {
    sendAjaxRequest('GET', url, { id: id }, function (data) {
        loadView(targetdivid, viewurl);
        showSuccessAlert();
    }, function (error) {
        showErrorAlert();
    });
}
