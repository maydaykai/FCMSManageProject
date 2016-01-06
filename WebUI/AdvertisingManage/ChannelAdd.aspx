<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelAdd.aspx.cs" Inherits="WebUI.AdvertisingManage.ChannelAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增渠道</title>
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
        $(function () {
            /*显示编辑的数据*/
            switch (Request["t"])
            {
                case "1":
                    $("#table1").show();
                    break;
                case "2":
                    $("#table2").show();
                    break;
                case "3":
                    $("#table2").show();
                    $("#tr1").show();
                    break;
            }

            /*是否修改*/
            if (Request["id"] != "" && Request["id"] != undefined)
            {
                id = Request["id"];
                getChannelFee();
                $("#sel_channel").attr("disabled", "disabled");
                $(".set").each(function () {
                    $(this).attr("src", $(this).attr("_src"));
                    $(this).attr("class", $(this).attr("_class"));
                });
            }

            /*返回*/
            returnUrl = "ChannelManage.aspx?columnId=<%=ColumnId%>"
            $("input[name=btnBack]").click(function () {
                window.location.href = returnUrl;
            });

            /*绑定提交事件*/
            $("input[name=Audit_Btn]").click(function () { dataSave(); });
        });

        /*获取渠道详情*/
        function getChannelFee() {
            $.ajax({
                type: "GET",
                async : false,
                url: "/HanderAshx/AdvertisingManage/ChannelManageHandler.ashx?t=get",
                data: { "tp": Request["t"], "id": id },
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    if (result != null && result.TotalRows > 0) {
                        var jsondatas = result.Rows[0];
                        $("#txtChannelName").val(jsondatas.Channel);
                        $("#txtFeeRebate").val(jsondatas.Rebate);
                        $("#sel_channel").val(jsondatas.dId);
                        $("#txtFoxpro").val(jsondatas.classifyName);
                        $("#txtFlyers").val(jsondatas.fClassifyName);
                    }
                    else {
                        $("#txtChannelName").val("");
                        $("#txtFeeRebate").val(0);
                        $("#sel_channel").val(-1);
                        $("#txtFoxpro").val("");
                        $("#txtFlyers").val("");
                    }
                }
            });
        }

        /*保存数据*/
        function dataSave() {
            if (!verification())
                return;

            var url = "";
            if (Request["id"] == "" || Request["id"] == undefined) {
                id = $("#sel_channel").val();
                url = "/HanderAshx/AdvertisingManage/ChannelManageHandler.ashx?t=add";
            }
            else
                url = "/HanderAshx/AdvertisingManage/ChannelManageHandler.ashx?t=set";
            var jsonData = {
                channel: $("#txtChannelName").val().trim(),
                rebate: $("#txtFeeRebate").val().trim(),
                classifyName: $("#txtFoxpro").val().trim(),
                fClassifyName: $("#txtFlyers").val().trim(),
                id: id,
                did: $("#sel_channel").val(),
                tp: Request["t"]
            };

            $.post(url, jsonData, function (result) {
                var msg = result.Result.split('|');
                switch (msg[0]) {
                    case "3":
                        MessageAlert(msg[1], 'warning', '');
                        break;
                    case "2":
                        MessageAlert(msg[1], 'error', '');
                        break;
                    case "1":
                        MessageAlert(msg[1], 'success', returnUrl);
                        break;
                }
            }, "json");
        }

        /*验证*/
        function verification() {
            switch (Request["t"]) {
                case "1":
                    if ($("#txtChannelName").val().trim() == "") {
                        MessageAlert('请输入渠道名称!', 'warning', '');
                        return false;
                    }
                    if ($("#txtFeeRebate").val().trim() == "" || isNaN($("#txtFeeRebate").val().trim())) {
                        MessageAlert('请输入正确费用返点!', 'warning', '');
                        return false;
                    }
                    break;
                case "2":
                    if ($("#sel_channel").val().trim() == "-1") {
                        MessageAlert('请选择渠道!', 'warning', '');
                        return false;
                    }
                    if ($("#txtFoxpro").val().trim() == "") {
                        MessageAlert('请输入二级分类!', 'warning', '');
                        return false;
                    }
                    break;
                case "3":
                    if ($("#sel_channel").val().trim() == "-1") {
                        MessageAlert('请选择渠道!', 'warning', '');
                        return false;
                    }
                    if ($("#txtFoxpro").val().trim() == "") {
                        MessageAlert('请输入二级分类!', 'warning', '');
                        return false;
                    }
                    if ($("#txtFlyers").val().trim() == "") {
                        MessageAlert('请输入三级分类!', 'warning', '');
                        return false;
                    }
                    break;
            }

            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" class="editTable" style="display:none;" id="table1">            
            <tr>
                <td style="text-align: right">渠道名称：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtChannelName" type="text" class="input_text" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">费用返点：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtFeeRebate" type="text" value="0" class="input_text" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                        <span class="fl" style="line-height: 29px;">&nbsp;%</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left; padding-left: 5px;">
                    <input name="Audit_Btn" type="button" value="提交" class="inputButton" style="width: 60px;" />
                    &nbsp;&nbsp<input type="button" name="btnBack" value="返 回" class="inputButton" />
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" class="editTable" style="display:none;" id="table2">            
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
                <td style="text-align: right">二级分类：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtFoxpro" type="text" class="input_text" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr  id="tr1" style="display:none;">
                <td style="text-align: right">三级分类：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtFlyers" type="text" class="input_text" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left; padding-left: 5px;">
                    <input name="Audit_Btn" type="button" value="提交" class="inputButton" style="width: 60px;" />
                    &nbsp;&nbsp<input type="button" name="btnBack" value="返 回" class="inputButton" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
