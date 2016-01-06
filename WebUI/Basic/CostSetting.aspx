<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CostSetting.aspx.cs" Inherits="WebUI.Basic.CostSetting" %>

<%@ Import Namespace="ManageFcmsCommon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设置收费比例</title>
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
                url: '/HanderAshx/Basic/CostSettingHander.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'Id', type: 'int' },
                    { name: 'CostVersionId', type: 'int' },
                    { name: 'FeeType', type: 'int' },
                    { name: 'CalculationMode', type: 'int' },
                    { name: 'CalInitialValue', type: 'number' },
                    { name: 'CalInitialProportion', type: 'number' },
                    { name: 'IncreasingMode', type: 'int' },
                    { name: 'IncreasUnit', type: 'number' },
                    { name: 'IncreasProportion', type: 'number' },
                    { name: 'EnableStatus', type: 'bool' },
                    { name: 'CreateTime', type: 'date' },
                    { name: 'UpdateTime', type: 'date' },
                    { name: 'FeeTypeTitle', type: 'string' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'FeeType';
                    data.sortorder = data.sortorder || 'asc';
                    data.feeType = $("#selFeeType").val() || "";
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
                var parm = column + "=" + value + "&columnId=<%=ColumnId%>";
                var link = "<a href='CostSettingEdit.aspx?" + parm + "'  target='_self' style='margin-left:5px;height:25px;line-height:25px;'>修改</a>";
                var html = $.jqx.dataFormat.formatlink(link);
                return html;
            };

            var checkBoxBool = function (row, column, value) {
                var html = "<div style='margin-left:20px;height:28px; line-height:28px;'><span class='iconCheckBox iconCheckBox-" + value + "' style='*margin-top:5px;'>&nbsp;</span></div>";
                return html;
            };

            var calMode = function (row, column, value) {
                var html = (value == 0) ? "<span style='height:25px; line-height:25px;margin-left:15px;color:#008000;'>天</span>" : value == 1 ? "<span  style='height:25px; line-height:25px;margin-left:15px;color:#ff0000;'>月</span>" : "<span  style='height:25px; line-height:25px;margin-left:15px;'>元</span>";
                return html;
            };

            var inMode = function (row, column, value) {
                var html = (value == 0) ? "<span style='height:25px; line-height:25px;margin-left:15px;'>固定比例</span>" : value == 1 ? "<span  style='height:25px; line-height:25px;margin-left:15px;'>以后按固定比例</span>" : "<span  style='height:25px; line-height:25px;margin-left:15px;'>以后按递增比例</span>";
                return html;
            };


            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1110,
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                    var addButton = $("<div style='float: left; margin-left: 5px; cursor:pointer;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>增加</span></div>");
                    container.append(addButton);
                    statusbar.append(container);
                    addButton.jqxButton({ width: 60, height: 20 });
                    addButton.click(function (event) {
                        window.location.href = "CostSettingEdit.aspx?columnId=<%=ColumnId%>";
                    });
                },
                showstatusbar: true,
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
                    { dataField: 'Id', text: '<b>修改</b>', width: 50, cellsrenderer: linkrenderer },
                    { dataField: 'FeeTypeTitle', text: '<b>费用类型</b>', width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'CalInitialValue', columngroup: 'CalMode', text: '初始值', cellsformat: "F2", width: 120, cellsalign: 'right', align: 'center' },
                    { dataField: 'CalculationMode', columngroup: 'CalMode', text: '单位', cellsrenderer: calMode, width: 50, cellsalign: 'center', align: 'center' },
                    { dataField: 'CalInitialProportion', columngroup: 'CalMode', text: '比例', cellsformat: "P2", width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'IncreasingMode', text: '增加方式', columngroup: 'InMode', cellsrenderer: inMode, width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'IncreasUnit', text: '增加初始值', columngroup: 'InMode', cellsformat: "F2", width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'IncreasProportion', text: '增加比例', columngroup: 'InMode', cellsformat: "P2", width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'EnableStatus', text: '<b>是否启用</b>', width: 70, cellsrenderer: checkBoxBool, cellsalign: 'center', align: 'center' },
                    { dataField: 'UpdateTime', text: '<b>更新时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 160, cellsalign: 'center', align: 'center' }
                ],
                columngroups:
                [
                    { text: '<b>计算方式</b>', align: 'center', name: 'CalMode' },
                    { text: '<b>增加方式</b>', align: 'center', name: 'InMode' }
                ]
            });

            $("#applyfilter").click(function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
            });

        });
    </script>
</head>
<body>
    <div class="selectDiv">
        <div>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" />
            </div>
            <select name="selFeeType" id="selFeeType" class="fl" runat="server">
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="/images/select_right.png" width="31" height="29" alt="" id="img_selFeeType" />
            </div>
            <div style="width: 100px; margin-left:5px;" class="fl">
                    <input type="button" id="applyfilter" value="查 询" class="inputButton" />
            </div>
        </div>
        
    </div>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; clear:both;">

        <div id="jqxgrid">
        </div>
    </div>
</body>
</html>

