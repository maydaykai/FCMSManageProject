<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppointmentBiddingUser.aspx.cs" Inherits="WebUI.p2p.AppointmentBiddingUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>预约投资用户管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
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
                url: '/HanderAshx/p2p/AppointmentBiddingUserHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'id', type: 'int' },
                    { name: 'mobile', type: 'string' },
                    { name: 'amount', type: 'number' },
                    { name: 'memberName', type: 'string' },
                    { name: 'status', type: 'int' },
                    { name: 'realName', type: 'string' },
                    { name: 'balance', type: 'number' },
                    { name: 'createTime', type: 'date' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.mobile = $("#txtMobile").val().trim() || "";
                    data.isExtend = $("#sel_isExtend").val() || "";
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
                var parm = "-";
                if (value === 0)
                    parm = "核实中";
                else if (value == 1)
                    parm = "核实通过";
                else if (value == 2)
                    parm = "已处理";
                else if (value == 3)
                    parm = "核实不通过";

                var link = "<span style='margin-left:50px;line-height:25px;'>" + parm + "</span>";
                return link;
            };

            var linkrenderer2 = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var parm = "AppointmentBiddingUserModify.aspx?id=" + data.id + "&columnId=" + <%=ColumnId %>;
                link = "<a style='color:rgb(22, 22, 221);margin-left:20px;;line-height:25px;' href='"+parm+"' target='_self'>"+(data.status==0?"审核":"查看")+"</a>";
                return link;
            };


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
                    { dataField: 'memberName', text: '<b>用户名</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'mobile', text: '<b>预约手机</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'realName', text: '<b>真实姓名</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'amount', text: '<b>预约金额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'balance', text: '<b>当前可用余额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'createTime', text: '<b>预约时间</b>', width: 250, cellsalign: 'center', cellsformat: "yyyy-MM-dd HH:mm:ss", align: 'center' },
                    { dataField: 'status', text: '<b>状态</b>', width: 150, cellsalign: 'center', align: 'center', cellsrenderer: linkrenderer },
                    { dataField: '', text: '<b>操作</b>', width: 70, cellsalign: 'center', align: 'center', cellsrenderer: linkrenderer2 }
                ]
            });
        });
    </script>
</head>
<body>
    <div id="searchbar" class="selectDiv">
        <span class="fl">手机号码：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input type="text" class="input_text fl"  maxlength="20" style="width: 150px;" id="txtMobile"  />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="sel_isExtend" id="sel_isExtend" class="fl">
            <option value="">--状态--</option>
            <option value="0">待确定</option>
            <option value="1">已确定</option>
            <option value="2">已处理</option>
            <option value="3">作废</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_isExtend" />
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
