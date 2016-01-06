<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ColumnEdit.aspx.cs" Inherits="WebUI.Basic.ColumnEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>栏目编辑</title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" class="editTable">
            <tr>
                <td style="text-align: right;width: 150px;">所属栏目：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <asp:Literal ID="litColumnInfo" runat="server" Text="栏目"></asp:Literal>
                    <input id="txtParentID" type="hidden" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right">栏目名称：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:4px;">
                    <span class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </span>
                    <input id="txtName" type="text" runat="server" class="input_text" style="width: 120px;" />
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">链接地址：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:4px;">
                    <span class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </span>
                    <input id="txtLinkUrl" type="text" runat="server" style="width: 300px;" class="input_text"/>
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>

            <tr>
                <td style="text-align: right">所拥有的权限组：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <asp:CheckBoxList ID="ckbRightList" runat="server" RepeatDirection="Horizontal" BorderColor="White" RepeatLayout="Flow" CssClass="checkList">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">排序编号：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:4px;">
                    <span class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </span>
                    <input id="txtSort" type="text" runat="server" style="width: 50px;" class="input_text"/>
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">启用状态：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <input id="chkVisible" name="chkVisible" type="checkbox" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: text-top;">栏目图标：</td>
                <td style="text-align: left; padding-left: 5px;padding-top: 5px;">
                    <input id="txtICon" type="hidden" runat="server" />
                    <span style="border: solid 1px #ddd;padding: 2px 0 0 2px;"><span id="selectedIcon">&nbsp;</span></span>（栏目标签前面的小图标,点击选择图标)
                    <div style="width: 360px; height: 450px; border: solid 1px #ddd; padding: 5px;margin: 5px 0;">
                        <%
                            for (int i = 1; i < 501; i++)
                            {
                        %>
                        <span class="icon icon-<%=i %>" style="float: left; cursor: pointer;" onclick="GetIcon(this)">&nbsp;</span>
                        <% } %>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: central;">描述：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <textarea id="txtDescription" cols="50" rows="2" runat="server" style="padding: 5px;"></textarea>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left; padding-left: 5px;">
                    <input id="Button1" type="button" value="提交" runat="server" onserverclick="Operator_Click" class="inputButton" />
                    <input type="button" value="返回" onclick="location.href = 'ColumnManage.aspx?columnId=<%=ColumnId%>';" class="inputButton" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<script type="text/javascript">
    $(function () {
        $('#selectedIcon').attr('class', $('#txtICon').val());
        $(':checkbox').tzCheckbox({ labels: ['Enable', 'Disable'] });
    });
    function GetIcon(obj) {
        $('#txtICon').val($(obj).attr('class'));
        $('#selectedIcon').attr('class', $(obj).attr('class'));
    }
</script>
