var Global = function () {

    function basicDurum(url, id) {
        Post(url,
            { id: id },
            function (response) {
                successCallBack(response);
            },
            function (x, y, z) {
                //Error
            },
            function () {
                //BeforeSend
            },
            function (r) {
                ReloadDataTable(r);
            },
            "json");
    }

    function basicDurumPopup(title, message, url, id) {
        Swal.fire({
            title: title,
            html: message,
            icon: "warning",
            showCancelButton: true,
            buttonsStyling: false,
            confirmButtonText: "Kaydet!",
            cancelButtonText: 'iptal',
            customClass: {
                confirmButton: "btn btn-success",
                cancelButton: "btn btn-danger"
            }
        }).then((result) => {
            if (result.isConfirmed) {
                basicDurum(url, id);
            } else {
                Swal.fire("iptal edildi");
            }
        });
    }

    function basicDelete(url, id) {
        Post(url,
            { id: id },
            function (response) {
                successCallBack(response);
            },
            function (x, y, z) {
                //Error
            },
            function () {
                //BeforeSend
            },
            function (r) {
                ReloadDataTable(r);
            },
            "json");
    }

    function basicDeletePopup(title, message, url, id) {
        Swal.fire({
            title: title,
            html: message,
            icon: "warning",
            showCancelButton: true,
            buttonsStyling: false,
            confirmButtonText: "Sil!",
            cancelButtonText: 'iptal',
            customClass: {
                confirmButton: "btn btn-success",
                cancelButton: "btn btn-danger"
            }
        }).then((result) => {
            if (result.isConfirmed) {
                basicDelete(url, id);
            } else {
                Swal.fire("iptal edildi");
            }
        });
    }

    function deleteAll(url) {
        Global.OpenDialog("Seçili kayıtları sil", "seçili kayıtlar silinecek onaylıyor musun?", function () {
            var selectedValues = [];
            $(".pager_toplu_secim:checked").each(function () {
                selectedValues.push($(this).val());
            });

            Post(url,
                { selectedValues: selectedValues },
                function (response) {
                    Global.SuccessCallBack(response);
                },
                function (x, y, z) {
                    //Error
                },
                function () {
                    //BeforeSend
                },
                function (r) {
                },
                "json");

        });
    }

    function activeAll(url) {
        Global.OpenDialog("Seçili kayıtları aktif yap", "seçili kayıtlar aktifleştirilecek onaylıyor musun?", function () {
            var selectedValues = [];
            $(".pager_toplu_secim:checked").each(function () {
                selectedValues.push($(this).val());
            });

            Post(url,
                { selectedValues: selectedValues },
                function (response) {
                    Global.SuccessCallBack(response);
                },
                function (x, y, z) {
                    //Error
                },
                function () {
                    //BeforeSend
                },
                function (r) {
                },
                "json");

        });
    }

    function passiveAll(url) {

        Global.OpenDialog("Seçili kayıtları pasif yap", "seçili kayıtlar pasifleştirilecek onaylıyor musun?", function () {
            var selectedValues = [];
            $(".pager_toplu_secim:checked").each(function () {
                selectedValues.push($(this).val());
            });

            Post(url,
                { selectedValues: selectedValues },
                function (response) {
                    Global.SuccessCallBack(response);
                },
                function (x, y, z) {
                    //Error
                },
                function () {
                    //BeforeSend
                },
                function (r) {
                },
                "json");

        });
    }

    $(document).on("click", "[event='BaseActive']", function (e) {
        e.preventDefault();
        var params = jQuery.parseJSON($(this).attr("params"));
        basicDurumPopup(params.title, "Durum Güncellenecek Onaylıyor musunuz?", params.url, params.id);
    });

    $(document).on("click", "[event='BaseSil']", function (e) {

        e.preventDefault();
        var params = jQuery.parseJSON($(this).attr("params"));
        basicDeletePopup(params.title, "kayıt silinecek onaylıyor musunuz?", params.url, params.id);
    });

    $(document).on("click", "[event='BaseFormPopup']", function (e) {

        e.preventDefault();
        var paramsString = $(this).attr("params").replace(/'/g, '"');
        var params = jQuery.parseJSON(paramsString);

        var title = $(this).attr("title");
        var url = $(this).attr("url");
        var formId = $(this).attr("formId");
        var kayitUrl = $(this).attr("kayitUrl");
        getFormWithParams(params, url, title, formId, kayitUrl, {}, function () {

        });
    });

    $(document).on("click", "#PagerMultiDelete", function (e) {
        e.preventDefault();
        var url = $(this).attr("url");
        deleteAll(url);
    });

    $(document).on("click", "#PagerMultiActive", function (e) {
        e.preventDefault();
        var url = $(this).attr("url");
        activeAll(url);
    });

    $(document).on("click", "#PagerMultiPassive", function (e) {
        e.preventDefault();
        var url = $(this).attr("url");
        passiveAll(url);
    });

    var init = function () {

    };

    var cardTemplateBasic = function (str) {
        var html = '<div class="card card-custom example example-compact">';
        html += '<div class="card-body">';
        html += str;
        html += '</div>';
        html += '</div>';

        return html;
    }

    function ReloadDataTable(r) {

        try {
            setTimeout(function () {
                var pagerVals = JSON.parse(localStorage.getItem('PagerVals'));
                Pager.init(pagerVals.url, pagerVals.targetDivId, pagerVals.adetSelectId);
            }, 1500);

        } catch {
            console.log(r);
            var message = r.responseText != undefined ? r.responseText : r.description;
            Swal.fire({
                title: message,
                icon: "error"
            });
        }
    }

    function successCallBack(response) {


        if (response.success == true) {
            Swal.fire({
                title: "Sonuç",
                html: response.description,
                icon: "success",
                showConfirmButton: false

            });
            ReloadDataTable();
        } else {
            if (response.description != undefined) {
                Swal.fire({
                    title: "Sonuç",
                    html: response.description,
                    icon: "error",
                    showConfirmButton: false
                });
            } else {
                Swal.fire({
                    title: "Sonuç",
                    html: response,
                    icon: "error",
                });
            }
        }

        bootbox.hideAll();
        setTimeout(function (e) {

            swal.close()
        }, 4000).tha
    }

    function Save(formId, ValidationFieldColumns, url, serializeOptions) {

        var validator = FormValidation.formValidation(
            document.getElementById(formId),
            {
                fields: ValidationFieldColumns,

                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row',
                        eleInvalidClass: '',
                        eleValidClass: ''
                    })
                }
            }
        );

        if (validator) {
            validator.validate().then(function (status) {

                if (status == 'Valid') {
                    var frm;
                    if (serializeOptions != undefined) {
                        frm = $("#" + formId).serializeJSON(serializeOptions);
                    }
                    else {
                        frm = $("#" + formId).serializeJSON();
                    }

                    var keys = Object.keys(frm);
                    var include = keys.slice(1, keys.length);
                    frm.Include = include;

                    $.each($(".manytomany"), function (index, item) {

                        var relationName = $(this).attr("relationName");
                        var propertyName = $(this).attr("propertyName");
                        var selected = $(this).val();
                        var relationRows = [];
                        for (var i = 0; i < selected.length; i++) {
                            var row = {};
                            row[propertyName] = selected[i]
                            relationRows.push(row)
                        }
                        frm[relationName] = relationRows;
                    })

                    Post(url,
                        { form: frm },
                        function (response) {

                            successCallBack(response);
                        },
                        function (x, y, z) {
                            //Error
                        },
                        function () {
                            //BeforeSend
                        },
                        function (r) {
                            Global.ReloadDataTable(r);
                        },
                        "json");

                }
            });
        }



    }

    function getFormWithParams(params, url, title, formId, kayitUrl, validationFields, completeCallBack) {

        Post(url,
            params,
            function (response) {
                bootbox.dialog({
                    title: title,
                    message: cardTemplateBasic(response),
                    message: response,
                    size: 'large',
                    buttons: {
                        cancel: {
                            label: "Kapat",
                            className: 'btn-danger',
                            callback: function () { }
                        },
                        ok: {
                            label: "Kaydet",
                            className: 'btn-info',
                            callback: function () {
                                Save(formId, validationFields, kayitUrl);
                                return false;
                            }
                        }
                    }
                });

            },
            function (x, y, z) {

            },
            function () {
                //BeforeSend
            },
            completeCallBack,
            "html");
    }

    function getFormCustomSave(params, url, title, saveFunction, completeCallBack) {

        Post(url,
            params,
            function (response) {
                bootbox.dialog({
                    title: title,
                    message: cardTemplateBasic(response),
                    message: response,
                    size: 'large',
                    buttons: {
                        cancel: {
                            label: "Kapat",
                            className: 'btn-danger',
                            callback: function () { }
                        },
                        ok: {
                            label: "Kaydet",
                            className: 'btn-info',
                            callback: saveFunction
                        }
                    }
                });

            },
            function (x, y, z) {
                //Error                
                var message = "";
                try {
                    var msg = JSON.parse(x.responseText);
                    message = msg.Message;
                }
                catch {
                    message = x.responseText;
                    console.log(x, y, z);
                }

                Swal.fire({
                    html: message,
                    icon: "error"
                });

            },
            function () {
                //BeforeSend
            },
            completeCallBack,
            "html");
    }



    function getFormHtml(params, url, successCallBack, completeCallBack) {

        Post(url,
            params,
            successCallBack,
            function (x, y, z) {
                //Error
                var message = "";
                try {
                    var msg = JSON.parse(x.responseText);
                    message = msg.Message;
                }
                catch {
                    message = x.responseText;
                    console.log(x, y, z);
                }

                Swal.fire({
                    html: message,
                    icon: "error"
                });

            },
            function () {
                //BeforeSend
            },
            completeCallBack,
            "html");
    }

    function openDialog(title, message, callbackFunc) {
        bootbox.dialog({
            title: title,
            message: cardTemplateBasic(message),
            size: 'large',
            buttons: {
                cancel: {
                    label: "Kapat",
                    className: 'btn-danger',
                    callback: function () { }
                },
                ok: {
                    label: "Kaydet",
                    className: 'btn-info',
                    callback: callbackFunc
                }
            }
        });
    }

    function openBootbox(url, params, title, completeCallbackFunc) {

        Post(url,
            params,
            function (response) {
                bootbox.dialog({
                    title: title,
                    message: cardTemplateBasic(response),
                    size: 'large',
                    buttons: {
                        cancel: {
                            label: "Kapat",
                            className: 'btn-danger',
                            callback: function () { }
                        }
                    }
                });
            },
            function (x, y, z) {
                //Error
            },
            function () {
                //BeforeSend
            },
            completeCallbackFunc,
            "html");


    }

    return {
        init: function () {
            init();
        },
        cardTemplateBasic: function (str) {
            cardTemplateBasic(str);
        },
        ReloadDataTable: function (r) {
            ReloadDataTable(r);
        },
        Kaydet: function (formId, ValidationFieldColumns, url, serializeOptions) {
            Save(formId, ValidationFieldColumns, url, serializeOptions);
        },
        GetFormWithParams: function (params, url, title, formId, kayitUrl, validationFields, completeCallBack) {
            getFormWithParams(params, url, title, formId, kayitUrl, validationFields, completeCallBack);
        },
        GetFormHtml: function (params, url, successCallBack, completeCallBack) {
            getFormHtml(params, url, successCallBack, completeCallBack);
        },
        OpenDialog: function (title, message, callbackFunc) {
            openDialog(title, message, callbackFunc);
        },
        OpenBootbox: function (url, params, title, completeCallbackFunc) {
            openBootbox(url, params, title, completeCallbackFunc)
        },
        SuccessCallBack: function (r) {
            successCallBack(r);
        },
        BasicDeletePopup: function (title, message, url, id) {
            basicDeletePopup(title, message, url, id);
        },
        BasicDurumPopup: function (title, message, url, id) {
            basicDurumPopup(title, message, url, id);
        },
        GetFormCustomSave: function (params, url, title, saveFunction, completeCallBack) {
            getFormCustomSave(params, url, title, saveFunction, completeCallBack);
        }
    };
}();