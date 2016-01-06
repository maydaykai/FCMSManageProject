<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DistributionPerson.aspx.cs" Inherits="WebUI.UserMarketing.Marketing_Ex_Person.DistributionPerson" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户分配角色</title>
    <link href="../../css/global.css" rel="stylesheet" />
    <link href="../../css/icon.css" rel="stylesheet" />
    <link href="../../js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <link href="../../css/select.css" rel="stylesheet" />
    <script src="../../js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="../../js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="../../js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="../../js/lhgdialog/ShowDialog.js"></script>


</head>
<body>
    <form id="form1" runat="server">

        <!--显示当前人员列表-->
        <fieldset>
            <legend>组员列表：</legend>
            <table cellpadding="0" cellspacing="0" class="editTable" border="0" style="min-width: 10px;">
                <tr>
                    <td style="text-align: left; width: 150px;">姓名</td>
                    <td style="text-align: left; width: 150px;">电话</td>
                    <td style="text-align: left; width: 150px;">年龄</td>
                    <td style="text-align: left; width: 150px;">性别</td>
                    <td style="text-align: left; width: 150px;">所属组长</td>
                </tr>
                <lable id="list_html" runat="server"></lable>
            </table>

        </fieldset>
        <br />
        <fieldset>
            <legend>分配人员信息：</legend>
            <div id="roleList">
                <asp:CheckBoxList ID="ckbRoleList" runat="server" RepeatDirection="Horizontal" BorderColor="White" RepeatLayout="Flow" CssClass="checkList" RepeatColumns="5" TextAlign="Left">
                </asp:CheckBoxList>
            </div>
            <label runat="server" id="lab_one"></label>
            <br />
            <asp:Button runat="server" ID="Save" CssClass="inputButton" Text="保存" OnClick="Save_Click" />
        </fieldset>


    </form>
    <script type="text/javascript">
        $(function () {
            $(':checkbox').tzCheckbox({ labels: ['Enable', 'Disable'] });
        });
    </script>

    <script src="../../js/select2css.js"></script>
</body>
</html>
