<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AboutUsEdit.aspx.cs" Inherits="WebUI.Information.AboutUsEdit" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
    <script type="text/javascript">
        $(function () {
            var editor;
            KindEditor.ready(function (K) {
                editor = K.create('#<%=txtContent.ClientID%>', {
                    cssPath: '/js/kindeditor-4.1.10/plugins/code/prettify.css',
                    uploadJson: '/js/kindeditor-4.1.10/asp.net/upload_json.ashx',
                    items: [
		                'source', '|', 'undo', 'redo', '|', 'preview', 'print', 'template', 'code', 'cut', 'copy', 'paste',
		                'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
		                'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
		                'superscript', 'clearhtml', 'quickformat', 'selectall', '|', 'fullscreen', '/',
		                'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold',
		                'italic', 'underline', 'strikethrough', 'lineheight', 'removeformat', '|', 'image',
		                'table', 'hr', 'emoticons', 'baidumap', 'pagebreak',
		                'anchor', 'link', 'unlink', '|', 'about'
                    ],
                    afterCreate: function () {
                        var self = this;
                        K.ctrl(document, 13, function () {
                            self.sync();
                            K('form[name=form1]')[0].submit();
                        });
                        K.ctrl(self.edit.doc, 13, function () {
                            self.sync();
                            K('form[name=form1]')[0].submit();
                        });
                    }
                });
                prettyPrint();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" class="editTable" border="0" style="min-width:800px;">
            <tr>
                <td style="text-align: right; width: 150px;">资讯标题：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtTitle" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 200px;" runat="server" disabled="disabled" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; ">资讯正文：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <textarea id="txtContent" runat="server" style="width:700px;height:500px;" name="content"></textarea>
                </td>
            </tr>
            <tr>
                    <td style="text-align: right; ">操作：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <asp:Button ID="btnSave1" runat="server" Text="提交" CssClass="inputButton" OnClick="btnSave1_Click" />&nbsp;&nbsp;<%--<input type="button" class="inputButton" value="返回" onclick="history.go(-1);" />--%>
                    </td>
                </tr>
        </table>
    </div>
    </form>
</body>
</html>
