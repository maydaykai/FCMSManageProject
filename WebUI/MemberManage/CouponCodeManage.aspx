<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CouponCodeManage.aspx.cs" Inherits="WebUI.MemberManage.CouponCodeManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/global.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script src="/js/select2css.js"></script>
    <link href="../css/table1.css" rel="stylesheet" type="text/css" />
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
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <style type="text/css">
        #searchbar span {
            height: 29px;
            text-indent: 5px;
            line-height: 29px;
        }

        .selectDiv .select_box {
            width: 85px;
        }

        .selectDiv2 .select_box {
            width: 105px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#applyfilter").click(function () {
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
                url: '/HanderAshx/MemberManage/CouponCodeHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'SerialNo', type: 'string' },
                    { name: 'CouponCode', type: 'string' },
                    { name: 'Amount', type: 'number' },
                    { name: 'LockAmount', type: 'number' },
                    { name: 'BeginTime', type: 'date' },
                    { name: 'EndTime', type: 'date' },
                    { name: 'MemberName', type: 'string' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'C.ID';
                    data.sortorder = data.sortorder || 'DESC';
                    data.uName = encodeURI($("#txtName").val()) || "";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function (column, direction) { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function (data) { source.totalrecords = data.TotalRows; }
            }

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
                width: 900,
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
                    { dataField: 'SerialNo', text: '<b>序列号</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'CouponCode', text: '<b>密码</b>', width: 120, cellsalign: 'center', align: 'center', sortable: false },
                    { dataField: 'Amount', text: '<b>面值(元)</b>', width: 110, cellsformat: "F2", cellsalign: 'center', align: 'center' },
                    { dataField: 'LockAmount', text: '<b>利息(元)</b>', width: 110, cellsformat: "F2", cellsalign: 'center', align: 'center' },
                    { dataField: 'BeginTime', text: '<b>生效时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'EndTime', text: '<b>失效时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'MemberName', text: '<b>绑定用户</b>', width: 100, cellsalign: 'center', align: 'center' }
                ]
            });
        });
    </script>
</head>
<body>
    <div class="top boder1px">
        <div class="query ">
            <span style="margin-left: 20px; height: 32px; line-height: 32px; float: left;">生成总数：</span>
            <span id="couponAmount" style="color: #ff0000; height: 32px; line-height: 32px; float: left;"></span>
            <span style="margin-left: 20px; height: 32px; line-height: 32px; float: left;">当前可用数量：</span>
            <span id="surCouponAmount" style="color: #ff0000; height: 32px; line-height: 32px; float: left;"></span>
            <span style="margin-left: 5px; height: 32px; line-height: 32px; float: left;">
                <input id="btnRefresh" type="button" value="刷新" class="inputButton" style="width: 40px;" /></span>
            <br />
            <div id="generateCoupon">
                <div class="top_left boder1px fl">
                    <h2>生成规则：</h2>
                    <div class="option_box">
                        <span class="o_list">
                            生成数量：
                            <input type="text" id="Text1" name="condition" value="" class="o_input" runat="server" /></span>
                    </div>
                    <div class="option_box">
                        <span class="o_list">
                            面值：
                            <input type="text" id="Text2" name="condition" value="" class="o_input" runat="server" />元</span>
                        <span class="o_list">
                            利息：
                            <input type="text" id="Text3" name="condition" value="" class="o_input" runat="server" />元</span>
                    </div>
                    <div class="option_box">
                        <span class="o_list">
                            生效日期：<input id="txtStartDate" type="text" class="o_input" onclick="WdatePicker()" style="width: 100px" />
                        </span>
                        <span class="o_list">
                            失效日期：<input id="Text4" type="text" class="o_input" onclick="WdatePicker()" style="width: 100px" />
                        </span>
                    </div>
                </div>
                <div class="top_right fr">
                    <textarea class="text" rows="10" cols="106" id="textReviewComments" style="width: 400px" runat="server"></textarea>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <div id="searchbar" class="selectDiv" style="min-width: 1000px; margin: 20px 0 60px 0;">
        <span class="fl">会员名：</span>
        <div style="width: 4px; float: left;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <div style="float: left;">
            <input id="txtName" type="text" name="txtName" value="" class="input_text fl" maxlength="20" style="width: 150px;" runat="server" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
        </div>
        <div style="float: left; margin-left: 5px;">
            <input type="button" id="applyfilter" value="查 询" class="inputButton" />
        </div>
    </div>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid">
        </div>
    </div>
</body>
</html>
