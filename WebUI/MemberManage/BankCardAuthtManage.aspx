<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankCardAuthtManage.aspx.cs" Inherits="WebUI.MemberManage.BankCardAuthtManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>
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
                url: '/HanderAshx/MemberManage/BankCardAuthtHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'BankName', type: 'string' },
                    { name: 'Account', type: 'string' },
                    { name: 'BankCardNo', type: 'string' },
                    { name: 'Amount', type: 'number' },
                    { name: 'VerTimes', type: 'number' },
                    { name: 'SignString', type: 'string' },
                    { name: 'PayTypeString', type: 'string' },
                    { name: 'AuthentResult', type: 'int' },
                    { name: 'AuthentStr', type: 'string' },
                    { name: 'PayStatusString', type: 'string' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'CreateTime', type: 'date' },
                    { name: 'UpdateTime', type: 'date' },
                    { name: 'REQ_SN', type: 'string' },
                    { name: 'Remark', type: 'string' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'P.UpdateTime';
                    data.sortorder = data.sortorder || 'desc';
                    data.memberName = encodeURI($("#txtName").val()) || "";
                    data.sign = $("#selSign").val() || "";
                    data.payType = $("#selPayType").val() || "";
                    data.payStatus = $("#selPayStatus").val() || "";
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
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var parm = "../MemberManage/BankCardAuthtQuery.aspx?REQ_SN=" + data.REQ_SN + "&columnId=<%=ColumnId %>";
                var link = "<a style='text-align:center;margin-left:30px;' href='javascript:void(0)' onclick=\"MessageWindow(270,100,'" + parm + "')\"; target='_self'>查看</a>";                    
                 if (data.AuthentResult == 0 && data.VerTimes == 3) {
                        link += "<a style='text-align:center;margin-left:30px;' href='javascript:void(0)' onclick=\"Unlock(" + data.ID + ");\"; target='_self'>解锁</a>";
                 }
                 if (data.AuthentResult == -1) {
                     link += "<a style='text-align:center;margin-left:30px;' href='javascript:void(0)' onclick=\"Reset(" + data.ID + ");\"; target='_self'>重置</a>";
                 }
                return link;
            };

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
                    { dataField: 'ID', text: '<b>操作</b>', width: 130, cellsalign: 'center', align: 'center', cellsrenderer: linkrenderer, sortable: false },
                    { dataField: 'REQ_SN', text: '<b>流水号</b>', width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'MemberName', text: '<b>会员名</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'BankName', text: '<b>银行名称</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'Account', text: '<b>开户人</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'BankCardNo', text: '<b>银行卡号</b>', width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'Amount', text: '<b>认证金额（元）</b>', width: 130, cellsalign: 'center', cellsformat: "F2", align: 'center' },
                    { dataField: 'AuthentStr', text: '<b>认证状态</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'VerTimes', text: '<b>认证次数</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'SignString', text: '<b>是否汇款</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'PayTypeString', text: '<b>汇款方式</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'PayStatusString', text: '<b>汇款状态</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'CreateTime', text: '<b>申请时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'UpdateTime', text: '<b>更新时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'Remark', text: '<b>失败原因</b>', width: 180, cellsalign: 'center', align: 'center' }
                ]
            });
        });
        function Unlock(ID) {
            $.dialog.confirm('确定解锁吗？', function () {
                $.ajax({
                    type: "GET",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    url: "/Handerashx/MemberManage/BankCardAuthUnlockHandler.ashx?ID=" + ID,
                    dataType: "json",
                    cache: false,
                    success: function (context) {
                        if (context == "1") {
                            MessageAlert('解锁成功', 'success', window.location.href);
                        } else {
                            MessageAlert('解锁失败', 'error', '');
                        }
                    },
                    error: function (xmlHttpRequest) {
                        alert(xmlHttpRequest.innerText);
                    },
                    complete: function (x) {
                    }
                });
            }, function () {
            });
        }
        function Reset(ID) {
            $.dialog.confirm('确定重置认证状态吗？', function () {
                $.ajax({
                    type: "GET",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    url: "/Handerashx/MemberManage/BankCardAuthResetHandler.ashx?ID=" + ID,
                    dataType: "json",
                    cache: false,
                    success: function (context) {
                        if (context == "1") {
                            MessageAlert('重置成功', 'success', window.location.href);
                        } else {
                            MessageAlert('重置失败', 'error', '');
                        }
                    },
                    error: function (xmlHttpRequest) {
                        alert(xmlHttpRequest.innerText);
                    },
                    complete: function (x) {
                    }
                });
            }, function () {
            });
        }
    </script>
</head>
<body>
    <div id="searchbar" class="selectDiv" style="min-width:1000px;">
        <span class="fl">会员名：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input type="text" class="input_text fl"  maxlength="20" style="width: 150px;" id="txtName"  />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="selSign" id="selSign" class="fl">
            <option value="">--是否汇款--</option>
            <option value="0">未汇款</option>
            <option value="1">已汇款</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selSign" />
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="selPayType" id="selPayType" class="fl">
            <option value="">--汇款方式--</option>
            <option value="0">线下</option>
            <option value="1">线上</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selPayType" />
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="selPayStatus" id="selPayStatus" class="fl" runat="server">
            <option value="">--汇款状态--</option>
            <option value="-1">汇款失败</option>
            <option value="0">汇款中</option>
            <option value="1">汇款成功</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selPayStatus" />
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
