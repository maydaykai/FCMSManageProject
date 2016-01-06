<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RechargeUnderLine.aspx.cs" Inherits="WebUI.MemberManage.RechargeUnderLine" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>线下充值申请</title>
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
    <script type="text/javascript">
        $(function () {
            $("input[name=btnBack]").click(function () {
                window.location.href = "RechargeManage.aspx?columnId=<%=ColumnId%>";
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" class="editTable">
            <tr>
                <td style="text-align: right; width: 150px;">会员名：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtMemberName" type="text" name="txtMemberName" value="" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" readonly="readonly" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span id="selectSpan" class="fl" style="line-height: 29px;margin-left: 3px;" runat="server">
                            <input id="select_btn" type="button" value="选择会员" class="inputButton" style="width: 62px; font-weight: normal;" onclick="MessageWindow(590, 360, '/MemberManage/MemberListSelect.aspx')" />
                        </span>
                        <input id="txtMemberID" name="txtMemberID" type="hidden" runat="server" />
                        <span class="fl" style="line-height: 29px; color: #dc143c;">&nbsp;&nbsp;需要充值的会员。</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">真实姓名：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtRealName" type="text" name="txtRealName" value="" class="input_Disabled fl" maxlength="50" style="width: 100px;" runat="server"  readonly="readonly" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">银行订单号：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/<%=(Id>0?"gray_left.png":"input_left.png") %>" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtOrderNumber" type="text" name="txtOrderNumber" value="" class="input_text fl" maxlength="50" style="width: 260px;" runat="server" />
                        <span class="fl">
                            <img src="../images/<%=(Id>0?"gray_right.png":"input_right.png") %>" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl" style="line-height: 29px; color: #dc143c;">&nbsp;&nbsp;指您在银行柜台办理成功后银行给您的交易号码。</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">交易时间：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/<%=(Id>0?"gray_left.png":"input_left.png") %>" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtCreateTime" type="text" name="txtCreateTime" value="" class="input_text fl" maxlength="20" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" style="width: 150px;" runat="server" />
                        <span class="fl">
                            <img src="../images/<%=(Id>0?"gray_right.png":"input_right.png") %>" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl" style="line-height: 29px; color: #dc143c;">&nbsp;&nbsp;具体交易时间。</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">申请充值金额：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/<%=(Id>0?"gray_left.png":"input_left.png") %>" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtAmount" type="text" name="txtAmount" value="" class="input_text fl" maxlength="20" style="width: 120px;" runat="server" />
                        <span class="fl">
                            <img src="../images/<%=(Id>0?"gray_right.png":"input_right.png") %>" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl" style="line-height: 29px;">&nbsp;元</span>
                        <span class="fl" style="line-height: 29px; color: #dc143c;">&nbsp;&nbsp;交易币种均为人民币。</span>
                    </div>
                </td>
            </tr>
            <tr id="CurrorStatus" runat="server" visible="False">
                <td style="text-align: right">当前状态：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <div class="fl">
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                        </div>
                        <select name="selIncreasingMode" id="selIncreasingMode" class="fl" runat="server" style="width: 150px;">
                            <option value="0">初审中</option>
                            <option value="1">复审中</option>
                            <option value="2">初审不通过</option>
                            <option value="3">复审不通过</option>
                            <option value="4">复审通过</option>
                        </select>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <img src="/images/gray_icon.png" width="31" height="29" alt="" id="img_selIncreasingMode" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr id="AuditStatus" runat="server" visible="False">
                <td style="text-align: right">选择审核状态：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <span style="float: left; height: 25px; line-height: 25px;">
                        <input id="Radio1" name="AuditStatus" type="radio" runat="server" value="1" /></span><span style="float: left; height: 25px; line-height: 25px; margin-top: -2px;">「审核通过」</span>
                    <span style="float: left; height: 25px; line-height: 25px; margin-left: 8px;">
                        <input id="Radio2" name="AuditStatus" type="radio" runat="server" value="2" /></span><span style="float: left; height: 25px; line-height: 25px; margin-top: -2px;">「审核不通过」</span>
                </td>
            </tr>
            <tr id="AreaNote" runat="server" visible="False">
                <td style="text-align: right; vertical-align: top; padding-top: 10px;">审核意见：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px; height: 120px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <textarea id="txtAreaNote" cols="50" rows="5" runat="server" style="color: #1B759F; padding: 5px; height: 100px; width: 300px;"></textarea>
                    </div>
                </td>
            </tr>
            <tr id="AuditReco" runat="server" visible="False">
                <td style="text-align: right">审核记录：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <span id="litAuditReco" runat="server" style="line-height: 30px;"></span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left; padding-left: 5px;">
                    <input id="Operate_Btn" type="button" value="提交申请" runat="server" class="inputButton" onserverclick="Operate_Btn_Click" style="width: 100px;" />
                    <input id="Audit_Btn" type="button" value="提交" runat="server" class="inputButton" onserverclick="Audit_Btn_Click" style="width: 60px;" visible="False" />
                    &nbsp;&nbsp<input type="button" name="btnBack" value="返 回" class="inputButton" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<script type="text/javascript">
    $(function () {
        $(':checkbox').tzCheckbox({ labels: ['Enable', 'Disable'] });
    });
</script>
<style type="text/css">
    .selectDiv #select_selCalculationMode { width: 65px; }
    .selectDiv #select_selIncreasingMode { width: 85px; }
</style>
