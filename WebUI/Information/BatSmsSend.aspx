<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatSmsSend.aspx.cs" Inherits="WebUI.Information.BatSmsSend" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <link href="../css/table1.css" rel="stylesheet" type="text/css" />

    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    
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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.edit.js"></script>
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <script src="../js/loan/init.js"></script>
    <link href="../css/select.css" rel="stylesheet" />
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        .selectDiv {
            min-width:1350px;
        }
        .selectDiv .select_box {
         width: 85px;
        }
        .selectDiv span {
            margin-top:5px;
        }
         .selectDiv2 .select_box {
         width: 110px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
           
            $("input[name='choose']").bind("click", function () {
                if ($('#radio_no')[0].checked == true) {
                    $("input[id^='ck']").attr("disabled", "disabled");
                    $("input[id^='txt_']").attr("disabled", "disabled");
                } else {
                    $("input[id^='ck']").removeAttr("disabled");
                    $("input[id^='txt_']").removeAttr("disabled");
                }
            });

            $('#btnSearch').bind("click", function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
            });
            $('#textReviewComments').bind("keydown", function () {
                $("#wordCount").text("字数：" + $('#textReviewComments').text().length);
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
                url: '/HanderAshx/Information/BatSmsHistoryHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'SmsContent', type: 'string' },
                    { name: 'SendCount', type: 'int' },
                    { name: 'CreateTime', type: 'date' },
                    { name: 'UserName', type: 'string' }
                ],

                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'CreateTime';
                    data.sortorder = data.sortorder || 'desc';
                    data.totalDate = $("#txtTotalDate").val() || "";
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
                var parm = column + "=" + value;
                var link = "<a href='../p2p/LoanAudit.aspx?" + parm + "&columnId=<%=ColumnId%>'  target='_self' style='margin-left:10px;'>审核</a>";
                var html = $.jqx.dataFormat.formatlink(link);
                return html;
            };
            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 850,
                sortable: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                editable: true,
                pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { text: '<b>ID</b>', dataField: 'ID', width: 80, cellsalign: 'center', align: 'center' },
                    { text: '<b>短信内容</b>', dataField: 'SmsContent', width: 450, cellsalign: 'center', align: 'center' },
                    { text: '<b>发送条数</b>', dataField: 'SendCount', width: 80, cellsalign: 'center', align: 'center' },
                    { text: '<b>操作时间</b>', dataField: 'CreateTime', width: 180, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                    { text: '<b>操作员</b>', dataField: 'UserName', width: 80, cellsalign: 'center', align: 'center' }
                ]
            });

        });
    </script>
    
    <script type="text/javascript">
        $(function() {
            GetSmsBalance();

            $("#btnRefresh").click(function () {
                GetSmsBalance();
            });
        });

        function GetSmsBalance() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/WebService/ThirdPartyInterface.svc/GetSmsBalance",
                dataType: "json",
                beforeSend: function () { $("#eucpSmsBalance").html("<img src=\"../images/Loading_H.gif\" style=\"border: none;height:10px;width:30px;margin-top: 11px; float: left;\" />"); },
                success: function (jsonData) {                   
                    var jsonObj = JSON.parse(jsonData.d);
                    if (jsonObj) {
                        $("#eucpSmsBalance").html(jsonObj.smsBalance+"&nbsp;元");
                    } else {
                        $("#eucpSmsBalance").html("0.00&nbsp;元");
                    }
                },
                error: function (responseText) {
                    alert(responseText.responseText);
                }
            });
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="top boder1px">
	    <div class="top_left boder1px fl">
    	    <h2>选择范围：</h2>
            <div class="option_box">
        	    <span class="o_list"><input id="radio_yes" type="radio" name ="choose" checked="True" runat="server"/>有条件用户</span>
        	    <span class="o_list"><input id="radio_no" type="radio" name ="choose"  runat="server"/>全部用户</span>
            </div>
            <div class="option_box">
                <span class="o_list"><input id="ck_nobid" name="condition" type="checkbox" value="" class="radio" runat="server"/>没有投资记录的新用户</span>
            </div>
            <div class="option_box">
                <span class="o_list"><input id="ck_canusebalance" name="condition" type="checkbox" value="" class="radio" runat="server"/><span>可用余额大于</span><input id="txt_canusebalance" type="text" name="condition" style="width: 80px" value="" class="o_input" runat="server"/><span>元，小于</span><input id="txt_canusebalance1" type="text" name="condition"  style="width: 80px" value="" class="o_input" runat="server"/><span>元的用户</span></span>
            </div>
            <div class="option_box">
                <span class="o_list"><input id="ck_nobidmonth" name="condition" type="checkbox" value="" class="radio" runat="server"/>超过1个月没有投资的用户</span>
            </div>
            <div class="option_box">
                <span class="o_list"><input id="ck_bidamount" name="condition" type="checkbox" value="" class="radio" runat="server"/><span>投资超过</span><input type="text" id="txt_bidamount" name="condition" value="" class="o_input" runat="server"/><span>元的用户</span></span>
            </div>
        </div>
        <div class="top_right fr">
    	    <textarea class="text" rows="10" cols="106" id="textReviewComments" style="width: 400px" runat="server"></textarea>
        </div>
        <div class="clear"></div>
        <div class="query ">
            <span class="fl" style="margin-top:5px; margin-left:5px;">发送时间：</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
           </div>
            <input id="txtSendTime" class="input_text" type="text" onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" style="width:140px;margin-top: 3px;" runat="server"/>
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
           </div>
            <div style="float: left; margin-left: 5px;">
	            <asp:Button ID="button1" runat="server" Text="批量发送" OnClick="Button1_Click" Width="60" CssClass="inputButton" />
                <span id = "wordCount" style="margin-top:5px; margin-left:5px;">字数：0</span>
            </div>
            <span style="margin-left: 20px; height: 32px; line-height: 32px;float:left;">短信账户当前余额：</span><span id="eucpSmsBalance" style="color: #ff0000;height: 32px; line-height: 32px;float:left;">0.00&nbsp;元</span>
            <span style="margin-left: 5px; height: 32px; line-height: 32px;float:left;"><input id="btnRefresh" type="button" value="刷新"  class="inputButton" style="width: 40px;"/></span>
        </div>
    </div>
        
    <div class="date boder1px">
	    <div id="searchbar" class="date_list" style="min-width:1250px;">
    	    <span class="fl" style="margin-top:5px; margin-left:5px;">查询日期：</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
           </div>
           <input id="txtTotalDate" class="input_text" type="text" onclick="WdatePicker()" style="width:100px" />
           <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
           </div>
           <div style="float: left; margin-left: 5px;"><input type="button" id="btnSearch" value="查 询" class="inputButton" /></div>
        </div>
       <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
           
        </div>
        <div class="clear"></div>
    </div>
         
           

    </form>
</body>
</html>
