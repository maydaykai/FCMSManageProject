<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FundRecordManage.aspx.cs" Inherits="WebUI.ReportStatistics.FundRecordManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
    <script src="../js/FormatDecimal.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <style type="text/css">
        #select_selAndOr { width: 40px; }
        .selectDiv .select_box { width: 85px; }
        .selectDiv2 .select_box { width: 115px; }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#btnSearch").click(function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
            });
            initFeeType();
            //主题
            var theme = "arctic";

            var formatedData = '';

            //数据源
            var source = {
                url: '/HanderAshx/ReportStatistics/FundRecordHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'PayeeMemberName', type: 'string' },
                    { name: 'PartyMemberName', type: 'string' },
                    { name: 'PayeeRealName', type: 'string' },
                    { name: 'PartyRealName', type: 'string' },
                    { name: 'LoanNumber', type: 'string' },
                    { name: 'FeeTypeString', type: 'string' },
                    { name: 'Amount', type: 'number' },
                    { name: 'PayeeBalance', type: 'number' },
                    { name: 'PartyBalance', type: 'number' },
                    { name: 'Status', type: 'int' },
                    { name: 'Description', type: 'string' },
                    { name: 'CreateTime', type: 'date' },
                    { name: 'UpdateTime', type: 'date' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'P.CreateTime';
                    data.sortorder = data.sortorder || 'DESC';
                    data.payeeType = $("#selPayeeType").val() || "0";
                    data.payeeMemberName = encodeURI($("#txtPayeeMemberName").val()) || "";
                    data.andor = $("#selAndOr").val() || "0";
                    data.partyType = $("#selPartyType").val() || "0";
                    data.partyMemberName = encodeURI($("#txtPartyMemberName").val()) || "";
                    data.status = $("#selStatus").val() || "";
                    data.feeType = $("#jqxSelectVal").val() || "";
                    data.startDate = $("#txtStartDate").val() || "";
                    data.endDate = $("#txtEndDate").val() || "";
                    data.bondingCo = $("#<%=selBondingCompany.ClientID%>").val() || "";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function (data) {
                    source.totalrecords = data.TotalRows;
                    $("#aggregate").html("总记录数：<span style='color:#ff0000;'>" + data.TotalRows + "&nbsp;</span>条&nbsp;&nbsp;总交易金额：<span style='color:#ff0000;'>" + formatNumber(data.AmountAggregate, 2, 1) + "</span>");
                }
            };

            //数据处理
            var dataadapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var statusRender = function (row, column, value) {
                var html = "<div style=\"text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;\">";
                switch (value) {
                    case 1: html += "正常"; break;
                    case 2: html += "冻结"; break;
                    case 3: html += "作废"; break;
                }
                html += "</div>";
                return html;
            }

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1250,
                sortable: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                    var aggregate = $("<div id='aggregate' style='float: right; margin-right: 0px; cursor:pointer;'></div>");
                    aggregate.jqxButton({ width: 400, height: 20 });
                    container.append(aggregate);
                    statusbar.append(container);
                },
                showstatusbar: true,
                columns: [
                    { dataField: 'PayeeMemberName', text: '会员名', width: 100, cellsalign: 'center', align: 'center', columngroup: 'Payee' },
                    { dataField: 'PayeeRealName', text: '真实姓名', width: 100, cellsalign: 'center', align: 'center', columngroup: 'Payee' },
                    { dataField: 'PartyMemberName', text: '会员名', width: 100, cellsalign: 'center', align: 'center', columngroup: 'Party' },
                    { dataField: 'PartyRealName', text: '真实姓名', width: 100, cellsalign: 'center', align: 'center', columngroup: 'Party' },
                    { dataField: 'LoanNumber', text: '<b>借款标编号</b>', width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'FeeTypeString', text: '<b>费用类型</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'Amount', text: '<b>交易金额(元)</b>', width: 140, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'PayeeBalance', text: '<b>收款方账户可用余额(元)</b>', width: 170, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'PartyBalance', text: '<b>出款方账户可用余额(元)</b>', width: 170, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'Status', text: '<b>状态</b>', width: 100, cellsalign: 'center', align: 'center', cellsrenderer: statusRender },
                    { dataField: 'Description', text: '<b>描述说明</b>', width: 280, cellsalign: 'center', align: 'center' },
                    { dataField: 'CreateTime', text: '<b>创建时间</b>', width: 180, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd HH:mm:ss" },
                    { dataField: 'UpdateTime', text: '<b>修改时间</b>', width: 180, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd HH:mm:ss" }
                ],
                columngroups:
                [
                    { text: '<b>收款方</b>', align: 'center', name: 'Payee' },
                    { text: '<b>出款方</b>', align: 'center', name: 'Party' }
                ]
            });

        });
        //参数组装
        function buildQueryString(data) {
            var str = ''; for (var prop in data) {
                if (data.hasOwnProperty(prop)) {
                    str += prop + '=' + data[prop] + '&';
                }
            }
            return str.substr(0, str.length - 1);
        }
        function initFeeType() {
            /*费用类型下拉框初始化*/
            //数据源
            var source = {
                url: '/HanderAshx/p2p/DimFeeType.ashx',
                datatype: "json",
                formatdata: function (data) {
                    data.sign = 1;
                    return buildQueryString(data);
                },
                datafields: [
                    { name: 'FeeTypeName' },
                    { name: 'FeeTypeVal' }
                ],
                async: false
            };

            //数据处理
            var dataadapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#jqxSelect").jqxDropDownList({ checkboxes: true, source: dataadapter, displayMember: "FeeTypeName", valueMember: "FeeTypeVal", width: 200, height: 25 });
            $("#jqxSelect").jqxDropDownList('checkIndex', 0);
            // subscribe to the checkChange event.
            $("#jqxSelect").on('checkChange', function (event) {
                if (event.args) {
                    var item = event.args.item;
                    if (item) {
                        //var valueelement = $("<div></div>");
                        //valueelement.text("Value: " + item.value);
                        //var labelelement = $("<div></div>");
                        //labelelement.text("Label: " + item.label);
                        //var checkedelement = $("<div></div>");
                        //checkedelement.text("Checked: " + item.checked);
                        //$("#selectionlog").children().remove();
                        //$("#selectionlog").append(labelelement);
                        //$("#selectionlog").append(valueelement);
                        //$("#selectionlog").append(checkedelement);
                        var items = $("#jqxSelect").jqxDropDownList('getCheckedItems');
                        if (items.length == 0) {
                            $("#jqxSelect").jqxDropDownList('checkIndex', 0);
                            $("#jqxSelectVal").val("-1");
                        } else {
                            var checkedItemsVal = "";
                            $.each(items, function(index) {
                                checkedItemsVal += this.value + ",";
                            });
                            $("#jqxSelectVal").val(checkedItemsVal);
                        }
                    }
                }
            });
        }
    </script>
