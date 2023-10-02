const loadingEl = document.createElement("div");
$(document).ajaxStart(function (event, jqxhr, settings, thrownError) {

    document.body.prepend(loadingEl);
    loadingEl.classList.add("page-loader");
    loadingEl.classList.add("flex-column");
    loadingEl.classList.add("bg-dark");
    loadingEl.classList.add("bg-opacity-25");
    loadingEl.innerHTML = `
        <span class="spinner-border text-primary" role="status"></span>
        <span class="text-gray-800 fs-6 fw-semibold mt-5">Yükleniyor...</span>`;

    KTApp.showPageLoading();
});

$(document).ajaxError(function (event, jqxhr, settings, thrownError) {
    
    KTApp.hidePageLoading();
    loadingEl.remove();
    console.log("event error", event);
    console.log("jqxhr error", jqxhr);
    console.log("settings error", settings);
    console.log("thrownError error", thrownError);
    console.log("statuscode", jqxhr.status);
    console.log("jqxhr.responseText", jqxhr.responseText);


    if (jqxhr.status == 403) {
        var res = JSON.parse(jqxhr.responseText);
        if (res.Data == undefined || res.Data == "") {
            var responseJson = JSON.parse(jqxhr.responseText);
            toastr.error(responseJson.Description != undefined ? responseJson.Description : responseJson.Message);
        } else {
            $("#" + res.Data).html("bu alanı görüntüleme yetkiniz yok.");
        }
    }

    else {

        if (jqxhr.responseJSON != undefined) {

            var message = jqxhr.responseJSON.Message != null ? jqxhr.responseJSON.Message : jqxhr.responseJSON.Description;
            Swal.fire({
                html: message,
                icon: "error",
                width: '100%',
                height:'100%'
            });
        }

        else {
            var message = "";
            try {
                var msg = JSON.parse(jqxhr.responseText);
                message = msg.Description != undefined ? msg.Description : msg.Message;
            }
            catch {
                message = jqxhr.responseText;
            }

            Swal.fire({
                html: message,
                icon: "error",
                width: '100%',
                height: '100%'
            });
        }
    }
});

$(document).ajaxComplete(function (event, jqxhr, settings, thrownError) {
    KTApp.hidePageLoading();
    loadingEl.remove();
});