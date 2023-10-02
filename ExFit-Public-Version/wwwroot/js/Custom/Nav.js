var Nav = function () {

    function init() {
        $(".navi-link").click(function (event) {
            event.preventDefault();

            $(".navi-link").removeClass("active");
            $(this).addClass("active");

            $("#cardTitle").html($(this).attr("cardLTitle"));
            $("#cardDescription").html($(this).attr("cardDescription"));

            var formUrl = $(this).attr("form");
            var displayType = $(this).attr("displayType");
            var id = $(this).attr("id");

            $(".eventButton").attr("event", $(this).attr("triggerEvent"));
            $(".eventButton").attr("params", $(this).attr("params"));

            $(".eventButton").attr("selectToRefresh", $(this).attr("selectToRefresh"));
            $(".eventButton").attr("dataTableToRefresh", $(this).attr("dataTableToRefresh"));
            $(".eventButton").attr("hideAllBotBox", $(this).attr("hideAllBotBox"));
            $(".eventButton").attr("hideBootBox", $(this).attr("hideBootBox"));

            $(".eventButton").text($(this).attr("triggerText"));

            if (displayType == "form") {

                var paramsString = $(this).attr("params").replace(/'/g, '"');
                var params = jQuery.parseJSON(paramsString);

                $("#kt_datatable").empty();
                $("#filterForm").empty();

                Form.GetForm(formUrl, params, "formArea");

            } else if (displayType == "list") {

                $("#formArea").empty();
                $("#filterForm").empty();
                var coloumns = $(this).attr("coloumns");
                var filterForm = $(this).attr("filterForm");
                if (filterForm !== "") {
                    Form.GetFilterForm(filterForm, "filterForm");
                }


                var paramsString = $(this).attr("params").replace(/'/g, '"');
                var params = jQuery.parseJSON(paramsString);

                Pager.init(formUrl, Prints, params.id)
            }

        });
    }
    function MemberNav() {
        $(".navi-link").click(function (event) {
            event.preventDefault();

            $(".navi-link").removeClass("active");
            $(this).addClass("active");

            $("#cardTitle").html($(this).attr("cardLTitle"));
            $("#cardDescription").html($(this).attr("cardDescription"));

            var formUrl = $(this).attr("form");
            var displayType = $(this).attr("displayType");
            var id = $(this).attr("id");

            $(".eventButton").attr("event", $(this).attr("triggerEvent"));
            $(".eventButton").attr("params", $(this).attr("params"));
            $(".eventButton").text($(this).attr("triggerText"));

            $(".eventButton").attr("selectToRefresh", $(this).attr("selectToRefresh"));
            $(".eventButton").attr("dataTableToRefresh", $(this).attr("dataTableToRefresh"));
            $(".eventButton").attr("hideAllBotBox", $(this).attr("hideAllBotBox"));
            $(".eventButton").attr("hideBootBox", $(this).attr("hideBootBox"));

            if (displayType == "form") {

                $("#filterForm").empty();

                Form.GetForm(formUrl, { id: id }, "formArea");

            } else if (displayType == "list") {

                $("#formArea").empty();
                $("#filterForm").empty();

                var coloumns = $(this).attr("coloumns");
                var filterForm = $(this).attr("filterForm");
                if (filterForm !== "") {
                    Form.GetFilterForm(filterForm, "filterForm");
                }


                var paramsString = $(this).attr("params").replace(/'/g, '"');
                var params = jQuery.parseJSON(paramsString);
                var rowSelection = $(this).attr("rowselection");
                var rowSelectionForm = $(this).attr("rowSelectionForm");


                Pager.init(formUrl, Prints, params.id)
            }

        });
    }

    return {
        // public functions
        Init: function () {
            init();
        },
        MemberNav: function () {
            MemberNav();
        }
    };
}();