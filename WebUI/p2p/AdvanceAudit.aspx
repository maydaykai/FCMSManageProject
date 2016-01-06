<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvanceAudit.aspx.cs" Inherits="WebUI.p2p.AdvanceAudit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>垫付操作</title>
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/icon.css" rel="stylesheet" />
    <script>
        function refreshParent() {
            alert('垫付成功');
            window.location.href = '../p2p/OverdueManage.aspx?columnId=<%=ColumnId%>';
        }
</script>
</head>
<body>
    <form id="form1" runat="server">

        <table border="0" cellspacing="0" cellpadding="0" class="editTableb">
          <tr>
            <td style="height: 29px;text-align: right;">借款编号：</td>
            <td class="auto-style1" style="text-align: left; padding-left: 5px;"><label id="lab_LoanNumber" runat="server" /></td>
          </tr>
         <tr>
            <td style="height: 29px;text-align: right;">借款金额：</td>
            <td class="auto-style1" style="text-align: left; padding-left: 5px;"><label id="lab_LoanAmount" runat="server" /></td>
          </tr>
        <tr>
            <td style="height: 29px;text-align: right;">利率：</td>
            <td class="auto-style1" style="text-align: left; padding-left: 5px;"><label id="lab_LoanRate" runat="server" /></td>
          </tr>
          <tr>
            <td style="height: 29px;text-align: right;">期限：</td>
            <td class="auto-style1" style="text-align: left; padding-left: 5px;"><label id="lab_LoanTerm" runat="server" /></td>
          </tr>
           <tr>
            <td style="height: 29px;text-align: right;">已还本息：</td>
            <td class="auto-style1" style="text-align: left; padding-left: 5px;"><label id="lab_ReceivedPrincipalAndInterest" runat="server" /></td>
          </tr>
            <tr>
            <td style="height: 29px;text-align: right;">逾期未还罚息：</td>
            <td class="auto-style1" style="text-align: left; padding-left: 5px;"><label id="lab_OverInterest" runat="server" /></td>
          </tr>
        <tr>
            <td style="height: 29px;text-align: right;">当期需还利息：</td>
            <td class="auto-style1" style="text-align: left; padding-left: 5px;"><label id="lab_ReInterest" runat="server" /></td>
          </tr>
            <tr>
            <td style="height: 29px;text-align: right;">当期需还本金：</td>
            <td class="auto-style1" style="text-align: left; padding-left: 5px;"><label id="lab_RePrincipal" runat="server" /></td>
          </tr>
            <tr>
            <td style="height: 29px;text-align: right;">居间服务费：</td>
            <td class="auto-style1" style="text-align: left; padding-left: 5px;"><label id="lab_LoanServiceFee" runat="server" /></td>
          </tr>
            <tr>
            <td style="height: 29px;text-align: right;">逾期还款所需总金额：</td>
            <td class="auto-style1" style="text-align: left; padding-left: 5px;"><label id="lab_RepaymentAmount" runat="server" /></td>
          </tr>
            <tr>
            <td style="height: 29px;text-align: right;">担保公司账户余额：</td>
            <td class="auto-style1" style="text-align: left; padding-left: 5px;"><label id="lab_GuaranteeBalance" runat="server" /></td>
          </tr>
            
        </table>

        <div>
            <input id="btnOK" type="button" value="确认垫付" runat="server" onserverclick="OK_Click" style="margin-right:5px;width:80px; " class="inputButton" />
        </div>
    </form>
</body>
</html>
