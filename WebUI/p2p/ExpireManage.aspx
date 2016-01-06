<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpireManage.aspx.cs" Inherits="WebUI.p2p.ExpireManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>逾期管理</title>
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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.export.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.export.js"></script>
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>
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
        function QueryAdvance(id)
        {
            if(confirm("确认申请垫付?"))
            {
                var _operator = <%=MemberId%>;
                var currStep = 1;
                
                $.ajax({
                    type: "GET",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    url: "/Handerashx/p2p/QueryAdvance.ashx?ID=" + id +"&_operator=" + _operator + "&currStep=" +currStep,
                    dataType: "html",
                    cache: false,
                    success: function (context) {
                            MessageAlert(context, 'success', window.location.href);
                    },
                    error: function (xmlHttpRequest) {
                        alert(xmlHttpRequest.innerText);
                    },
                    complete: function (x) {
                    }
                });
                return true;
            }
            else
            {
                return false;
            }
        }

        $(function () {
            $("#applyfilter").click(function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
            });

            var btnoutput = <%=_btnOutPut %>;

            if (btnoutput == 0) {
                $("#btnOutput").hide();
                $("#ExportExcel").hide();
            }
            
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
            if (getPValueByName("loantype") != "") {
                $("#sel_loantype").val(getPValueByName("loantype"));
            }

            //数据源
            var source = {
                url: '/HanderAshx/p2p/ExpireManage.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'string' },
                    { name: 'LoanNumber', type: 'string' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'RealName', type: 'string' },
                    { name: 'Balance', type: 'number' },
                    { name: 'PeNumber', type: 'int' },
                    { name: 'RePrincipal', type: 'number' },
                    { name: 'ReInterest', type: 'number' },
                    { name: 'ReOverInterest', type: 'number' },
                    { name: 'StatusStr', type: 'string' },
                    { name: 'RePayTime', type: 'date' },
                    { name: 'ExpireDays', type: 'int' },
                    { name: 'Agency', type: 'string' },
                    { name: 'loantypeid', type: 'int' },
                    
                ],
                pagesize: 20,
                formatdata: function(data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'ExpireDays';
                    data.sortorder = data.sortorder || 'ASC';
                    data.memberName = $("#txtName").val() || "";
                    data.loanNumber = $("#txtLoanNumber").val() || "";
                    data.expireDays = $("#txtExpireDays").val() || "";
                    data.filter = " OverStatus in (0,1,5) and status<2 ";
                    data.MemberId = <%=MemberId%>;
                    data.loantypeID = $("#sel_loantype").val();

                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function() { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function(data) { source.totalrecords = data.TotalRows; }
            };

            //数据处理
            var dataadapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            

            var linkrenderer = function (row, column, value) {
                var link = "<a style='text-align:center;margin-left:10px;height:25px; line-height:25px;' href='javascript:void(0)' onclick=\"QueryAdvance('"+value+"')\"; target='_self'>申请垫付</a>";
                var html = link;
                return html;
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
                    //{ dataField: 'ID', text: '<b>操作</b>', width: 80, cellsrenderer: linkrenderer, align: 'center', sortable: false },
                    {
                        text: '<b>操作</b>', dataField: 'ID', width: 80, cellsalign: 'center', align: 'center', cellsrenderer: function (row, columnfield, value) {
                            var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                            if (data["loantypeid"]!="4") {
                                return '';
                                //return "<a style='text-align:center;margin-left:10px;height:25px; line-height:25px;' href='javascript:void(0)' onclick=\"QueryAdvance('"+value+"')\"; target='_self'>申请垫付</a>";
                            }
                            else{
                                return '';
                            }
                        }  
                    },
                    {
                        //dataField: 'LoanNumber', text: '<b>标号</b>', width: 150, cellsalign: 'center', align: 'center'
                        text: '<b>标号</b>', dataField: 'LoanNumber', width: 180, cellsalign: 'center', align: 'center', cellsrenderer: function (row, columnfield, value) {
                            var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                        
                            if (data["loantypeid"]!="4") {
                                return '<div style="text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;">' + value + '</div>';
                            }
                            else {
                                return '<div style="text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;">' + value + ' <span style="color:red;">（净）</span></div>';
                            }
                        }
                    },
                    { dataField: 'MemberName', text: '<b>会员名</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'RealName', text: '<b>真实姓名</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'Balance', text: '<b>可用余额(元)</b>', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'PeNumber', text: '<b>期数</b>', width: 50, cellsalign: 'center', align: 'center' },
                    { dataField: 'RePrincipal', columngroup: 'ReMode', text: '<b>应还本金(元)</b>', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'ReInterest', columngroup: 'ReMode', text: '<b>应还利息(元)</b>', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'ReOverInterest', columngroup: 'ReMode', text: '<b>应还逾期利息(元)</b>', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    
                    {
                        text: '<b>到期天数</b>', dataField: 'ExpireDays', width: 100, cellsalign: 'center', align: 'center', cellsrenderer: function (row, columnfield, value) {
                            var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                            if (data["loantypeid"]!="4") {
                                return '<div style="text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;">' + value + '</div>';
                            }
                            else {
                                return '<div style="text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;color:red;">' + value + '</div>';
                            }
                        }
                    },
                    { dataField: 'StatusStr', text: '<b>还款状态</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'RePayTime', text: '<b>应还款时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'Agency', text: '<b>分支机构</b>', width: 150, cellsalign: 'center', align: 'center' },
                ],
                columngroups:
                [
                    { text: '<b>应还金额</b>', align: 'center', name: 'ReMode' },
                    { text: '<b>未还金额</b>', align: 'center', name: 'SurMode' }
                ]
            });
            $("#btnOutput").jqxButton({ theme: theme });            
            $("#ExportExcel").jqxButton({ theme: theme });
            $("#btnOutput").click(function () {
                $("#jqxgrid").jqxGrid('exportdata', 'xls', 'jqxGrid');
            });

            $("#ExportExcel").click(function(){
                $.ajax({
                    type:"post",
                    url:"/HanderAshx/ReportStatistics/DimChannel.ashx",
                    data: {"method":"ExportExcel"},
                    success:function(data){
                        //window.open("http://localhost:1844" + data.url,"_blank");
                        window.open("http://manage.rjb777.com" + data.url,"_blank");
                    }
                });
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
        <input type="text" class="input_text fl" maxlength="20" style="width: 150px;" id="txtName" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl">标号：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input type="text" class="input_text fl" maxlength="20" style="width: 150px;" id="txtLoanNumber" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl">到期天数小于：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input type="text" class="input_text fl" maxlength="20" style="width: 150px;" id="txtExpireDays" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl">天</span>
        <span class="fl">贷款类型：</span>
        <div style="width: 4px; float: left;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select id="sel_loantype" name="loantype">
           <option value="0" selected="selected">选择借款类型</option>
           <option value="1">普通借款</option>
           <option value="2">净值借款</option>
       </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_loantype" />
        </div>
        <div style="float: left; margin-left: 5px;">
            <input type="button" id="applyfilter" value="查 询" class="inputButton" />
        </div>
        <div style="float: left; margin-left: 5px;">
            <input type="button" id="btnOutput" value="导 出" class="inputButton" />
        </div>
        <div style="float: left; margin-left: 5px;">
            <input type="button" id="ExportExcel" value="导出全部数据" class="inputButton" style="width: 120px;" />
        </div>
    </div>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid">
        </div>
    </div>
</body>
</html>

