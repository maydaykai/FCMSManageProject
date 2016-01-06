<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelUserAdd.aspx.cs" Inherits="WebUI.AdvertisingManage.ChannelUserAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增渠道商</title>
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
        var id = 0;
        var returnUrl = "";
        $(function () {
            /*是否修改*/
            if (Request["id"] != "" && Request["id"] != undefined) {
                id = Request["id"];
                getChannelFee();
                $("#txtChannelName").attr("disabled", "disabled");                
                $(".set").each(function () {
                    $(this).attr("src", $(this).attr("_src"));
                    $(this).attr("class", $(this).attr("_class"));
                });
            }
            else
                $("#tr1").show();

            /*返回*/
            returnUrl = "ChannelUserManage.aspx?columnId=<%=ColumnId%>";
            $("input[name=btnBack]").click(function () {
                window.location.href = returnUrl;
            });

            /*绑定提交事件*/
            $("#Audit_Btn").click(function () { dataSave(); });
        });

        /*获取费用详情*/
        function getChannelFee() {
            $.ajax({
                type: "GET",
                url: "/HanderAshx/AdvertisingManage/ChannelUserHandler.ashx?t=get",
                data: { "id": id },
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {    
                    if (result != null && result.TotalRows > 0) {
                        var jsondatas = result.Rows[0];
                        $("#txtChannelName").val(jsondatas.ChannelName);
                        $("#txtChannelPwd").val(jsondatas.ChannelPwd);
                        $("#sel_channel").val(jsondatas.DimChannelId);
                        $("#txtContactPerson").val(jsondatas.ContactPerson);
                        $("#txtRegisteredFormula").val(jsondatas.RegisteredFormula);
                        $("#txtInvestFormula").val(jsondatas.InvestFormula);
                        $("#txtRegInvestFormula").val(jsondatas.RegInvestFormula);
                        $("#txtInvestQuota").val(jsondatas.InvestQuota);
                        $("input[name='radio']").each(function () {
                            if ($(this).val() == jsondatas.IsDelete)
                                $(this).attr("checked", "checked");
                        });
                        
                        $("input[name='checkbox']").each(function () {
                            if ((jsondatas.Permissions + ",").indexOf($(this).val() + ",") > -1)
                                $(this).attr("checked", "checked");
                        });
                    }
                }
            });
        }

        /*保存数据*/
        function dataSave() {
            if (!verification())
                return;
            var _permissions="";
            $("input[name='checkbox']:checked ").each(function () {
                _permissions += $(this).val() + ",";
            });

            if (_permissions != "")
                _permissions = _permissions.substring(0, _permissions.length - 1);

            var jsonData = {
                channelName: $("#txtChannelName").val().trim(),
                channelPwd: $("#txtChannelPwd").val().trim(),
                channelId: $("#sel_channel").val().trim(),
                contactPerson: $("#txtContactPerson").val().trim(),
                registeredFormula: $("#txtRegisteredFormula").val().trim(),
                investFormula: $("#txtInvestFormula").val().trim(),
                regInvestFormula: $("#txtRegInvestFormula").val().trim(),
                investQuota: $("#txtInvestQuota").val().trim(),
                permissions: _permissions,
                isDelete: $("input[name='radio']:checked ").val(),
                Id: id
            };
            var url = (id == "" || id == undefined) ? "/HanderAshx/AdvertisingManage/ChannelUserHandler.ashx?t=add" : "/HanderAshx/AdvertisingManage/ChannelUserHandler.ashx?t=set";

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
            if (id == "" || id == undefined)
            {
                if ($("#txtChannelName").val().trim() == "") {
                    MessageAlert('请输入用户名!', 'warning', '');
                    return false;
                }

                if ($("#txtChannelPwd").val().trim() == "") {
                    MessageAlert('请输入密码!', 'warning', '');
                    return false;
                }
            }

            if ($("#sel_channel").val().trim() == "-1") {
                MessageAlert('请选择渠道!', 'warning', '');
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
                <td style="text-align: right">用户名：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" class="set" _src="/images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtChannelName" type="text" class="input_text set" _class="input_Disabled fl" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" class="set" _src="/images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr id="tr1" style="display:none;">
                <td style="text-align: right">密码：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtChannelPwd" type="text" class="input_text" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                    </div>
                </td>
            </tr>        
            <tr>
                <td style="text-align: right">对应渠道：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <asp:DropDownList name="sel_channel" ID="sel_channel" runat="server" class="fl" ClientIDMode="Static">
                        </asp:DropDownList>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <img src="/images/select_right.png" width="31" height="29" alt="" id="img_sel_channel" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">启用查看权限：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        [<span>汇总数据</span><input type="checkbox" name="checkbox" value="1" />]
                        [<span>数据明细</span><input type="checkbox" name="checkbox" value="2" />]
                        [<span>结算费用</span><input type="checkbox" name="checkbox" value="3" />]
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">联系人：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtContactPerson" type="text" class="input_text" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr >
                <td style="text-align: right" rowspan="4">结算公式：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtRegisteredFormula" type="text" class="input_text" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                        <span class="fl" style="line-height: 29px;">&nbsp;注册人数[A]：费用=A*?</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtInvestFormula" type="text" class="input_text" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                        <span class="fl" style="line-height: 29px;">&nbsp;投资人数[S]：费用=S*?</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtRegInvestFormula" type="text" class="input_text" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                        <span class="fl" style="line-height: 29px;">&nbsp;注册人数[A]+投资人数[S]：费用=A+S</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtInvestQuota" type="text" class="input_text" style="width: 150px;" />
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                        <span class="fl" style="line-height: 29px;">&nbsp;投资额[I]：费用=I/?</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">帐户状态：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">                        
                        [启用<input type="radio" name="radio" value="1" checked="checked" />]
                        [禁用<input type="radio" name="radio" value="0" />]
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