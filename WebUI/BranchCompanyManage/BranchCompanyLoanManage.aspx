<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BranchCompanyLoanManage.aspx.cs" Inherits="WebUI.BranchCompanyManage.BranchCompanyLoanManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.export.js"></script> 
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.export.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <script src="../js/loan/init.js"></script>
    <link href="../css/select.css" rel="stylesheet" />
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <link href="../css/global.css" rel="stylesheet" />
    <style type="text/css">
        .selectDiv {
            min-width:1350px;
        }
        .selectDiv .select_box {
         width: 85px;
        }
        .selectDiv span {
            margin-top:5px;
        }
         .selectDiv2 .select_box {
         width: 110px;
        }
    </style>

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

            if (getPValueByName("loanNumber") != "") {
                $("#txtNumber").val(getPValueByName("loanNumber"));
            }
            if (getPValueByName("loanUser") != "") {
                $("#sel_LoanUser").val(getPValueByName("loanUser"));
            }
            if (getPValueByName("loanMemberName") != null) {
                if (getPValueByName("loanMemberName") != "") {
                    $("#txtLoanUser").val(unescape(getPValueByName("loanMemberName")));
                }
            }
            if (getPValueByName("examStatus") != "") {
                $("#sel_examstatus").val(getPValueByName("examStatus"));
            }
            if (getPValueByName("createTime") != "") {
                $("#wdCreateTime").val(getPValueByName("createTime"));
            }
            if (getPValueByName("guaranteeId") != "") {
                $("#sel_Guarantee").val(getPValueByName("guaranteeId"));
            }
            if (getPValueByName("tradeType") != "") {
                $("#sel_TradeType").val(getPValueByName("tradeType"));
            }

            //数据源
            var source = {
                url: '/HanderAshx/p2p/Loan.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'RealName', type: 'string' },
                    { name: 'Mobile', type: 'string' },
                    { name: 'LoanNumber', type: 'string' },
                    { name: 'LoanAmount', type: 'number' },
                    { name: 'LoanTermInfo', type: 'string' },
                    { name: 'CreateTime', type: 'date' },
                    { name: 'FullScaleTime', type: 'date' },
                    { name: 'LoanUseName', type: 'string' },
                    { name: 'SoonStatus', type: 'int' },
                    { name: 'ExamStatus', type: 'int' },
                    { name: 'ExamStatusName', type: 'string' },
                    { name: 'GuaranteeName', type: 'string' },
                    { name: 'ReviewTime', type: 'date' },
                    //{ name: 'RepaymentLastTime', type: 'date' }

                ],

                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'CreateTime';
                    data.sortorder = data.sortorder || 'desc';
                    data.loanNumber = $("#txtNumber").val() || "";
                    data.loanUser = $("#sel_LoanUser").val() || 0;
                    data.loanMemberName = encodeURI($("#txtLoanUser").val()) || "";
                    data.examStatus = $("#sel_examstatus").val() || 0;
                    data.createTime = $("#wdCreateTime").val() || "";
                    data.guaranteeId = $("#sel_Guarantee").val() || "";
                    data.tradeType = $("#sel_TradeType").val() || -1;
                    data.branchCompanyId = "0";
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

            var linkrenderer = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var parm = column + "=" + value;
                var link = "<a href='../p2p/LoanAuditNew.aspx?" + formatedData + '&' + parm + "&columnId=<%=ColumnId%>'  target='_self' style='margin-left:10px;margin-right:5px;'>" + ((data.examStatus == 1 || data.examStatus == 3 || data.examStatus == 7) ? "查看" : "审核") + "</a>";
                if ((data.ExamStatus == 5 && data.SoonStatus == 1) || data.ExamStatus >= 7) {
                    link += "|<a href='../p2p/BiddingView.aspx?" + parm + "&columnId=<%=ColumnId%>'  target='_self' style='margin-left:5px;'>投标记录</a>";
                }
                var html = $.jqx.dataFormat.formatlink(link);
                return html;
            };
            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1200,
                sortable: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesizeoptions: ['10', '20', '30', '500'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                        { text: '<b>操作</b>', dataField: 'ID', width: 110, cellsalign: 'center', align: 'center', cellsrenderer: linkrenderer },
                        { text: '<b>借款人</b>', dataField: 'MemberName', width: 120, cellsalign: 'center', align: 'center' },
                        { text: '<b>真实姓名</b>', dataField: 'RealName', width: 80, cellsalign: 'center', align: 'center' },
                        { text: '<b>电话</b>', dataField: 'Mobile', width: 120, cellsalign: 'center', align: 'center' },
                        { text: '<b>借款编号</b>', dataField: 'LoanNumber', width: 120, cellsalign: 'center', align: 'center' },
                        { text: '<b>贷款额度(元)</b>', dataField: 'LoanAmount', cellsformat: "F2", width: 120, cellsalign: 'right', align: 'center' },
                        { text: '<b>借款期限</b>', dataField: 'LoanTermInfo', width: 70, cellsalign: 'center', align: 'center' },
                        { text: '<b>申请时间</b>', dataField: 'CreateTime', width: 180, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                        { text: '<b>满标时间</b>', dataField: 'FullScaleTime', width: 180, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                        { text: '<b>贷款用途</b>', dataField: 'LoanUseName', width: 80, cellsalign: 'center', align: 'center' },
                        { text: '<b>审核状态</b>', dataField: 'ExamStatusName', width: 120, cellsalign: 'center', align: 'center' },
                        { text: '<b>担保公司</b>', dataField: 'GuaranteeName', width: 180, cellsalign: 'center', align: 'center' },
                        { text: '<b>放款日</b>', dataField: 'ReviewTime', width: 180, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' }  
                ]
            });

            $("#btnOutput").jqxButton({ theme: theme });
            $("#btnOutput").click(function () {
                $("#jqxgrid").jqxGrid('exportdata', 'xls', 'jqxGrid');
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="searchbar" class="selectDiv" style="min-width:800px;">
        <span class="fl">编号：</span>
        <div style="width: 4px; float: left;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input type="text" id="txtNumber" name="input" class="fl input_text" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl" style="margin-left:5px;">状态：</span>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select id="sel_examstatus" runat="server" name="examstatus">
             <option value="0" selected="selected">不限</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_examstatus" />
        </div>
        <span class="fl" style="margin-left:5px;">线上线下：</span>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select id="sel_TradeType" runat="server" name="TradeType">
            <option value="-1" selected="selected">不限</option>
            <option value="0" >线上</option>
            <option value="1" >线下</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_TradeType" />
        </div>
       <div class="selectDiv2">
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <div class="selectDiv2"></div>
        <select id="sel_Guarantee" runat="server">
           <option value="0" selected="selected">选择担保公司</option>
       </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_Guarantee" />
        </div></div>
        
        <div class="clear"></div>

        
        
        <select id="sel_LoanUser" name="loanUser">
           <option value="0" selected="selected">选择借款人</option>
           <option value="1">用户名</option>
           <option value="2">真实姓名</option>
       </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_loanUser" />
        </div>
        <input type="text" id="txtLoanUser" name="input" class="fl input_text" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl" style="margin-left:5px;">申请时间：</span>
         <div style="width: 4px; float: left;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input id="wdCreateTime" class="fl input_text" type="text" onclick="WdatePicker()" style="width:100px" runat="server" />
        <div class="fl" style="margin-right:5px;">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
       <div style="float: left; margin-left: 5px;"><input type="button" id="btnSearch" value="查 询" class="inputButton" /></div><div style="float: left; margin-left: 5px;"><input type="button" id="btnOutput" value="导 出" class="inputButton" /></div>
    </div>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid">
        </div>
    </div>

    </form>
    <script src="../js/select2css.js"></script>
</body>
</html>