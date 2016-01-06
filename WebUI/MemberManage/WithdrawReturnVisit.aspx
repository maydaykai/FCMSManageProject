<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WithdrawReturnVisit.aspx.cs" Inherits="WebUI.MemberManage.WithdrawReturnVisit" %>

<%@ Import Namespace="System.Data" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/table.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <link href="../css/bankBg.css" rel="stylesheet" />
    <script src="../js/jQuery/jquery-1.8.3.min.js"></script>
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>
    <script src="../js/select2css.js"></script>
    <link href="../js/jquery.ganged/jquery.inputbox.css" rel="stylesheet" />
    <script src="../js/jquery.ganged/jquery.inputbox.js"></script>
    <style type="text/css">
        .sb .opts a { border: 1px #ffffff solid; }
            .sb .opts a:hover { background: #ffffff; color: #000000; border: 1px #ff4500 solid; }
            .sb .opts a.selected { background: #ffffff; color: #000000; }
            .sb .opts a.none { background: #ffffff; color: #000000; }
    </style>
    <script type="text/javascript">        
        function validate() {
            var radValue = $("input[name='AuditStatus']:checked").val();
            if (radValue == undefined) {
                MessageAlert('请选择审核状态', 'Warning', '');
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" class="editTable">
            <tr>
                <td style="text-align: right; width: 120px;">会员名：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtMemberName" type="text" name="txtMemberName" value="" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtMemberID" name="txtMemberID" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">开户人：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtAccountHolder" type="text" name="txtAccountHolder" value="" class="input_text fl" maxlength="50" style="width: 100px;" runat="server" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">手机号码：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtMobile" type="text" class="input_text fl" maxlength="50" style="width: 100px;" runat="server" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr id="bankListTr" runat="server">
                <td style="text-align: right; width: 120px;">收款银行帐号：</td>
                <td id="bankList" style="text-align: left; padding-left: 5px; padding-top: 5px; padding-bottom: 5px;" runat="server">请选择会员之后再选择银行卡
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">提现金额：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtCashAmount" type="text" name="txtCashAmount" value="" class="input_text fl" maxlength="50" style="width: 120px;" runat="server" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <span class="fl" style="line-height: 25px; margin-left: 3px;">元
                    </span>
                    <input id="txtCashID" type="hidden" runat="server" />
                </td>
            </tr>        
            <tr>
                <td style="text-align: right; width: 120px;">选择回访状态：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span style="float: left; height: 25px; line-height: 25px;">
                        <input id="Radio1" name="AuditStatus" type="radio" runat="server" /></span><label for="Radio1" style="float: left; height: 25px; line-height: 25px; margin-top: -2px;">「存疑」</label>
                    <span style="float: left; height: 25px; line-height: 25px; margin-left: 8px;">
                        <input id="Radio2" name="AuditStatus" type="radio" runat="server" /></span><label for="Radio2" style="float: left; height: 25px; line-height: 25px; margin-top: -2px;">「拒绝」</label>
                    <span style="float: left; height: 25px; line-height: 25px; margin-left: 8px;">
                        <input id="Radio3" name="AuditStatus" type="radio" runat="server" /></span><label for="Radio2" style="float: left; height: 25px; line-height: 25px; margin-top: -2px;">「通过」</label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px; vertical-align: top; padding-top: 3px;">备注：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <textarea id="txtNote" cols="100" rows="8" style="width: 480px; height: 120px; padding: 5px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">回访记录：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span id="spWarningRecord" runat="server" style="line-height: 30px;"></span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;"></td>
                <td style="text-align: left;">
                    <asp:Button ID="btnSave" Text="提交" CssClass="inputButton" runat="server" Width="100px" OnClientClick="return validate();" OnClick="btnSave_Click"/>
                    <input type="button" value="返回" class="inputButton" onclick="window.location.href = 'WithdrawAlert.aspx?<%=HttpContext.Current.Request.QueryString.ToString()%>  '" />
                </td>
            </tr>            
        </table>
    </form>
</body>
</html>
<style type="text/css">
    .selectDiv #select_selBankType { width: 110px; }
    .selectDiv #select_selCurrStatus { width: 80px; }
</style>
