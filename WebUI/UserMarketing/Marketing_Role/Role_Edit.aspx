<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Role_Edit.aspx.cs" Inherits="WebUI.UserMarketing.Marketing_Role.Role_Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/kindeditor-4.1.10/themes/default/default.css" />
    <link rel="stylesheet" href="/js/kindeditor-4.1.10/plugins/code/prettify.css" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="/js/select2css.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <script charset="utf-8" src="/js/kindeditor-4.1.10/kindeditor.js"></script>
    <script charset="utf-8" src="/js/kindeditor-4.1.10/lang/zh_CN.js"></script>
    <script charset="utf-8" src="/js/kindeditor-4.1.10/plugins/code/prettify.js"></script>
    <script src="../js/juploader-1.0/jquery.jUploader-1.0.js"></script>
    <style type="text/css">
        .selectDiv .select_box {
            width: 175px;
        }

        .selectDiv ul {
            width: 190px;
        }

        .noBorderTable {
            border: none;
        }

            .noBorderTable td {
                border: none;
            }

        .imgS {
            max-width: 400px;
            width: expression(document.body.clientWidth > 400?"400px":"auto" );
        }
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" class="editTable" border="0" style="min-width: 800px;">
                <tr>
                    <td style="text-align: right; width: 150px;">角色名称：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <div style="float: left; margin-bottom: -3px;">
                            <span class="fl">
                                <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                            <input id="txt_RoleName" type="text" value="" class="input_text fl" style="width: 400px;" runat="server" />
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
                        <asp:Button ID="btnSave1" runat="server" Text="提交" CssClass="inputButton" OnClick="btnSave_ServerClick" />&nbsp;&nbsp;<input type="button" class="inputButton" value="返回" onclick="location.href = 'InformationManage.aspx?columnId=<%=ColumnId%> ';" />
                    </td>
                </tr>
       
            </table>
        </div>
    </form>

</body>
</html>

