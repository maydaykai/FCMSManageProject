<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IdentityAuthentEdit.aspx.cs" Inherits="WebUI.MemberManage.IdentityAuthentEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>实名认证管理</title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <link href="/css/select.css" rel="stylesheet" />
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" class="editTable">
            <tr>
                <td style="text-align: right; width: 200px;">会员名称：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtMemberName" type="text" name="txtMemberName" value="" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" disabled="True" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">真实姓名：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtRealName" type="text" name="txtRealName" value="" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" disabled="True" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">身份证号码：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtIdentityCard" type="text" name="txtIdentityCard" value="" class="input_Disabled fl" maxlength="20" style="width: 200px;" runat="server" disabled="True" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                    <div class='tip_wrap'>
                        <div class="tip_content" style="color: #DC143C">
                            <asp:Literal ID="litAnthStatus" runat="server"></asp:Literal>
                        </div>
                        <span class='arrow_left' style='top: 1px;'></span>
                    </div>
                </td>
            </tr>
            <%--<tr>
                <td style="text-align: right; vertical-align: top;">身份证照片：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="width: 80px; height: 100px; border: solid 1px #ccc; padding: 2px; margin: 3px;">
                        &nbsp;
                    </div>
                </td>
            </tr>--%>
            <tr>
                <td style="text-align: right">手机号码：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtMobile" type="text" name="txtMobile" value="" class="input_Disabled fl" maxlength="20" style="width: 150px;" runat="server" disabled="True" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">认证有效期：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtExpireTime" type="text" name="txtExpireTime" value="" disabled="True" class="input_Disabled fl" maxlength="20" style="width: 145px;" runat="server" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left; padding-left: 5px;">
                    <% if (RightUpdate)
                       { %>
                    <input type="button" id="btn_OperaterA" value="保存" class="inputButton" runat="server" onserverclick="btn_Operater_Click" />
                    <input id="Button1" type="button" value="重置" class="inputButton" onserverclick="btn_Click" runat="server" />
                    <% }
                       else
                       { %>
                    <input type="button" id="btn_OperaterB" value="保存" class="inputButtonDisabled" disabled="True" runat="server" onserverclick="btn_Operater_Click" />
                    <% } %>
                    <input type="button" value="返回" class="inputButton" onclick="window.history.go(-1);" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
<style type="text/css">
    .selectDiv .select_box { width: 60px; }

    .selectAnthStatus .select_box { width: 75px; }
</style>
<script type="text/javascript">
    $(function () {
        $(':checkbox').tzCheckbox({ labels: ['Enable', 'Disable'] });
    });
</script>
