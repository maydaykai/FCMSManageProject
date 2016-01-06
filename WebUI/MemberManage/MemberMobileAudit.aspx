<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberMobileAudit.aspx.cs" Inherits="WebUI.MemberManage.MemberMobileAudit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/table.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <script src="../js/jQuery/jquery-1.8.3.min.js"></script>
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>
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
                    <input id="txtMemberName" type="text" name="txtMemberName" value="" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" readonly="readonly" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">真实姓名：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtRealName" type="text" name="txtRealName" value="" class="input_Disabled fl" maxlength="50" style="width: 100px;" runat="server" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">身份证号码：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtIdentityCard" type="text" name="txtIdentityCard" value="" class="input_Disabled fl" maxlength="50" style="width: 100px;" runat="server" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">原手机号码：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtOldMobile" type="text" name="txtOldMobile" value="" class="input_Disabled fl" maxlength="50" style="width: 150px;" runat="server" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">将修改手机号码：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtNewMobile" type="text" name="txtNewMobile" value="" class="input_Disabled fl" maxlength="50" style="width: 150px;" runat="server" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">申诉说明：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <textarea id="auditRemark" name="auditRemark" cols="100" rows="8" style="width: 480px; height: 120px; padding: 5px;" disabled="disabled" runat="server"></textarea>
                </td>
            </tr>
            <tr id="Tr_AuditStatus" runat="server" visible="False">
                <td style="text-align: right; width: 120px;">选择审核状态：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span style="float: left; height: 25px; line-height: 25px;">
                        <input id="Radio1" name="AuditStatus" type="radio" runat="server" value="1" /></span><label for="Radio1" style="float: left; height: 25px; line-height: 25px; margin-top: -2px;">「审核通过」</label>
                    <span style="float: left; height: 25px; line-height: 25px; margin-left: 8px;">
                        <input id="Radio2" name="AuditStatus" type="radio" runat="server" value="2" /></span><label for="Radio2" style="float: left; height: 25px; line-height: 25px; margin-top: -2px;">「审核不通过」</label>
                </td>
            </tr>
            <tr id="Tr_AuditRecords" runat="server" visible="False">
                <td style="text-align: right; width: 120px;">审核记录：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <label id="AuditRecords" runat="server"></label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;"></td>
                <td style="text-align: left;">
                    <asp:Button ID="Btn_Audit" Text="确定审核" CssClass="inputButton" runat="server" Width="80px" OnClick="Audit_Click" />
                    <input type="button" value="返回" class="inputButton" onclick="window.location.href = 'MemberMobileManege.aspx?columnId='<%=ColumnId %>" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