</head>
<body>
    <form runat="server" id="form1">
        <div id="searchbar" class="selectDiv" style="min-width: 1250px;">
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="selPayeeType" id="selPayeeType" class="fl" runat="server">
                <option value="0">收款方会员名</option>
                <option value="1">收款方姓名</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selPayeeType" />
            </div>
            <div style="float: left;">
                <input id="txtPayeeMemberName" type="text" name="txtPayeeMemberName" value="" class="input_text fl" maxlength="20" style="width: 120px;" runat="server" />
                <div class="fl">
                    <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                </div>
            </div>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="selAndOr" id="selAndOr" class="fl" runat="server">
                <option value="0">与</option>
                <option value="1">或</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selAndOr" />
            </div>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="selPartyType" id="selPartyType" class="fl" runat="server">
                <option value="0">出款方会员名</option>
                <option value="1">出款方姓名</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selPartyType" />
            </div>
            <div style="float: left; margin-right: 5px;">
                <input id="txtPartyMemberName" type="text" name="txtPartyMemberName" value="" class="input_text fl" maxlength="20" style="width: 120px;" runat="server" />
                <div class="fl">
                    <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                </div>
            </div>
            <div class="selectDiv2">
                <div class="fl">
                    <img src="/images/input_left.png" width="4" height="29" alt="" />
                </div>
                <asp:DropDownList ID="selBondingCompany" runat="server"></asp:DropDownList>
                <div class="fl" style="margin-left: -5px; cursor: pointer;">
                    <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selBondingCompany" />
                </div>
            </div>
            <div class="clear"></div>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="selStatus" id="selStatus" class="fl" runat="server">
                <option value="">--状态--</option>
                <option value="1">正常</option>
                <option value="2">冻结</option>
                <option value="3">作废</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer; margin-right: 5px;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selStatus" />
            </div>
            <div style='float: left;' id='jqxSelect'>
            </div>
            <input type="hidden" id='jqxSelectVal' runat="server" />
            <span class="fl" style="margin-top: 5px; margin-left: 5px;">记录时间：</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtStartDate" class="input_text" type="text" onclick="WdatePicker({ dateFmt: 'yyyy-M-d H:mm:ss', maxDate: '#F{$dp.$D(\'txtEndDate\')}' })" style="width: 150px" runat="server" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <span class="fl" style="margin-top: 8px; margin-left: 5px; margin-right: 5px;">～</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtEndDate" class="input_text" type="text" onclick="WdatePicker({ dateFmt: 'yyyy-M-d H:mm:ss', minDate: '#F{$dp.$D(\'txtStartDate\')}' })" style="width: 150px" runat="server" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="btnSearch" value="查 询" class="inputButton" />
            </div>
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="btnOutput" value="导 出" class="inputButton" runat="server" onserverclick="btnOutput_Click" />
            </div>
        </div>
        <div id="jqxWidget" style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>
