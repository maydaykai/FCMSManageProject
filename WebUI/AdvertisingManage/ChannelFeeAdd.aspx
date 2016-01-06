<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelFeeAdd.aspx.cs" Inherits="WebUI.AdvertisingManage.ChannelFeeAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增渠道费用</title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/My97DatePicker/WdatePicker.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="../js/select2css.js"></script>
    <script src="/js/Common.js"></script>
    <script type="text/javascript">
        var _date = "1";
        var id = 0;
        var returnUrl = "";
        var originalDayFee = 0;
        $(function () {
            /*是否修改*/
            if (Request["id"] != "" && Request["id"] != undefined)
                id = Request["id"];
            var cid = Request["cid"];
            var dayTime = Request["date"];
            if (cid != "" && cid != undefined && dayTime != "" && dayTime != undefined)
            {
                $("#sel_channel").val(cid);
                $("#txtStartDate").val(dayTime)
                getChannelFee();

                $("#sel_channel").attr("disabled", "disabled");
                $("#txtStartDate").attr("disabled", "disabled");
                $(".set").each(function () {                    
                    $(this).attr("src", $(this).attr("_src"));
                    $(this).attr("class", $(this).attr("_class"));
                });
            }

            /*返回*/
            returnUrl = ((id == "" || id == undefined) && (cid == "" || cid == undefined)) ? "ChannelFeeManage.aspx?columnId=<%=ColumnId%>" : "ChannelFeeDetails.aspx?cid=" + Request["cid"] + "&st=" + Request["st"] + "&et=" + Request["et"] + "&columnId=<%=ColumnId%>";
            $("input[name=btnBack]").click(function () {
                window.location.href = returnUrl;
            });

            /*选中日期后进行查询*/
            $("#txtStartDate").blur(function () {
                $(".open").unbind("click");
                $(".open").click(function () {
                    getChannelFee();
                });
                if ($(this).val() != "" && $(this).val() != _date)
                    _date = $(this).val();
                    getChannelFee();
            });

            /*当天费用填写后其他金额累加*/
            $("#txtDayFee").blur(function () {
                var dayfee = parseFloat($(this).val().trim());
                var _zhou = $("#txtZhouFee");
                var _month = $("#txtMonthFee");
                var _sum = $("#txtFeeSum");
                _zhou.val(parseFloat(_zhou.val().trim()) + dayfee);
                _month.val(parseFloat(_month.val().trim()) + dayfee);
                _sum.val(parseFloat(_sum.val().trim()) + dayfee);
            });

            /*绑定提交事件*/
            $("#Audit_Btn").click(function () { dataSave(); });
        });

        /*获取费用详情*/
        function getChannelFee()
        {
            $.ajax({
                type: "GET",
                url: "/HanderAshx/AdvertisingManage/ChannelFeeHandler.ashx?t=get",
                data: { "startDate": "" + $("#txtStartDate").val() + "", "channelId": $("#sel_channel").val(), "id": id },
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    if (result != null && result.TotalRows > 0) {
                        var jsondatas = result.Rows[0];
                        $("#txtDayFee").val(jsondatas.dayFee);
                        $("#txtZhouFee").val(jsondatas.zhouFee);
                        $("#txtMonthFee").val(jsondatas.monthFee);
                        $("#txtFeeSum").val(jsondatas.feeSum);
                        originalDayFee = jsondatas.dayFee;
                    }
                    else {
                        $("#txtDayFee").val(0);
                        $("#txtZhouFee").val(0);
                        $("#txtMonthFee").val(0);
                        $("#txtFeeSum").val(0);
                    }
                }
            });
        }

        /*保存数据*/
        function dataSave() {
            if (!verification())
                return;
            var jsonData = {
                channelID: $("#sel_channel").val().trim(),
                dayFee: $("#txtDayFee").val().trim(),
                zhouFee: $("#txtZhouFee").val().trim(),
                monthFee: $("#txtMonthFee").val().trim(),
                feeSum: $("#txtFeeSum").val().trim(),
                createTime: $("#txtStartDate").val().trim(),
                id: id,
                originalDayFee: originalDayFee
            };
            var url = (id == "" || id == undefined) ? "/HanderAshx/AdvertisingManage/ChannelFeeHandler.ashx?t=add" : "/HanderAshx/AdvertisingManage/ChannelFeeHandler.ashx?t=set";
 
            $.post(url, jsonData, function (result)
            {
                if (result.Result > 1)
                    result.Result = 1;
                switch (result.Result)
                {
                    case -1:
                        MessageAlert('已有重复日期数据!', 'warning', '');
                        break;
                    case 0:
                        MessageAlert('数据提交失败!', 'error', '');
                        break;
                    case 1:
                        MessageAlert('数据提交成功!', 'success', returnUrl);
                        break;
                }
            }, "json");
        }

        /*验证*/
        function verification()
        {
            if ($("#sel_channel").val().trim() == "-1")
            {
                MessageAlert('请选择渠道!', 'warning', '');
                return false;
            }

            if ($("#txtStartDate").val().trim() == "")
            {
                MessageAlert('请选择日期!', 'warning', '');
                return false;
            }

            if ($("#txtDayFee").val().trim() == "" || isNaN($("#txtDayFee").val().trim())) {
                window.MessageAlert('请输入正确金额!', 'warning', '');
                return false;
            }

            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" class="editTable">            
            <tr>
                <td style="text-align: right">渠道类型：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="/images/input_left.png" class="set" _src="/images/gray_left.png" width="4" height="29" alt="" />
                        </span>
                        <asp:DropDownList name="sel_channel" ID="sel_channel" runat="server" class="fl" ClientIDMode="Static">
                        </asp:DropDownList>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <img src="/images/select_right.png" class="set" _src="/images/gray_icon.png" width="31" height="29" alt="" id="img_sel_channel" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 150px;">新增时间：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" class="set" _src="/images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtStartDate" class="input_text set" _class="input_Disabled fl" type="text" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" style="width: 150px" />
                        <div class="fl">
                            <img src="../images/input_right.png" class="set" _src="/images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">当天费用：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtDayFee" type="text" value="0" class="input_text" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                        <span class="fl" style="line-height: 29px;">&nbsp;元</span>
                    </div>
                </td>
            </tr>
            <tr style="display:none;">
                <td style="text-align: right">本周费用：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtZhouFee" type="text" value="0" class="input_text" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                        <span class="fl" style="line-height: 29px;">&nbsp;元</span>
                    </div>
                </td>
            </tr>
            <tr style="display:none;">
                <td style="text-align: right">本月费用：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtMonthFee" type="text" value="0" class="input_text" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                        <span class="fl" style="line-height: 29px;">&nbsp;元</span>
                    </div>
                </td>
            </tr>
            <tr style="display:none;">
                <td style="text-align: right">累计费用：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtFeeSum" type="text" value="0" class="input_text" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                        <span class="fl" style="line-height: 29px;">&nbsp;元</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left; padding-left: 5px;">
                    <input id="Audit_Btn" type="button" value="提交" class="inputButton" style="width: 60px;" />
                    &nbsp;&nbsp<input type="button" name="btnBack" value="返 回" class="inputButton" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>