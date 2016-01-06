<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveAdvance.aspx.cs" Inherits="WebUI.p2p.ApproveAdvance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>垫付审核管理</title>
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
        function Auditing(id)
        {
            if(confirm("确认审核吗?"))
            {
                var _operator = <%=MemberId%>;
                
                $.ajax({
                    type: "GET",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    url: "/Handerashx/p2p/AdvanceAuditing.ashx?ID=" + id +"&_operator=" + _operator,
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
                url: '/HanderAshx/p2p/ApproveAdvance.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'string' },
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
                    { name: 'RePayTime', type: 'date' },
                    { name: 'loantypeid', type: 'int' }
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
                    data.filter = "<%=MemberId%>";
                    data.loantypeID = $("#sel_loantype").val();
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
                var link = "<a style='text-align:center;margin-left:10px;height:25px; line-height:25px;' href='javascript:void(0)' onclick=\"Auditing('" + value + "')\"; target='_self'>审核</a>";
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
                    { dataField: 'ID', text: '<b>操作</b>', width: 80, cellsrenderer: linkrenderer, align: 'center', sortable: false },
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
    </div>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid">
        </div>
    </div>
</body>
</html>