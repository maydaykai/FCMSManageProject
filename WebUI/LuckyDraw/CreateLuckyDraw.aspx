<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateLuckyDraw.aspx.cs" Inherits="WebUI.LuckyDraw.CreateLuckyDraw" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>
        <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
        <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.ui-start.css" type="text/css" />

        <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"> </script>
        <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqx-all.js"> </script>
    </head>

    <body>

        <div>
            <div id="msgBox">
                <div>提示信息</div>
                <div>
                    <div id="msgBoxContent" style="color: rgb(90, 187, 249);padding: 10px;"></div>
                    <div>
                        <div style="float: right; margin-top: 10px;">
                            <input type="button" id="ok" value="确定" style="cursor: pointer; margin-right: 10px;" />
                            <input type="button" id="cancel" value="取消" style="cursor: pointer; margin-right: 10px;" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </body>

    <script>
        $.jqx = $.jqx || {};
        $.jqx.theme = "ui-start";
    </script>

    <script>
        var CreateElements = function() {
            $('#msgBox').jqxWindow({
                autoOpen: false,
                minHeight: 10,
                minWidth: 200,
                height: 100,
                width: 300,
                resizable: false,
                isModal: true,
                modalOpacity: 0.3,
                okButton: $('#ok'),
                cancelButton: $('#cancel'),
                position: 'center, left',
                initContent: function() {
                    $('#ok').jqxButton({ width: '65px' });
                    $('#cancel').jqxButton({ width: '65px' });
                    $('#ok').focus();
                }
            });
        };

        $(document).ready(function() {
            CreateElements();
        });
    </script>

    <%-- ReSharper disable ConditionIsAlwaysConst --%>
    <script>
        var ShowMessage = function(message) {
            $("#msgBoxContent").html(message);
            $('#msgBox').jqxWindow('open');
        };

        $(document).ready(function() {
            var message = "<%= _error %>";
            if (message == "") {
                $('#msgBox').on('close', function() { window.location.href = "Default.aspx"; });
                ShowMessage("操作成功");
            } else {
                $('#msgBox').on('close', function() { history.back(); });
                ShowMessage(message);
            }
        });
    </script>
    <%-- ReSharper restore ConditionIsAlwaysConst --%>
</html>