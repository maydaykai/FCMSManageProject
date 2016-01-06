<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailyReport.aspx.cs" Inherits="WebUI.ReportStatistics.DailyReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>RJB运营日报表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/global.css" rel="stylesheet" />
    <script src="../js/jQuery/jquery-1.8.3.min.js"></script>
    <script src="/js/select2css.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <style type="text/css">
        #searchbar span { height: 29px; text-indent: 5px; line-height: 29px; }
        .selectDiv .select_box { width: 85px; }
        .selectDiv2 .select_box { width: 105px; }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#btn_Download").click(function () {
                if ($("#txtDownloadDate").val() == "") {
                    $("#txtDownloadDate").focus();
                    alert("请选择要生成的日期！");
                    return;
                }
                var downDate = $("#txtDownloadDate").val().replace(/\-/g, "");
                $("#DownloadUrl").html("<a href=\"http://file.rjb777.com/ExcelReport/" + downDate.substring(0, 6) + "/RJB运营日报表(" + downDate + ").xls\">下载 RJB运营日报表(" + downDate + ").xls</a>");
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1000px;">
            <div style="float: left;">
                <div class="fl">
                    <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                </div>
                <input id="txtDownloadDate" type="text" name="txtDownloadDate" value="" onclick="WdatePicker({ maxDate: '%y-%M-%d' })" class="input_text fl" maxlength="20" style="width: 100px;" runat="server" placeholder="选择要生成的日期" />
                <div class="fl">
                    <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                </div>
            </div>
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="btn_Download" value="生成报表下载连接" class="inputButton" style="width: 140px;" />
                <span id="DownloadUrl"></span>
                <%--                <input id="ExcelExport" type="button" value="导出Excel" class="inputButton" style="width: 100px;" runat="server" onserverclick="ExcelExport_Click" />--%>
            </div>
        </div>
    </form>
</body>
</html>
