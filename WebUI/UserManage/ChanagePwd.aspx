<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChanagePwd.aspx.cs" Inherits="WebUI.UserManage.ChanagePwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户密码修改</title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <script src="/js/jQuery/jquery-1.8.3.min.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" class="editTable">
            <tr>
                <td style="text-align: right; width: 150px;">用户名：</td>
                <td style="text-align: left; padding-left: 5px; color: #f00; padding-top: 5px;">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" id="imgUserNameLeft" runat="server" />
                    </span>
                    <input id="txtUserName" type="text" runat="server" style="width: 120px;" class="input_Disabled" disabled="True" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" id="imgUserNameRight" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">原密码：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div>
                    <input id="txtOldPwd" type="password" runat="server" style="width: 120px;" class="fl input_text" />
                    <div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">新密码：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div>
                    <input id="txtNewPwd" type="password" runat="server" style="width: 120px;" class="fl input_text" />
                    <div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">确认密码：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div>
                    <input id="txtConfirmPwd" type="password" runat="server" style="width: 120px;" class="fl input_text" />
                    <div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left; padding-left: 5px;">
                    <input id="Operater_Btn" type="button" value="提交" runat="server" class="inputButton"  onserverclick="Operater_Btn_Click"/>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

