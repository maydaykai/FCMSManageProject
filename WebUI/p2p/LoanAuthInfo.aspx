<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanAuthInfo.aspx.cs" Inherits="WebUI.p2p.LoanAuthInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>审核项目信息</title>
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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxnumberinput.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.selection.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxpanel.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.edit.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcalendar.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdatetimeinput.js"></script>
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>
    <script src="../js/datetime.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script><style type="text/css">
        #searchbar span { height: 29px; text-indent: 5px; line-height: 29px; }
        .selectDiv .select_box { width: 85px; }
        .selectDiv2 .select_box { width: 105px; }
    </style>
    
    <script type="text/javascript">
        $(function () {

            var api = frameElement.api, W = api.opener;
        
            $("#btn_Confirm").bind("click", function () {
                W.AuthInfoList(getPValueByName("loanId"),2);
                api.close();
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

            var formatData = function (row, column, value) {
                var html = "<span style='height:25px; line-height:25px;margin-right:10px;color:#dc143c;float:right;'>" + formatNumber(value, 2, 1) + "</span>";
                return html;
            };

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


            //数据源
            var source = {
                url: '/HanderAshx/p2p/LoanAuthInfo.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                updaterow: function (rowid, rowdata, commit) {
                    commit(true);
                },
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'LoanId', type: 'int' },
                    { name: 'AuthInfoId', type: 'int' },
                    { name: 'AuthDate', type: 'string' },
                    { name: 'AuthProductName', type: 'string' },
                    { name: 'UpdateTime', type: 'date' },
                    { name: 'IsAuth', type: 'bool' }
                ],
                pagesize: 10,
                formatdata: function (data) {
                    data.loanId = getPValueByName("loanId");
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function (column, direction) { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function (data) { source.totalrecords = data.TotalRows; }
            };

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
                width: 570,
                sortable: false,
                pageable: false,
                autoheight: true,
                virtualmode: true,
                editable: true,
                editmode: 'click',
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'AuthProductName', text: '<b>审核项目</b>', width: 200, cellsalign: 'center', align: 'center', editable: false },
                    { dataField: 'IsAuth', text: '<b>是否审核</b>', width: 170, cellsalign: 'center', align: 'center', columntype: 'checkbox'},
                    { dataField: 'AuthDate', text: '<b>审核日期</b>', width: 200, cellsalign: 'center', align: 'center', columntype: 'datetimeinput', cellsformat: "yyyy-MM-dd",
                        validation: function (cell, value) {
                            var data = $("#jqxgrid").jqxGrid('getrowdata', args.rowindex);
                            UpdateData(getPValueByName("loanId"), data.IsAuth, value.getFullYear() + '-' + (value.getMonth() + 1) + '-' + value.getDate(), data.ID);

                            return true;
                        }
                    }
                ]
            });

            var mydate = new Date();
            var t = mydate.getFullYear() + "-" + (mydate.getMonth() + 1) + "-" + mydate.getDate();

            $("#jqxgrid").on('cellendedit', function (event) {
                var args = event.args;
                var data = $("#jqxgrid").jqxGrid('getrowdata', args.rowindex);
                if (args.datafield == 'IsAuth') {
                    data.AuthDate = t;
                    UpdateData(data.LoanId, args.value, data.AuthDate, data.ID);
                }
                $("#cellendeditevent").text("Event Type: cellendedit, Column: " + args.datafield + ", Row: " + (1 + args.rowindex) + ", Value: " + args.value);
            });
        });
        
        
        
        function UpdateData(loanid,isAuth, authDate,id) {
            $.ajax({
                type: "POST",
                url: "/HanderAshx/p2p/LoanAuthInfo.ashx?sign=1&loanId=" + loanid + "&Id=" + id + "&IsAuth=" + isAuth + "&AuthDate=" + authDate,
                data: "text",
                dataType: "text",
                success: function (context) {
                    $("#sp_SumScoreLevel").text(context);
                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid">
        </div>
    </div>
    <div style="margin-top: 30px;">
        <input id ="btn_Confirm" type="button" class="inputButton fr" value="确认" />
    </div>
    </form>
</body>
</html>
