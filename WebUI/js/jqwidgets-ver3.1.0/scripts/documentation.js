try {
    $(document).ready(function () {
        if (navigator.userAgent.indexOf('MSIE 7.0') >= 0 || navigator.userAgent.indexOf('MSIE 8.0') >= 0)
        {
            $("em").hide();
            $("em").next().hide();
        }
    });
}
catch (error) {
    var er = error;
}