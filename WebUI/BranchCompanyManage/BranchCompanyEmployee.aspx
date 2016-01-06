<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BranchCompanyEmployee.aspx.cs" Inherits="WebUI.BranchCompanyManage.BranchCompanyEmployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/global.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
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
    <script src="/js/FormatDecimal.js"></script>
    <script src="../js/loan/init.js"></script>
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <style type="text/css">
        #searchbar span { height: 29px; text-indent: 5px; line-height: 29px; }
        .selectDiv .select_box { width: 85px; }
        .selectDiv2 .select_box { width: 105px; }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#applyfilter").click(function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
            });
            getBranchCompanyList("selBranchCompany");
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
                url: '/HanderAshx/MemberManage/MemberManageHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'RealName', type: 'string' },
                    { name: 'Mobile', type: 'string' },
                    { name: 'Email', type: 'string' },
                    { name: 'IsLocked', type: 'string' },
                    { name: 'Balance', type: 'number' },
                    { name: 'Type', type: 'string' },
                    { name: 'CompleStatus', type: 'string' },
                    { name: 'RegTime', type: 'date' },
                    { name: 'LastLoginTime', type: 'date' },
                    { name: 'MemberLevel', type: 'int' },
                    { name: 'Name', type: 'string' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'P.RegTime';
                    data.sortorder = data.sortorder || 'DESC';
                    data.sUserType = $("#selSUserType").val() || "0";
                    data.uName = encodeURI($("#txtName").val()) || "";
                    data.isLock = $("#selIsLock").val() || "";
                    data.type = $("#selType").val() || "";
                    data.compleStatus = $("#selCompleStatus").val() || "";
                    data.isVip = $("#selIsVIP").val() || "";
                    data.totalDate = $("#txtTotalDate").val() || "";
                    data.branchCompanyId = $("#selBranchCompany").val() || "0";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function (column, direction) { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function (data) {
                    source.totalrecords = data.TotalRows;
                    $("#aggregate").html("总记录数：<span style='color:#ff0000;'>" + data.TotalRows + "&nbsp;</span>条&nbsp;&nbsp;可用余额总额：<span style='color:#ff0000;' title='提示：不包含平台可用余额'>" + formatNumber(data.AmountAggregate, 2, 1) + "</span>");
                }
            };

            //数据处理
            var dataadapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var linkrenderer = function (row, column, value) {
                var parm = "/MemberManage/MemberEdit.aspx?type=1&" + column + "=" + value + "&columnId=<%=ColumnId%>";
                var link = "<a href='" + parm + "'  target='_self' style='margin-left:5px;height:25px;line-height:25px;'>修改</a>";
                var html = link;
                return html;
            };

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1200,
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                    var aggregate = $("<div id='aggregate' style='float: right; margin-right: 0px; cursor:pointer;'></div>");
                    var addButton = $("<div style='float: left; margin-left: 5px;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>增加</span></div>");
                    aggregate.jqxButton({ width: 400, height: 20 });
                    addButton.jqxButton({ width: 60, height: 20 });
                    container.append(aggregate);
                    container.append(addButton);
                    statusbar.append(container);
                    addButton.click(function (event) {
                        window.location.href = '/BranchCompanyManage/BranchCompanyEmployeeAdd.aspx?columnId=<%=ColumnId%>';
                    });
                },
                sortable: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                showstatusbar: true,
                columns: [
                    { dataField: 'ID', text: '<b>操作</b>', width: 40, cellsrenderer: linkrenderer, align: 'center', sortable: false },
                    { dataField: 'MemberName', text: '<b>会员名</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'RealName', text: '<b>真实姓名</b>', width: 110, cellsalign: 'center', align: 'center' },
                    { dataField: 'Mobile', text: '<b>手机号码</b>', width: 110, cellsalign: 'center', align: 'center' },
                    { dataField: 'Email', text: '<b>电子邮箱</b>', width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'Name', text: '<b>所属分公司</b>', width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'IsLocked', text: '<b>是否锁定</b>', width: 80, cellsalign: 'center', align: 'center' },
                    { dataField: 'Balance', text: '<b>可用余额</b>', width: 100, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'CompleStatus', text: '<b>注册完成状态</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'RegTime', text: '<b>注册时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'LastLoginTime', text: '<b>最后一次登陆时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'MemberLevel', text: '<b>会员等级</b>', width: 100, cellsalign: 'center', align: 'center' }
                ]
            });

            //输出数据
            $("#btnOutput").click(function () {
                var data = Object();
                data.sortdatafield = data.sortdatafield || 'P.RegTime';
                data.sortorder = data.sortorder || 'DESC';
                data.sUserType = $("#selSUserType").val() || "0";
                data.uName = ($("#txtName").val()) || "";
                data.isLock = $("#selIsLock").val() || "";
                data.type = $("#selType").val() || "";
                data.compleStatus = $("#selCompleStatus").val() || "";
                data.isVip = $("#selIsVIP").val() || "";
                formatedData = buildQueryString(data);
                window.open("/HanderAshx/MemberManage/MemberManageHandler.ashx?output=1&" + formatedData, "_blank");
            });
        });
    </script>
</head>
<body>
    <div id="searchbar" class="selectDiv" style="min-width: 1000px;">
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="selSUserType" id="selSUserType" class="fl" runat="server">
            <option value="0">会员名</option>
            <option value="1">真实姓名</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selSUserType" />
        </div>
        <div style="float: left;">
            <input id="txtName" type="text" name="txtName" value="" class="input_text fl" maxlength="20" style="width: 150px;" runat="server" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="selIsLock" id="selIsLock" class="fl">
            <option value="">--是否锁定--</option>
            <option value="0">否</option>
            <option value="1">是</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selIsLock" />
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="selType" id="selType" class="fl">
            <option value="">--会员类型--</option>
            <option value="0">个人会员</option>
            <option value="1">企业会员</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selType" />
        </div>
        <div class="selectDiv2 fl">
            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="selCompleStatus" id="selCompleStatus" class="fl">
                <option value="">--注册完成状态--</option>
                <option value="1">注册完成</option>
                <option value="2">个人/企业认证完成</option>
                <option value="3">银行卡认证完成</option>
                <option value="4">VIP已申请</option>
                <option value="5">填写个人详细信息</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selCompleStatus" />
            </div>
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="selIsVIP" id="selIsVIP" class="fl">
            <option value="">--是否为VIP--</option>
            <option value="0">否</option>
            <option value="1">是</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selIsVIP" />
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select id="selBranchCompany" name="selBranchCompany" class="fl">
            <option value="0">--分公司--</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selBranchCompany" />
        </div>
        <div style="float: left; margin-left: 5px;">
            <input type="button" id="applyfilter" value="查 询" class="inputButton" />
        </div>
        
        <span class="fl" style="margin-top:5px; margin-left:5px;">导出日期：</span>
           <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
           </div>
           <input id="txtTotalDate" class="input_text" type="text" onclick="WdatePicker()" style="width:100px" />
           <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
           </div>
        <div style="float: left; margin-left: 5px;">
            <input type="button" id="btnOutput" value="导 出" class="inputButton" />
        </div>
    </div>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid">
        </div>
    </div>
    <script src="/js/select2css.js"></script>
</body>
</html>