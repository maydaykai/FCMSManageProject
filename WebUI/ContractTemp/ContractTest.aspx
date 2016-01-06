<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractTest.aspx.cs" Inherits="WebUI.ContractTemp.ContractTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="/js/jQuery/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js" type="text/javascript"></script>
    <script src="/js/lhgdialog/ShowDialog.js" type="text/javascript"></script>
    <title>生成合同</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="txtLoanID" type="text" runat="server" />
            <input id="Contract_Btn" type="button" value="生成合同" runat="server" OnServerClick="Contract_Btn_Click" />
        </div>
    </form>
</body>
</html>
