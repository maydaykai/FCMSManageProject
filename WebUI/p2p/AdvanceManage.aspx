<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvanceManage.aspx.cs" Inherits="WebUI.p2p.AdvanceManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>垫付管理</title>
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
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <style type="text/css">
        #searchbar span {
            height:29px;text-indent: 5px;line-height: 29px;
        }
        .selectDiv .select_box {
        width: 85px;
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
                url: '/HanderAshx/p2p/AdvanceManage.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'LoanID', type: 'int' },
                    { name: 'LoanNumber', type: 'string' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'PeNumber', type: 'int' },
                    { name: 'RePrincipal', type: 'number' },
                    { name: 'ReInterest', type: 'number' },
                    { name: 'ReOverInterest', type: 'number' },
                    { name: 'SurPrincipal', type: 'number' },
                    { name: 'SurReInterest', type: 'number' },
                    { name: 'SurOverInterest', type: 'number' },
                    { name: 'IsExtendStr', type: 'string' },
                    { name: 'StatusStr', type: 'string' },
                    { name: 'RePayTime', type: 'date' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'LoanID';
                    data.sortorder = data.sortorder || 'DESC';
                    data.memberName = $("#txtName").val() || "";
                    //data.isExtend = $("#sel_isExtend").val() || "";
                    //data.status = $("#sel_status").val() || "";
                    data.filter = "";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
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
                    { dataField: 'LoanNumber', text: '<b>标号</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'MemberName', text: '<b>会员名</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'PeNumber', text: '<b>期数</b>', width: 50, cellsalign: 'center', align: 'center' },
                    { dataField: 'RePrincipal', columngroup: 'ReMode', text: '<b>应还本金(元)</b>', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'ReInterest', columngroup: 'ReMode', text: '<b>应还利息(元)</b>', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'ReOverInterest', columngroup: 'ReMode', text: '<b>应还逾期利息(元)</b>', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SurPrincipal', columngroup: 'SurMode', text: '<b>未还本金(元)</b>', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SurReInterest', columngroup: 'SurMode', text: '<b>未还利息(元)</b>', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SurOverInterest', columngroup: 'SurMode', text: '<b>未还逾期利息(元)</b>', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'IsExtendStr', text: '<b>是否展期</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'StatusStr', text: '<b>还款状态</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'RePayTime', text: '<b>应还款时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' }
                ],
                columngroups:
                [
                    { text: '<b>应还金额</b>', align: 'center', name: 'ReMode' },
                    { text: '<b>未还金额</b>', align: 'center', name: 'SurMode' }
                ]
            });
        });
    </script>
</head>
<body>
    <div id="searchbar" class="selectDiv">
        <span class="fl">用户名：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input type="text" class="input_text fl"  maxlength="20" style="width: 150px;" id="txtName"  />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <%--<div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="sel_isExtend" id="sel_isExtend" class="fl">
            <option value="">--是否展期--</option>
            <option value="0">未展期</option>
            <option value="1">已展期</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_isExtend" />
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="sel_status" id="sel_status" class="fl">
            <option value="">--还款状态--</option>
            <option value="0">未还</option>
            <option value="1">部分已还</option>
            <option value="2">全额已还</option>
            <option value="3">作废</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_status" />
        </div>--%>
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