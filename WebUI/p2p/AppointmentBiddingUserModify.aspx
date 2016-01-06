<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppointmentBiddingUserModify.aspx.cs" Inherits="WebUI.p2p.AppointmentBiddingUserModify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/table.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <link href="../css/bankBg.css" rel="stylesheet" />
    <script src="../js/jQuery/jquery-1.8.3.min.js"></script>
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>
    <script src="../js/select2css.js"></script>
    <link href="../js/jquery.ganged/jquery.inputbox.css" rel="stylesheet" />
    <script src="../js/jquery.ganged/jquery.inputbox.js"></script>
    <style type="text/css">
        .sb .opts a {
            border: 1px #ffffff solid;
        }

        .sb .opts a:hover {
                background: #ffffff;
                color: #000000;
                border: 1px #ff4500 solid;
            }

        .sb .opts a.selected {
                background: #ffffff;
                color: #000000;
            }

        .sb .opts a.none {
                background: #ffffff;
                color: #000000;
            }

        .selectDiv #select_selBankType {
            width: 110px;
        }

        .selectDiv #select_selCurrStatus {
            width: 80px;
        }
    </style>
    <script type="text/javascript">
        $(function(){
            //$("#txtMemberName").blur(function(){
            //    if($(this).val().trim()!="")
            //    {
            //        $.ajax({
            //            type: "POST",
            //            contentType: "application/json; charset=utf-8",
            //            url: "/HanderAshx/p2p/AppointmentBiddingUserModifyHandler.ashx?name="+$(this).val().trim(),
            //            dataType: "json",
            //            cache: false,
            //            success: function (result) {
            //                if (result.TotalRows > 0)
            //                {                                
            //                    $("#spError").text("用户绑定手机号码："+result.Rows[0].Mobile);
            //                    $("#txtMemberID").val(result.Rows[0].ID);
            //                }
            //                else
            //                {
            //                    $("#spError").text("未查到用户数据");
            //                    $("#txtMemberID").val("");
            //                }
            //            }
            //        });
            //    }
            //    else
            //        $("#txtMemberID").val("");
            //});
        });

        function validate()
        {
            if($("#txtMemberID").val().trim()=="" && $("#Radio2").is(":checked"))
            {
                $("#spError").text("请选择用户");
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" class="editTable">            
            <tr>
                <td style="text-align: right; width: 120px;">预约手机：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtMobile" type="text" class="input_Disabled fl" disabled="disabled" style="width: 100px;" runat="server" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">预约金额：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtAmount" type="text" name="txtAmount" disabled="disabled" class="input_Disabled fl" style="width: 120px;" runat="server" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <span class="fl" style="line-height: 25px; margin-left: 3px;">元
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">用户名：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtMemberName" type="text" name="txtMemberName" disabled="disabled" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <span class="fl" style="line-height: 30px; margin-left: 20px; color:red;" id="spError"></span>
                    <input id="txtMemberID" name="txtMemberID" clientidmode="static" type="hidden" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">申请时间：</td>
                <td style="text-align: left; padding-left: 5px;"><span id="spCreateTime" runat="server"></span></td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">选择审核状态：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span style="float: left; height: 25px; line-height: 25px;">
                        <input id="Radio1" name="AuditStatus" type="radio" runat="server" /></span><label for="Radio1" style="float: left; height: 25px; line-height: 25px; margin-top: -2px;">「核实中」</label>
                    <span style="float: left; height: 25px; line-height: 25px; margin-left: 8px;">
                        <input id="Radio2" name="AuditStatus" type="radio" runat="server" /></span><label for="Radio2" style="float: left; height: 25px; line-height: 25px; margin-top: -2px;">「核实通过」</label>
                    <span style="float: left; height: 25px; line-height: 25px; margin-left: 8px;">
                        <input id="Radio3" name="AuditStatus" type="radio" runat="server" /></span><label for="Radio2" style="float: left; height: 25px; line-height: 25px; margin-top: -2px;">「核实不通过」</label>
                    <span style="float: left; height: 25px; line-height: 25px; margin-left: 8px;">
                        <input id="Radio4" name="AuditStatus" type="radio" runat="server" /></span><label for="Radio2" style="float: left; height: 25px; line-height: 25px; margin-top: -2px;"  id="lblRadio4" runat="server">「已处理」</label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px; vertical-align: top; padding-top: 3px;">备注：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <textarea id="txtNote" cols="100" rows="8" style="width: 480px; height: 120px; padding: 5px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">回访记录：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span id="spWarningRecord" runat="server" style="line-height: 30px;"></span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;"></td>
                <td style="text-align: left;">
                    <asp:Button ID="btnSave" Text="提交" CssClass="inputButton" runat="server" Width="100px" OnClick="btnSave_Click" />
                    <input type="button" value="返回" class="inputButton" onclick="window.location.href = 'AppointmentBiddingUser.aspx?<%=HttpContext.Current.Request.QueryString.ToString()%>    '" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
