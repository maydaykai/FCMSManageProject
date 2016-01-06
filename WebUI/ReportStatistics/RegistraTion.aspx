<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistraTion.aspx.cs" Inherits="WebUI.ReportStatistics.RegistraTion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/global.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script src="/js/select2css.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcheckbox.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxlistbox.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdropdownlist.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxmenu.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.pager.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.sort.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.filter.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.columnsresize.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.selection.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxpanel.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.aggregates.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.edit.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.export.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.export.js"></script>

    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />

    <style type="text/css">
        .selectDiv .select_box { width: 85px; }

        .selectDiv2 .select_box { width: 115px; }
    </style>

    <script type="text/javascript">

        //参数组装
        function buildQueryString(data) {
            var str = ''; for (var prop in data) {
                if (data.hasOwnProperty(prop)) {
                    str += prop + '=' + data[prop] + '&';
                }
            }
            return str.substr(0, str.length - 1);
        }

        function initChannel() {
            /*费用类型下拉框初始化*/
            //数据源
            var source = {
                url: '/HanderAshx/ReportStatistics/DimChannel.ashx',
                data: {"method" : "GetChannel"},
                datatype: "json",
                formatdata: function (data) {
                    data.sign = 1;
                    return buildQueryString(data);
                },
                datafields: [
                    { name: 'ChannelKey' },
                    { name: 'ChannelValue' }
                ],
                async: false
            };

            //数据处理
            var dataadapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            
            $("#jqxSelect").jqxDropDownList({ checkboxes: true, source: dataadapter, displayMember: "ChannelValue", valueMember: "ChannelKey", width: 200, height: 25 });
            $("#jqxSelect").jqxDropDownList('checkIndex', 0);
            // subscribe to the checkChange event.
            $("#jqxSelect").on('checkChange', function (event) {
                if (event.args) {
                    var item = event.args.item;
                    if (item) {
                        var items = $("#jqxSelect").jqxDropDownList('getCheckedItems');
                        if (items.length == 0) {
                            $("#jqxSelect").jqxDropDownList('checkIndex', 0);
                            $("#jqxSelectVal").val("-1");
                        } else {
                            var checkedItemsVal = "";
                            $.each(items, function(index) {
                                checkedItemsVal += this.value + ",";
                            });
                            $("#jqxSelectVal").val(checkedItemsVal);
                        }
                    }                    
                }
            });
        }

        $(function () {
            
            initChannel();

            $("#btnSearch").click(function () {
                $("#jqxgrid").jqxGrid('updatebounddata');                
                var dateType = $("#sel_DateType").val() || 1;
                var type=$("#sel_TotalType").val() || 1;
                if(parseInt(dateType) != 0&&type==1) {
                    $("#jqxgrid").jqxGrid('showcolumn','regtime');
                }
                else {
                    $("#jqxgrid").jqxGrid('hidecolumn','regtime');
                }

            });
            (function ($) {
                var FormatDateTime = function FormatDateTime() { };
                $.FormatDateTime = function (obj) {
                    var myDate = obj;
                    var year = myDate.getFullYear();
                    var month = ("0" + (myDate.getMonth() + 1)).slice(-2);
                    var day = ("0" + myDate.getDate()).slice(-2);
                    var h = ("0" + myDate.getHours()).slice(-2);
                    var m = ("0" + myDate.getMinutes()).slice(-2);
                    var s = ("0" + myDate.getSeconds()).slice(-2);
                    var mi = ("00" + myDate.getMilliseconds()).slice(-3);

                    return year + "-" + month + "-" + day;
                }
            })(jQuery);
            
            $("#txtDateStart").val($.FormatDateTime(new Date()));
            $("#txtDateEnd").val($.FormatDateTime(new Date()));
            
            var btnoutput = <%=_btnOutPut %>;

            if (btnoutput == 0) {
                $("#btnOutput").hide();
            }

            //主题
            var theme = "arctic";

            ////参数组装
            //function buildQueryString(data) {
            //    var str = ''; for (var prop in data) {
            //        if (data.hasOwnProperty(prop)) {
            //            str += prop + '=' + data[prop] + '&';
            //        }
            //    }
            //    return str.substr(0, str.length - 1);
            //}

            var formatedData = '';

            //数据源
            var source = {
                url: '/HanderAshx/ReportStatistics/RegistrationHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    //{ name:'TotalDate',type:'date'},
                    //{ name:'UserNum',type:'int'},
                    //{ name:'BidNumSina',type:'int'},
                    //{ name:'BidAmountSina' , type:'number'},
                    //{ name:'UserNum360',type:'int'},
                    //{ name:'BidNum360',type:'int'},
                    //{ name:'BidAmount360' , type:'number'},
                    //{ name:'UserNumQQ' , type:'int'},
                    //{ name:'BidNumQQ',type:'int'},
                    //{ name:'BidAmountQQ' , type:'number'},
                    //{ name:'UserNumSohu' , type:'int'},
                    //{ name:'BidNumSohu',type:'int'},
                    //{ name:'BidAmountSohu' , type:'number'},
                    //{ name:'UserNumHexun' , type:'int'},
                    //{ name:'BidNumHexun',type:'int'},
                    //{ name:'BidAmountHexun' , type:'number'},
                    //{ name:'UserNumFuyi' , type:'int'},
                    //{ name:'BidNumFuyi',type:'int'},
                    //{ name:'BidAmountFuyi' , type:'number'},
                    //{ name:'OtherUsers',type:'int'},
                    //{ name:'BidAmountOther' , type:'number'},
                    //{ name:'Numb' , type:'int'},
                    //{ name:'RegNum' , type:'int'}
                    { name:'Channel',type:'string'},
                    { name:'regcount',type:'int'},
                    { name:'bidcount',type:'int'},
                    { name:'bidamount' , type:'number'},
                    { name:'paycount' , type:'number'},
                    { name:'payamount' , type:'number'},
                    { name:'cashcount' , type:'number'},
                    { name:'cashamount' , type:'number'},
                    { name:'collectamount' , type:'number'},
                    { name:'regtime' , type:'string'}
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.dateStart = $("#txtDateStart").val() || "";
                    data.dateEnd = $("#txtDateEnd").val() || "";
                    data.typeId = $("#sel_TotalType").val() || 1;
                    data.ChannelType = $("#jqxSelectVal").val() || "";
                    data.dateType = $("#sel_DateType").val()||1;
                    if(data.typeId==2){
                        data.dateType=1;
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

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1200,
                sortable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                showaggregates: true,
                showstatusbar: true,
                statusbarheight: 50,
                editable: true,
                selectionmode: 'singlecell',
                rendergridrows: function (args) {
                    return args.data;
                },
                //columnsresize: true,
                columns: [
                       //{ dataField: 'TotalDate', text: '<b>日期</b>', width: 100, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd" },
                       //{ dataField: 'UserNum', text: '<b>新浪注册数</b>', width: 80, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'BidNumSina', text: '<b>新浪投资人数</b>', width: 100, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'BidAmountSina', text: '<b>新浪投资额</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'UserNum360', text: '<b>360注册数</b>', width: 80, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'BidNum360', text: '<b>360投资人数</b>', width: 100, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'BidAmount360', text: '<b>360投资额</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'UserNumQQ', text: '<b>腾讯注册数</b>', width: 80, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'BidNumQQ', text: '<b>腾讯投资人数</b>', width: 100, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'BidAmountQQ', text: '<b>腾讯投资额</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'UserNumSohu', text: '<b>搜狐注册数</b>', width: 80, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'BidNumSohu', text: '<b>搜狐投资人数</b>', width: 100, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'BidAmountSohu', text: '<b>搜狐投资额</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'UserNumHexun', text: '<b>和讯注册数</b>', width: 80, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'BidNumHexun', text: '<b>和讯投资人数</b>', width: 100, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'BidAmountHexun', text: '<b>和讯投资额</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'UserNumFuyi', text: '<b>扶翼注册数</b>', width: 80, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'BidNumFuyi', text: '<b>扶翼投资人数</b>', width: 100, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'BidAmountFuyi', text: '<b>扶翼投资额</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'OtherUsers', text: '<b>其他渠道注册数</b>', width: 150, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'BidAmountOther', text: '<b>其他渠道投资额</b>', width: 150, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'Numb', text: '<b>实际投资人数</b>', width: 100, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                       //{ dataField: 'RegNum', text: '<b>当日注册人数</b>', width: 100, cellsalign: 'center', align: 'center', aggregates: ['sum'] }
                    { dataField: 'Channel', text: '<b>渠道</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'regtime', datafield: 'userRegTime', text: '<b>时间</b>', width: 210, cellsalign: 'center', align: 'center',hidden: true },
                    { dataField: 'regcount', text: '<b>注册数</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'bidcount', text: '<b>投资人数</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'bidamount', text: '<b>投资额</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'paycount', text: '<b>充值人数</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'payamount', text: '<b>充值金额</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'cashcount', text: '<b>提现人数</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'cashamount', text: '<b>提现金额</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'collectamount', text: '<b>待收金额</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] }
                ]
            });
            $("#btnOutput").jqxButton({ theme: theme });
            $("#btnOutput").click(function () {
                $("#jqxgrid").jqxGrid('exportdata', 'xls', 'jqxGrid');
            });
        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1250px;">

            <span class="fl" style="margin-top: 5px; margin-left: 5px;">查询日期从：</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtDateStart" class="input_text" type="text" onclick="WdatePicker()" style="width: 100px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <span class="fl" style="margin-top: 8px; margin-left: 5px; margin-right: 5px;">至</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtDateEnd" class="input_text" type="text" onclick="WdatePicker()" style="width: 100px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <span class="fl" style="margin-top: 5px; margin-left: 5px;">统计类型：</span>

            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <div class="fl">
                <select id="sel_TotalType" runat="server" style="width: 100px" name="TotalType">
                    <option value="1" selected="selected">按注册日期</option>
                    <option value="2">按投资日期</option>
                </select>
            </div>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_TotalType" />
            </div>
            <%-----------------------%>
            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <div class="fl">
                <select id="sel_DateType" runat="server" style="width: 50px;" name="DateType">
                    <option value="0" selected="selected">全部</option>
                    <option value="1">天</option>
                    <option value="4">周</option>
                    <option value="2">月</option>
                    <option value="3">年</option>
                </select>
                <div class="fl" style="margin-left: -5px; cursor: pointer;">
                    <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_DateType" />
                </div>
            </div>
            <%-----------------------%>
            <div style='float: left; margin-left: 5px;' id='jqxSelect'>
            </div>
            <input type="hidden" id='jqxSelectVal' runat="server" />
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="btnSearch" value="查 询" class="inputButton" />
            </div>
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="btnOutput" value="导 出" class="inputButton" />
            </div>
        </div>
        <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>
