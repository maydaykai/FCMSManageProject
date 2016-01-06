<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelManage.aspx.cs" Inherits="WebUI.AdvertisingManage.ChannelManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>渠道号管理</title>
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
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <script src="../js/FormatDecimal.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="/js/Common.js"></script>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
         
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

            var formatDate = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var parm = "/AdvertisingManage/ChannelAdd.aspx?columnId=<%=ColumnId%>";
                return "<div style='line-height:25px;text-align: center;'><a href='" + parm + "&t=1&id=" + data.dId + "' style='cursor:pointer;' >[渠道修改]</a> &nbsp;&nbsp;<a href='" + parm + "&t=2&id=" + data.id + "' style='cursor:pointer;' >[二级修改]</a> &nbsp;&nbsp;<a href='" + parm + "&t=3&id=" + data.fId + "' style='cursor:pointer;' >[三级修改]</a></div>";
            };

            var formatDate2 = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                return "<div style='line-height:25px;text-align: center;'><a href='javascript:void(0);' onclick='copyUrl(\"channel=" + data.EncryptID + "&channelRemark=" + (data.fId == null ? data.id : data.fId) + "\");' style='cursor:pointer;' >[复制]</a> channel=" + data.EncryptID + "&channelRemark=" + (data.fId == null ? data.id : data.fId) + " </div>";
            };

            //数据源
            var source = {
                url: '/HanderAshx/AdvertisingManage/ChannelManageHandler.ashx?t=list',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'rowNum', type: 'int' },
                    { name: 'dId', type: 'int' },
                    { name: 'EncryptID', type: 'string' },
                    { name: 'id', type: 'int' },
                    { name: 'fId', type: 'int' },
                    { name: 'Channel', type: 'string' },
                    { name: 'classifyName', type: 'string' },
                    { name: 'fClassifyName', type: 'string' }
                ],
                pagesize: 50,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 50;
                    data.secondary = $("#txtSecondary").val() || "";
                    data.channelId = $("#sel_channel").val() || -1;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function (data) {
                    source.totalrecords = data.TotalRows;
                }
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
                width: 1080,
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 2px;'></div>");
                    var addButton = $("<div style='float: left; margin-left: 5px; cursor:pointer;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>新增渠道</span></div>");
                    container.append(addButton);
                    statusbar.append(container);
                    addButton.jqxButton({ width: 120, height: 20 });
                    addButton.click(function (event) {
                        window.location.href = "/AdvertisingManage/ChannelAdd.aspx?t=1&columnId=<%=ColumnId%>";
                    });

                    var container2 = $("<div style='overflow: hidden; position: relative; margin: 2px;'></div>");
                    var addButton2 = $("<div style='float: left; margin-left: 25px; cursor:pointer;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>新增二级分类</span></div>");
                    container.append(addButton2);
                    statusbar.append(container2);
                    addButton2.jqxButton({ width: 120, height: 20 });
                    addButton2.click(function (event) {
                        window.location.href = "/AdvertisingManage/ChannelAdd.aspx?t=2&columnId=<%=ColumnId%>";
                    });

                    var container3 = $("<div style='overflow: hidden; position: relative; margin: 2px;'></div>");
                    var addButton3 = $("<div style='float: left; margin-left: 25px; cursor:pointer;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>新增三级分类</span></div>");
                    container.append(addButton3);
                    statusbar.append(container3);
                    addButton3.jqxButton({ width: 120, height: 20 });
                    addButton3.click(function (event) {
                        window.location.href = "/AdvertisingManage/ChannelAdd.aspx?t=3&columnId=<%=ColumnId%>";
                    });

                    var container4 = $("<div style='overflow: hidden; position: relative; margin: 2px;'></div>");
                    var addButton4 = $("<div style='float: left; margin-left: 25px; cursor:pointer;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>导入渠道分类</span></div>");
                    container.append(addButton4);
                    statusbar.append(container4);
                    addButton4.jqxButton({ width: 120, height: 20 });
                    addButton4.click(function (event) {
                        $('input:file').val("");
                        var ie = !-[1, ];
                        if (ie) {
                            $('input:file').trigger('click').trigger('change');
                        } else {
                            $('input:file').click();
                        }
                    });
                },
                showstatusbar: true,
                sortable: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesizeoptions: ['50', '100', '200'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'rowNum', text: '<b>序号</b>', width: 50, cellsalign: 'center', align: 'center' },
                    { dataField: 'Channel', text: '<b>渠道</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'classifyName', text: '<b>二级分类</b>', width: 160, cellsalign: 'center', align: 'center' },
                    { dataField: 'fClassifyName', text: '<b>三级分类</b>', width: 160, cellsalign: 'center', align: 'center' },
                    { dataField: '', text: '<b>操作</b>', width: 250, cellsalign: 'center', align: 'center', cellsrenderer: formatDate },
                    { dataField: 'A', text: '<b>对应渠道号</b>', width: 600, cellsalign: 'center', align: 'center', cellsrenderer: formatDate2 }
                ]
            });
        });

        /*复制*/
        function copyUrl(txt) {
            window.clipboardData.setData("Text", txt);
            MessageAlert("复制成功!", 'warning', '');
        }
        var fileUrl;
        /*导入选择路径*/
        function fileChange() {
            fileUrl = $('#fileUp').val();
            if (fileUrl != "") {
                var urls = fileUrl.split('.');
                var suffix = urls[urls.length - 1];
                if (suffix == "xls" || suffix == "xlsx")
                    MessageAlertOk('是否开始导入数据?', 'warning', 'btnImport');
                else
                    MessageAlert("文件类型错误!", 'warning', '');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1000px;">
            <span class="fl">分类编号：</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtSecondary" class="input_text" type="text" runat="server" style="width: 150px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <asp:DropDownList name="sel_channel" ID="sel_channel" runat="server" class="fl" ClientIDMode="Static">
            </asp:DropDownList>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_channel" />
            </div>
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="applyfilter" value="查 询" class="inputButton" />
                <input id="ExcelExport1" type="button" value="导出Excel" class="inputButton" style="width: 100px;" runat="server" onserverclick="ExcelExport1_Click" />
                <input type="button" id="btnImport" onserverclick="btnImport_Click" runat="server" style="display:none;" />
            </div>
            <%--<dvi id="Dfile"><input type="file" name="fileupload"  value="" onchange="fileChange();" /></dvi>--%>            
        </div>
        <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
        <asp:FileUpload runat="server" ClientIDMode="Static" ID="fileUp" style="display:none;" onchange="fileChange();" />
    </form>
</body>
</html>