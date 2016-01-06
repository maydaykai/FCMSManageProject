<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Role_Distribution.aspx.cs" Inherits="WebUI.UserMarketing.Marketing_User.Role_Distribution" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户分配角色</title>
    
    <link href="../../css/icon.css" rel="stylesheet" />
    <link href="../../css/global.css" rel="stylesheet" />
    <link href="../../js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <script src="../../js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="../../js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="../../js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="../../js/lhgdialog/ShowDialog.js"></script>

        
    <link href="../../js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="roleList">
         <asp:CheckBoxList ID="ckbRoleList" runat="server" RepeatDirection="Horizontal" BorderColor="White" RepeatLayout="Flow" CssClass="checkList" RepeatColumns="5">
                    </asp:CheckBoxList>
    </div>
        <br />
        <asp:Button  runat="server" ID="Save"  CssClass="inputButton" Text="保存" OnClick="Save_Click"/>

    </form>
        <script type="text/javascript">
            $(function () {
                $(':checkbox').tzCheckbox({ labels: ['Enable', 'Disable'] });
            });
    </script>

    <script src="../../js/select2css.js"></script>
</body>
</html>
