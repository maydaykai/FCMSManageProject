<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewLuckyDraw.aspx.cs" Inherits="WebUI.LuckyDraw.PreviewLuckyDraw" %>
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
                    <div id="msgBoxContent" style="color: rgb(90, 187, 249); height: 100%; width: 100%;">
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
                minWidth: 550,
                height: 550,
                width: 550,
                resizable: false,
                isModal: true,
                modalOpacity: 0.3,

                position: 'center, left',
                initContent: function() {
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
            $('#msgBox').on('close', function() { history.back(); });
            ShowSweepstakeMember("PreviewLuckyDrawDemo.aspx");
        });

        var ShowSweepstakeMember = function(v) {
            var content = '<iframe frameborder=0 id="iframeid" name="iframename" scrolling=no src="' + v + '" style="width: 100%;height: 100%"></iframe> ';
            ShowMessage(content);
        };

    </script>
    <%-- ReSharper restore ConditionIsAlwaysConst --%>
</html>