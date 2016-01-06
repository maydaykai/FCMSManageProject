<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanAuditNew.aspx.cs" Inherits="WebUI.p2p.LoanAuditNew" ValidateRequest="false" EnableEventValidation="false" %>

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
    <script src="../js/datetime.js"></script>

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
                    $("#lab_daymonth").html("(年化) 服务费：0.00 元");
                    $("#lab_loanTerm").html("(按天)");
                } else {
                    $("#txtLoanServiceCharges").val("0");
                    $("#lab_daymonth").html("(按月)");
                    $("#lab_loanTerm").html("(按月)");
                }
            });

            $("#txtLoanServiceCharges").blur(function(){
                if($('#sel_repaymentMethod').val() == 2 && $(this).val()!="")
                {
                    var fee=parseInt($("#txt_LoanAmount").val())*parseInt($("#txt_LoanTerm").val())*parseInt($("#txtLoanServiceCharges").val())/100/365;
                    $("#lab_daymonth").html("(年化) 服务费："+ fee.toFixed(2)+" 元");
                }
            });

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
            
            $("#options_sel_LoanScaleType li").live("click", function () {
                $.ajax(
                    {
                        type: "POST",
                        async: false,
                        url:"../HanderAshx/p2p/DimProjectTemplate.ashx?productTypeId=" + $("#sel_LoanScaleType").val(),
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        success: function(result) {
                            var jsondatas = result[0];
                            editor.html(jsondatas.Template);
                        }
                    });
            });

            var loanid = getQueryString("ID");
            loadInfo(loanid);
        });

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
                                "<td width=\"160\">" + $.FormatDateTime(n.AuditTime,2) + "</td>" +
                                "<td width=\"177\">" + n.Reason + "</td>" +
                                "<td width=\"176\">" + n.ReviewComments + "</td>" +
                                "</tr>";
                            $("#AuditRecord").append(temphtml);
                            
                        });
                    }
                });
            AuthInfoList(loanid, 2);
        }
        
        function sumScore(loanId) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/HanderAshx/p2p/LoanScore.ashx?sign=2&loanId=" + loanId,
                data: "text",
                dataType: "json",
                success: function (result) {
                    $("#lab_SumScore").text(result[0].SumScore);
                    $("#lab_ScoreLevel").text(result[0].ScoreLevel);
                }
            });
        }

        function AuthInfoList(loanid,sign) {
            var params = { "loanid": "" + loanid + "","sign": "" + sign + "" };
            $.ajax(
                 {
                     type: "get",
                     url: "../HanderAshx/p2p/LoanAuthInfo.ashx",
                     async: false,//true表示异步 false表示同步  
                     contentType: "application/json; charset=utf-8",
                     data: params,
                     dataType: 'json',
                     success: function(result) {
                         var jsondatas = eval(result);
                         $("#AuthList").html("");
                         var temphtml = "";
                         $.each(jsondatas, function (i, n) {
                             temphtml = "<tr>" +
                                 "<td width=\"150\">" + n.AuthProductName + "</td>";
                             temphtml += "<td width=\"80\"><img src=\"../images/ok_icon.png\" width=\"15\" height=\"15\" class=\"ok_icon\"/></td>";
                             temphtml += "<td width=\"200\">" + n.AuthDate + "</td>" +
                                 "</tr>";
                             $("#AuthList").append(temphtml);
                            
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
        <table cellpadding="0" cellspacing="0" class="editTable">
            <tr>
                <td style="text-align: right;width: 15%">贷款会员：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 35%">
                    <label id="lab_MemberName" runat="server"></label>
                    <asp:Button ID="btnLookMemberInfo" runat="server" Text="查看会员信息" Width="120" CssClass="inputButton" OnClick="btnLookMemberInfo_Click" />
                </td>
                <td style="text-align: right;width: 10%">所在地区：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 40%">
                    <label id="lab_province" runat="server"></label>
                    <label id="lab_city" runat="server"></label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">当前状态：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <label id="lab_examStatus" runat="server"></label>
                </td>
                <td style="text-align: right">借款编号：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <label id="lab_LoanNumber" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">借款标题：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">
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
                <td style="text-align: right;width: 15%">借款标类型名称：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 35%">
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
                <td style="text-align: right;width: 10%">借款用途：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 40%">
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
            </tr>
            <tr>
                <td style="text-align: right;width: 15%">还款方式：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 35%">
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
                <td style="text-align: right;width: 10%">线上线下：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 40%">
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
                    <span class="fl" style="margin-top: 5px;color: red;" id="Span1">(线下方式目前主要针对年会活动)</span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;width: 15%">贷款期限：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 35%">
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
                <td style="text-align: right;width: 10%">贷款额度：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 40%">
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
                    <span class="fl" style="margin-top: 5px;" id="SpanLoanAmount">大写：<label id="lab_AuditAmountBig" runat="server"></label></span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;width: 15%">贷款利率(年)：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 35%" >
                    <div class="selectDiv" style="margin-top: 6px; display: inline-block;">
                        <div style="width: 4px; float: left;">
                            <%if (txtLoanRate.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="txtLoanRate" name="rate" runat="server" class="fl input_text" />
                        <div class="fl">
                            <%if (txtLoanRate.Attributes["readonly"] == "true")
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
                <td style="text-align: right;width: 10%">自动投标比例：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 40%" >
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
                <td style="text-align: right;width: 15%">最低投资金额：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 35%">
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
                    </div>
                </td>
                <td style="text-align: right;width: 10%">最高投资金额：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 40%">
                    <div class="selectDiv" style="margin-top: 5px;">
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
                <td style="text-align: right;width: 15%">竞标开始时间：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 35%">
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
                     </div>
                </td>
                <td style="text-align: right;width: 10%">竞标截止时间：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 40%">
                    <div style="margin-top: 5px;">
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
                <td style="text-align: right;width: 15%">担保公司：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 35%">
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
                <td style="text-align: right;width: 10%">收取担保费：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 40%">
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
                <td style="text-align: right;width: 15%">收取借款人居间服务费：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 35%">
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
                <td style="text-align: right;width: 10%">收取利息管理费：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 40%">
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
                <td style="text-align: right;width: 15%">资料认证：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 35%">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="audit" id="AuthList">
                        <tr class="weight">
                            <td width="150">审核项目</td>
                            <td width="80">状态</td>
                            <td width="200">通过日期</td>
                        </tr>

                    </table>
                    <asp:Button id="BtnAuth" runat="server" Text="认证" Width="60" CssClass="inputButton" OnClick="BtnAuth_Click" />
                </td>
                <td style="text-align: right;width: 10%">信用评分：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;width: 40%">
                    <label id="lab_SumScore" class="fl" runat="server"></label><span class="fl">&nbsp&nbsp等级：</span>
                    <label id="lab_ScoreLevel" class="fl" runat="server"></label>
                    <asp:Button id="BtnScore" runat="server" Text="评分" OnClick="BtnScore_Click" Width="60" CssClass="inputButton" />
                </td>
            </tr>
            
            <tr>
                <td style="text-align: right;width: 15%">借款合同号：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 35%>
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (text_ContractNo.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="text_ContractNo" name="rate" runat="server" class="fl input_text" maxlength="20"/>
                        <div class="fl">
                            <%if (text_ContractNo.Attributes["readonly"] == "true")
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
                <td style="text-align: right;width: 10%">分支机构：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 40%">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (text_Agency.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="text_Agency" name="rate" runat="server" class="fl input_text" maxlength="20"/>
                        <div class="fl">
                            <%if (text_Agency.Attributes["readonly"] == "true")
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
                <td style="text-align: right;width: 15%">项目负责人一：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 35%>
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (text_LinkmanOne.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="text_LinkmanOne" name="rate" runat="server" class="fl input_text" maxlength="20"/>
                        <div class="fl">
                            <%if (text_LinkmanOne.Attributes["readonly"] == "true")
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
                <td style="text-align: right;width: 10%">电话：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 40%">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (txt_TelOne.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="txt_TelOne" name="rate" runat="server" class="fl input_text" maxlength="11"/>
                        <div class="fl">
                            <%if (txt_TelOne.Attributes["readonly"] == "true")
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
                <td style="text-align: right;width: 15%">项目负责人二：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 35%>
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (text_LinkmanTwo.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="text_LinkmanTwo" name="rate" runat="server" class="fl input_text" maxlength="20"/>
                        <div class="fl">
                            <%if (text_LinkmanTwo.Attributes["readonly"] == "true")
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
                <td style="text-align: right;width: 10%">电话：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 40%">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (txt_TelTwo.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="txt_TelTwo" name="rate" runat="server" class="fl input_text" maxlength="11"/>
                        <div class="fl">
                            <%if (txt_TelTwo.Attributes["readonly"] == "true")
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
                <td style="text-align: right;width: 15%">月收入：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 35%>
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (txt_MonthlyProfit.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="txt_MonthlyProfit" runat="server" class="fl input_text" maxlength="20"/>
                        <div class="fl">
                            <%if (txt_MonthlyProfit.Attributes["readonly"] == "true")
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
                <td style="text-align: right;width: 10%">负债总额：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 40%">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (txt_LiabilitiesAmount.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="txt_LiabilitiesAmount" runat="server" class="fl input_text" maxlength="11"/>
                        <div class="fl">
                            <%if (txt_LiabilitiesAmount.Attributes["readonly"] == "true")
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
                <td style="text-align: right;width: 15%">负债比：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 35%>
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (txt_LiabilitiesRatio.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="txt_LiabilitiesRatio" runat="server" class="fl input_text" maxlength="20"/>
                        <div class="fl">
                            <%if (txt_LiabilitiesRatio.Attributes["readonly"] == "true")
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
                <td style="text-align: right;width: 10%">月可支配收入：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 40%">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <div style="width: 4px; float: left;">
                            <%if (txt_MonthlyControlIncome.Attributes["readonly"] == "true")
                              { %>
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            <%}
                              else
                              { %>
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                            <%} %>
                        </div>
                        <input type="text" id="txt_MonthlyControlIncome" runat="server" class="fl input_text" maxlength="11"/>
                        <div class="fl">
                            <%if (txt_MonthlyControlIncome.Attributes["readonly"] == "true")
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
                <td style="text-align: right;width: 15%">是否优质职业：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 35%>
                    <div class="selectDiv" style="margin-top: 5px;">

                        <asp:RadioButtonList ID="rdo_QualityProfessional" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="否" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>

                    </div>
                </td>
                <td style="text-align: right;width: 10%">是否有房产：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 40%">
                    <div class="selectDiv" style="margin-top: 5px;">

                        <asp:RadioButtonList ID="rdo_HouseProperty" runat="server" RepeatDirection="Horizontal" >
                            <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="否" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>

                    </div>
                </td>
            </tr>

            <tr>
                <td style="text-align: right;width: 10%">诉讼查询：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 40%" colspan="3">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <asp:TextBox runat="server" ID="txt_LitigationSeach" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                </td>
            </tr>

            <tr>
                <td style="text-align: right;width: 10%">社保查询：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 40%" colspan="3">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <asp:TextBox runat="server" ID="txt_SocialSecuritySeach" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                </td>
            </tr>

            <tr>
                <td style="text-align: right;width: 10%">信用记录查询：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 40%" colspan="3">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <asp:TextBox runat="server" ID="txt_CreditRecordSeach" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                </td>
            </tr>

            <tr>
                <td style="text-align: right;width: 10%">联系人情况说明：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 40%" colspan="3">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <asp:TextBox runat="server" ID="txt_ContactSituation" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                </td>
            </tr>

            <tr>
                <td style="text-align: right;width: 10%">其他情况说明：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;"width: 40%" colspan="3">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <asp:TextBox runat="server" ID="txt_OtherSituation" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                </td>
            </tr>

            <tr>
                <td style="text-align: right;">借款标描述：<br /><asp:Button id="Button3" runat="server" Text="临时保存" Width="80" CssClass="inputButton" OnClick="BtnTempSave" /></td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">
                    <textarea id="content1" name="content1" rows="10" style="width: 100%; visibility: hidden;" runat="server"></textarea>
                </td>
            </tr>

            <tr id="trAppointment" runat="server">
                <td style="text-align: right;">预约投资用户：<br /></td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3" runat="server" id="tdAppointment">
                    
                </td>
            </tr>
            
            <tr>
                <td style="text-align: right;">上传附件：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">
                    <div id="fileQueue"></div>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="上传附件" CssClass="inputButton" Width="100" /><br />
                    文件列表：<br />
                    <asp:Table ID="tbDirInfo" runat="server" /><br />
                    <div style="width: 4px; float: left;">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </div>
                    <input type="text" id="text_FileName" runat="server" class="fl input_text" />
                    <asp:Button ID="BtnDeleteFile" runat="server" Text="删除文件" CssClass="inputButton" Width="100" OnClick="BtnDeleteFile_Click" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">审核原因：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">
                    <div class="selectDiv" style="margin-top: 5px; display: inline-block;">
                        <input type="radio" id="radio_audit" name="audit" runat="server" checked="True" class="fl" style="margin-top: 5px;" /><span class="fl" style="margin-top: 5px; margin-right: 5px;">审核通过</span>
                        <input type="radio" id="radio_noAudit" name="audit" runat="server" class="fl" style="margin-top: 5px;" /><span class="fl" style="margin-top: 5px;">审核不通过</span>
                        <span class="fl" style="margin-top: 5px;">&nbsp不通过理由：</span>

                        <div style="width: 4px; float: left;">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </div>
                        <input type="text" id="textReason" runat="server" class="fl input_text" style="width: 250px;" maxlength="20"/>
                        <div class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">评审意见：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">
                    <textarea rows="5" style="width: 100%;" id="textReviewComments" runat="server">
                </textarea>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">历史审核记录：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="audit" id="AuditRecord">
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
                <td style="border-bottom: 0; text-align: left; padding-left: 5px;" colspan="3">
                    <div class="selectDiv">
                        <input type="button" class="inputButton" value="审核" style="width:60px;" onclick="MessageAlertOk('确定进行此操作吗？','warning','button1');" />                        
                        <asp:Button ID="button1" runat="server"  style="display:none;" Text="审核" OnClick="Button1_Click" Width="60" CssClass="inputButton" />
                        <input type="button" value="返回" style="width: 60px;" class="inputButton" onclick="window.location.href = 'LoanManage.aspx?<%=HttpContext.Current.Request.QueryString.ToString()%>';" />
                    </div>
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
