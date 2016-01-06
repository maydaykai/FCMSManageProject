<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnterpriseAuthtManage.aspx.cs" Inherits="WebUI.MemberManage.EnterpriseAuthtManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>企业信息认证管理</title>
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

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
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
                url: '/HanderAshx/MemberManage/EnterpriseAuthtHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'MemberID', type: 'int' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'EnterpriseName', type: 'string' },
                    { name: 'RegistrationNo', type: 'string' },
                    { name: 'OrganizationCode', type: 'string' },
                    { name: 'LegalName', type: 'string' },
                    { name: 'AuthentNumber', type: 'int' },
                    { name: 'AuthentResult', type: 'int' },
                    { name: 'ApplyTime', type: 'date' },
                    { name: 'UpdateTime', type: 'date' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'P.ID';
                    data.sortorder = data.sortorder || 'DESC';
                    data.selType = $("#selType").val() || "";
                    data.uName = encodeURI($("#txtName").val()) || "";
                    data.anthStatus = $("#selAnthStatus").val() || "";
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

            var linkrenderer = function (row, column, value) {
                var parm = "/MemberManage/EnterpriseAuthtEdit.aspx?" + column + "=" + value + "&columnId=" +<%=ColumnId %> +"";
                var link = "<a href='" + parm + "'  target='_self' style='margin-left:5px;height:25px;line-height:25px;'>查看</a>";
                var html = link;
                return html;
            };

            var authentResultString = function (row, column, value) {
                var link = value == "1" ? "<span style='color:#8A2BE2;margin-left:10px;float:left;height:25px;line-height:25px;'>认证通过</span>" : value == "-1" ? "<span style='color:#FF0000;margin-left:10px;height:25px;line-height:25px;'>认证不通过</span>" : "<span style='color:#FF7F00;margin-left:10px;height:25px;line-height:25px;'>未认证</span>";
                var html = link;
                return html;
            };

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1280,
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
                    { dataField: 'ID', text: '<b>操作</b>', width: 40, cellsrenderer: linkrenderer, align: 'center' },
                    { dataField: 'MemberName', text: '<b>会员名</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'EnterpriseName', text: '<b>企业名称</b>', width: 250, cellsalign: 'center', align: 'center' },
                    { dataField: 'RegistrationNo', text: '<b>营业执照注册号</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'OrganizationCode', text: '<b>组织机构代码</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'LegalName', text: '<b>法人代表</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'AuthentNumber', text: '<b>认证次数</b>', width: 80, cellsalign: 'center', align: 'center' },
                    { dataField: 'AuthentResult', text: '<b>认证状态</b>', width: 80, cellsalign: 'center', align: 'center', cellsrenderer: authentResultString, },
                    { dataField: 'ApplyTime', text: '<b>申请时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'UpdateTime', text: '<b>更新时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' }
                ]
            });

            $("#applyfilter").click(function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
            });

        });
    </script>

</head>
<body>
    <div class="selectDiv"  style="min-width:1000px;">
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="selType" id="selType" class="fl" runat="server">
            <option value="0">会员名</option>
            <option value="1">企业名称</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selType" />
        </div>
        <div style="float: left;">
            <input id="txtName" type="text" name="txtName" value="" class="input_text fl" maxlength="20" style="width: 150px;" runat="server" />
            <span class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </span>
        </div>
    </div>
    <div class="selectDiv selectAnthStatus">
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="selAnthStatus" id="selAnthStatus" class="fl" runat="server">
            <option value="">--认证状态--</option>
            <option value="0">未认证</option>
            <option value="1">认证通过</option>
            <option value="-1">认证不通过</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selAnthStatus" />
        </div>
    </div>
    <div style="float: left; margin-left: 5px;">
        <input type="button" id="applyfilter" value="查 询" class="inputButton" />
    </div>

    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid">
        </div>
    </div>

</body>
</html>
<style type="text/css">
    .selectDiv .select_box { width: 62px; }

    .selectAnthStatus .select_box { width: 85px; }
</style>

