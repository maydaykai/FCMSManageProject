<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BidPieTotal.aspx.cs" Inherits="WebUI.ReportStatistics.BidPieTotal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <script src="/js/select2css.js"></script>
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdraw.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxchart.core.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        .selectDiv .select_box {
            width: 85px;
        }

        .selectDiv2 .select_box {
            width: 115px;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#btnSearch").click(function () {
                $('#chartContainer').jqxChart(settings);
            });
            // prepare chart data as an array
            //主题
            var theme = "arctic";

            //参数组装
            function buildQueryString(data) {
                var str = ''; for (var prop in data) {
                    if (data.hasOwnProperty(prop)) {
                        str += prop + '=' + data[prop] + '&';
                    }
                }
                return str.substr(0, str.length - 1);
            }

            var formatedData = '';

            //数据源
            var source = {
                url: '/HanderAshx/ReportStatistics/BidPieTotalHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'TotalType', type: 'string' },
                    { name: 'BidPercent', type: 'number' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    //data.typeId = $("#sel_TotalType").val() || 3;
                    //获取参数
                    data.typeId = $("#sel_TotalType").val() || 1;
                    if ($("#txtStartDate").val() != "" && $("#txtEndDate").val() != "" && $("#txtStartDate").val() != "undefined" && $("#txtEndDate").val() != "undefined") {
                        data.EnterType = "1";
                        data.BeginTime = $("#txtStartDate").val();
                        data.EndTime = $("#txtEndDate").val();
                    }
                    else {
                        data.EnterType = "0";
                        data.BeginTime = "";
                        data.EndTime = "";
                    }




                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function (data) { source.totalrecords = data.TotalRows; }
            };
            //数据处理
            var dataadapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            // prepare jqxChart settings
            var settings = {
                title: "用户投资分析",
                description: "",
                enableAnimations: true,
                showLegend: true,
                showBorderLine: true,
                legendLayout: { left: 700, top: 160, width: 300, height: 200, flow: 'vertical' },
                padding: { left: 5, top: 5, right: 5, bottom: 5 },
                titlePadding: { left: 0, top: 0, right: 0, bottom: 10 },
                source: dataadapter,
                colorScheme: 'scheme03',
                seriesGroups:
                [
                    {
                        type: 'pie',
                        showLabels: true,
                        series:
                        [
                            {
                                dataField: 'BidPercent',
                                displayText: 'TotalType',
                                labelRadius: 170,
                                initialAngle: 15,
                                radius: 145,
                                centerOffset: 0,
                                formatFunction: function (value) {
                                    if (isNaN(value))
                                        return value;
                                    return parseFloat(value) + '%';
                                },
                            }
                        ]
                    }
                ]
            };
            // setup the chart
            $('#chartContainer').jqxChart(settings);
        });
    </script>
    <script type="text/javascript">
        //选择查询
        function SearcheChart(start,end) {
            //给当前时间赋值 格式 2015-06-01
            //获取当前时间
           // alert(addDate(new Date(),-1));
            
            $("#txtEndDate").val(addDate(new Date(), start));
            $("#txtStartDate").val(addDate(new Date(), end));
           // 
          
        }

        function addDate(date, days) {
            var d = new Date(date);
            d.setDate(d.getDate() + days);
            var m = d.getMonth() + 1;
            if (m >= 10) {
                return d.getFullYear() + '-' + m + '-' + d.getDate();
            }
            else {
                return d.getFullYear() + '-0' + m + '-' + d.getDate();
            }
           
        }
        

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="selectDiv">
            <span class="fl" style="margin-left: 5px;">统计类型：</span>

            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>



            <div class="fl" style="cursor: pointer;">
                <select id="sel_TotalType" runat="server" name="TotalType">
                    <option value="1" selected="selected">年龄</option>
                    <option value="2">期限</option>
                    <option value="3">标种</option>
                    <option value="4">省份</option>
                    <option value="5">城市</option>
                    <option value="6">投标方式</option>
                    <option value="7">渠道来源</option>
                </select>
            </div>


            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_TotalType" />

            </div>


            <div class="fl" style="padding: 0 0 0 20px;">
                <a href="javascript:void(0)" onclick="SearcheChart(-1,-1)">昨天</a>
                <a href="javascript:void(0)" onclick="SearcheChart(0,-7)">7天内</a>
                <a href="javascript:void(0)" onclick="SearcheChart(0,-30)">30天内</a>

                开始时间：
                <input id="txtStartDate" type="text" onclick="WdatePicker()" />
                结束时间:
                <input id="txtEndDate" type="text" onclick="WdatePicker()" />
            </div>

            <div class="fl" style="width: 100px; padding: 0 0 0 20px;">
                <input type="button" id="btnSearch" value="查 询" class="inputButton" />
            </div>

            <div id='chartContainer' style="width: 850px; height: 500px;">
            </div>
        </div>
    </form>
</body>
</html>
