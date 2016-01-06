
function getProvinceListByHandler(ulProvince, divProvince, hdProvince, ulCity, divCity, hdCity) {
    $.ajax({
        type: "GET",
        async: true,
        contentType: "application/json; charset=utf-8",
        url: "/Handerashx/UserManage/Areahandler.ashx",
        dataType: "json",
        cache: false,
        success: function (context) {
            ulProvince.append("<li index=''>--省份--</li>");
            if (context.length > 0) {
                $(context).each(function () {
                    if (hdProvince) {
                        if (this["ID"] == hdProvince.val()) {//初始化                           
                            divProvince.html(this["Name"]);
                            ulProvince.append("<li index='" + this["ID"] + "' class='selected'>" + this["Name"] + "</li>");
                            getCityListByProvinceID(hdProvince.val(), ulCity, divCity, hdCity);
                        } else {
                            ulProvince.append("<li index='" + this["ID"] + "'>" + this["Name"] + "</li>");
                        }
                    } else {
                        ulProvince.append("<li index='" + this["ID"] + "'>" + this["Name"] + "</li>");
                    }
                });
            }
            ulProvince.find("li").live('click', function () {
                getCityListByProvinceID($(this).attr("index"), ulCity, divCity, hdCity);
            });
        },
        error: function (xmlHttpRequest) {
            alert(xmlHttpRequest.innerText);
        },
        complete: function (x) {
        }
    });
}

function getCityListByProvinceID(id, ulCity, divCity, hdCity) {
    if (id.length <= 0) {
        ulCity.empty();
        hdCity.val("");
        divCity.html("--城市--");
    } else {
        ulCity.empty();
        $.ajax({
            type: "GET",
            async: false,
            contentType: "application/json; charset=utf-8",
            url: "/Handerashx/UserManage/Areahandler.ashx?parentid=" + id,
            dataType: "json",
            cache: false,
            success: function (context) {
                if (context.length > 0) {
                    divCity.html("--城市--");
                    $(context).each(function () {
                        if (hdCity) {
                            if (this["ID"] == hdCity.val()) { //初始化
                                divCity.html(this["Name"]);
                                ulCity.append("<li index='" + this["ID"] + "' class='selected'>" + this["Name"] + "</li>");
                            } else {
                                ulCity.append("<li index='" + this["ID"] + "'>" + this["Name"] + "</li>");
                            }
                        } else {
                            ulCity.append("<li index='" + this["ID"] + "'>" + this["Name"] + "</li>");
                        }
                    });
                }
            },
            error: function (xmlHttpRequest) {
                alert(xmlHttpRequest.innerText);
            },
            complete: function (x) {
            }
        });
    }
}
