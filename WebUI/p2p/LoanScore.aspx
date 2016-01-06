<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanScore.aspx.cs" Inherits="WebUI.p2p.LoanScore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>信用评分</title>
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
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
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>

    <style type="text/css">
        #searchbar span { height: 29px; text-indent: 5px; line-height: 29px; }
        .selectDiv .select_box { width: 85px; }
        .selectDiv2 .select_box { width: 105px; }
    </style>
    
    <script type="text/javascript">
        $(function () {

            var api = frameElement.api, W = api.opener;

            $("#btn_Confirm").bind("click", function () {
                W.sumScore(getPValueByName("loanId"));
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
                url: '/HanderAshx/p2p/LoanScore.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                updaterow: function (rowid, rowdata, commit) {
                    commit(true);
                },
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'LoanId', type: 'int' },
                    { name: 'ScoreItemsId', type: 'int' },
                    { name: 'ScoreItemsName', type: 'string' },
                    { name: 'FullMarks', type: 'number' },
                    { name: 'Score', type: 'number' },
                    { name: 'UpdateTime', type: 'date' }
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
                    { dataField: 'ScoreItemsName', text: '<b>评分项目</b>', width: 200, cellsalign: 'center', align: 'center',editable:false },
                    { dataField: 'FullMarks', text: '<b>满分分值</b>', width: 200, cellsalign: 'center', align: 'center', editable: false },
                    { dataField: 'Score', text: '<b>打分</b>', width: 170, cellsalign: 'center', align: 'center', columntype: 'numberinput',
                        validation: function (cell, value) {
                        var data = $("#jqxgrid").jqxGrid('getrowdata', args.rowindex);
                        if (value < 0 || value > data.FullMarks) {
                            return { result: false, message: "打分值需在 0-" + data.FullMarks + " 之间" };
                        }
                        $.ajax({
                            type: "POST",
                            url: "/HanderAshx/p2p/LoanScore.ashx?sign=1&loanId=" + getPValueByName("loanId") + "&Id=" + data.ID + "&score=" + value,
                            data: "text",
                            dataType: "text",
                            success: function (context) {
                                $("#sp_SumScoreLevel").text(context);
                            }
                        });
                        return true;
                        }
                    }
                ]
            });
        });

        function sumScore(loanId) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/HanderAshx/p2p/LoanScore.ashx?sign=2&loanId=" + loanId,
                data: "text",
                dataType: "json",
                success: function (result) {
                    $("#sp_SumScoreLevel").text("信用评分：" + result[0].SumScore + " 等级：" + result[0].ScoreLevel);
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
        <span id ="sp_SumScoreLevel"></span>
        <input id ="btn_Confirm" type="button" class="inputButton fr" value="确认" />
    </div>
    </form>
</body>
</html>
