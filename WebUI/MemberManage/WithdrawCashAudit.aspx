

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WithdrawCashAudit.aspx.cs" Inherits="WebUI.MemberManage.WithdrawCashAudit" %>

<%@ Import Namespace="System.Data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        .sb .opts a { border: 1px #ffffff solid; }
            .sb .opts a:hover { background: #ffffff; color: #000000; border: 1px #ff4500 solid; }
            .sb .opts a.selected { background: #ffffff; color: #000000; }
            .sb .opts a.none { background: #ffffff; color: #000000; }
    </style>
    <script type="text/javascript">
        $(function() {
            GetBankTypeList($("#sel_BankTypeOpts")); //银行卡列表
            $('div[name="sel_BankType"]').inputbox({ 'width': 150, 'height': 40 });
        });
        //借款期限类型 obj 下拉款对象
        function GetBankTypeList(obj) {
            $.ajax({
                type: "POST",
                url: "/Handerashx/MemberManage/CommonHandler.ashx?sign=1",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (jsonData) {
                    var array = [];
                    if (jsonData.length > 0) {
                        $(jsonData).each(function (i) {
                            array.push("<a href='javascript:void(0);' val='" + jsonData[i].ID + "' style=\"height:36px;line-height:36px;\"><span class=\"icon_box fl\" style=\"margin-left:-5px; border:none;\"><span class=\"icon_b " + jsonData[i].EnglishName + "\"></span></span></a>");
                        });
                        obj.append(array.join(''));
                    }
                }
            });                      

        }
        
        //验证
        function Verification() {
            var reg =/^([1-9][\d]{0,7}|0)(\.[\d]{1,2})?$/;
            if ($("#txtMemberID").val() == "") {
                MessageAlert('请选择会员。','warning', '');
                return false;
            }
            if ($("#txtAccountHolder").val() == "") {
                MessageAlert('请填写开户人真实姓名。','warning', '');
                $("#txtAccountHolder").focus();
                return false;
            }
            if ($("#sel_BankType").val() == "") {
                MessageAlert('请选择收款银行名称。','warning', '');
                return false;
            }
            if ($("#txtBankAccount").val() == "") {
                MessageAlert('请填写收款银行帐号。','warning', '');
                $("#txtBankAccount").focus();
                return false;
            }
            if ($("#txtCashAmount").val() == "") {
                MessageAlert('请输入提现金额。','warning', '');
                $("#txtCashAmount").focus();
                return false;
            }
            if (!reg.test($.trim($("#txtCashAmount").val()))) {
                MessageAlert('申请提现金额输入错误。','warning', '');
                $("#txtCashAmount").focus();
                return false;
            }
            return true;
        }
        
        function validate() {
            var radValue = $("input[name='AuditStatus']:checked").val();
            if (radValue == undefined) {
                MessageAlert('请选择审核状态', 'Warning', '');
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
                <td style="text-align: right; width: 120px;">会员名：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtMemberName" type="text" name="txtMemberName" value="" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" readonly="readonly" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <span id="selectSpan" class="fl" style="line-height: 29px; margin-left: 3px;" runat="server">
                        <input id="select_btn" type="button" value="选择会员" class="inputButton" style="width: 62px; font-weight: normal;" onclick="MessageWindow(590, 360, '/MemberManage/MemberListSelect.aspx')" />
                    </span>
                    <input id="txtMemberID" name="txtMemberID" type="hidden" runat="server" />
                    <span class="fl" style="line-height: 29px; color: #dc143c;">&nbsp;&nbsp;需要提现的会员。</span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">开户人：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/<%=(Id>0?"gray_left.png":"input_left.png") %>" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtAccountHolder" type="text" name="txtAccountHolder" value="" class="input_text fl" maxlength="300" style="width: 250px;" runat="server" />
                        <span class="fl">
                            <img src="../images/<%=(Id>0?"gray_right.png":"input_right.png") %>" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr id="bankTypeTr" runat="server">
                <td style="text-align: right; width: 120px;">收款银行名称：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div name="sel_BankType" type="selectbox">
                        <div id="sel_BankTypeOpts" class="opts">
                            <a href="javascript:void(0);" val="">-请选择收款银行名称-</a>
                        </div>
                    </div>
                </td>
            </tr>
            <tr id="bankAccountTr" runat="server">
                <td style="text-align: right; width: 120px;">收款银行帐号：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtBankAccount" type="text" name="txtBankAccount" value="" class="input_text fl" maxlength="50" style="width: 260px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr id="bankListTr" runat="server" visible="False">
                <td style="text-align: right; width: 120px;">收款银行帐号：</td>
                <td id="bankList" style="text-align: left; padding-left: 5px; padding-top: 5px; padding-bottom: 5px;" runat="server">请选择会员之后再选择银行卡
                </td>
            </tr>
            <tr id="REQ_SN_TR" runat="server" visible="False">
                <td style="text-align: right; width: 120px;">交易流水号：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtREQ_SN" type="text" name="txtREQ_SN" value="" class="input_text fl" maxlength="50" style="width: 220px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">提现金额：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/<%=(Id>0?"gray_left.png":"input_left.png") %>" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtCashAmount" type="text" name="txtCashAmount" value="" class="input_text fl" maxlength="50" style="width: 120px;" runat="server" />
                        <span class="fl">
                            <img src="../images/<%=(Id>0?"gray_right.png":"input_right.png") %>" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl" style="line-height: 25px; margin-left: 3px;">元
                        </span>
                    </div>
                </td>
            </tr>
            <tr id="CashFee_TR" runat="server" visible="False">
                <td style="text-align: right; width: 120px;">手续费：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtCashFee" type="text" name="txtCashFee" value="" class="input_text fl" maxlength="50" style="width: 120px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl" style="line-height: 25px; margin-left: 3px;">元
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
             <td style="text-align: right; width: 120px;">申请理由：</td>
             <td  style="text-align: left; padding-left: 5px;">
               <textarea id="txtreason" name="content1" cols="100" rows="8" style="width: 480px; height: 80px; padding: 5px;" runat="server"></textarea>
             </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">支付平台类型：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px; padding-bottom: 5px;">
                    <p class="clear" style="padding: 5px;">
                        <span class="fl" style="line-height: 36px;">
                            <input id="tlpay" name="rdPayment" type="radio" runat="server" value="1" checked="true" />
                        </span>
                        <span class="icon_box fl">
                            <label class="icon_b TL" for="tlpay"></label>
                        </span>
                        <span class="fl" style="line-height: 36px;padding-left:10px;">
                            <input id="tlsspay" name="rdPayment" type="radio" runat="server" value="3" />
                        </span>
                        <span class="icon_box fl">
                            <label class="icon_b TLSS" for="tlsspay"></label>
                        </span>
                        <%--<span class="fl" style="line-height: 36px;padding-left:10px;">
                            <input id="llpay" name="rdPayment" type="radio" runat="server" value="2" />
                        </span>
                        <span class="icon_box fl">
                            <label class="icon_b LL" for="llpay"></label>
                        </span>
                        <span style="float: left; height: 38px; line-height: 38px; padding-left:50px;">
                            连连账户余额：<label id="balance" runat="server"></label>
                        </span>--%>
                    </p>
                </td>
            </tr>
            <tr id="selCurrStatus_TR" runat="server" visible="False">
                <td style="text-align: right; width: 120px;">当前审核状态：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 3px;">
                    <div class="selectDiv">
                        <div class="fl">
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                        </div>
                        <select name="selCurrStatus" id="selCurrStatus" class="fl" runat="server" style="width: 80px;" disabled="True">
                            <option value="0">初审中</option>
                            <option value="1">复审中</option>
                            <option value="2">初审不通过</option>
                            <option value="3">复审不通过</option>
                            <option value="4">复审通过</option>
                        </select>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <img src="/images/gray_icon.png" width="31" height="29" alt="" id="img_selCurrStatus" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr id="AuditStatus_TR" runat="server" visible="False">
                <td style="text-align: right; width: 120px;">选择审核状态：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span style="float: left; height: 25px; line-height: 25px;">
                        <input id="Radio1" name="AuditStatus" type="radio" runat="server" value="1" /></span><label for="Radio1" style="float: left; height: 25px; line-height: 25px; margin-top: -2px;">「审核通过」</label>
                    <span style="float: left; height: 25px; line-height: 25px; margin-left: 8px;">
                        <input id="Radio2" name="AuditStatus" type="radio" runat="server" value="2" /></span><label for="Radio2" style="float: left; height: 25px; line-height: 25px; margin-top: -2px;">「审核不通过」</label>
                </td>
            </tr>
            <tr id="AuditRemark_TR" runat="server" visible="False">
                <td style="text-align: right; width: 120px; vertical-align: top; padding-top: 3px;">审核意见：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <textarea id="auditRemark" name="content1" cols="100" rows="8" style="width: 480px; height: 120px; padding: 5px;" runat="server"></textarea>
                </td>
            </tr>
            <tr id="AuditRecords_TR" runat="server" visible="False">
                <td style="text-align: right; width: 120px;">审核记录：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span id="litAuditReco" runat="server" style="line-height: 30px;"></span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;"></td>
                <td style="text-align: left;">
                    <asp:Button ID="Btn_Audit" Text="线上提现审核" CssClass="inputButton" runat="server" Width="100px" OnClick="Audit_Click" OnClientClick="return validate();" />
                    <asp:Button ID="cashApply_Btn" Text="线下提现申请" CssClass="inputButton" runat="server" Width="100px" OnClientClick="return Verification();" OnClick="cashApply_Btn_Click" />
                    <asp:Button ID="cashAudit_Btn" Text="线下提现审核" CssClass="inputButton" runat="server" Width="100px" OnClick="cashAudit_Btn_Click" />
                    <input type="button" value="返回" class="inputButton" onclick="window.location.href = 'WithdrawCashManage.aspx?<%=HttpContext.Current.Request.QueryString.ToString()%>'" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px;">查看进程</td>
                <td  style="text-align: left; padding-left: 5px;">
                    <asp:Button ID="Button1" Text="查看当前状态" CssClass="inputButton_C" runat="server" Width="100px" OnClick="Query_Click" />
                    <label id="lab_result" runat="server"></label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
<style type="text/css">
    .selectDiv #select_selBankType { width: 110px; }
    .selectDiv #select_selCurrStatus { width: 80px; }
</style>
