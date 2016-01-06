<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarketingPerson.aspx.cs" Inherits="WebUI.UserMarketing.Marketing_Person.MarketingPerson" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>业务员信息</title>
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


    <script src="../../js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="../../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../../js/lhgdialog/ShowDialog.js"></script>
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
                url: '/HanderAshx/UserMarketing/PersonHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'Name', type: 'string' },
                    { name: 'Sex', type: 'string' },
                    { name: 'Mobile', type: 'string' },
                    { name: 'Age', type: 'int' },
                    { name: 'CreateTime', type: 'string' },
                    { name: 'ExpandToXML', type: 'string' },
                    { name: 'MemberId', type: 'string' },
                    { name: 'RoleName', type: 'string' },
                    { name: 'IsDel', type: 'int' }


                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'ID';
                    data.sortorder = data.sortorder || 'desc';
                    //data.filterscount = data.filterscount || 0;
                    data.uname = encodeURI($("#userName").val()) || "";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                //filter: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'filter'); },
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
                //删除之后不能分配角色
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                if (parseInt(data.IsDel) != 1) {
                    var link = "<a href='/UserMarketing/Marketing_User/Role_Distribution.aspx?" + parm + "'>分配角色</a> | <a href='javascript:void(0)' id='del' onclick='DelInfo(" + value + ")'> 删除</a>";
                    var html = $.jqx.dataFormat.formatlink(link);
                    return html;
                }
                else {
                    var link = "";
                    var html = $.jqx.dataFormat.formatlink(link);
                    return html;
                }
               
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

            var isSex = function (row, column, value) {
                var strHtml = '<div style="text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;">';
                if (value) {
                    strHtml += "男";
                } else {
                    strHtml += "女";
                }
                strHtml += "</div>";
                return strHtml;
            };
            var isDel = function (row, column, value) {
                var strHtml = '<div style="text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;">';
                if (value==1) {
                    strHtml += "已删除";
                } else {
                    strHtml += " ";
                }
                strHtml += "</div>";
                return strHtml;
            };


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
                //filterable: true,
                //showfilterrow: true,//过滤
                rendergridrows: function (args) {
                    return args.data;
                },
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                    var addButton = $("<div style='float: left; margin-left: 5px; cursor:pointer;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>同步营销人员</span> </div>");
                    container.append(addButton);
                    statusbar.append(container);
                    addButton.jqxButton({ width: 100, height: 20 });
                    addButton.click(function (event) {
                        // 
                        AddMarketing_Ex_Person();
                    });
                },
                showstatusbar: true,
                showtoolbar: true,
                rendertoolbar: function (toolbar) {
                    var me = this;
                    var container = $("<div style='margin: 5px;'></div>");
                    var span = $("<span style='float: left; margin-top: 5px; margin-right: 4px;'>用户名称: </span>");
                    var input = $("<input class='jqx-input jqx-widget-content jqx-rc-all' id='userName' type='text' style='height: 23px; float: left; width: 223px;' /> ");
                    toolbar.append(container);
                    container.append(span);
                    container.append(input);
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
                        { text: '<b>操作</b>', dataField: 'ID', width: 120, cellsalign: 'center', align: 'center', cellsrenderer: linkrenderer },
                        { text: '<b>名称</b>', dataField: 'Name', width: 120, cellsalign: 'center', align: 'center' },
                        { text: '<b>性别</b>', dataField: 'Sex', width: 150, cellsalign: 'center', align: 'center', cellsrenderer: isSex },
                        { text: '<b>手机号码</b>', dataField: 'Mobile', width: 150, cellsalign: 'center', align: 'center' },
                        { text: '<b>年龄</b>', dataField: 'Age', width: 150, cellsalign: 'center', align: 'center' },
                        { text: '<b>角色</b>', dataField: 'RoleName', width: 150, cellsalign: 'center', align: 'center' },
                        { text: '<b>状态</b>', dataField: 'IsDel', width: 150, cellsalign: 'center', align: 'center', cellsrenderer: isDel },
                        { text: '<b>创建时间</b>', dataField: 'CreateTime', width: 150, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' }
                      
                       
                ]
            });

        });
    </script>
    <script type="text/javascript">
        //同步营销人员
        function AddMarketing_Ex_Person()
        {
            $.ajax({
                type: "GET",
                async: true,
                contentType: "application/json; charset=utf-8",
                url: "/HanderAshx/UserMarketing/SynchronizeHandler.ashx?mouth='2015-10'",
                dataType: "json",
                cache: false,
                success: function (resultData) {
                    alert("同步完成");
                },
                error: function (xmlHttpRequest) {
                    alert(xmlHttpRequest.innerText);
                },
                complete: function (x) {
                }
            });

        }

        //删除信息
        function DelInfo(Id)
        {
            $.ajax({
                type: "GET",
                async: true,
                contentType: "application/json; charset=utf-8",
                url: "/HanderAshx/UserMarketing/DelPersonHandler.ashx?id="+Id,
                dataType: "json",
                cache: false,
                success: function (resultData) {
                    alert("删除成功");
                },
                error: function (xmlHttpRequest) {
                    alert(xmlHttpRequest.innerText);
                },
                complete: function (x) {
                }
            });

        }
    </script>
</head>
<body>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left;">
        <div id="jqxgrid" >
        </div>
    </div>
</body>
</html>

