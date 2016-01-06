<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanSwitchManage.aspx.cs" Inherits="WebUI.p2p.LoanSwitchManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>借款管理</title>
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <link href="/css/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.js"></script>
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
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <script src="../js/loan/init.js"></script>
    <link href="../css/select.css" rel="stylesheet" />
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <link href="../css/global.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

            $('#btnSearch').bind("click", function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
            });

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
                url: '/HanderAshx/p2p/LoanSwitch.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'RealName', type: 'string' },
                    { name: 'LoanNumber', type: 'string' },
                    { name: 'LoanAmount', type: 'number' },
                    { name: 'LoanTermInfo', type: 'string' },
                    { name: 'CreateTime', type: 'date' },
                    { name: 'EndRePayTime', type: 'date' },
                    { name: 'SwitchAutoRepayment', type: 'bool' },
                    { name: 'SwitchBuildOverdueFee', type: 'bool' },
                    { name: 'SwitchAutoPass', type: 'bool' }

                ],

                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'LoanNumber';
                    data.sortorder = data.sortorder || 'desc';
                    data.loanNumber = $("#txtNumber").val() || "";

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
                width: 1220,
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
                        { text: '<b>借款人</b>', dataField: 'MemberName', width: 80, cellsalign: 'center', align: 'center' },
                        { text: '<b>真实姓名</b>', dataField: 'RealName', width: 80, cellsalign: 'center', align: 'center' },
                        { text: '<b>借款编号</b>', dataField: 'LoanNumber', width: 150, cellsalign: 'center', align: 'center' },
                        { text: '<b>贷款额度(元)</b>', dataField: 'LoanAmount', cellsformat: "F2", width: 120, cellsalign: 'right', align: 'center' },
                        { text: '<b>借款期限</b>', dataField: 'LoanTermInfo', width: 70, cellsalign: 'center', align: 'center' },
                        { text: '<b>申请时间</b>', dataField: 'CreateTime', width: 180, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                        { text: '<b>到期日</b>', dataField: 'EndRePayTime', width: 180, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                        {
                            text: '<b>自动还款开关</b>', dataField: 'SwitchAutoRepayment', width: 120, cellsalign: 'center', align: 'center', cellsrenderer: function (row, columnfield, value) {
                                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                                if (value) {
                                    return '<div style="text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;">已开启 <a style="color:red;" href="javascript:;" onclick="turnSwitch(1,' + data.ID + ',0,' + row + ')">关闭</a></div>';
                                }
                                else {
                                    return '<div style="text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;">已关闭 <a style="color:green;" href="javascript:;" onclick="turnSwitch(1,' + data.ID + ',1,' + row + ')">开启</a></div>';
                                }
                            }
                        },
                        {
                            text: '<b>生成逾期管理费开关</b>', dataField: 'SwitchBuildOverdueFee', width: 120, cellsalign: 'center', align: 'center', cellsrenderer: function (row, columnfield, value) {
                                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                                if (value) {
                                    return '<div style="text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;">已开启 <a style="color:red;" href="javascript:;" onclick="turnSwitch(2,' + data.ID + ',0)">关闭</a></div>';
                                }
                                else {
                                    return '<div style="text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;">已关闭 <a style="color:green;" href="javascript:;" onclick="turnSwitch(2,' + data.ID + ',1)">开启</a></div>';
                                }
                            }
                        },
                        {
                            text: '<b>自动流标开关</b>', dataField: 'SwitchAutoPass', width: 120, cellsalign: 'center', align: 'center', cellsrenderer: function (row, columnfield, value) {
                                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                                if (value) {
                                    return '<div style="text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;">已开启 <a style="color:red;" href="javascript:;" onclick="turnSwitch(2,' + data.ID + ',0)">关闭</a></div>';
                                }
                                else {
                                    return '<div style="text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;">已关闭 <a style="color:green;" href="javascript:;" onclick="turnSwitch(2,' + data.ID + ',1)">开启</a></div>';
                                }
                            }
                        }
                ]
            });
        });
        function turnSwitch(type, id, status, row) {
            var ajaxdata = { "type": type, "id": id, "status": status };
            $.ajax({
                type: "POST",
                url: '/HanderAshx/p2p/TurnSwitch.ashx',
                data: ajaxdata,
                beforeSend: function () {

                },
                success: function (result) {
                    var jsondata = parseInt(result);
                    if (jsondata > 0) {
                        alert('修改成功');
                        window.location.href = window.location.href;
                    }
                    else {
                        alert('修改失败');
                    }
                },
                error: function () { alert("error"); }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="fl" style="width: 100%">
            <span class="fl">借款编号：</span>
            <div style="width: 4px; float: left;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input type="text" id="txtNumber" name="input" class="fl input_text" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <input type="button" id="btnSearch" value="查询" class="inputButton" />
        </div>
        <div id='jqxWidget' style="font-size: 13px; font-family: Verdana;">
            <div id="jqxgrid">
            </div>
        </div>

    </form>
    <script src="../js/select2css.js"></script>
</body>
</html>
