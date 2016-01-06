<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyAbandonLoanAudit.aspx.cs" Inherits="WebUI.p2p.ApplyAbandonLoanAudit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>申请流标审核</title>
    <link href="../css/table.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <link href="../js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="../js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="../js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>
    <script type="text/javascript">
        function validate(){
            var radValue = $("input[name='Rad_Check']:checked").val();
            if(radValue == undefined){
                MessageAlert('请先审核','Warning','');
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" class="editTable">
        <tr>
            <td style="text-align: right; width: 120px;">借款标题：</td>
            <td style="text-align: left; padding-left: 5px;">
                <label id ="lab_loanNum" runat="server"></label>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 120px;">展期期限：</td>
            <td style="text-align: left; padding-left: 5px;">
                <label id ="lab_loanTerm" runat="server"></label>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 120px;">借款金额：</td>
            <td style="text-align: left; padding-left: 5px;">
                <label id ="lab_loanAmount" runat="server"></label>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 120px;">申请备注：</td>
            <td style="text-align: left; padding-left: 5px;">
                <textarea id="applyRemark" name="content1" cols="100" rows="8" style="width: 480px; height: 120px;" disabled="disabled" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 120px;">审核备注：</td>
            <td style="text-align: left; padding-left: 5px;">
                <textarea id="auditRemark" name="content1" cols="100" rows="8" style="width: 480px; height: 120px;" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 120px;">审核记录：</td>
            <td style="text-align: left; padding-left: 5px;">
                <label id ="lab_auditRecords" runat="server"></label>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 120px;">审核状态：</td>
            <td style="text-align: left; padding-left: 5px;">
                <span class="fl" style="margin-top:10px;">
                    <asp:RadioButtonList ID="Rad_Check" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">初审不通过</asp:ListItem>
                        <asp:ListItem Value="2">初审通过</asp:ListItem>
                        <asp:ListItem Value="3">复审不通过</asp:ListItem>
                        <asp:ListItem Value="4">复审通过</asp:ListItem>
                    </asp:RadioButtonList>
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 120px;"></td>
            <td style="text-align: left;">
                <%--<input type="button" value="查看应收费用" class="inputButton_C" onclick="window.location.href = 'DeferLoanManage.aspx?columnId=<%=ColumnId%>    ';" />--%>
                <asp:Button ID="Btn_Audit" Text="保存审核" CssClass="inputButton_B" runat="server" OnClick="Audit_Click" OnClientClick="return validate();" />
                <input type="button" value="返回" class="inputButton" onclick="window.location.href = 'AbandonLoanManage.aspx?columnId=<%=ColumnId%>    ';" />
            </td>            
        </tr>
    </table>
    </form>
</body>
</html>

