var Form = function () {

    function isValid(formId, fields) {

        var validation = FormValidation.formValidation(
            KTUtil.getById(formId),
            {
                fields: fields,
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    submitButton: new FormValidation.plugins.SubmitButton(),
              
                    bootstrap: new FormValidation.plugins.Bootstrap()
                }
            }
        );

        return validation;

    }

    function GetForm(formUrl, params, appendto) {
        Post(formUrl,
            params,
            function (response) {
                $("#" + appendto).empty().html(response);
            },
            function (x, y, z) {
                //Error
            },
            function () {
                //BeforeSend
            },
            function () {
                //Complete
            },
            "html");
    }

    function GetFilterForm(formUrl, appendto) {
        Post(formUrl,
            {},
            function (response) {

                $("#" + appendto).empty().html(response);
            },
            function (x, y, z) {
                //Error
            },
            function () {
                //BeforeSend
            },
            function () {
                //Complete
            },
            "html");
    }

    function GetFilterFormWithParams(formUrl,params ,appendto) {
        Post(formUrl,
            params,
            function (response) {

                $("#" + appendto).empty().html(response);
            },
            function (x, y, z) {
                //Error
            },
            function () {
                //BeforeSend
            },
            function () {
                //Complete
            },
            "html");
    }
    function GetFilterFormWithParams(formUrl, params, appendto) {
        Post(formUrl,
            params,
            function (response) {

                $("#" + appendto).empty().html(response);
            },
            function (x, y, z) {
                //Error
            },
            function () {
                //BeforeSend
            },
            function () {
                //Complete
            },
            "html");
    }
    function GetFilterFormWithParamsCompleteFunction(formUrl, params, appendto, ajaxParams) {
        Post(formUrl,
            params,
            function (response) {

                $("#" + appendto).empty().html(response);
            },
            function (x, y, z) {
                //Error
            },
            function () {
                //BeforeSend
            },
            function () {
                //Complete
                if (ajaxParams.repeater!='') {
                    FormRepeater.initByIdDeleteTrigger(ajaxParams.repeater, ajaxParams.triggerDeleteButonId, ajaxParams.event);
                }
            },
            "html");
    }

    return {
        // public functions
        SubmitForm: function (formId,) {
            return isValid(formId, fields);
        },
        GetForm(formUrl, id, appendto) {
            GetForm(formUrl, id, appendto);
        },
        GetFilterForm(formUrl, appendto) {
            GetFilterForm(formUrl, appendto);
        },
        GetFilterFormWithParams(formUrl, params, appendto) {
            GetFilterFormWithParams(formUrl, params, appendto);
        },
        GetFilterFormWithParamsCompleteFunction(formUrl, params, appendto,ajaxParams) {
            GetFilterFormWithParamsCompleteFunction(formUrl, params, appendto, ajaxParams);
        }
    };
}();

