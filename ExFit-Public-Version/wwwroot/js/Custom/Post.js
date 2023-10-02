function Post(url, data, fnSuccess,fnError, fnBeforeSend,fnComplete,dataType) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: fnSuccess,
        error: fnError,
        beforeSend: fnBeforeSend,
        complete: fnComplete,
        dataType: dataType,
         
    });
}

function Post(url, data, fnSuccess, fnError, fnBeforeSend, fnComplete, dataType,traditional) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: fnSuccess,
        error: fnError,
        beforeSend: fnBeforeSend,
        complete: fnComplete,
        dataType: dataType,
        traditional:traditional
    });
}