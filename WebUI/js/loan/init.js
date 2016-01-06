/*获取借款用途*/
function getLoanUseList() {
    var obj = new Object();
    obj.where = "1";
    var jsonobj = JSON.stringify(obj);
    $.ajax({
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "/WebService/DimLoanUse.svc/getDimLoanUseModelList",
        data: jsonobj,
        dataType: "json",
        success: function (context) {
            var result = eval('(' + context.d + ')');
            var sel_loanUse = $("#sel_loanUse");
            if (result.length > 0) {
                $(result).each(function () {
                    sel_loanUse.append($("<option>").text(this["LoanUseName"]).val(this["ID"]));
                });
            }
        },
        error: function (XMLHttpRequest, textStatus) {
            alert("error");
        },
        complete: function (x) {
        }
    });
}

function getLoanUseListFromHandler() {
    $.ajax({
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "/HanderAshx/p2p/DimLoanUse.ashx",
        dataType: "json",
        success: function (context) {
            var sel_loanUse = $("#sel_loanUse");
            if (context.length > 0) {
                sel_loanUse.append($("<option>").text("不限").val("").attr("selected", "selected"));
                $(context).each(function () {
                    sel_loanUse.append($("<option>").text(this["LoanUseName"]).val(this["ID"]));
                });
            }
        },
        error: function (XMLHttpRequest, textStatus) {
            alert("error");
        },
        complete: function (x) {
        }
    });
}

/*获取省份*/
function getProvinceList() {
    $.ajax({
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "/WebService/Area.svc/getProvinceList",
        data: null,
        dataType: "json",
        success: function (context) {
            var result = eval('(' + context.d + ')');
            var province = $("#sel_province");
            if (result.length > 0) {
                $(result).each(function () {
                    province.append($("<option>").text(this["Name"]).val(this["ID"]));
                });
            }
        },
        error: function (XMLHttpRequest, textStatus) {
            alert("error");
        },
        complete: function (x) {
        }
    });
}

function getProvinceListFromHandler() {
    $.ajax({
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "/Handerashx/UserManage/Areahandler.ashx",
        dataType: "json",
        cache: false,
        success: function (context) {
            var sel_province = $("#sel_province");
            sel_province.append($("<option>").text("--请选择--").val("").attr("selected", "selected"));
            $("#sel_city").html($("<option>").text("--请选择--").val("").attr("selected", "selected"));
            if (context.length > 0) {
                $(context).each(function () {
                    sel_province.append($("<option>").text(this["Name"]).val(this["ID"]));
                });
            }
        },
        error: function (XMLHttpRequest, textStatus) {
            alert("error");
        },
        complete: function (x) {
        }
    });
}

/*根据省份获取城市*/
function getCityListByParentID(id) {
    var city = $("#sel_city");
    city.empty();
    if (id <= 0) {
        city.append($("<option>").text('请选择').val(0));
    } else {
        var obj = new Object();
        obj.id = id;
        var jsonobj = JSON.stringify(obj);
        $.ajax({
            type: "POST",
            async: false,
            contentType: "application/json; charset=utf-8",
            url: "/WebService/Area.svc/getCityListByParentID",
            data: jsonobj,
            dataType: "json",
            success: function (context) {
                var result = eval('(' + context.d + ')');
                if (result.length > 0) {
                    $(result).each(function () {
                        city.append($("<option>").text(this["Name"]).val(this["ID"]));
                    });
                }
            },
            error: function (XMLHttpRequest, textStatus) {
                alert("error");
            },
            complete: function (x) {
            }
        });
    }
}
function getCityListFromHandlerByParentID(id,city) {
    var sel_city = city == undefined ? $("#sel_city") : $("#" + city);
    if (id.length <= 0) {
        sel_city.html($("<option>").text("--请选择--").val("").attr("selected", "selected"));
    } else {
        sel_city.empty();
        $.ajax({
            type: "GET",
            async: false,
            contentType: "application/json; charset=utf-8",
            url: "/Handerashx/UserManage/Areahandler.ashx?parentid=" + id,
            dataType: "json",
            cache: false,
            success: function (context) {
                if (context.length > 0) {
                    $(context).each(function () {
                        sel_city.append($("<option>").text(this["Name"]).val(this["ID"]));
                    });
                }
            },
            error: function (XMLHttpRequest, textStatus) {
                alert("error");
            },
            complete: function (x) {
            }
        });
    }
}

