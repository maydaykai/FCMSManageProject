<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberReferr.aspx.cs" Inherits="WebUI.MemberManage.MemberReferr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../css/global.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script src="/js/select2css.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.edit.js"></script>
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
        #searchbar span { height: 29px; text-indent: 5px; line-height: 29px; }
        .selectDiv .select_box { width: 80px; }
        .selectDiv2 .select_box { width: 105px; }
    </style>

        <script type="text/javascript">
            $(function () {
                $("#btnSearch").click(function () {
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
                    url: '/HanderAshx/MemberManage/MemberReferrHandler.ashx',
                    cache: false,
                    datatype: "json",
                    root: 'Rows',
                    datafields: [
                        { name: 'ID', type: 'int' },
                        { name: 'MemberName', type: 'string' },
                        { name:'RealName', type:'string'},
                        { name: 'Mobile', type: 'int' }
                    ],
                    pagesize: 20,
                    formatdata: function (data) {
                        data.pagenum = data.pagenum || 0;
                        data.pagesize = data.pagesize || 20;
                        data.sortdatafield = data.sortdatafield || 'P.ID';
                        data.sortorder = data.sortorder || 'DESC';
                        data.selRecMember = $("#selRecMember").val() || "";
                        data.txtRecMember = encodeURI(encodeURI($.trim($("#txtRecMember").val()))) || "";
                        formatedData = buildQueryString(data);
                        return formatedData;
                    },
                    sort: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                    beforeprocessing: function (data) { source.totalrecords = data.TotalRows; }
                };
                //数据处理
                var dataadapter = new $.jqx.dataAdapter(source, {
                    contentType: "application/json; charset=utf-8",
                    loadError: function(xhr, status, error) {
                        alert(error);
                    }
                });
                var linkrenderer = function (row, column, value) {
                    var parm = "/MemberManage/MemberReferral.aspx?ID=" + value + "&columnId=<%=ColumnId%>";
                                var link = "<a href='" + parm + "'  target='_self' style='margin-left:5px;height:25px;line-height:25px;'>修改</a>";
                                var html = link;
                                return html;
                            };
                //数据绑定
                $("#jqxgrid").jqxGrid({
                    theme: theme,
                    source: dataadapter,
                    width: 400,
                    sortable: false,
                    pageable: true,
                    autoheight: true,
                    virtualmode: true,
                    sorttogglestates: 1,
                    pagesizeoptions: ['10', '20', '30'],
                    rendergridrows: function (args) {
                        return args.data;
                    },
                    columns: [
                        { dataField: 'ID', text: '<b>操作</b>', width: 40, cellsrenderer: linkrenderer, align: 'center', sortable: false },
                        { dataField: 'MemberName', text: '会员名', width: 120, cellsalign: 'center', align: 'center' },
                        { dataField: 'RealName', text: '真实姓名', width: 120, cellsalign: 'center', align: 'center' },
                        { dataField: 'Mobile', text: '手机号码', width: 120, cellsalign: 'center', align: 'center' },
                    ],

                });
            });
    </script>
</head>
<body>
    <form id="form1" runat="server">
                <div id="searchbar" class="selectDiv" style="min-width: 900px;">

            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="selRecMember" id="selRecMember" class="fl" runat="server">
                <option value="">--未推荐人--</option>
                <option value="0">真实姓名</option>
                <option value="1">会员名</option>
                <option value="2">手机号码</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selRecMember" />
            </div>
            <div style="float: left;">
                <input id="txtRecMember" type="text" name="txtRecMember" value="" class="input_text fl" maxlength="20" style="width: 120px;" runat="server" />
                <div class="fl">
                    <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                </div>
            </div>
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="btnSearch" value="查 询" class="inputButton" />
            </div>
        </div>
        <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>
