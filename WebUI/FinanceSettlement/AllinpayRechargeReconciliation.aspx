<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllinpayRechargeReconciliation.aspx.cs" Inherits="WebUI.FinanceSettlement.AllinpayRechargeReconciliation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>通联充值对账</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxscrollbar.js"></script>
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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.edit.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.export.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.export.js"></script>

    <script src="/js/My97DatePicker/WdatePicker.js"></script>
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/icon.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
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
                url: '/HanderAshx/FinanceSettlement/AllinpayRechargeRecoHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'MemberName', type: 'string' },
                    { name: 'AllinpaySerialNumber', type: 'string' },
                    { name: 'MerchantOrderNumber', type: 'string' },
                    { name: 'PaymentStatus', type: 'string' },
                    { name: 'AllinPayStatus', type: 'string' },
                    { name: 'RjbStatus', type: 'string' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.SettlementDate = encodeURI($("#SettlementDate").val()) || "";
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
            var payStatus = function (row, column, value) {
                var link;
                link = value == "支付失败" ? "<span style='color:#ff0000;padding-left:40px;height:25px;line-height:25px;'>支付失败</span>" : "<span style='padding-left:40px;height:25px;line-height:25px;'>" + value + "</span>";
                return link;
            };
            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 880,
                sortable: false,
                selectionmode: 'singlecell',
                pageable: false,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                rendergridrows: function (args) {
                    return args.data;
                },
                showstatusbar: true,
                showtoolbar: true,
                rendertoolbar: function (toolbar) {
                    var me = this;
                    var container = $("<div style='margin: 5px;'></div>");
                    var span = $("<span style='float: left; margin-top: 5px; margin-right: 4px;'>结算日期: </span>");
                    var input = $("<input class='jqx-input jqx-widget-content jqx-rc-all' id='SettlementDate' onclick='WdatePicker()' type='text' style='height: 23px; float: left; width: 180px;' />");
                    var exButton = $("<div style='float: right; margin-right: 5px;margin-top: -3px; cursor:pointer;'><img id='excelExport' style='cursor:pointer;position: relative; margin-top: 2px;' title='导出EXCEL' src='/js/jqwidgets-ver3.1.0/images/ex_excel.png'/></div>");
                    toolbar.append(container);
                    container.append(span);
                    container.append(input);
                    container.append(exButton);
                    if (theme != "") {
                        input.addClass('jqx-widget-content-' + theme);
                        input.addClass('jqx-rc-all-' + theme);
                    }
                    var oldVal = "";
                    input.on('blur', function (event) {
                        if (input.val().length > 0) {
                            if (me.timer) {
                                clearTimeout(me.timer);
                            }
                            if (oldVal != input.val()) {
                                me.timer = setTimeout(function () {
                                    $("#jqxgrid").jqxGrid('updatebounddata');
                                }, 500);
                                oldVal = input.val();
                            }
                        }
                    });
                },
                columns: [
                        { text: '会员名', dataField: 'MemberName', width: 120, cellsalign: 'center', align: 'center' },
                        { text: '通联订单号', dataField: 'AllinpaySerialNumber', width: 200, cellsalign: 'center', align: 'center' },
                        { text: '商户订单号', dataField: 'MerchantOrderNumber', width: 200, cellsalign: 'center', align: 'center' },
                        { text: '通联标识', dataField: 'PaymentStatus', width: 120, cellsalign: 'center', align: 'center' },
                        { text: '通联支付状态', dataField: 'AllinPayStatus', width: 120, cellsalign: 'center', align: 'center', cellsrenderer: payStatus },
                        { text: '融金宝状态', dataField: 'RjbStatus', width: 120, cellsalign: 'center', align: 'center', cellsrenderer: payStatus }
                ]
            });

            $("#excelExport").jqxButton();
            $("#excelExport").click(function () {
                $("#jqxgrid").jqxGrid('exportdata', 'xls', 'jqxGrid');
            });

        });
    </script>
</head>
<body>
    <div style="color: #ff0000; height: 35px; line-height: 35px;">
        温馨提示：<b style="color: #0000ff;">ZF（</b>支付成功<b style="color: #0000ff;">）  TH（</b>退款成功，资金已经退款至持卡人账户<b style="color: #0000ff;">）  CX（</b>冲销成功，未能成功退款，资金已经退回给商户<b style="color: #0000ff;">）</b>
    </div>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left;">
        <div id="jqxgrid">
        </div>
    </div>
</body>
</html>
