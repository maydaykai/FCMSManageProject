<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanAudit.aspx.cs" Inherits="WebUI.p2p.LoanAudit" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>借款审核</title>
    <link href="../css/table.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <link rel="stylesheet" href="/js/kindeditor-4.1.10/themes/default/default.css" />
    <link rel="stylesheet" href="/js/kindeditor-4.1.10/plugins/code/prettify.css" />
    <script charset="utf-8" src="/js/kindeditor-4.1.10/kindeditor.js"></script>
    <script charset="utf-8" src="/js/kindeditor-4.1.10/lang/zh_CN.js"></script>
    <script charset="utf-8" src="/js/kindeditor-4.1.10/plugins/code/prettify.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="../js/loan/init.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="../js/loan/MoneyUpper.js"></script>

    <script type="text/javascript">
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('textarea[name="content1"]', {
                resizeType: 1,
                allowPreviewEmoticons: false,
                allowImageUpload: false,
                items: [
                    'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
                    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
                    'insertunorderedlist', '|', 'link', 'unlink', 'preview']
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            
            $('#options_sel_repaymentMethod li').live("click", function () {
                if ($('#sel_repaymentMethod').val() == 2) {
                    $("#txtLoanServiceCharges").val("0");
                    $("#lab_daymonth").html("(按天)");
                    $("#lab_loanTerm").html("(按天)");
                } else {
                    $("#txtLoanServiceCharges").val("0");
                    $("#lab_daymonth").html("(按月)");
                    $("#lab_loanTerm").html("(按月)");
                }

            });

            
            //$('#options_sel_LoanScaleType li').live("click", function() {
            //    var selRepaymentMethod =  "sel_repaymentMethod";
            //    var repaymentMethod = $("#" + selRepaymentMethod);
            //    var divRepaymentMethod = $("#select_info_" + selRepaymentMethod);
            //    var ulRepaymentMethod = $("#options_" + selRepaymentMethod);
            //    var repaymentMethodId = $('#sel_repaymentMethod').val();
            //    if ($('#sel_LoanScaleType').val() == 5) {
            //        $('#txt_LoanTerm').val("1");
            //        $('#txt_LoanTerm').attr("readonly", "true");
            //        $('#txt_LoanTerm').attr("class","input_Disabled");
                    
                    
            //        repaymentMethod.empty();
            //        ulRepaymentMethod.empty();
            //        divRepaymentMethod.html("");
                    
            //        repaymentMethod.append($("<option>").text("按月付息到期还本").val(3));
            //        rOptions(2, "sel_repaymentMethod");
            //        mouseSelects("sel_repaymentMethod");

            //        $('#sel_repaymentMethod').attr("disabled","disabled");
            //        $('#sel_repaymentMethod').click();
            //        $("#txtLoanServiceCharges").val("0");
            //        $("#lab_daymonth").html("(按月)");
            //        $("#lab_loanTerm").html("(按月)");
            //    } else {
            //        $('#txt_LoanTerm').removeAttr("readonly");
            //        $('#txt_LoanTerm').attr("class","fl input_text");
                    
            //        repaymentMethod.empty();
            //        ulRepaymentMethod.empty();
            //        divRepaymentMethod.html("");
            //        $('#sel_repaymentMethod').removeAttr("disabled");
            //        $('#sel_repaymentMethod').click();
            //        repaymentMethod.append($("<option>").text("请选择").val(0));
            //        repaymentMethod.append($("<option>").text("按天计息按月还款").val(1));
            //        repaymentMethod.append($("<option>").text("按天计息一次性还款").val(2));
            //        repaymentMethod.append($("<option>").text("按月付息到期还本").val(3));
            //        repaymentMethod.append($("<option>").text("按月等额本息").val(4));
                    
            //        rOptions(2, "sel_repaymentMethod");
            //        mouseSelects("sel_repaymentMethod");

            //        $("#txtLoanServiceCharges").val("0");
            //        $("#lab_daymonth").html("(按月)");
            //        //$("#lab_loanTerm").html("(按月)");
            //    }
            //});
            
            $("input[name^='txt']").keydown(function (event) {
                var keyCode = event.which;
                if (keyCode == 46 || keyCode == 8 || keyCode == 9 || keyCode ==190 || (keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 105) ||keyCode == 110) {
                    return true;
                } else {
                    return false;
                }
            }).focus(function () {
                this.style.imeMode = 'disabled';
            });
            
            

            var loanid = getQueryString("ID");
            loadInfo(loanid);
        });

        (function ($) {
            var FormatDateTime = function FormatDateTime() { };
            $.FormatDateTime = function (obj) {
                var correcttime1 = eval('( new ' + obj.replace(new RegExp("\/", "gm"), "") + ')');
                var myDate = correcttime1;
                var year = myDate.getFullYear();
                var month = ("0" + (myDate.getMonth() + 1)).slice(-2);
                var day = ("0" + myDate.getDate()).slice(-2);
                var h = ("0" + myDate.getHours()).slice(-2);
                var m = ("0" + myDate.getMinutes()).slice(-2);
                var s = ("0" + myDate.getSeconds()).slice(-2);
                var mi = ("00" + myDate.getMilliseconds()).slice(-3);

                return year + "-" + month + "-" + day + " " + h + ":" + m + ":" + s;
            }
        })(jQuery);

        
        function loadInfo(loanid) {
            var params = { "loanid": "" + loanid + "" };
            $.ajax(
                {
                    type: "get",
                    url: "../HanderAshx/p2p/AuditHistory.ashx",
                    async: false,//true表示异步 false表示同步  
                    contentType: "application/json; charset=utf-8",
                    data: params,
                    dataType: 'json',
                    success: function(result) {
                        var jsondatas = eval(result);
                        var temphtml = "";
                        $.each(jsondatas, function (i, n) {
                            temphtml = "<tr>" +
                                "<td width=\"100\">" + n.ProcessName + "</td>";
                            temphtml += "<td width=\"65\"><img src=\"../images/" + (n.Result == true ? "ok_icon.png" : "no_icon.png") + "\" width=\"15\" height=\"15\" class=\"ok_icon\"/></td>";
                            temphtml += "<td width=\"70\">" + n.UserName + "</td>" +
                                "<td width=\"160\">" + $.FormatDateTime(n.AuditTime) + "</td>" +
                                "<td width=\"177\">" + n.Reason + "</td>" +
                                "<td width=\"176\">" + n.ReviewComments + "</td>" +
                                "</tr>";
                            $("#AuditRecord").append(temphtml);
                            
                        });
                    }
                });
        }
    </script>

    <style type="text/css">
        .auto-style1 {
            height: 29px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellspacing="0" cellpadding="0" class="editTable">
            <tr>
                <td style="text-align: right; width: 200px;">贷款会员：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <label id="lab_MemberName" runat="server"></label>
                    <asp:Button ID="btnLookMemberInfo" runat="server" Text="查看会员信息" Width="120" CssClass="inputButton" OnClick="btnLookMemberInfo_Click" />
                </td>
            </tr>
            <tr>
                <td style="height: 29px; text-align: right;">借款编号：</td>
                <td class="auto-style1" style="text-align: left; padding-left: 5px;">
                    <label id="lab_LoanNumber" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">所在地区：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <label id="lab_province" runat="server"></label>
                    <label id="lab_city" runat="server"></label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">借款用途：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div class="fl">
                            <%if (sel_loanUseName.Attributes["disabled"] == "disabled")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>

                        <select id="sel_loanUseName" runat="server">
                            <option value="0" selected="selected">选择借款用途</option>
                        </select>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <%if (sel_loanUseName.Attributes["disabled"] == "disabled")
                              { %>
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_loanUseName" style="display: none;" />
                            <%}
                              else
                              { %>
                            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_loanUseName" />
                            <%} %>
                        </div>
                    </div>
                </td>



                <%--<td style="text-align: right;">贷款用途：</td>
            <td style="text-align: left; padding-left: 5px;">
                <label id ="lab_loanUseName" runat="server"></label>
            </td>--%>
            </tr>
            <tr>
                <td style="text-align: right;">借款标题：</td>
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
                <td style="text-align: right;">借款标类型名称：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div class="fl">
                            <%if (sel_LoanScaleType.Attributes["disabled"] == "disabled")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>

                        <select id="sel_LoanScaleType" runat="server">
                            <option value="0" selected="selected">选择借款标类型</option>
                        </select>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <%if (sel_LoanScaleType.Attributes["disabled"] == "disabled")
                              { %>
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_LoanScaleType" style="display: none;" />
                            <%}
                              else
                              { %>
                            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_LoanScaleType" />
                            <%} %>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">还款方式：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div class="fl">
                            <%if (sel_repaymentMethod.Attributes["disabled"] == "disabled")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <select id="sel_repaymentMethod" runat="server">
                            <option id="repayment0" value="0" selected="selected">请选择</option>
                            <option id="repayment1" value="1">按天计息按月还款</option>
                            <option id="repayment2" value="2">按天计息一次性还款</option>
                            <option id="repayment3" value="3">按月付息到期还本</option>
                            <option id="repayment4" value="4">按月等额本息</option>
                        </select>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <%if (sel_repaymentMethod.Attributes["disabled"] == "disabled")
                              { %>
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_repaymentMethod" style="display: none;" />
                            <%}
                              else
                              { %>
                            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_repaymentMethod" />
                            <%} %>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">贷款期限：</td>
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
                    <label class="fl" style="margin-top: 5px;color: red;" id="lab_loanTerm" runat="server" ></label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">线上线下：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div class="fl">
                            <%if (sel_TradeType.Attributes["disabled"] == "disabled")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <select id="sel_TradeType" class="fl input_text" style ="width: 150px;height: 32px" runat="server">
                            <option value="0" selected="selected">线上</option>
                            <option value="1">线下</option>
                        </select>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <%if (sel_TradeType.Attributes["disabled"] == "disabled")
                              { %>
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_TradeType" style="display: none;" />
                            <%}
                              else
                              { %>
                            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_TradeType" />
                            <%} %>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">贷款额度：</td>
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
                    <span class="fl" style="margin-top: 5px;" id="SpanLoanAmount">当前未还贷款金额：￥<label id="lab_SurAmount" runat="server"></label>，审批中的申请金额：￥<label id="lab_AuditAmount" runat="server"></label>，贷款额度大写：<label id="lab_AuditAmountBig" runat="server"></label></span>
                </td>

                <%--<td style="text-align: left; padding-left: 5px;">
                <label id ="lab_LoanAmount" runat="server"></label> 元
                <span id="SpanLoanAmount">当前未还贷款金额：￥<label id ="lab_SurAmount" runat="server"></label>，审批中的申请金额：￥<label id ="lab_AuditAmount" runat="server"></label></span>
            </td>--%>
            </tr>
            <tr>
                <td style="text-align: right;">当前状态：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <label id="lab_examStatus" runat="server"></label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">担保公司：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div class="fl">
                            <%if (sel_Guarantee.Attributes["disabled"] == "disabled")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>

                        <select id="sel_Guarantee" runat="server">
                            <option value="0" selected="selected">选择担保公司</option>
                        </select>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <%if (sel_Guarantee.Attributes["disabled"] == "disabled")
                              { %>
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_Guarantee" style="display: none;" />
                            <%}
                              else
                              { %>
                            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_Guarantee" />
                            <%} %>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">利率类型(年利率)：</td>
                <td style="text-align: left; padding-left: 5px;"><span class="fl" style="margin-top: 12px;">贷款利率：</span>
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
                        <span class="fl" style="margin-top: 6px;">%&nbsp&nbsp</span>
                        <span class="fl" style="margin-top: 6px;">在即将发布列表中显示为不低于：</span>
                        <span style="width: 4px; float: left;">
                            <%if (txtReleasedRate.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </span>
                        <input type="text" id="txtReleasedRate" name="rate" runat="server" class="fl input_text" />
                        <span class="fl">
                            <%if (txtReleasedRate.Attributes["readonly"] == "true")
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
                <td style="text-align: right;">自动投标比例：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (txtAutoBidScale.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="txtAutoBidScale" name="rate" runat="server" class="fl input_text" /><span style="margin-top: 5px; display: inline-block;">%</span>
                        <div class="fl">
                            <%if (txtAutoBidScale.Attributes["readonly"] == "true")
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
                <td style="text-align: right;">最低投资金额：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (txtBidAmountMin.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="txtBidAmountMin" runat="server" class="fl input_text" />
                        <div class="fl">
                            <%if (txtBidAmountMin.Attributes["readonly"] == "true")
                              { %>
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%}
                              else
                              { %>
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%} %>
                        </div>

                        <span class="fl" style="margin-top: 4px;">&nbsp&nbsp最高投资金额：</span>
                        <div style="width: 4px; float: left;">
                            <%if (txtBidAmountMax.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="txtBidAmountMax" runat="server" class="fl input_text" />
                        <div class="fl">
                            <%if (txtBidAmountMax.Attributes["readonly"] == "true")
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
                <td style="text-align: right;">竞标开始时间：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (WdataBidStartTime.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input id="WdataBidStartTime" class="fl input_text" type="text" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm' })" style="width: 150px" runat="server" />
                        <div class="fl" style="margin-right: 5px;">
                            <%if (WdataBidStartTime.Attributes["readonly"] == "true")
                              { %>
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%}
                              else
                              { %>
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%} %>
                        </div>
                        <span class="fl" style="margin-top: 4px;">&nbsp;竞标截止时间：</span>
                        <div style="width: 4px; float: left;">
                            <%if (WdataBidEndTime.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input id="WdataBidEndTime" class="fl input_text" type="text" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm' })" style="width: 150px" runat="server" />
                        <div class="fl" style="margin-right: 5px;">
                            <%if (WdataBidEndTime.Attributes["readonly"] == "true")
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
                <td style="text-align: right;">是否收取担保费：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span style="margin-top: 5px; display: inline-block;">
                        <span style="margin-top: 5px; display: inline-block;" class="fl">
                            <input type="checkbox" id="cbNeedGuarantee" runat="server" /></span><span class="fl" style="margin-left: 5px; margin-top: 5px;">收取比例：&nbsp</span>
                        <span style="width: 4px; float: left;">
                            <%if (txtGuaranteeFee.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </span>
                        <input type="text" id="txtGuaranteeFee" name="rate" runat="server" class="fl input_text" />
                        <span class="fl">
                            <%if (txtGuaranteeFee.Attributes["readonly"] == "true")
                              { %>
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%}
                              else
                              { %>
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%} %>
                        </span>
                        <span class="fl" style="margin-top: 5px;">%</span>
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">是否收取借款人居间服务费：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span style="margin-top: 5px; display: inline-block;">
                        <span style="margin-top: 5px; display: inline-block;" class="fl">
                            <input type="checkbox" id="cbNeedLoanCharges" runat="server" /></span><span class="fl" style="margin-left: 5px; margin-top: 5px;">收取比例：&nbsp</span>
                        <span style="width: 4px; float: left;">
                            <%if (txtLoanServiceCharges.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </span>
                        <input type="text" id="txtLoanServiceCharges" name="rate" runat="server" class="fl input_text" />
                        <span class="fl">
                            <%if (txtGuaranteeFee.Attributes["readonly"] == "true")
                              { %>
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%}
                              else
                              { %>
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%} %>
                        </span>
                        <span class="fl" style="margin-top: 5px;">%</span>
                        <label class="fl" style="margin-top: 5px;color: red;" id="lab_daymonth" runat="server"></label>
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">是否收取投资人居间服务费：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span style="margin-top: 5px; display: inline-block;">
                        <span style="margin-top: 5px; display: inline-block;" class="fl">
                            <input type="checkbox" id="cbNeedBidCharges" runat="server" /></span><span class="fl" style="margin-left: 5px; margin-top: 5px;">收取比例：&nbsp</span>
                        <span style="width: 4px; float: left;">
                            <%if (txtBidServiceCharges.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </span>
                        <input type="text" id="txtBidServiceCharges" name="rate" runat="server" class="fl input_text" />
                        <span class="fl">
                            <%if (txtBidServiceCharges.Attributes["readonly"] == "true")
                              { %>
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%}
                              else
                              { %>
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            <%} %>
                        </span>
                        <span class="fl" style="margin-top: 5px;">%</span>
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">资料认证：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <asp:CheckBoxList ID="ckbAuthList" runat="server" RepeatDirection="Horizontal" RepeatColumns="7"  BorderColor="White" RepeatLayout="Flow" CssClass="checkList">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">借款标描述：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <textarea id="content1" name="content1" cols="100" rows="8" style="width: 500px; height: 200px; visibility: hidden;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">评审意见：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <textarea rows="10" cols="106" id="textReviewComments" runat="server">
                </textarea>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">上传附件：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div id="fileQueue"></div>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="上传附件" CssClass="inputButton" Width="100" /><br />
                    文件列表：<br />
                    <asp:Table ID="tbDirInfo" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">审核原因：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="selectDiv" style="margin-top: 5px; display: inline-block;">
                        <input type="radio" id="radio_audit" name="audit" runat="server" checked="True" class="fl" style="margin-top: 5px;" /><span class="fl" style="margin-top: 5px; margin-right: 5px;">审核通过</span>
                        <input type="radio" id="radio_noAudit" name="audit" runat="server" class="fl" style="margin-top: 5px;" /><span class="fl" style="margin-top: 5px;">审核不通过</span>
                        <span class="fl" style="margin-top: 5px;">&nbsp不通过理由：</span>

                        <span style="width: 4px; float: left;">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input type="text" id="textReason" runat="server" class="fl input_text" style="width: 250px;" maxlength="20"/>
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">历史审核记录：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <table width="750" border="0" cellspacing="0" cellpadding="0" class="audit" id="AuditRecord">
                        <tr class="weight">
                            <td width="100">审核流程</td>
                            <td width="65">审核结果</td>
                            <td width="70">审核人</td>
                            <td width="160">时间</td>
                            <td width="177">原因</td>
                            <td width="176">评审意见</td>
                        </tr>

                    </table>

                </td>
            </tr>
            <tr>
                <td style="border-bottom: 0; text-align: right;">&nbsp;</td>
                <td style="border-bottom: 0; text-align: left; padding-left: 5px;">
                    <div class="selectDiv">
                        <asp:Button ID="button1" runat="server" Text="审核" OnClick="Button1_Click" Width="60" CssClass="inputButton" />
                        <input type="button" value="返回" style="width: 60px;" class="inputButton" onclick="window.location.href = 'LoanManage.aspx?<%=HttpContext.Current.Request.QueryString.ToString()%>';" />
                    </div>
                    <%--<input type="button" id="button1" runat="server" value="审核" OnServerClick="Button1_Click"/>--%>
                </td>

            </tr>
        </table>

        <div>
        </div>
    </form>
    <script type="text/javascript">
            $(function () {
                $(':checkbox').tzCheckbox({ labels: ['Enable', 'Disable'] });
            });
    </script>
    <script src="../js/select2css.js"></script>
</body>
</html>
