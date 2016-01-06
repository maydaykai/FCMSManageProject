<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanStudentInfo.aspx.cs" Inherits="WebUI.p2p.LoanStudentInfo" %>
<%@ Import Namespace="DocumentFormat.OpenXml.Drawing.Spreadsheet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
</head>
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
                    url: '../HanderAshx/p2p/LoanStudentHandler.ashx',
                    cache: false,
                    datatype: "json",
                    root: 'Rows',
                    datafields: [
                        { name: 'ID', type: 'int' },
                        { name: 'CreditLine', type: 'decimal' },
                        { name: 'CardNumber', type: 'string' },
                        { name: 'CreditNumber', type: 'string' },
                        { name: 'IdentityCard', type: 'string' },
                        { name: 'CreateTime', type: 'date' },
                        { name: 'UpdateTime', type: 'date' },
                        { name: 'OpUid', type: 'int' }
                    ],
                    pagesize: 20,
                    formatdata: function (data) {
                        data.pagenum = data.pagenum || 0;
                        data.pagesize = data.pagesize || 20;
                        data.sortdatafield = data.sortdatafield || '';
                        data.sortorder = data.sortorder || 'ASC';
                        data.memberName = $("#txtName").val() || "";
                        data.status = $("#selStatus").val() || "";
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
                
                var linkrenderer = function (row, column, value) {
                    var parm = "/p2p/LoanMemberInfo.aspx?ID=" + value + "&columnId=<%=ColumnId%>";
                                    var link = "<a href='" + parm + "'  target='_self' style='margin-left:5px;height:25px;line-height:25px;'>修改</a>";
                                    var html = link;
                                    return html;
                                };

                //数据绑定
                $("#jqxgrid").jqxGrid({
                    theme: theme,
                    source: dataadapter,
                    width: 910,
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
                        { dataField: 'ID', text: '<b>修改</b>', width: 40, cellsrenderer: linkrenderer, align: 'center', sortable: false },
                        { dataField: 'CreditLine', text: '<b>授信额度</b>', width: 80, cellsalign: 'center', align: 'center' },
                        { dataField: 'CardNumber', text: '<b>卡号</b>', width: 180, cellsalign: 'center', align: 'center' },
                        { dataField: 'CreditNumber', text: '<b>授信编号</b>', width: 80, cellsalign: 'center', align: 'center' },
                        { dataField: 'IdentityCard', text: '<b>身份证号码</b>', width: 180, cellsalign: 'center', align: 'center' },
                        { dataField: 'CreateTime', text: '<b>创建时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 150, cellsalign: 'center', align: 'center' },
                        { dataField: 'UpdateTime', text: '<b>更新时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 150, cellsalign: 'center', align: 'center' },
                        { dataField: 'OpUid', text: '<b>操作员</b>', width:50, cellsalign: 'center', align: 'center' }
                    ]
                });
            });
    </script>
<body>

   <div id="searchbar" class="selectDiv"  style="min-width:1000px;">
<%--        <span class="fl">会员名：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>--%>
     <%--   <input type="text" class="input_text fl"  maxlength="20" style="width: 150px;" id="txtName"  />--%>
           <span id="selectSpan" class="fl" style="line-height: 29px; margin-left: 3px;" runat="server">
            <a href="LoanMemberInfo.aspx?columnId=<%=ColumnId%>"> <input type="button" id="btnSaveDetailInfo" value="添加新用户" class="inputButton" width="200px;" runat="server" /></a>
          </span>
        <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid">
        </div>
    </div> 
     </div>
</body>
</html>
