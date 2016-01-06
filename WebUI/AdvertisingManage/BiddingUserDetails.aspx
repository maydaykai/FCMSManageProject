<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BiddingUserDetails.aspx.cs" Inherits="WebUI.AdvertisingManage.BiddingUserDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>投资用户明细</title>
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
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <script src="../js/FormatDecimal.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
         
    <script type="text/javascript">
        $(function () {
            $("#applyfilter").click(function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
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

            $("#txtStartDate").val($.FormatDateTime(new Date()));
            $("#txtEndDate").val($.FormatDateTime(new Date()));

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

            //获取参数值
            function param(name, value) {
                this.name = name;
                this.value = value;
            }

            var url = window.location.href;
            var p = url.split("?");
            var all = new Array();
            var params = p.length > 1 ? p[1].split("&") : new Array();
            for (var i = 0; i < params.length; i++) {
                var pname = params[i].split("=")[0];
                var pvalue = params[i].split("=")[1];
                all[i] = new param(pname, pvalue);
            }

            function getPValueByName(name) {
                for (var i = 0; i < all.length; i++) {
                    if (all[i].name == name)
                        return all[i].value;
                }
            }

            //数据源
            var source = {
                url: '/HanderAshx/AdvertisingManage/BiddingUserDetailsHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'CreateTime', type: 'date' },
                    { name: 'CreateTime2', type: 'date' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'Channel', type: 'string' },
                    { name: 'BidAmount', type: 'number' },
                    { name: 'BidType', type: 'string' },
                    { name: 'payGap', type: 'int' },
                    { name: 'RechargeChannel', type: 'string' },
                    { name: 'investGap', type: 'int' },
                    { name: 'RegSource', type: 'string' },
                    { name: 'VisitPreUrl', type: 'string' },
                    { name: 'VisitUrl', type: 'string' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.Secondary = $("#txtSecondary").val() || "";
                    data.startDate = $("#txtStartDate").val() || "";
                    data.endDate = $("#txtEndDate").val() || "";
                    data.name = $("#txtName").val() || "";
                    data.regSource = $("#sel_RegSource").val() || "";
                    data.investTerminal = $("#sel_InvestTerminal").val() || "";
                    data.payTerminal = $("#sel_PayTerminal").val() || "";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function (data) {
                    source.totalrecords = data.TotalRows;
                }
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
                width: 1100,
                sortable: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'CreateTime', text: '<b>首投日期</b>', cellsformat: "yyyy/MM/dd", width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'CreateTime2', text: '<b>时间</b>', cellsformat: "hh:mm:ss", width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'MemberName', text: '<b>用户名</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'Channel', text: '<b>渠道</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: '', text: '<b>二级分类</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'a', text: '<b>三级分类</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'BidAmount', text: '<b>投资金额</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'payGap', text: '<b>首次充值间隔(天)</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'investGap', text: '<b>首次投资时隔(天)</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'RechargeChannel', text: '<b>充值终端</b>', width: 200, cellsalign: 'center', align: 'center' },
                    { dataField: 'BidType', text: '<b>投资终端</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'RegSource', text: '<b>注册终端</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'VisitPreUrl', text: '<b>访问来源URL</b>', width: 200, cellsalign: 'center', align: 'center' },
                    { dataField: 'VisitUrl', text: '<b>受访页面URL</b>', width: 200, cellsalign: 'center', align: 'center' }
                ]
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1000px;">
            <span class="fl">用户名：</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtName" class="input_text" type="text" runat="server" style="width: 150px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <span class="fl">注册时间：</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtStartDate" class="input_text" type="text" runat="server" onclick="WdatePicker({ maxDate: '#F{$dp.$D(\'txtEndDate\')}', dateFmt: 'yyyy-MM-dd' })" style="width: 150px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <span class="fl" style="margin-left: 5px; margin-right: 5px;">～</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtEndDate" class="input_text" type="text" runat="server" onclick="WdatePicker({ minDate: '#F{$dp.$D(\'txtStartDate\')}', dateFmt: 'yyyy-MM-dd' })" style="width: 150px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            
            <span class="fl">分类编号：</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtSecondary" class="input_text" type="text" runat="server" style="width: 150px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>         
        </div>

        <div id="Div1" class="selectDiv" style="min-width: 1000px; float: left; margin-top: 20px; margin-left: 6px;">                   
            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="sel_RegSource" id="sel_RegSource" runat="server" class="fl">
                <option value="">--注册终端--</option>
                <option value="0">PC</option>
                <option value="1">WAP</option>
                <option value="2">IOS</option>
                <option value="3">Android</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_RegSource" />
            </div>

            
            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="sel_InvestTerminal" id="sel_InvestTerminal" runat="server" class="fl">
                <option value="">--投资终端--</option>
                <option value="0">WEB</option>
                <option value="1">自动竞标</option>
                <option value="2">IOS</option>
                <option value="3">Android</option>
                <option value="4">WAP</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_InvestTerminal" />
            </div>

            
            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="sel_PayTerminal" id="sel_PayTerminal" runat="server" class="fl">
                <option value="">--充值终端--</option>
                <option value="0">线下充值</option>
                <option value="1">通联支付</option>
                <option value="2">通联移动支付（IOS）</option>
                <option value="3">通联移动支付（Android）</option>
                <option value="4">通联WAP支付</option>
                <option value="5">连连支付</option>
                <option value="6">连连移动支付（IOS）</option>
                <option value="7">连连移动支付（Android）</option>
                <option value="8">连连WAP支付</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_PayTerminal" />
            </div>

            <div style="float: left; margin-left: 5px;">
                <input type="button" id="applyfilter" value="查 询" class="inputButton" />
                <input id="ExcelExport1" type="button" value="导出Excel" class="inputButton" style="width: 100px;" runat="server" onserverclick="ExcelExport1_Click" />
            </div>
        </div>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid">
        </div>
    </div>
    </form>
</body>
</html>