/*获取借款期限*/
function getLoanTermByMode(modeNum) {
    var loanTerm = $("#sel_loanTerm");
    loanTerm.empty();
    loanTerm.append($("<option>").text("请选择").val(0));
    if (modeNum == 0) {
        for (var i = 1; i <= 30; i++) {
            loanTerm.append($("<option>").text(i).val(i));
        }
    } else if (modeNum == 1) {
        for (var i = 1; i <= 36; i++) {
            loanTerm.append($("<option>").text(i).val(i));
        }
    }
}
/*添加*/
function add(url, obj, buttons) {
    var jsonobj = JSON.stringify(obj);
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: url,
        data: jsonobj,
        dataType: "json",
        beforeSend: function (XMLHttpRequest) {
            if (buttons && buttons.length > 0) { buttons.attr({ "disabled": true }) };
        },
        success: function (context) {
            var result = eval('(' + context.d + ')');
            if (result > 0) {
                $.jBox.tip('保存成功', 'success');
                window.setTimeout(function () { location.href = "../Member/MemberDefault.aspx" }, 2000);
            }
        },
        error: function (XMLHttpRequest, textStatus) {
            alert("error");
        }
    });
}
/*获取借款用途ID对应的名称*/
function getLoanUseName(ID) {
    var LoanUseName;
    var obj = new Object();
    obj.where = ID;
    var jsonobj = JSON.stringify(obj);

    $.ajax({
       type: "POST",
       url: "../WebService/DimLoanUse.svc/getDimLoanUseName",
       async: false,//true表示异步 false表示同步  
       contentType: "application/json; charset=utf-8",
       data: jsonobj,
       dataType: 'json',
       success: function (result) {
           var jsondatas = eval("(" + result.d + ")");
           LoanUseName = jsondatas[0].LoanUseName;
       }
   })
    return LoanUseName;
}
/*获取URL参数*/
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

/*获取审核状态*/
function getExamStatusList() {
    $.ajax({
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "../HanderAshx/p2p/DimExamStatus.ashx",
        dataType: "text",
        success: function (context) {
            var result = eval('(' + context + ')');
            var sel_examstatus = $("#sel_examstatus");
            if (result.length > 0) {
                $(result).each(function () {
                    sel_examstatus.append($("<option>").text(this["ExamStatusName"]).val(this["ID"]));
                });
            }
        },
        error: function (XMLHttpRequest, textStatus) {
            alert("error");
        },
        complete: function (x) {
        }
    });
}

/*获取担保公司列表*/
function getGuaranteeList() {
    $.ajax({
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "../HanderAshx/p2p/Guarantee.ashx",
        dataType: "text",
        success: function (context) {
            var result = eval('(' + context + ')');
            var sel_Guarantee = $("#sel_Guarantee");
            if (result.length > 0) {
                $(result).each(function () {
                    sel_Guarantee.append($("<option>").text(this["GuaranteeName"]).val(this["ID"]));
                });
            }
        },
        error: function (XMLHttpRequest, textStatus) {
            alert("error");
        },
        complete: function (x) {
        }
    });
}

/*获取借款标号列表*/
function getLoanNumberList(memberId) {
    var sel_LoanNumber = $("#sel_LoanNumber");
    sel_LoanNumber.empty();
    sel_LoanNumber.append($("<option>").text("请选择").val(0));
    $.ajax({
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "../HanderAshx/p2p/LoanApplyNumber.ashx?memberId=" + memberId,
        dataType: "json",
        success: function (context) {
            if (context.length > 0) {
                $(context).each(function () {
                    sel_LoanNumber.append($("<option>").text(this["LoanNumber"]).val(this["ID"]));
                });
            }
        },
        error: function (XMLHttpRequest, textStatus) {
            alert("error");
        },
        complete: function (x) {
        }
    });
}

function getLoanTerm() {
    var loanTerm = $("#sel_loanTerm");
    loanTerm.empty();
    loanTerm.append($("<option>").text("请选择").val(0));
    for (var i = 1; i <= 36; i++) {
        loanTerm.append($("<option>").text(i + " 天/月").val(i));
    }
}
//获取分公司列表
function getBranchCompanyList(id) {
    $.ajax({
        type: "GET",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "/Handerashx/BranchCompanyManage/BranchCompanyHandler.ashx",
        dataType: "json",
        cache: false,
        success: function (context) {
            if (context.length > 0) {
                $(context).each(function () {
                    $("#" + id).append($("<option>").text(this["Name"]).val(this["ID"]));
                });
            }
        },
        error: function (XMLHttpRequest, textStatus) {
            alert("error");
        },
        complete: function (x) {
        }
    });
}
