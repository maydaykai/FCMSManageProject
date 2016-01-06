function configValidate() {
    var name = $.trim($("#name").val());
    var phone = $.trim($("#phone").val());
    var loanAmount = $.trim($("#loanAmount").val());
    var loanUse = $("#sel_loanUse").val();
    var loanTerm = $.trim($("#loanTerm").val());
    var province = $("#sel_province").val();

    var re = /^1[358][0-9]{9}$/;
    if (name == "") {
        $.jBox.tip('请输入名字', 'warning');
        $("#name").focus();
        return false;
    }
    if (phone == "") {
        $.jBox.tip('请输入手机号码', 'warning');
        $("#phone").focus();
        return false;
    } else if (!re.test(phone)) {
        $.jBox.tip('手机号码格式错误', 'warning');
        $("#phone").focus();
        return false;
    }
    if (loanAmount == "" || loanAmount <= 0 || !/^[1-9]\d{0,3}$/.test(loanAmount)) {
        $.jBox.tip('请输入正确的金额', 'warning');
        $("#loanAmount").focus();
        return false;
    }
    if (loanUse == 0) {
        $.jBox.tip('请选择借款用途', 'warning');
        $("#sel_loanUse").focus();
        return false;
    }
    if (loanTerm == "" || loanTerm <= 0 || !/^[1-9]\d{0,3}$/.test(loanTerm)) {
        $.jBox.tip('请输入正确的借款期限', 'warning');
        $("#loanTerm").focus();
        return false;
    }
    if (province <= 0) {
        $.jBox.tip('请选择省份', 'warning');
        $("#sel_province").focus();
        return false;
    }
    return true;
}
function addLoanRapid() {
    var jsonobj = JSON.stringify(getCreateObj());
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "/WebService/LoanRapid.svc/addLoanRapid",
        data: jsonobj,
        dataType: "json",
        success: function (context) {
            var result = eval('(' + context.d + ')');
            if (result > 0) {
                $.jBox.tip('保存成功', 'success');
            }
        },
        error: function (XMLHttpRequest, textStatus) {
            alert("error");
        },
        complete: function (x) {
        }
    });
}
function getCreateObj() {
    var obj = new Object();
    obj.name = $.trim($("#name").val());
    obj.phone = $.trim($("#phone").val());
    obj.loanAmount = $.trim($("#loanAmount").val());
    obj.loanUseID = $("#sel_loanUse").val();
    obj.loanTerm = $.trim($("#loanTerm").val());
    obj.loanMode = $("input[name='loanMode']:checked").val();
    obj.province = $("#sel_province").val();
    obj.city = $("#sel_city").val();
    return obj;
}
$(document).ready(function () {
    getLoanUseList();
    getProvinceList();
    $("#sel_province").change(function () {
        getCityListByParentID($("#sel_province").val());
    });
    $("#btn_addLoanRapid").click(function () {
        if (configValidate()) {
            add("/WebService/LoanRapid.svc/addLoanRapid", getCreateObj(), $("#btn_addLoanRapid"));
        }
    });
    $(document).keydown(function (event) {
        switch (event.keyCode) {
            case 13:
                $("#btn_addLoanRapid").click();
        }
    });
});
