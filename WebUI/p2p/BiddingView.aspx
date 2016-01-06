<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BiddingView.aspx.cs" Inherits="WebUI.p2p.BiddingView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>投资列表</title>
    <link href="../css/table.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <style type="text/css">
        .TableSelect {
            background-color: #fcefa1;
        }
        .ml20 {
            margin-left: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="selectDiv">
            <input type="button" value="返回" style="width: 60px;" class="inputButton" onclick="window.location.href = 'LoanManage.aspx?columnId=<%=ColumnId%>    ';" />
            
            <label class="ml20">借款标题：</label><label id="loanTitle" runat="server"></label>
            <label class="ml20">借款编号：</label><label id="loanNumber" runat="server"></label>
        </div>
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
                <table border="0" cellspacing="0" cellpadding="0" class="editTable" style="width:85%" id="AuditRecord">
                    <tr style="font-weight: bold; background-color: #99BBE8">
                        <td width="100">投标人</td>
                        <td width="65">姓名</td>
                        <td width="65">推荐人</td>
                        <td width="65">当前利率</td>
                        <td width="80">投标金额</td>
                        <td width="80">有效金额</td>
                        <td width="150">投标时间</td>
                        <td width="76">投标类型</td>
                        <td width="50">状 态</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr onmouseover="this.className = 'TableSelect';" onmouseout="this.className = '';" class="">
                    <td><%#Eval("MemberName") %></td>
                    <td><%#Eval("RealName") %></td>
                    <td><%#Eval("RecRealName") %></td>
                    <td><%#Eval("LoanRate") %>/年</td>
                    <td><%#string.Format("{0:C}",Eval("BidAmount")) %></td>
                    <td><%#string.Format("{0:C}",Eval("BidAmount")) %></td>
                    <td><%#Eval("CreateTime") %></td>
                    <td><%#Convert.ToInt32(Eval("BidType")) == 0 ? "手动竞标" : "自动竞标" %></td>
                    <td><%#Convert.ToInt32(Eval("BidStatus")) == 1 ? "正常" : "作废" %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <div class="selectDiv">
            <input type="button" value="返回" style="width: 60px;" class="inputButton" onclick="window.location.href = 'LoanManage.aspx?columnId=<%=ColumnId%>    ';" />
        </div>
    </form>
</body>
</html>
