<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanStudent_Credits.aspx.cs" Inherits="WebUI.p2p.LoanStudent_Credits" %>

<%@ Register Assembly="ManageFcmsCommon" Namespace="ManageFcmsCommon" TagPrefix="cc1" %>
<%@ Import Namespace="DocumentFormat.OpenXml.Drawing.Spreadsheet" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <link href="../css/icon.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <link href="../css/icon.css" rel="stylesheet" />



    <script src="../js/jQuery/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxlistbox.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxdropdownlist.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxmenu.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.pager.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.sort.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.filter.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.columnsresize.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.selection.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxpanel.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.export.js"></script>
    <script type="text/javascript" src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.export.js"></script>

    <script src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxtabs.js"></script>
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />

    <script src="../js/Area.js"></script>


    <script src="../js/loan/init.js"></script>
    <link href="../css/select.css" rel="stylesheet" />
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <link href="../css/global.css" rel="stylesheet" />

    <script src="../js/jquery.query.js"></script>



    <style type="text/css">
        #searchbar span {
            height: 29px;
            text-indent: 5px;
            line-height: 29px;
        }

        .selectDiv .select_box {
            width: 85px;
        }
    </style>
</head>
<script type="text/javascript">
    $(function () {
        getProvinceListByHandler($("#ulProvince"), $("#divProvince"), "", $("#ulCity"), $("#divCity"), "");

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
        //  //查询条件 会员名称 审核状态 省份 城市  申请时间 结束时间 ExamStatus
        //数据源
        var source = {
            url: '../HanderAshx/p2p/LoanStudent_Credits.ashx',
            cache: false,
            datatype: "json",
            root: 'Rows',
            datafields: [
                { name: 'ID', type: 'int' },
                { name: 'LoanId', type: 'int' },
                { name: 'MemberName', type: 'string' },
                { name: 'RealName', type: 'string' },
                { name: 'Mobile', type: 'string' },
                { name: 'UniversityName', type: 'string' },
                { name: 'AuditRecords', type: 'string' },
                { name: 'stu_ExamStatus', type: 'int' },
                { name: 'LoanAmount', type: 'string' },
                { name: 'CreateTime', type: 'date' },
                { name: 'PName', type: 'string' },
                { name: 'PCity', type: 'string' },
                { name: 'StudentLoanApplyId', type: 'int' },
                { name: 'ExamStatusName1', type: 'string' }



            ],
            pagesize: 20,
            formatdata: function (data) {
                //获取参数
                data.pagenum = data.pagenum || 0;
                data.pagesize = data.pagesize || 20;
                data.sortdatafield = data.sortdatafield || '';
                data.sortorder = data.sortorder || ' a.ID ASC';
                //获取参数
                data.begintime = $("#txt_begin").val();
                data.endtime = $("#txt_end").val();

                data.memberName = $("#txtName").val() || "";
                data.status = $("#selStatus").val() || "";

                // alert($("#selType").val());
                // alert($("#selCompleStatus").val());
                //省份 城市 审核状态
                data.checkstatus = $("#selIsLock").val();

                data.province = $("#hdProvince").val();
                data.status = $("#hdCity").val();

                //data.province = $("#selType").val();
                //alert($('#selCompleStatus option:first').val());
                //if ($("#selCompleStatus").val().length = 0) {
                //    data.city = $('#selCompleStatus option:first').val();
                //}
                //else {
                //    data.city = $("#selCompleStatus").val();
                //}


                formatedData = buildQueryString(data);
                return formatedData;
            },
            sort: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
            beforeprocessing: function (data) { source.totalrecords = data.TotalRows; }
        }

        //数据处理
        var dataadapter = new $.jqx.dataAdapter(source, {
            contentType: "application/json; charset=utf-8",
            loadError: function (xhr, status, error) {
                alert(error);
            }
        });

        var linkrenderer = function (row, column, value) {
            var data = $("#jqxgrid").jqxGrid('getrowdata', row);
            // alert(data.LoanId);
            var link = "";
            var parm = "/p2p/Loan_StudentEdit.aspx?ID=" + value + "&columnId=<%=ColumnId%>" + "&LoanId=" + data.LoanId + "&StudentLoanApplyId=" + data.StudentLoanApplyId;
   

            if (data.ExamStatusName1 != "平台二审中" && data.ExamStatusName1 != "平台初审中") {
                link = "<a href='" + parm + "'  target='_self' style='margin-left:5px;height:25px;line-height:25px;'>查看</a>";
            }
            else {
                link = "<a href='" + parm + "'  target='_self' style='margin-left:5px;height:25px;line-height:25px;'>审核</a>";
            }
            var html = link;
            return html;
        };

        //数据绑定
        $("#jqxgrid").jqxGrid({
            theme: theme,
            source: dataadapter,
            width: 910,
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
                { dataField: 'ID', text: '<b>操作</b>', width: 40, cellsrenderer: linkrenderer, align: 'center', sortable: false },
                { dataField: 'MemberName', text: '<b>会员名称</b>', width: 80, cellsalign: 'center', align: 'center' },
                { dataField: 'RealName', text: '<b>真实姓名</b>', width: 100, cellsalign: 'center', align: 'center' },
                { dataField: 'Mobile', text: '<b>手机号码</b>', width: 100, cellsalign: 'center', align: 'center' },
                { dataField: 'UniversityName', text: '<b>院校名称</b>', width: 100, cellsalign: 'center', align: 'center' },
                { dataField: 'LoanAmount', text: '<b>借款金额</b>', width: 100, cellsalign: 'center', align: 'center' },
                { dataField: 'ExamStatusName1', text: '<b>审核状态</b>', width: 100, cellsalign: 'center', align: 'center' },
                //{ dataField: 'stu_ExamStatus', text: '<b>审批结果</b>', width: 100, cellsalign: 'center', align: 'center' },
                { dataField: 'PName', text: '<b>所在省份</b>', width: 100, cellsalign: 'center', align: 'center' },
                { dataField: 'PCity', text: '<b>所在城市</b>', width: 100, cellsalign: 'center', align: 'center' },
                { dataField: 'CreateTime', text: '<b>申请时间</b>', width: 100, cellsformat: "yyyy-MM-dd", cellsalign: 'center', align: 'center' }
            ]
        });

        //输出数据
        $("#btnOutput").click(function () {

            var data = Object();
            data.pagenum = data.pagenum || 0;
            data.pagesize = data.pagesize || 20;
            data.sortdatafield = data.sortdatafield || '';
            data.sortorder = data.sortorder || ' a.ID ASC';
            //获取参数
            data.begintime = $("#txt_begin").val();
            data.endtime = $("#txt_end").val();

            data.memberName = $("#txtName").val() || "";
            data.status = $("#selStatus").val() || "";
            data.checkstatus = $("#selIsLock").val();
            data.province = $("#hdProvince").val();
            data.status = $("#hdCity").val();

            formatedData = buildQueryString(data);

            window.open("../HanderAshx/p2p/LoanStudent_Credits.ashx?output=1&" + formatedData, "_blank");
        });
    });
