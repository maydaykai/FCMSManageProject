<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BranchCompanyEmployeeAdd.aspx.cs" Inherits="WebUI.BranchCompanyManage.BranchCompanyEmployeeAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxtabs.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="/js/Area.js"></script>
    <script src="../js/loan/init.js"></script>
    <link href="../js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" rel="stylesheet" />
    <link href="../js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <style type="text/css">
        .selectDiv .select_box {
            width: 150px;
        }

        .TableSelect {
            background-color: #fcefa1;
        }
    </style>

    <style type="text/css">
        .auto-style1 {
            height: 35px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id='jqxTabs'>
            <ul>
                <li>分公司账号设置</li>
            </ul>
        </div>
        <table>
            <tr>
                <td style="text-align: right; width: 150px;">会员名：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtMemberName" type="text" name="txtMemberName" value="" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" readonly="readonly" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span id="selectSpan" class="fl" style="line-height: 29px; margin-left: 3px;" runat="server">
                            <input id="select_btn" type="button" value="选择会员" class="inputButton" style="width: 62px; font-weight: normal;" onclick="MessageWindow(590, 360, '/MemberManage/MemberListSelect.aspx')" />
                        </span>
                        <input id="txtMemberID" name="txtMemberID" type="hidden" runat="server" />
                        <input id="txtType" name="txtType" type="hidden" runat="server" />
                        <span class="fl" style="line-height: 29px; color: #dc143c;">&nbsp;&nbsp;请选择会员。</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">真实姓名：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtRealName" type="text" name="txtRealName" value="" class="input_Disabled fl" maxlength="50" style="width: 100px;" runat="server" readonly="readonly" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 150px;">分公司名称：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtBranchCompanyName" type="text" name="txtBranchCompanyName" value="" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" readonly="readonly" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span id="Span1" class="fl" style="line-height: 29px; margin-left: 3px;" runat="server">
                            <input type="button" value="选择分公司" class="inputButton_B" style="width: 72px; font-weight: normal;" onclick="MessageWindow(590, 360, '/BranchCompanyManage/BranchCompanyListSelect.aspx')" />
                        </span>
                        <input id="txtBranchCompanyID" name="txtBranchCompanyID" type="hidden" runat="server" />
                        <span class="fl" style="line-height: 29px; color: #dc143c;">&nbsp;&nbsp;请选择分公司。</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right"></td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <div style="float: left; margin-left: 5px;">
                        </div>
                        <asp:Button ID="Button1" runat="server" class="inputButton" Text="确定" OnClick="Button_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
