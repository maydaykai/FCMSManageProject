<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParameterConfiguration.aspx.cs" Inherits="WebUI.Basic.ParameterConfiguration" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>参数配置</title>
    <link href="../css/global.css" rel="stylesheet" />
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" border="0" class="editTable">
            <asp:Repeater ID="Repeater1" runat="server"
                OnItemDataBound="Repeater1_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td style="width: 220px; text-align: right; background-color: #E0ECFF;">
                            <asp:HiddenField ID="hidID" runat="server" Value='<%#Eval("ID") %>' />
                            <asp:HiddenField ID="hidType" runat="server" Value='<%#Eval("ParameterType") %>' />
                            <%#Eval("ParameterName")%>：
                        </td>
                        <td style="width: 120px; padding-top:5px; padding-left:5px; padding-right:5px;">
                            <span class="fl">
                                <img src="/images/input_left.png" width="4" height="29" alt="" />
                            </span>
                            <input id="txtValue" type="text" runat="server" style="width: 90px;" value='<%#Eval("ParameterValue") %>' class="input_text" />
                            <span class="fl">
                                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                            <span style="display:inline-block;margin-top:5px;">&nbsp;<%#Eval("ParameterUnit")%></span>
                        </td>
                        <td style="text-align: left; padding-left: 10px;">
                            <%#Eval("Remarks")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td style="text-align: center;" colspan="3">
                    <asp:Button ID="btnOK" runat="server" Text="保存" OnClick="btnOK_Click" CssClass="inputButton" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
