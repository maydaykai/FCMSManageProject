<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanApplyManage.aspx.cs" Inherits="WebUI.p2p.LoanApplyManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>借款申请审批管理</title>
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <script src="../js/jQuery/jquery-1.7.2.min.js"></script>
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
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        .selectDiv .select_box {
         width: 85px;
        }
    </style>
    <script type="text/javascript">
        $(function () {

            $('#btnSearch').bind("click", function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
            });
            //省市绑定
            getProvinceListByHandler($("#ulProvince"), $("#divProvince"), $("#hdProvince"), $("#ulCity"), $("#divCity"), $("#hdCity"));

            //借款用途绑定
           // etLoanUseListByHandler($("#ulLoanUse"), $("#divLoanUse"), $("#hdloanUse"));

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
                url: '/HanderAshx/p2p/LoanApply.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'LoanUseName', type: 'string' },
                    { name: 'Mobile', type: 'string'} ,
                    { name: 'LoanAmount', type: 'number' },
                    { name: 'LoanTerm', type: 'int' },
                    { name: 'BorrowMode', type: 'int' },
                    { name: 'ExamStatus', type: 'int' },
                    { name: 'CreateTime', type: 'date' },
                    { name: 'ProvinceName', type: 'string' },
                    { name: 'CityName', type: 'string' },
                    { name: 'RepaymentMethodName', type: 'string' },
                    { name: 'RepaymentSource', type: 'string' },
                    { name: 'RealName', type: 'string' }
                    
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'CreateTime';
                    data.sortorder = data.sortorder || 'desc';
                    data.name = $("#txtName").val() || "";
                    data.status = $("#sel_status").val() || "";
                    data.startCreateTime = $("#txtStartTime").val() || "";
                    data.endCreateTime = $("#txtEndTime").val() || "";
                    data.loanuse = $("#hdloanUse").val() || "";
                    data.city = $("#hdCity").val() || "";
                    data.province = $("#hdProvince").val() || "";
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

            var loanModeRenderer = function (row, column, value) {
                var temp = parseInt(value);
                var html = "<div style=\"text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;\">";
                switch (temp) {
                    case 0: html += "按天"; break;
                    case 1: html += "按月"; break;
                }
                html += "</div>";
                return html;
            };

            var loanStatusRender = function (row, column, value) {
                var temp = parseInt(value);
                var html = "<div style=\"text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;\">";
                switch (temp) {
                    case 0: html += "初审中"; break;
                    case 1: html += "复审中"; break;
                    case 2: html += "初审不通过"; break;
                    case 3: html += "复审不通过"; break;
                    case 4: html += "复审通过"; break;
                    case 5: html += "已发标"; break;
                }
                html += "</div>";
                return html;
            };

            var linkrenderer = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var parm = "/p2p/LoanApplyAudit.aspx?" + column + "=" + value + "&columnId=<%=ColumnId%>";
                var link = "<a href='" + parm + "'  target='_self' style='margin-left:5px;height:25px;line-height:25px;'>" + ((data.ExamStatus == 0 || data.ExamStatus == 1) ? "审核" : "查看") + "</a>";

                var html = link;
                return html;
            };

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1025,
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
                        { text: '<b>操作</b>', dataField: 'ID', width: 50, cellsalign: 'center', align: 'center', cellsrenderer: linkrenderer, sortable: false },
                        { text: '<b>会员名</b>', dataField: 'MemberName', width: 80, cellsalign: 'center', align: 'center' },
                        { text: '<b>真实姓名</b>', dataField: 'RealName', width: 110, cellsalign: 'center', align: 'center' },
                        { text: '<b>手机号码</b>', dataField: 'Mobile', width: 100, cellsalign: 'center' , align: 'center'  },
                        { text: '<b>贷款额度(元)</b>', dataField: 'LoanAmount', cellsformat: "F2", width: 120, cellsalign: 'right', align: 'center' },
                        { text: '<b>借款用途</b>', dataField: 'LoanUseName', width: 100, cellsalign: 'center', align: 'center' },
                        { text: '<b>借款期限</b>', dataField: 'LoanTerm', width: 65, cellsalign: 'center', align: 'center' },
                        { text: '<b>还款类型</b>', dataField: 'RepaymentMethodName', width: 120, cellsalign: 'center', align: 'center' },
                        { text: '<b>借款方式</b>', dataField: 'BorrowMode', width: 80, cellsalign: 'center', align: 'center', cellsrenderer: loanModeRenderer },
                        { text: '<b>审核状态</b>', dataField: 'ExamStatus', width: 80, cellsalign: 'center', align: 'center', cellsrenderer: loanStatusRender },
                        { text: '<b>所在省份</b>', dataField: 'ProvinceName', width: 80, cellsalign: 'center', align: 'center', sortable: false },
                        { text: '<b>所在城市</b>', dataField: 'CityName', width: 80, cellsalign: 'center', align: 'center', sortable: false },
                        { text: '<b>申请时间</b>', dataField: 'CreateTime', width: 180, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                        { text: '<b>还款来源</b>', dataField: 'RepaymentSource', width: 250, cellsalign: 'center', align: 'center' }
                        
                ]
            });


            //输出数据
            $("#btnOutput").click(function () {
                var data = Object();
                data.sortdatafield = data.sortdatafield || 'CreateTime';
                data.sortorder = data.sortorder || 'desc';
                data.name = $("#txtName").val() || "";
                data.status = $("#sel_status").val() || "";
                data.startCreateTime = $("#txtStartTime").val() || "";
                data.endCreateTime = $("#txtEndTime").val() || "";
                data.loanuse = $("#hdloanUse").val() || "";
                data.city = $("#hdCity").val() || "";
                data.province = $("#hdProvince").val() || "";
                formatedData = buildQueryString(data);
                window.open("/HanderAshx/p2p/LoanRapid.ashx?output=1&" + formatedData, "_blank");
            });
        });
    </script>
    <script src="/js/Area.js"></script>
