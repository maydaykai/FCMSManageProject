<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditFunctionOperation.aspx.cs" Inherits="WebUI.UserMarketing.Marketing_Competence.EditFunctionOperation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link href="../../css/icon.css" rel="stylesheet" />
    <link href="../../css/global.css" rel="stylesheet" />
    <link href="../../js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <script src="../../js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="../../js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="../../js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="../../js/lhgdialog/ShowDialog.js"></script>



</head>
<body>
    <form id="form1" runat="server">
            <table cellpadding="0" cellspacing="0" class="editTable" border="0" style="min-width: 800px;">
                <tr>
                    <td style="text-align: right; width: 150px;">功能名称：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <div style="float: left; margin-bottom: -3px;">
                            <span class="fl">
                                <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                            <input id="txt_OperationName" type="text" value="" class="input_text fl" style="width: 400px;" runat="server" />
                            <span class="fl">
                                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 150px;">功能名称代码：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <div style="float: left; margin-bottom: -3px;">
                            <span class="fl">
                                <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                            <input id="txt_OperationCode" type="text" value="" class="input_text fl" style="width: 400px;" runat="server" />
                            <span class="fl">
                                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 150px;">设置营销角色的可见范围：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <div id="roleList">
                            <asp:CheckBoxList ID="ckbRoleList" runat="server" RepeatDirection="Horizontal" BorderColor="White" RepeatLayout="Flow" CssClass="checkList">
                            </asp:CheckBoxList>
                        </div>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right; width: 150px;">备注：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <div style="float: left; margin-bottom: -3px;">
                            <span class="fl">
                                <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                            <input id="txt_Remake" type="text" value="" class="input_text fl" style="width: 400px;" runat="server" />
                            <span class="fl">
                                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">操作：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <input type="hidden" id="hie_Image_value" runat="server" />
                        <input type="hidden" id="hie_source" runat="server" />
                        <asp:Button ID="btnSave1" runat="server" Text="提交" CssClass="inputButton" OnClick="btnSave_ServerClick" />&nbsp;&nbsp;<input type="button" class="inputButton" value="返回" onclick="location.href = 'InformationManage.aspx?columnId=<%=ColumnId%>    ';" />
                    </td>
                </tr>

            </table>


    </form>
    <script type="text/javascript">
        $(function () {
            $(':checkbox').tzCheckbox({ labels: ['Enable', 'Disable'] });
        });
    </script>

    <script src="../../js/select2css.js"></script>
</body>
</html>


