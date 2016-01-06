<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Marketing_Data.aspx.cs" Inherits="WebUI.UserMarketing.Marketing_Data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>营销报表数据</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
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
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/icon.css" rel="stylesheet" />


    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="/js/My97DatePicker/WdatePicker.js"></script>

    <script type="text/javascript">
        function LoadFirst(sign, SecondId) {
            //var JsonData = new {

            //};

            $.ajax({
                type: "GET",
                async: true,
                contentType: "application/json; charset=utf-8",
                url: "/HanderAshx/UserMarketing/LoadPersonListHandler.ashx?f=" + Math.random() + "&sign=" + sign + "&firstId=" + $("#marketingId").val() + "&SecondId=" + SecondId,
                dataType: "json",
                cache: false,
                success: function (resultData) {
                    //组合参数
                    var html = " <option value=\"-1\">全部</option>";
                    for (var i = 0; i < resultData.length; i++) {
                        html += "<option value=" + resultData[i].Id + ">" + resultData[i].RoleName + "</option>";
                    }
                    //alert(html);
                    if (sign == 0) {
                        $("#selType").append(html);
                    }
                    else {
                        $("#selType_1").empty();
                        $("#selType_1").append(html);
                    }


                },
                error: function (xmlHttpRequest) {
                    alert(xmlHttpRequest.innerText);
                },
                complete: function (x) {
                }
            });
        }

    </script>



    <script type="text/javascript">
        $(document).ready(function () {
            //主题
            //默认的当前月份
            var myDate = new Date();
            var seperator1 = "-";
            var seperator2 = ":";
            var month = myDate.getMonth() + 1;
            var strDate = myDate.getDate();
            if (month >= 1 && month <= 9) {
                month = "0" + month;
            }
            if (strDate >= 0 && strDate <= 9) {
                strDate = "0" + strDate;
            }

            var starttime = myDate.getFullYear() + seperator1 + month + seperator1 + "01";
            var endtime = myDate.getFullYear() + seperator1 + month + seperator1 + "31";

            var curr_marketId = $("#marketingId").val();
            var curr_userid = $("#userId").val();
            LoadData(starttime, endtime, curr_userid, curr_marketId);//获取当前角色Id
            LoadFirst(0, 0);

            $("#selType").click(function () {
                var id = $("#selType").val();
                //getCityListFromHandlerByParentID(id, "selCompleStatus");
                LoadFirst(1, id);
                // alert(id);
            });

        });
    </script>
    <script type="text/javascript">
        //查询数据
        function LoadData(starttime, endtime, userid, roleId) {

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
                url: '/HanderAshx/UserMarketing/ExDataHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'Id', type: 'int' },
                    { name: 'Department', type: 'string' },
                    { name: 'GroupName', type: 'string' },
                    { name: 'RoleName', type: 'string' },
                    { name: 'Regcount', type: 'int' },
                    { name: 'Regmoney', type: 'decimal' },
                    { name: 'BidNumContinued', type: 'int' },
                    { name: 'SumBidAmount', type: 'decimal' },
                    { name: 'SuccessTransferMoney', type: 'decimal' },
                    { name: 'RealMoney', type: 'decimal' },
                    { name: 'Interest', type: 'decimal' },
                    { name: 'Curr_MouthMoney', type: 'decimal' }
                ],
                pagesize: 20,
                formatdata: function (data) {

                    var RoleId_One = -1;
                    var RoleId_Second = -1;
                    RoleId_One = $("#selType").val() != "" ? $("#selType").val() : "-1";
                    RoleId_Second = $("#selType_1").val() != null ? $("#selType_1").val() : "-1";


                    if (parseInt(RoleId_Second) == -1) {
                        RoleId_Second = RoleId_One;
                    }
                    //LoadData(starttime, endtime,userid, roleId)txt_begin
                    var myDate = new Date();
                    var seperator1 = "-";
                    var seperator2 = ":";
                    var month = myDate.getMonth() + 1;
                    var strDate = myDate.getDate();
                    if (month >= 1 && month <= 9) {
                        month = "0" + month;
                    }
                    if (strDate >= 0 && strDate <= 9) {
                        strDate = "0" + strDate;
                    }

                    var starttime = $("#txt_begin").val() ? $("#txt_begin").val() : myDate.getFullYear() + seperator1 + month + seperator1 + "01";
                    var endtime = $("#txt_end").val() ? $("#txt_end").val() : myDate.getFullYear() + seperator1 + month + seperator1 + "31";
                    //alert($("#txt_begin").val());
                    //alert(endtime);
                    var curr_marketId = $("#marketingId").val();

                    // LoadData(starttime, endtime, $("#userId").val(), RoleId_Second);
                    if (parseInt(RoleId_Second) > -1) {
                        roleId = RoleId_Second;
                    }


                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.starttime = starttime;
                    data.endtime = endtime;
                    data.userId = userid;
                    data.roleId = roleId;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                //filter: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'filter'); },
                beforeprocessing: function (data) {
                    source.totalrecords = data.TotalRows;
                    var currhtml = "汇总[注册人数：<span style='color:#ff0000;'>" + data.SumRegcount + "&nbsp;</span>人&nbsp;&nbsp;注册金额：<span style='color:#ff0000;'>" + data.SumRegmoney + "</span>";
                    currhtml += "续投人数：<span style='color:#ff0000;'>" + data.SumBidNumContinued + "&nbsp;</span>人&nbsp;&nbsp;续投金额：<span style='color:#ff0000;'>" + data.SumBidAmount + "&nbsp;</span>&nbsp;&nbsp;";
                    currhtml += "成功转让金额：<span style='color:#ff0000;'>" + data.SumSuccessTransferMoney + "&nbsp;</span>&nbsp;&nbsp;实际投资金额：<span style='color:#ff0000;'>" + data.SumRealMoney + "&nbsp;</span>&nbsp;&nbsp;已收利息:<span style='color:#ff0000;'>" + data.SumInterest + "&nbsp;</span>&nbsp;&nbsp;当月投资金额:<span style='color:#ff0000;'>" + data.SumCurr_MouthMoney + "&nbsp;</span>]";
                    $("#aggregate").html(currhtml);

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
                var parm = column + "=" + value + "&columnId=<%=ColumnId%>";
                var link = "<a href='Role_Distribution.aspx?" + parm + "'  target='_self' style='margin-left:10px;height:25px;line-height:25px;'>分配</a>";
                var html = $.jqx.dataFormat.formatlink(link);
                return html;
            };

            var isLockRenderer = function (row, column, value) {
                var strHtml = '<div style="text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;">';
                if (value) {
                    strHtml += "是";
                } else {
                    strHtml += "否";
                }
                strHtml += "</div>";
                return strHtml;
            };

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1500,
                sortable: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesizeoptions: ['10', '20', '30'],
                //filterable: true,
                //showfilterrow: true,//过滤
                rendergridrows: function (args) {
                    return args.data;
                },
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                    var aggregate = $("<div id='aggregate' style='float: right; margin-right: 0px; cursor:pointer;'></div>");
                    aggregate.jqxButton({ width: 1000, height: 20 });
                    container.append(aggregate);
                    statusbar.append(container);
                },
                showstatusbar: true,
                showtoolbar: true,
                rendertoolbar: function (toolbar) {
                    var me = this;
                    var container = $("<div style='margin: 5px;'></div>");
                    var span = $("<span style='float: left; margin-top: 5px; margin-right: 4px;'>用户名: </span>");
                    var input = $("<input class='jqx-input jqx-widget-content jqx-rc-all' id='userName' type='text' style='height: 23px; float: left; width: 223px;' />");
                    toolbar.append(container);
                    // container.append(span);
                    //container.append(input);
                    if (theme != "") {
                        input.addClass('jqx-widget-content-' + theme);
                        input.addClass('jqx-rc-all-' + theme);
                    }
                    var oldVal = "";
                    input.on('keydown', function (event) {
                        if (input.val().length > 0) {

                            if (me.timer) {
                                clearTimeout(me.timer);
                            }
                            if (oldVal != input.val()) {
                                me.timer = setTimeout(function () {
                                    $("#jqxgrid").jqxGrid('updatebounddata');
                                }, 1000);
                                oldVal = input.val();
                            }
                        }
                    });
                },
                columns: [
                        { text: '<b>所属部门</b>', dataField: 'Department', width: 120, cellsalign: 'center', align: 'center' },
                        { text: '<b>所属组</b>', dataField: 'GroupName', width: 150, cellsalign: 'center', align: 'center' },
                        { text: '<b>名字</b>', dataField: 'RoleName', width: 100, cellsalign: 'center', align: 'center' },
                        { text: '<b>新增注册人数</b>', dataField: 'Regcount', width: 100, cellsalign: 'center', align: 'center' },
                        { text: '<b>新增注册金额</b>', dataField: 'Regmoney', width: 180, cellsalign: 'center', align: 'center' },
                        { text: '<b>续投人数</b>', dataField: 'BidNumContinued', width: 80, cellsalign: 'center', align: 'center' },
                        { text: '<b>续投金额</b>', dataField: 'SumBidAmount', width: 180, cellsalign: 'center', align: 'center' },
                        { text: '<b>成功转让金额</b>', dataField: 'SuccessTransferMoney', width: 180, cellsalign: 'center', align: 'center' },
                        { text: '<b>实际投资金额</b>', dataField: 'RealMoney', width: 180, cellsalign: 'center', align: 'center' },
                        { text: '<b>已收利息</b>', dataField: 'Interest', width: 180, cellsalign: 'center', align: 'center' },
                        { text: '<b>当月投资金额</b>', dataField: 'Curr_MouthMoney', width: 180, cellsalign: 'center', align: 'center' },
                ]
            });

            }
    </script>
    <script type="text/javascript">
        function qureyData() {
            $("#jqxgrid").jqxGrid('updatebounddata');
        }

    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <!--根据当前角色加载查询列表-->
            <select name="selType" id="selType" class="fl" runat="server">
            </select>

            <select name="selType_1" id="selType_1" class="fl" runat="server">
            </select>

            开始时间:
            <input type="text" id="txt_begin" onclick="WdatePicker()" />
            结束时间<input type="text" id="txt_end" onclick="    WdatePicker()" />

            <input type="button" value="查询" onclick="qureyData()" />
            <br />


        </div>
        <input type="hidden" id="marketingId" runat="server" />
        <input type="hidden" id="userId" runat="server" />

        <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>
