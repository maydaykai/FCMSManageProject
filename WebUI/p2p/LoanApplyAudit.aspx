<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanApplyAudit.aspx.cs" Inherits="WebUI.p2p.LoanApplyAudit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <script src="../js/jqgrid/js/jquery-1.9.0.min.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="../js/select2css.js"></script>
    
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
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

    <style type="text/css">
        .table_left {
            width: 220px; text-align: right; background-color: #E0ECFF;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            
            $("input[name=btnBack]").click(function () {
                window.location.href = "RechargeManage.aspx?columnId=<%=ColumnId%>";
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

            var formatedData = '';

            //数据源
            var source = {
                url: '/HanderAshx/p2p/LoanApplyProductInfo.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ProductInfoName', type: 'string' },
                    { name: 'ContentInfo', type: 'string' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.loanApplyId = getPValueByName("ID");

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

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 950,
                sortable: false,
                pageable: false,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                        { text: '<b>产品名称</b>', dataField: 'ProductInfoName', width: 150, cellsalign: 'center', align: 'center' },
                        { text: '<b>信息</b>', dataField: 'ContentInfo', width: 800, cellsalign: 'center', align: 'center' }
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table cellpadding="0" cellspacing="0" class="editTable">
          <tr>
             <td style="text-align: right">会员账号：</td>
             <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                <label id ="lab_MemberName" runat="server"></label>
             </td>
          </tr>
          <tr>
             <td style="text-align: right">真实姓名：</td>
             <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                <label id ="lab_RealName" runat="server"></label>
             </td>
          </tr>
          <tr>
            <td style="text-align: right">所在地区：</td>
            <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                <label id ="lab_province" runat="server"></label>
                <label id ="lab_city" runat="server"></label>
            </td>
          </tr>
          <tr>
            <td style="text-align: right">借款用途：</td>
            <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                <label id ="lab_loanUseName" runat="server"></label>
            </td>
          </tr>
          <tr>
              <td style="text-align: right">借款期限：</td>
            <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                <label id ="lab_loanTerm" runat="server"></label>
                <label id="lab_loanModel" runat="server"></label>
            </td>
          </tr>
           <tr>
            <td style="text-align: right">借款额度：</td>
            <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                <label id ="lab_loanAmount" runat="server"></label> 元
            </td>
          </tr>
          <tr>
               <td style="text-align: right">申请时间：</td>
              <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                   <label id ="lab_createTime" runat="server"></label>
              </td>
          </tr>
          <tr>
               <td style="text-align: right">还款来源：</td>
              <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                   <label id ="lab_RepaymentSource" runat="server"></label>
              </td>
          </tr>
          <tr id="CurrorStatus" runat="server" visible="False">
                <td style="text-align: right">当前状态：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <div class="fl">
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                        </div>
                        <select name="selIncreasingMode" id="selIncreasingMode" class="fl" runat="server" style="width: 150px;">
                            <option value="0">初审中</option>
                            <option value="1">复审中</option>
                            <option value="2">初审不通过</option>
                            <option value="3">复审不通过</option>
                            <option value="4">复审通过</option>
                            <option value="5">已发标</option>
                        </select>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <img src="/images/gray_icon.png" width="31" height="29" alt="" id="img_selIncreasingMode" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr id="AuditStatus" runat="server" visible="False">
                <td style="text-align: right">选择审核状态：</td>
                <td >
                    <span style="float: left; height: 25px; line-height: 25px;">
                        <input id="Radio1" name="AuditStatus" type="radio" runat="server" value="1" /></span><span style="float: left; height: 25px; line-height: 25px; margin-top: -2px;">「审核通过」</span>
                    <span style="float: left; height: 25px; line-height: 25px; margin-left: 8px;">
                        <input id="Radio2" name="AuditStatus" type="radio" runat="server" value="2" /></span><span style="float: left; height: 25px; line-height: 25px; margin-top: -2px;">「审核不通过」</span>
                </td>
            </tr>
            <tr id="AreaNote" runat="server" visible="False">
                <td style="text-align: right; vertical-align: top; padding-top: 10px;">审核意见：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px; height: 120px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <textarea id="txtAreaNote" cols="50" rows="5" runat="server" style="color: #1B759F; padding: 5px; height: 100px; width: 300px;"></textarea>
                    </div>
                </td>
            </tr>
            <tr id="AuditReco" runat="server" visible="False">
                <td style="text-align: right">审核记录：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <span id="litAuditReco" runat="server" style="line-height: 30px;"></span>
                </td>
            </tr>
          <tr>                                               
            <td style="text-align: right">&nbsp;</td>
            <td style="text-align: left; padding-left: 5px; padding-top: 5px;"><asp:Button ID="BtnAudit" Text="审核" CssClass="inputButton" runat="server" OnClick="BtnAudit_ServerClick" />&nbsp;&nbsp<input type="button" value="返回" onclick="javascript: history.go(-1)" class="inputButton" /></td>
          </tr>
      </table>
        
    </div>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left;">
        <div id="jqxgrid">

        </div>
    </div>
    </form>
</body>
</html>

