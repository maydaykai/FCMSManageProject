<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanDelayedAudit.aspx.cs" Inherits="WebUI.p2p.LoanDelayedAudit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/icon.css" rel="stylesheet" />
    <link href="../js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="../js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="../js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#ckbEnable').tzCheckbox({ labels: ['Enable', 'Disable'] });
        });
    </script>
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>
</head>

<body>
    <form id="form1" runat="server">
    <div>
        <input id="btnOK" type="button" value="通过" runat="server" onserverclick="OK_Click" style="margin-right:5px; " class="inputButton" />
        <input id="btnNO" type="button" value="不通过" runat="server" onserverclick="NO_Click" style="margin-right:10px; " class="inputButton" />
    </div>
    </form>
</body>
</html>
