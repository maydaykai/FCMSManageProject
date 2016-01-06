<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberAccountManage.aspx.cs" Inherits="WebUI.MemberManage.MemberAccountManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.aggregates.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.edit.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.export.js"></script> 
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.export.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>

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
        .selectDiv2 .select_box {
        width: 105px;
        }
    </style>
    <script type="text/javascript">
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

            //数据源
            var source = {
                url: '/HanderAshx/MemberManage/MemberAccountManageHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'MemberName', type: 'string' },
                    { name: 'RealName', type: 'string' },
                    { name: 'Mobile', type: 'string' },
                    { name: 'Balance', type: 'number' },
                    { name: 'FreezeAmount', type: 'number' },
                    { name: 'AccountPayable', type: 'number' },
                    { name: 'AccountPayableDate', type: 'number' },
                    { name: 'IdentityCard', type: 'string' },
                    { name: 'ProvinceCity', type: 'string' },
                    { name: 'regtime', type: 'date' },
                    { name: 'RecMemberName', type: 'string' },
                    { name: 'RecRealName', type: 'string' },
                    { name: 'RecMobile', type: 'string' },
                    { name: 'Address', type: 'string' }
                ],
                pagesize: 20,
                formatdata: function(data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'M.Balance';
                    data.sortorder = data.sortorder || 'DESC';
                    data.sUserType = $("#selSUserType").val() || "0";
                    data.uName = encodeURI($("#txtName").val()) || "";
                    data.balance = $("#txtBalance").val() || 0;
                    data.accountPayable = $("#txtAccountPayable").val() || 0;
                    data.accountPayableMax = $("#txtAccountPayableMax").val() || 0;
                    data.dateEnd = $("#txtTotalDate").val() || "";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function (column, direction) { $("#jqxgrid").jqxGrid('updatebounddata', 'sort');  },
                beforeprocessing: function (data) { source.totalrecords = data.TotalRows;  }
                
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
                width: 1200,
                //sortable: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                showaggregates: true,
                showstatusbar: true,
                statusbarheight: 50,
                pagesizeoptions: [ '20', '30', '100', '500'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'MemberName', text: '<b>会员名</b>', width: 110, cellsalign: 'center', align: 'center' },
                    { dataField: 'RealName', text: '<b>真实姓名</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'Mobile', text: '<b>手机号码</b>', width: 110, cellsalign: 'center', align: 'center' },
                    { dataField: 'Balance', text: '<b>可用余额</b>', width: 120, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'FreezeAmount', text: '<b>冻结金额</b>', width: 120, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'AccountPayable', text: '<b>待收额度</b>', width: 120, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'AccountPayableDate', text: '<b>到截止日待收额度</b>', width: 120, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'IdentityCard', text: '<b>证件号码</b>', width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'ProvinceCity', text: '<b>证件省市</b>', width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'regtime', text: '<b>注册时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'RecMemberName', text: '<b>推荐人会员名</b>', width: 110, cellsalign: 'center', align: 'center' },
                    { dataField: 'RecRealName', text: '<b>推荐人真实姓名</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'RecMobile', text: '<b>推荐人手机号码</b>', width: 110, cellsalign: 'center', align: 'center' },
                    { dataField: 'Address', text: '<b>会员通讯地址</b>', width: 300, cellsalign: 'center', align: 'center' }
                ]
            });

           
            $("#btnOutput").jqxButton({ theme: theme });
            $("#btnOutput").click(function () {
                $("#jqxgrid").jqxGrid('exportdata', 'xls', 'jqxgrid');
            });
        });
    </script>
</head>
<body>
    <div id="searchbar" class="selectDiv" style="min-width:1000px;">
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="selSUserType" id="selSUserType" class="fl" runat="server">
            <option value="0">会员名</option>
            <option value="1">真实姓名</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selSUserType" />
        </div>
        <div style="float: left;">
            <input id="txtName" type="text" name="txtName" value="" class="input_text fl" maxlength="20" style="width: 100px;" runat="server" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
        </div>
        <span class="fl">可用余额大于等于：</span>
        <div style="width: 4px; float: left;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input type="text" id="txtBalance" name="input" class="fl input_text" style="width: 80px;" runat="server"/>
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl"> 待收额度大于等于：</span>
        <div style="width: 4px; float: left;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input type="text" id="txtAccountPayable" name="input" class="fl input_text" style="width: 80px;" runat="server"/>
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl">待收额度小于：</span>
        <div style="width: 4px; float: left;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input type="text" id="txtAccountPayableMax" name="input" class="fl input_text" style="width: 80px;" runat="server"/>
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl"> 待收截止日期(含)：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input id="txtTotalDate" class="input_text" type="text" onclick="WdatePicker()" style="width:80px" runat="server" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <div style="float: left; margin-left: 5px;">
            <input type="button" id="applyfilter" value="查 询" class="inputButton" />
        </div>
        <div style="float: left; margin-left: 5px;">
            <input type="button" id="btnOutput" value="导 出" class="inputButton" runat="server" />
        </div>
    </div>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid">
        </div>
    </div>
</body>
</html>
