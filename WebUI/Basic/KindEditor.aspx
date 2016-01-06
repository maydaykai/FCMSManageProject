<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KindEditor.aspx.cs" Inherits="WebUI.Basic.KindEditor" ValidateRequest="false" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>KindEditor ASP.NET</title>
    <link rel="stylesheet" href="/js/kindeditor-4.1.10/themes/default/default.css" />
    <link rel="stylesheet" href="/js/kindeditor-4.1.10/plugins/code/prettify.css" />
    <script charset="utf-8" src="/js/kindeditor-4.1.10/kindeditor.js"></script>
    <script charset="utf-8" src="/js/kindeditor-4.1.10/lang/zh_CN.js"></script>
    <script charset="utf-8" src="/js/kindeditor-4.1.10/plugins/code/prettify.js"></script>
    <script>
        KindEditor.ready(function (K) {
            var editor1 = K.create('#content1', {
                cssPath: '/js/kindeditor-4.1.10/plugins/code/prettify.css',
                uploadJson: '/js/kindeditor-4.1.10/asp.net/upload_json.ashx',
                fileManagerJson: '/js/kindeditor-4.1.10/asp.net/file_manager_json.ashx',
                allowFileManager: true,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                }
            });
            prettyPrint();
        });
    </script>
    <script>
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('textarea[name="content"]', {
                resizeType: 1,
                allowPreviewEmoticons: false,
                allowImageUpload: false,
                items: [
                    'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                    'insertunorderedlist', '|', 'link', 'unlink', 'preview']
            });
        });
    </script>
</head>
<body>

    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <div id="divHtml" runat="server"></div>
    <form id="example" runat="server">
        <textarea id="content1" cols="100" rows="8" style="width: 700px; height: 200px; visibility: hidden;" runat="server"></textarea>
        <br />
        <textarea id="content" name="content" cols="100" rows="8" style="width: 500px; height: 200px; visibility: hidden;" runat="server"></textarea>
        <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
    </form>
</body>
</html>
