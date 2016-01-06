<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberAdd.aspx.cs" Inherits="WebUI.MemberManage.MemberAdd" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>会员注册</title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <style type="text/css">
         .selectDiv .select_box {
         width: 85px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" class="editTable">
            <%--<tr>
                <td style="text-align: right; width:150px;">用户名：</td>
                <td style="text-align: left; padding-left: 5px; color: #f00; padding-top:5px;">
                    <span class="fl">
                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" id="imgUserNameLeft" runat="server" />
                    </span>
                    <input id="txtUserName" type="text" runat="server" style="width: 120px;" class="input_text" />
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" id="imgUserNameRight" runat="server" />
                    </span>
                </td>
            </tr>--%>
            <tr>
                <td style="text-align: right">密码：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:5px;">
                    <div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div>
                    <input id="txtPwd" type="password" runat="server" style="width: 120px;" class="fl input_text" /><div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>&nbsp;&nbsp;
                    <span class="fl" style="margin-top:5px; margin-left:5px;">不填默认：</span><div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div><input id="txt_defaultPwd" type="text" runat="server" style="width: 120px;" class="fl input_text" value="17777777777" />
                    <div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">手机号码：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:5px;">
                    <div class="fl">
                        <img src="/images/gray_left.png" width="4" height="29" alt="" />
                    </div>
                    <input id="txtMobile" type="text" runat="server" style="width: 120px;" class="input_Disabled" value="17777777777" />
                    <div class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">真实姓名：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:5px;">
                    <div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div>
                    <input id="txtRealName" type="text" runat="server" style="width: 120px;" class="fl input_text" />
                    <div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">身份证号码：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:5px;">
                    <div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div>
                    <input id="txtIdentity" type="text" runat="server" style="width: 200px;" class="fl input_text" />
                    <div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left; padding-left: 5px;">
                    <asp:Button Text="提交" runat="server" OnClick="Button_Click" OnClientClick="return validate();" CssClass="inputButton" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl" style="margin-top:5px; margin-left:5px;">循环读取IdentityNum数据条数：</span>
                    <div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div><input id="txtNum" type="text" runat="server" style="width: 80px;" class="fl input_text" />
                    <div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>
                    <span class="fl" style="margin-top:5px; margin-left:5px;">条</span>
                    <span class="fl" style="margin-top:5px; margin-left:5px;">当前可用条数：</span>
                    <span class="fl" style="margin-top:5px;" id="currentNum" runat="server">0</span>
                    <span class="fl" style="margin-top:5px; margin-left:5px;">条</span>
                    <span class="fl" style="margin-top:5px; margin-left:5px;color:red;">tips:请先核实新手标可投次数</span><br /><br />
                    <asp:Button Text="提交" runat="server" OnClick="Button_Loop" OnClientClick="return loopValidate();" CssClass="inputButton fl" />
                    <span class="fl" style="margin-top:5px; margin-left:5px;color:red;" id="errorMsg" runat="server"></span>
                </td>
            </tr>
        </table>
    </form>
    </body>
</html>

<script src="../js/select2css.js"></script>
<script type="text/javascript">
    function validate() {
        //var userName = $('#txtUserName').val();
        var realName = $('#txtRealName').val();
        var identity = $('#txtIdentity').val();
        //if ($.trim(userName) == "") {
        //    MessageAlert('用户名不能为空。', 'warning', '');
        //    return false;
        //}
        //if (!new RegExp("^[A-Za-z0-9]+$").test($.trim(userName))) {
        //    MessageAlert('用户名不包含除26个英文字母或数字以外的字符。', 'warning', '');
        //    return false;
        //}
        if ($.trim(realName) == "") {
            MessageAlert('请输入真实姓名。', 'warning', '');
            return false;
        }
        if ($.trim(realName).length < 2 || $.trim(realName).length > 12) {
            MessageAlert('您输入的真实姓名长度错误,应在2~12个字符之间,请确认。', 'warning', '');
            return false;
        }
        if ($.trim(identity) == "") {
            MessageAlert('请输入身份证号码。', 'warning', '');
            return false;
        }
        var msg = isCardID($.trim(identity));
        if (!(msg === true)) {
            MessageAlert(msg, 'warning', '');
            return false;
        }
        return true;
    }
    function loopValidate(){
        var num = $('#txtNum').val();
        if ($.trim(num) == "") {
            MessageAlert('请输入要循环的条数。', 'warning', '');
            return false;
        }
        if (!new RegExp("^[0-9]{1,1000}$").test($.trim(num))) {
            MessageAlert('请输入正确的循环条数，0-10000之间。', 'warning', '');
            return false;
        }
        $('#errorMsg').text("");
        return true;
    }
    var aCity = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外" }
    function isCardID(sId) {
        var iSum = 0;
        var info = "";
        if (!/^\d{17}(\d|x)$/i.test(sId)) return "你输入的身份证长度或格式错误";
        sId = sId.replace(/x$/i, "a");
        if (aCity[parseInt(sId.substr(0, 2))] == null) return "你的身份证地区非法";
        sBirthday = sId.substr(6, 4) + "-" + Number(sId.substr(10, 2)) + "-" + Number(sId.substr(12, 2));
        var d = new Date(sBirthday.replace(/-/g, "/"));
        if (sBirthday != (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate())) return "身份证上的出生日期非法";
        for (var i = 17; i >= 0; i--) iSum += (Math.pow(2, i) % 11) * parseInt(sId.charAt(17 - i), 11);
        if (iSum % 11 != 1) return "你输入的身份证号非法";
        return true;//aCity[parseInt(sId.substr(0,2))]+","+sBirthday+","+(sId.substr(16,1)%2?"男":"女") 
    }
</script>
