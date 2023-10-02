var Pager = function () {
    var orderBy = "";
    var orderWay = "";

    function countSelectedColumns() {
        var count = $(".pager_toplu_secim:checked").length;
        if (count > 0) {
            $("#pager_toplu_secim_div").show();
            $("#pager_toplu_secim_count").html(`<b>${count}  kayıt seçildi</b>`);
        } else {
            $("#pager_toplu_secim_div").hide();
            $("#pager_toplu_secim_count").html("");
        }
    }

    function toggleAllColumns() {
        var checked = $("#PagerTopluSecim").prop("checked");
        var checks = $(".pager_toplu_secim");

        if (checked) {
            $.each(checks, function (i, v) {
                $(v).prop("checked", true);
            });
        } else {
            $.each(checks, function (i, v) {
                $(v).prop("checked", false);
            });
        }
        countSelectedColumns();
    }


    var init = function (url, targetDivId, adetSelectId) {

        function getData(page) {

            var adet = $("#" + adetSelectId).val();
            var searchText = $("#PagerSearchText").val();

            var filter = {
                page: page,
                adet: adet,
                searchText: searchText,
                orderBy: orderBy,
                orderWay: orderWay
            };

            var filterVals = $(".PagerFilter");
            $.each(filterVals, function (i, v) {
                var name = $(v).attr("name");
                var val = $(v).val();
                filter[name] = val;
            });


            $.ajax({
                url: url,
                type: "GET",
                data: filter,
                dataType: 'html',
                contentType: 'application/json;charset=utf-8',
                success: function (result) {
                    $("#" + targetDivId).html(result);
                },
                error: function (r) {

                },
                complete: function () {
                    var pagerVals = {
                        url: url,
                        targetDivId: targetDivId,
                        adetSelectId: adetSelectId,
                        searchText: searchText,
                    };
                    localStorage.setItem('PagerVals', JSON.stringify(pagerVals));
                    toggleAllColumns();


                }
            });
        }

        $(document).on("click", '.page-link', function (e) {

            e.preventDefault();
            var page = $(this).attr("page");
            getData(page);
        });

        $(document).on("change", "#" + adetSelectId, function (e) {
            e.preventDefault();
            getData(1);
        });

        $(document).on("change", "#PagerSearchText", function (e) {
            e.preventDefault();
            setTimeout(function () {
                getData(1);
            }, 500);
        });

        $(document).on("change", ".PagerFilter", function (e) {
            e.preventDefault();
            getData(1);
        });

        $(document).on("change", "#PagerTopluSecim", function (e) {
            e.preventDefault();
            toggleAllColumns();
        });

        $(document).on("click", ".pager_toplu_secim", function (e) {
            countSelectedColumns();
        });

        $(document).on("click", "[event='sortable']", function (e) {
            e.preventDefault();

            orderBy = $(this).attr("order");
            orderWay = orderWay == "Asc" ? "Desc" : "Asc";

            getData(1);

        });

        getData(1);
    }



    return {

        init: function (url, targetDivId, adetSelectId) {
            init(url, targetDivId, adetSelectId);
        }
    };
}();