</head>
<body>
    
    <form id="form1" runat="server">
      <div style="min-width:1200px;width:100%;">
        <span class="fl" style="margin-top:10px;">会员名：</span>
        <div class="inputText fl">
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input type="text" id="txtName" class="fl input_text" name="txtName" runat="server" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div></div>
        <div class="fl selectDiv inputText">
            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select id="sel_status" name="sel_status" class="fl" >
                <option value="-1" selected="selected" class="fl" >--审核状态--</option>
                <option value="0">初审中</option>
                <option value="1">复审中</option>
                <option value="2">初审不通过</option>
                <option value="3">复审不通过</option>
                <option value="4">复审通过</option>
                <option value="5">已发标</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_status" />
            </div>
        </div>
          <div class="diy_select">
              <input id="hdloanUse" name="" class="diy_select_input" type="hidden"/>
              <div style="width: 4px; float: left;">
                  <img src="/images/input_left.png" width="4" height="29" alt="" />
              </div>
              <div id="divLoanUse" class="diy_select_txt" style="width: 80px; float: left;">--借款用途--</div>
              <div class="diy_select_btn">&nbsp;</div>
              <ul id="ulLoanUse" style="display: none;" class="diy_select_list">
              </ul>
          </div>
          <div class="diy_select">
              <input id="hdProvince" name="" class="diy_select_input" type="hidden"/>
              <div style="width: 4px; float: left;">
                  <img src="/images/input_left.png" width="4" height="29" alt="" />
              </div>
              <div id="divProvince" class="diy_select_txt" style="width: 70px; float: left;">--省份--</div>
              <div class="diy_select_btn">&nbsp;</div>
              <ul id="ulProvince" style="display: none;" class="diy_select_list">
              </ul>
          </div>
          <div class="diy_select">
              <input id="hdCity" name="" class="diy_select_input" type="hidden" />
              <div style="width: 4px; float: left;">
                  <img src="/images/input_left.png" width="4" height="29" alt="" />
              </div>
              <div id="divCity" class="diy_select_txt" style="width: 70px; float: left;">--城市--</div>
              <div class="diy_select_btn"></div>
              <ul id="ulCity" style="display: none;" class="diy_select_list">
              </ul>
          </div>
        <span class="fl" style="margin-top:10px;">申请时间：</span>
          <div class="inputText fl">
          <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
          </div>
          <input id="txtStartTime" class="input_text" type="text" onclick="WdatePicker()" style="width:100px" runat="server" />
          <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
          </div><span class="fl" style="margin-top:5px;">~</span><div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
          </div>
          <input id="txtEndTime" class="input_text" type="text" onclick="WdatePicker()" style="width:100px" runat="server" />
          <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
          </div></div>
          <div style="float: left; margin-left: 5px;" class="inputText"><input type="button" id="btnSearch" value="查 询" runat="server" class="inputButton" />
              <asp:Button ID="ExcelExport" runat="server" Text="导出Excel" CssClass="inputButton" Width="100" OnClick="ExcelExport_Click" />
               </div><%--<div style="float: left; margin-left: 5px;" class="inputText"><input type="button" id="btnOutput" value="导 出" class="inputButton" /></div>--%>
          </div>  
             
      <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left;">
        <div id="jqxgrid">

        </div>
    </div>
    <script src="/js/selectDiv.js"></script>

    </form>
    <script type="text/javascript">
        function getLoanUseListByHandler(ulLoanUse, divLoanUse, hdLoanUse) {
            $.ajax({
                type: "POST",
                async: false, 
                contentType: "application/json; charset=utf-8",
                url: "/HanderAshx/p2p/DimLoanUse.ashx",
                dataType: "json",
                success: function (context) {
                    if (context.length > 0) {
                        ulLoanUse.append("<li index=''>--借款用途--</li>");
                        $(context).each(function () {
                            if (hdLoanUse) {
                                if (this["ID"] == hdLoanUse.val()) {//初始化  
                                    divLoanUse.html(this["LoanUseName"]);
                                    ulLoanUse.append("<li index='" + this["ID"] + "' class='selected'>" + this["LoanUseName"] + "</li>");
                                } else {
                                    ulLoanUse.append("<li index='" + this["ID"] + "'>" + this["LoanUseName"] + "</li>");
                                }
                            }
                        });
                    }
                },
                error: function (XMLHttpRequest, textStatus) {
                    alert("error");
                },
                complete: function (x) {
                }
            });
        }
    </script>
    <script src="../js/select2css.js"></script>
     
</body>
</html>
