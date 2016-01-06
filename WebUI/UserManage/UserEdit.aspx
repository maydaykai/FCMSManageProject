<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="WebUI.UserManage.UserEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户信息编辑</title>
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
            <tr>
                <td style="text-align: right; width:150px;">用户名：</td>
                <td style="text-align: left; padding-left: 5px; color: #f00; padding-top:5px;">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" id="imgUserNameLeft" runat="server" />
                    </span>
                    <input id="txtUserName" type="text" runat="server" style="width: 120px;" class="input_Disabled" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" id="imgUserNameRight" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">登录密码：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:5px;">
                    <div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div>
                    <input id="txtPwd" type="password" runat="server" style="width: 120px;" class="fl input_text"  /><div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>&nbsp;&nbsp;<span class="fl" style="margin-top:5px; margin-left:5px;">确认密码：</span><div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div><input id="txtPwdConfirm" type="password" runat="server" style="width: 120px;" class="fl input_text" />
                    <div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
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
                <td style="text-align: right">别名：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:5px;">
                    <div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div>
                    <input id="txtAnotherName" type="text" runat="server" style="width: 120px;" class="fl input_text" />
                    <div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">性别：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <asp:RadioButtonList ID="rblSex" runat="server" RepeatColumns="2" BorderStyle="None">
                        <asp:ListItem Value="1">男</asp:ListItem>
                        <asp:ListItem Value="0">女</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">所属角色：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <asp:CheckBoxList ID="ckbRoleList" runat="server" RepeatDirection="Horizontal" BorderColor="White" RepeatLayout="Flow" CssClass="checkList">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">所属担保公司：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:5px;">
                    <div class="selectDiv">
                        <div class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </div>
                        <select id="selParentId" runat="server">
                        </select>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selParentId" />
                        </div>
                    </div>
                    <span style="color: #f00; display:inline-block;margin-top:5px;">（温馨提示：主要用于担保公司,如非担保公司下属用户（担保初审、核保审批）不需选择。）</span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">联系电话：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:5px;">
                    <div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div>
                    <input id="txtPhone" type="text" runat="server" style="width: 120px;" class="fl input_text" />
                    <div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">手机号码：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:5px;">
                    <div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div>
                    <input id="txtMobile" type="text" runat="server" style="width: 120px;" class="fl input_text" />
                    <div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">Email：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:5px;">
                    <div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div>
                    <input id="txtEmail" type="text" runat="server" style="width: 200px;" class="fl input_text" />
                    <div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">QQ：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:5px;">
                    <div class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div>
                    <input id="txtQQ" type="text" runat="server" style="width: 120px;" class="fl input_text" />
                    <div class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">注册时间：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <asp:Literal ID="litRegTime" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">最后登陆时间：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <asp:Literal ID="litLastLoginTime" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">最后登陆IP地址：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <asp:Literal ID="litLastLoginIP" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">登录次数：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <asp:Literal ID="litTimes" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">是否锁定：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <input id="chkLock" name="chkLock" type="checkbox" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left; padding-left: 5px;">
                    <input id="Button1" type="button" value="提交" runat="server" onserverclick="Button1_Click" class="inputButton" />
                    <input type="button" value="返回" class="inputButton" onclick="window.history.go(-1);" />
                </td>
            </tr>
        </table>
    </form>
    </body>
</html>

<script type="text/javascript">
    $(function () {
        $(':checkbox').tzCheckbox({ labels: ['Enable', 'Disable'] });
    });
</script>
<script src="../js/select2css.js"></script>
