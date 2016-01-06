<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReturnVisitEdit.aspx.cs" Inherits="WebUI.ReportStatistics.ReturnVisitEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/table.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <link href="../css/bankBg.css" rel="stylesheet" />
    <script src="../js/jQuery/jquery-1.8.3.min.js"></script>
    <link href="../js/jquery.ganged/jquery.inputbox.css" rel="stylesheet" />
    <script src="../js/jquery.ganged/jquery.inputbox.js"></script>
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script src="/js/datetime.js"></script>
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

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>
    <script src="../js/select2css.js"></script>
    <style type="text/css">
        .sb .opts a { border: 1px #ffffff solid; }
            .sb .opts a:hover { background: #ffffff; color: #000000; border: 1px #ff4500 solid; }
            .sb .opts a.selected { background: #ffffff; color: #000000; }
            .sb .opts a.none { background: #ffffff; color: #000000; }
    </style>
    <script type="text/javascript">
        function GetRequest() {
            var url = location.search; //获取url中"?"符后的字串
            var theRequest = new Object();
            if (url.indexOf("?") != -1) {
                var str = url.substr(1);
                strs = str.split("&");
                for(var i = 0; i < strs.length; i ++) {
                    theRequest[strs[i].split("=")[0]]=(strs[i].split("=")[1]);
                }
            }
            return theRequest;
        }
        
        var Request=new Object();
        Request=GetRequest();

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
                url: '/HanderAshx/ReportStatistics/ReturnVisitHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'memberId', type: 'int' },
                    { name: 'operator', type: 'string' },
                    { name: 'record', type: 'string' },
                    { name: 'notes', type: 'string' },
                    { name: 'createTime', type: 'date' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.currentpage = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.mid = Request["mid"];
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
                width: 1235,
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
                    { dataField: 'createTime', text: '<b>回访时间</b>', width: 225, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd HH:mm:ss" },
                    { dataField: 'operator', text: '<b>操作员</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'record', text: '<b>回访结果</b>', width: 160, cellsalign: 'center', align: 'center' },
                    { dataField: 'notes', text: '<b>回访备注</b>', width: 700, cellsalign: 'center', align: 'center' }
                ]
            });
        });

        function backUp(cId)
        {
            window.location.href = "Advertising.aspx?s=" + Request["s"] + "&e=" + Request["e"] + "&r=" + Request["r"] + "&i=" + Request["i"] + "&columnId=" + cId + "&cr=" + Request["cr"] + "&c=" + Request["c"] + "&mina=" + Request["mina"] + "&maxa=" + Request["maxa"] + "";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" class="editTable">
            <tr>
                <td style="text-align: right; width: 120px;">会员帐号：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtMemberName" type="text" disabled="disabled" name="txtMemberNameHolder" value="" class="input_Disabled fl" maxlength="50" style="width: 100px;" runat="server" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">真实姓名：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtRealName" type="text" disabled="disabled" name="txtRealNameHolder" value="" class="input_Disabled fl" maxlength="50" style="width: 100px;" runat="server" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">手机号码：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtMobile" type="text" disabled="disabled" name="txtMobileHolder" value="" class="input_Disabled fl" maxlength="50" style="width: 100px;" runat="server" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">回访结果：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 3px;">
                    <div class="selectDiv">
                        <div class="fl">
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                        </div>
                        <select name="selCurrStatus" id="selCurrStatus" class="fl" runat="server" style="width: 80px;">
                            <option value="回访成功">回访成功</option>
                            <option value="回访失败">回访失败</option>
                            <option value="回访跟进">回访跟进</option>
                        </select>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <img src="/images/select_right.png" width="31" height="29" alt="" id="img_selCurrStatus" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px; vertical-align: top; padding-top: 3px;">备注：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <textarea id="txtNotes" name="content1" maxlength="200" style="width: 480px; height: 120px; padding: 5px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;"></td>
                <td style="text-align: left;">
                    <asp:Button ID="btnSubmit" Text="提交" CssClass="inputButton" runat="server" Width="100px" OnClick="btnSubmit_Click" />
                    <input type="button" value="返回" class="inputButton" onclick="backUp(<%=ColumnId %>);" />
                </td>
            </tr>
        </table>
        <br />
        <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>
