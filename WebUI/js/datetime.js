﻿(function ($) {
    var FormatDateTime = function FormatDateTime() { };
    $.FormatDateTime = function (obj, type) {
        type == undefined ? 1 : type;
        var correcttime1 = eval('( new ' + obj.replace(new RegExp("\/", "gm"), "") + ')');
        var myDate = correcttime1;
        var year = myDate.getFullYear();
        var month = ("0" + (myDate.getMonth() + 1)).slice(-2);
        var day = ("0" + myDate.getDate()).slice(-2);
        var h = ("0" + myDate.getHours()).slice(-2);
        var m = ("0" + myDate.getMinutes()).slice(-2);
        var s = ("0" + myDate.getSeconds()).slice(-2);
        var mi = ("00" + myDate.getMilliseconds()).slice(-3);
        if (type == 1)
            return year + "-" + month + "-" + day + " " + h + ":" + m;
        else if (type == 2)
            return year + "-" + month + "-" + day + " " + h + ":" + m + ":" + s;
        else if (type == 3)
            return year + "-" + month + "-" + day;
    }
})(jQuery);