</script>
<script type="text/javascript">
    function getCityListFromHandlerByParentID(id, city) {
        //  alert(id);
        var sel_city = city == undefined ? $("#sel_city") : $("#" + city);
        if (id.length <= 0) {
            sel_city.html($("<option>").text("--请选择--").val("").attr("selected", "selected"));
        } else {
            sel_city.empty();
            $.ajax({
                type: "GET",
                async: false,
                contentType: "application/json; charset=utf-8",
                url: "/Handerashx/UserManage/Areahandler.ashx?parentid=" + id,
                dataType: "json",
                cache: false,
                success: function (context) {
                    // alert(context);
                    if (context.length > 0) {
                        $(context).each(function () {
                            sel_city.append($("<option>").text(this["Name"]).val(this["ID"]));
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
    }


</script>
<body>

    <div id="searchbar" class="selectDiv" style="min-width: 1000px;">




        <span class="fl" style="margin-top: 10px;">会员名：</span>

        <div style="float: left;">
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtName" type="text" name="txtName" value="" class="input_text fl" maxlength="20" style="width: 150px;" runat="server" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
        </div>

        <div class="fl selectDiv inputText">
            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="selIsLock" id="selIsLock" class="fl">
                <option value="">--审核状态--</option>
                <option value="1">已审</option>
                <option value="0">未审</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selIsLock" />
            </div>
        </div>

        <div class="diy_select">
            <input id="hdProvince" name="" class="diy_select_input" type="hidden" runat="server" />
            <div style="width: 4px; float: left;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <div id="divProvince" class="diy_select_txt" style="width: 70px; float: left;">--省份--</div>
            <div class="diy_select_btn">&nbsp;</div>
            <ul id="ulProvince" style="display: none;" class="diy_select_list">
            </ul>
        </div>
        <div class="diy_select">
            <input id="hdCity" name="" class="diy_select_input" type="hidden" runat="server" />
            <div style="width: 4px; float: left;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <div id="divCity" class="diy_select_txt" style="width: 70px; float: left;">--城市--</div>
            <div class="diy_select_btn"></div>
            <ul id="ulCity" style="display: none;" class="diy_select_list">
            </ul>
        </div>




        <div style="float: left; margin-left: 5px;">
            申请时间:<input type="text" id="txt_begin" onclick="WdatePicker()" /><input type="text" id="txt_end" onclick="    WdatePicker()" />
        </div>



        <div style="float: left; margin-left: 5px;">
            <input type="button" id="applyfilter" value="查 询" class="inputButton" />
        </div>
        <div style="float: left; margin-left: 5px;">
            <input type="button" id="btnOutput" value="导出" class="inputButton" />
        </div>


        <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </div>
</body>
</html>
<script src="../js/select2css.js"></script>
<script src="/js/selectDiv.js"></script>

