<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberListSelect.aspx.cs" Inherits="WebUI.MemberManage.MemberListSelect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>选择会员</title>
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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.selection.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxpanel.js"></script>
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>
    <script src="../js/jquery.query.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <style type="text/css">
        #searchbar span { height: 29px; text-indent: 5px; line-height: 29px; }
        .selectDiv .select_box { width: 85px; }
        .selectDiv2 .select_box { width: 105px; }
    </style>
    <script type="text/javascript">
        $(function () {

            var api = frameElement.api, W = api.opener;

            $("#applyfilter").click(function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
            });
            var branchCompanyId = $.query.get("branchCompanyId");

            $("#jqxgrid").on('rowclick', function (event) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', event.args.rowindex);
                if (branchCompanyId == "") {
                    if (W.document.getElementById('txtMemberName') != null)
                        W.document.getElementById('txtMemberName').value = data.MemberName;
                    if (W.document.getElementById('txtMemberID') != null)
                        W.document.getElementById('txtMemberID').value = data.ID;
                    if (W.document.getElementById('txtType') != null)
                        W.document.getElementById('txtType').value = data.Type;
                    if (W.document.getElementById('txtRealName') != null)
                        W.document.getElementById('txtRealName').value = data.RealName;
                    if (typeof (eval(W.SelectFun)) == "function") {
                        W.SelectFun();
                    }
                } else {
                    if (W.document.getElementById('txtMemberName2') != null)
                        W.document.getElementById('txtMemberName2').value = data.MemberName;
                    if (W.document.getElementById('txtMemberID2') != null)
                        W.document.getElementById('txtMemberID2').value = data.ID;
                    if (typeof (eval(W.SelectFun2)) == "function") {
                        W.SelectFun2();
                    }
                }
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


            /* 
            将数值四舍五入后格式化. 
            @param num 数值(Number或者String) 
            @param cent 要保留的小数位(Number) 
            @param isThousand 是否需要千分位 0:不需要,1:需要(数值类型); 
            @return 格式的字符串,如'1,234,567.45' 
            @type String 
            */
            function formatNumber(num, cent, isThousand) {
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num))//检查传入数值为数值类型. 
                    num = "0";
                if (isNaN(cent))//确保传入小数位为数值型数值. 
                    cent = 0;
                cent = parseInt(cent);
                cent = Math.abs(cent);//求出小数位数,确保为正整数. 
                if (isNaN(isThousand))//确保传入是否需要千分位为数值类型. 
                    isThousand = 0;
                isThousand = parseInt(isThousand);
                if (isThousand < 0)
                    isThousand = 0;
                if (isThousand >= 1) //确保传入的数值只为0或1 
                    isThousand = 1;
                var sign = (num == (num = Math.abs(num)));//获取符号(正/负数) 
                //Math.floor:返回小于等于其数值参数的最大整数 
                num = Math.floor(num * Math.pow(10, cent) + 0.50000000001);//把指定的小数位先转换成整数.多余的小数位四舍五入. 
                var cents = num % Math.pow(10, cent); //求出小数位数值. 
                num = Math.floor(num / Math.pow(10, cent)).toString();//求出整数位数值. 
                cents = cents.toString();//把小数位转换成字符串,以便求小数位长度. 
                while (cents.length < cent) {//补足小数位到指定的位数. 
                    cents = "0" + cents;
                }
                if (isThousand == 0) //不需要千分位符. 
                    return (((sign) ? '' : '-') + num + '.' + cents);
                //对整数部分进行千分位格式化. 
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' +
                    num.substring(num.length - (4 * i + 3));
                return (((sign) ? '' : '-') + num + '.' + cents);
            }

            //数据源
            var source = {
                url: '/HanderAshx/MemberManage/MemberManageHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'RealName', type: 'string' },
                    { name: 'Mobile', type: 'string' },
                    { name: 'Email', type: 'string' },
                    { name: 'IsLocked', type: 'string' },
                    { name: 'Balance', type: 'number' },
                    { name: 'Type', type: 'int' },
                    { name: 'TypeStr', type: 'string' },
                    { name: 'CompleStatus', type: 'string' },
                    { name: 'RegTime', type: 'date' },
                    { name: 'LastLoginTime', type: 'date' },
                    { name: 'MemberLevel', type: 'int' }
                ],
                pagesize: 10,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 10;
                    data.sortdatafield = data.sortdatafield || 'P.RegTime';
                    data.sortorder = data.sortorder || 'DESC';
                    data.sUserType = $("#selSUserType").val() || "0";
                    data.uName = encodeURI($("#txtName").val()) || "";
                    data.isLock = $("#selIsLock").val() || "";
                    data.type = $("#selType").val() || "";
                    data.compleStatus = $("#selCompleStatus").val() || "";
                    data.isVip = $("#selIsVIP").val() || "";
                    data.branchCompanyId = branchCompanyId == "" ? "-1" : "0";
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

            var linkrenderer = function (row, column, value) {

                var link = "暂无分类";
                switch (value) {
                    case '平台账户':
                        link = "<span style='color:#ff0000;line-height:25px;margin-left:30px;'>平台账户<span>";
                        break;
                    case '个人会员':
                        link = "<span style='line-height:25px;margin-left:30px;'>个人会员<span>";
                        break;
                    case '企业会员':
                        link = "<span style='line-height:25px;margin-left:30px;'>企业会员<span>";
                        break;
                    case '担保公司':
                        link = "<span style='color:#D15FEE;line-height:25px;margin-left:30px;'>担保公司<span>";
                        break;
                    case '回购公司':
                        link = "<span style='color:#D15FEE;line-height:25px;margin-left:30px;'>回购公司<span>";
                        break;
                }
                var html = $.jqx.dataFormat.formatDate(link);
                return html;
            };

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 570,
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
                    { dataField: 'MemberName', text: '<b>会员名</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'RealName', text: '<b>真实姓名</b>', width: 110, cellsalign: 'center', align: 'center' },
                    { dataField: 'Mobile', text: '<b>手机号码</b>', width: 110, cellsalign: 'center', align: 'center' },
                    { dataField: 'Balance', text: '<b>可用余额（¥）</b>', width: 120, cellsalign: 'right', align: 'center', cellsformat: "F2", cellsrenderer: formatData },
                    { dataField: 'TypeStr', text: '<b>会员类型</b>', width: 110, cellsalign: 'center', align: 'center', cellsrenderer: linkrenderer }
                ]
            });
        });
    </script>
</head>
<body>
    <div id="searchbar" class="selectDiv" style="width: 570px; margin: 0 auto;">
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="selSUserType" id="selSUserType" class="fl" runat="server">
            <option value="0">会员名</option>
            <option value="1">真实姓名</option>
            <option value="2">手机号码</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selSUserType" />
        </div>
        <div style="float: left;">
            <input id="txtName" type="text" name="txtName" value="" class="input_text fl" maxlength="20" style="width: 150px;" runat="server" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
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
