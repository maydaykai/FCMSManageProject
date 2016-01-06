$(function () {
    /*浏览器不支持trim方法时，设置trim方法 add by 2015-12-09 lxy*/
    if (String.prototype.trim == undefined) {
        String.prototype.trim = function () {
            return this.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
        }
    }
});

var Request = new Object();
Request = GetRequest();

/*add by 2015-12-09 lxy 获取url参数*/
function GetRequest() {
    var url = location.search; //获取url中"?"符后的字串
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = (strs[i].split("=")[1]);
        }
    }
    return theRequest;
}

/*add by 2015-09-09 lxy 日期转换*/
function dateFormat(time, format) {
    time = time.replace("/Date(", "").replace(")/", "");
    if (time.indexOf("T") > -1) {
        time = time.replace(/(\d{4})-(\d{2})-(\d{2})T(.*)/, "$1/$2/$3 $4");
        if (time.indexOf(".") > -1)
            time = time.split(".")[0];
    } else
        time = parseInt(time);
    var date = new Date(time);
    var map = {
        "M": date.getMonth() + 1, //月份 
        "d": date.getDate(), //日 
        "h": date.getHours(), //小时 
        "m": date.getMinutes(), //分 
        "s": date.getSeconds(), //秒 
        "q": Math.floor((date.getMonth() + 3) / 3), //季度 
        "S": date.getMilliseconds() //毫秒 
    };

    format = format.replace(/([yMdhmsqS])+/g, function (all, t) {
        var v = map[t];
        if (v !== undefined) {
            if (all.length > 1) {
                v = '0' + v;
                v = v.substr(v.length - 2);
            }
            return v;
        }
        else if (t === 'y') {
            return (date.getFullYear() + '').substr(4 - all.length);
        }
        return all;
    });
    return format;
}