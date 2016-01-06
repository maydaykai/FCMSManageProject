<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishLoan.aspx.cs" Inherits="WebUI.p2p.PublishLoan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../css/table.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="../js/loan/init.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function() {
            $("#sel_province").live("click", function() {
                getCityListFromHandlerByParentID($("#sel_province").val());
            });
            $("#sel_province").click();
            $("#sel_repaymentMethod").live("click", function () {
                if ($('#sel_repaymentMethod').val() == 2) {
                    $("#lab_loanTerm").html("(按天)");
                } else {
                    $("#lab_loanTerm").html("(按月)");
                }
            });
            $("#sel_repaymentMethod").click();
            //$("#sel_LoanScaleType").change(function() {
            //    if ($('#sel_LoanScaleType').val() == 5) {
            //        $('#txt_LoanTerm').val(1);
            //        $('#txt_LoanTerm').attr("readonly", "true");
            //        $('#txt_LoanTerm').attr("class", "input_Disabled");
            //        $('#sel_repaymentMethod').val(3);
            //        $('#sel_repaymentMethod').attr("disabled", "disabled");
            //    } else {
            //        $('#sel_repaymentMethod').removeAttr("disabled");
            //        $('#txt_LoanTerm').removeAttr("readonly");
            //        $('#txt_LoanTerm').attr("class", "fl input_text");
            //    }
            //    $("#lab_loanTerm").html("(按月)");
            //});
            $("input[name^='txt']").keydown(function(event) {
                var keyCode = event.which;
                if (keyCode == 46 || keyCode == 8 || keyCode == 9 || keyCode == 190 || (keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 105) || keyCode == 110) {
                    return true;
                } else {
                    return false;
                }
            }).focus(function() {
                this.style.imeMode = 'disabled';
            });
        });
    </script>
 </head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellspacing="0" cellpadding="0" class="editTable">
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
                        <span class="fl" style="line-height: 29px; color: #dc143c;">&nbsp;&nbsp;需要借款的会员。</span>
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
                <td style="text-align: right;width: 150px;">借款标题：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (text_LoanTitle.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="text_LoanTitle" name="rate" runat="server" class="fl input_text" maxlength="15"/>
                        <div class="fl">
                            <%if (text_LoanTitle.Attributes["readonly"] == "true")
                              { %>
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%}
                              else
                              { %>
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%} %>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">借款标类型：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px; ">
                        <select id="sel_LoanScaleType" class="fl input_text" style ="width: 150px;height: 32px" runat="server">
                            <option value="0" selected="selected">选择借款标类型</option>
                        </select>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">借款用途：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <select id="sel_loanUseName" class="fl input_text" style ="width: 150px;height: 32px" runat="server">
                            <option value="0" selected="selected">选择借款用途</option>
                        </select>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">借款金额：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (txt_LoanAmount.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="txt_LoanAmount" runat="server" class="fl input_text" />
                        <div class="fl">
                            <%if (txt_LoanAmount.Attributes["readonly"] == "true")
                              { %>
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%}
                              else
                              { %>
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%} %>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">还款方式：</td>
                <td style="text-align: left; padding-left: 5px;">

                        <select id="sel_repaymentMethod" class="fl input_text" style ="width: 150px;height: 32px" runat="server">
                            <option id="repayment0" value="0" selected="selected">--请选择--</option>
                            <option id="repayment1" value="1">按天计息按月还款</option>
                            <option id="repayment2" value="2">按天计息一次性还款</option>
                            <option id="repayment3" value="3">按月付息到期还本</option>
                            <option id="repayment4" value="4">按月等额本息</option>
                        </select>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">借款期限：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (txt_LoanTerm.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="txt_LoanTerm" runat="server" class="fl input_text" />
                        <div class="fl">
                            <%if (txt_LoanTerm.Attributes["readonly"] == "true")
                              { %>
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%}
                              else
                              { %>
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%} %>
                        </div>
                    </div>
                    <label class="fl" style="margin-top: 5px;" id="lab_loanTerm" runat="server"></label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">借款年利率：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="selectDiv" style="margin-top: 6px; display: inline-block;">
                        <span style="width: 4px; float: left;">
                            <%if (txtLoanRate.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </span>
                        <input type="text" id="txtLoanRate" name="rate" runat="server" class="fl input_text" />
                        <span class="fl">
                            <%if (txtLoanRate.Attributes["readonly"] == "true")
                              { %>
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%}
                              else
                              { %>
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%} %>
                        </span>
                        <span class="fl" style="margin-top: 6px;">%</span>
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">所属地区：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <select id="sel_province" class="fl input_text" runat="server">
                            <option value="0" selected="selected">选择省份</option>
                        </select>
                    <select id="sel_city" class="fl input_text" runat="server">
                            <option value="0" selected="selected">选择城市</option>
                        </select>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">线上线下：</td>
                <td style="text-align: left; padding-left: 5px;">
                        <select id="sel_TradeType" class="fl input_text" style ="width: 150px;height: 32px" runat="server">
                            <option value="0" selected="selected">线上</option>
                            <option value="1">线下</option>
                        </select>
                </td>
            </tr>
            <tr>
                <td style="border-bottom: 0; text-align: right;">&nbsp;</td>
                <td style="border-bottom: 0; text-align: left; padding-left: 5px;">

                <asp:Button ID="button1" runat="server" Text="立即申请" OnClick="Button1_Click" Width="60" CssClass="inputButton" />

                </td>

            </tr>
        </table>
    </form>
</body>
</html>
