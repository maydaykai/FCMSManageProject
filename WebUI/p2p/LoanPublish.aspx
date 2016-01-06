<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanPublish.aspx.cs" Inherits="WebUI.p2p.LoanPublish" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
    <script src="../js/jquery.formatCurrency-1.4.0.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>

    <script type="text/javascript">
        var ids = new Array();
        $(function () {
            $("#sel_province,#sel_nativeProvince,#sel_liveProvince").live("click", function () {
                getCityListFromHandlerByParentID($(this).val(), $(this).attr("id").replace("Province", "City").replace("province", "city"));
            });
            $("#sel_province,#sel_nativeProvince,#sel_liveProvince").click();
            $("#sel_repaymentMethod").live("click", function () {
                if ($('#sel_repaymentMethod').val() == 2) {
                    $("#lab_loanTerm").html("(按天)");
                } else {
                    $("#lab_loanTerm").html("(按月)");
                }
            });
            $("#sel_repaymentMethod").click();
            $("input[name^='txt']").keydown(function (event) {
                var keyCode = event.which;
                if (keyCode == 46 || keyCode == 8 || keyCode == 9 || keyCode == 190 || (keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 105) || keyCode == 110) {
                    return true;
                } else {
                    return false;
                }
            }).focus(function () {
                this.style.imeMode = 'disabled';
            });

            $("#sel_LoanNumber").live("change", function () {
                loadInfo($("#sel_LoanNumber").val());
            });
            getLoanNumberList($("#txtMemberID").val());
            $("#clear").click(function() {
                $("#balance,#lockAmount").text("");
                $("#txtMemberID2,#txtMemberName2").val("");
                $("#accountInfo,#greenChannelInfo").hide();
            });

            $("#sel_LoanScaleType").change(function () {
                isShow();
            });

            $("input[name='rad_appointment']").click(function () {
                isShow();
            });
            isShow();
        });

        /*是否显示融金客*/
        function isShow()
        {
            if ($("#sel_LoanScaleType").val() == 25) {
                $(".sfrjk").show();
                if($("input[name='rad_appointment']:checked").val() == 1)
                    $(".sfyy").show();
                else
                    $(".sfyy").hide();
            }
            else
                $(".sfrjk").hide();
        }

        function loadInfo(id) {
            $.ajax({
                type: "POST",
                async: false,
                url: "../HanderAshx/p2p/LoanApply.ashx?Id=" + id + "&status=" + 4,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    var jsondatas = result.Rows[0];

                    $("#sel_LoanScaleType").val(jsondatas.ProductTypeId);
                    $("#sel_loanUseName").val(jsondatas.LoanUseId);
                    $("#txt_LoanAmount").val(jsondatas.LoanAmount);
                    $("#sel_repaymentMethod").val(jsondatas.RepaymentMethod);
                    $("#txt_LoanTerm").val(jsondatas.LoanTerm);
                    $("#txtLoanRate").val(jsondatas.LoanRate);
                    $("#sel_province,#sel_nativeProvince,#sel_liveProvince").val(jsondatas.Province);
                    $("#sel_province,#sel_nativeProvince,#sel_liveProvince,#sel_repaymentMethod").click();
                    $("#sel_city,#sel_nativeCity,#sel_liveCity").val(jsondatas.CityId);
                    $("#txtGuaranteeNo").val(jsondatas.GuaranteeNo);
                }
            });
        }

        function SelectFun() {
            getLoanNumberList($("#txtMemberID").val());
            getUserInfo($("#txtMemberID").val(), $("#txtType").val());
        }
        function SelectFun2() {
            branchCompanyMemberInfo($("#txtMemberID2").val());
        }
        function getUserInfo(memberId, type) {
            if (type > 0) {
                $("#memberInfo").hide();
                $("#enterpriseInfo").show();
                if (type == 1)
                    $("#enterpriseTitle").text("企业信息");
                else if (type == 2)
                    $("#enterpriseTitle").text("担保公司信息");
                else if (type == 3)
                    $("#enterpriseTitle").text("回购公司信息");
            }
            $.ajax({
                type: "POST",
                async: false,
                url: "../HanderAshx/p2p/LoanUserInfo.ashx?memberId=" + memberId + "&type=" + type,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    if (result == null)
                        return;
                    else
                        initUserInfo(type, result[0]);
                }
            });
        }
        function initUserInfo(type, data) {
            if (type == 0) {
                $("#txt_memberAge").val(data.Age);
                $("input[name='rad_maritalStatus'][value='" + data.MaritalStatus + "']").attr("checked", "checked");
                $("input[name='rad_sexStatus'][value='" + data.Sex + "']").attr("checked", "checked");
                $("#txt_familyNum").val(data.FamilyNum);
                $("#txt_monthlyIncome").val(data.MonthlyPay);
                $("input[name='rad_houseStatus'][value='" + (data.HaveHouse ? 1 : 0) + "']").attr("checked", "checked");
                $("input[name='rad_carStatus'][value='" + (data.HaveCar ? 1 : 0) + "']").attr("checked", "checked");
                $("#text_workDuration").val(data.WorkingLife);
                $("input[name='rad_jobStatus'][value='" + data.JobStatus + "']").attr("checked", "checked");
            } else {
                $("#sel_companyNature").val(data.Nature);
                $("#sel_industry").val(data.IndustryCategory);
                $("#text_regCapital").val(data.RegisteredCapital);
                $("#text_mainBusiness").val(data.MainProducts);
                $("#text_companyAge").val(data.SetUpyear);
                $("#text_businessScope").val(data.BusinessScope);
                $("#text_employeeNum").val(data.Size);
            }
        }
        var branchCompanyMemberInfo = function (memberId) {
            $.ajax({
                type: "POST",
                async: false,
                url: "../HanderAshx/BranchCompanyManage/BranchCompanyEmployeeHandler.ashx?memberId=" + memberId,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    $("#balance").text(result.balance).formatCurrency();
                    $("#lockAmount").text(result.lockAmount).formatCurrency();
                    $("#surDeposit").text(result.balance - result.lockAmount).formatCurrency();
                    $("#accountInfo,#greenChannelInfo").show();
                }
            });
        };

        function isShowAppointment(obj)
        {
            
        }

        /*选择预约则判断预约金额是否大于借款金额*/
        function saveData()
        {
            if ($("input[name='rad_appointment']:checked").val() == 1)
            {
                if (parseFloat($("#txtAppointmentAmount").text()) > parseFloat($("#txt_LoanAmount").val()))
                {
                    MessageAlert('预约金额不能大于借款金额！', 'warning', '');
                    return false;
                }

            }
            return true;
        }
    </script>
    <style type="text/css">
        .auto-style1
        {
            height: 35px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellspacing="0" cellpadding="0" class="editTable" style="min-width: 500px; width: 40%; float: left;">
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
                        <span id="selectSpan" class="fl" style="line-height: 29px; margin-left: 3px;" runat="server">
                            <input id="select_btn" type="button" value="选择会员" class="inputButton" style="width: 62px; font-weight: normal;" onclick="MessageWindow(590, 360, '/MemberManage/MemberListSelect.aspx')" />
                        </span>
                        <input id="txtMemberID" name="txtMemberID" type="hidden" runat="server" />
                        <input id="txtType" name="txtType" type="hidden" runat="server" />
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
                        <input id="txtRealName" type="text" name="txtRealName" value="" class="input_Disabled fl" maxlength="50" style="width: 100px;" runat="server" readonly="readonly" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">已复审借款申请：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <select id="sel_LoanNumber" class="fl input_text" style="width: 150px; height: 32px" runat="server">
                            <option value="0" selected="selected">选择申请号</option>
                        </select>
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 150px;">借款标题：</td>
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
                        <input type="text" id="text_LoanTitle" name="rate" runat="server" class="fl input_text" maxlength="15" />
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
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <select id="sel_LoanScaleType" class="fl input_text" style="width: 150px; height: 32px" runat="server">
                            <option value="0" selected="selected">选择借款标类型</option>
                        </select>
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">借款用途：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <select id="sel_loanUseName" class="fl input_text" style="width: 150px; height: 32px" runat="server">
                            <option value="0" selected="selected">选择借款用途</option>
                        </select>
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
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
                    <span class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </span>
                    <select id="sel_repaymentMethod" class="fl input_text" style="width: 150px; height: 32px" runat="server">
                        <option id="repayment0" value="0" selected="selected">--请选择--</option>
                        <option id="repayment1" value="1">按天计息按月还款</option>
                        <option id="repayment2" value="2">按天计息一次性还款</option>
                        <option id="repayment3" value="3">按月付息到期还本</option>
                        <option id="repayment4" value="4">按月等额本息</option>
                    </select>
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
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
                    <span class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </span>
                    <select id="sel_province" class="fl input_text" style="margin: 0; width: 100px;" runat="server">
                        <option value="0" selected="selected">选择省份</option>
                    </select>
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <span class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </span>
                    <select id="sel_city" class="fl input_text" style="margin: 0; width: 100px;" runat="server">
                        <option value="0" selected="selected">选择城市</option>
                    </select>
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">线上线下：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </span>
                    <select id="sel_TradeType" class="fl input_text" style="width: 150px; height: 32px" runat="server">
                        <option value="0" selected="selected">线上</option>
                        <option value="1">线下</option>
                    </select>
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">担保函号：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtGuaranteeNo" type="text" name="textGuaranteeNo" value="" class="input_Disabled fl" maxlength="50" style="width: 100px;" runat="server" readonly="readonly" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">导入人(选填)：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtMemberName2" type="text" name="textGuaranteeNo" value="" class="input_Disabled fl" maxlength="50" style="width: 100px;" runat="server" readonly="readonly" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl" style="line-height: 29px; margin-left: 3px;" runat="server">
                            <input type="button" value="选择会员" class="inputButton" style="width: 62px; font-weight: normal;" onclick="MessageWindow(590, 360, '/MemberManage/MemberListSelect.aspx?branchCompanyId=1')" />
                            <input id="clear" type="button" value="清除" class="inputButton" style="width: 62px; font-weight: normal;" />
                        </span>
                        <input id="txtMemberID2" name="txtMemberID" type="hidden" runat="server" />
                    </div>
                </td>
            </tr>
            <tr id="accountInfo" class="hide">
                <td style="text-align: right">账户信息：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left;">
                        <span class="fl">账户余额：</span><label class="fl" id="balance" runat="server"></label>
                        <span class="fl" style="margin: 0 0 0 5px;">冻结保证金：</span><label class="fl" id="lockAmount" runat="server"></label><br />
                        <span class="fl">剩余保证金：</span><label class="fl" id="surDeposit" runat="server"></label>
                    </div>
                </td>
            </tr>
            <tr id="greenChannelInfo" class="hide">
                <td style="text-align: right">绿色通道比例：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtScale" type="text" name="textGuaranteeNo" value="" class="input_text fl" maxlength="50" style="width: 100px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl" style="margin-top: 6px;">%</span>
                    </div>
                </td>
            </tr>
            <tr class="sfrjk">
                <td style="text-align: right">是否开放预约：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <label>
                            <input type="radio" name="rad_appointment" checked="true" value="0" runat="server"  />
                            否
                        </label>
                        <label>
                            <input type="radio" name="rad_appointment" value="1" runat="server" />
                            是
                        </label>
                        <input type="hidden" id="hdAppointment" runat="server" />
                    </div>
                </td>
            </tr>
            <%-- <tr class="sfrjk sfyy"> --%>
            <tr style="display:none;">
                <td style="text-align: right">预约开始时间：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtBiddingTime" type="text" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" class="input_text fl" runat="server" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr class="sfrjk sfyy">
                <td style="text-align: right">预约截至时间：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtAppointmentEndTime" type="text" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'2014-07-01',errDealMode:'1'})" class="input_text fl" runat="server" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="border-bottom: 0; text-align: right;">&nbsp;</td>
                <td style="border-bottom: 0; text-align: left; padding-left: 5px;">
                    <asp:Button ID="button1" runat="server" Text="立即申请" OnClientClick="return saveData();"  OnClick="Button1_Click" Width="60" CssClass="inputButton" />
                </td>
            </tr>
        </table>
        <table id="memberInfo" border="0" cellspacing="0" cellpadding="0" class="editTable" style="min-width: 500px; width: 40%; float: left; margin-left: 10px;">
            <tr>
                <td style="padding-top: 5px; text-align: center; font-weight: bold;" colspan="2">用户信息</td>
            </tr>
            <tr>
                <td style="text-align: right">年龄：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txt_memberAge" type="text" value="" class="input_text fl" maxlength="50" style="width: 100px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl" style="margin: 5px 0 0 5px;">岁</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">婚姻状况：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <label>
                            <input type="radio" name="rad_maritalStatus" value="1" checked="true" runat="server" />
                            已婚
                        </label>
                        <label>
                            <input type="radio" name="rad_maritalStatus" value="2" runat="server" />
                            未婚
                        </label>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 150px;">性别：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <label>
                            <input id="Radio1" type="radio" name="rad_sexStatus" checked="true" value="男" runat="server" />
                            男
                        </label>
                        <label>
                            <input id="Radio2" type="radio" name="rad_sexStatus" value="女" runat="server" />
                            女
                        </label>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">籍贯：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span><select id="sel_nativeProvince" class="fl input_text" style="margin: 0; width: 100px;" runat="server">
                            <option value="0" selected="selected">选择省份</option>
                        </select>
                        
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <select id="sel_nativeCity" class="fl input_text" style="margin: 0; width: 100px;" runat="server">
                            <option value="0" selected="selected">选择城市</option>
                        </select>
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;" class="auto-style1">家庭人数：</td>
                <td style="text-align: left; padding-left: 5px;" class="auto-style1">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txt_familyNum" type="text" name="txt_familyNum" value="" class="input_text fl" maxlength="50" style="width: 100px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl" style="margin: 5px 0 0 5px;">人</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">月收入水平：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txt_monthlyIncome" type="text" name="txt_monthlyIncome" value="" class="input_text fl" maxlength="50" style="width: 100px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">有无房产：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <label>
                            <input id="Radio3" type="radio" name="rad_houseStatus" checked="true" value="1" runat="server" />
                            有
                        </label>
                        <label>
                            <input id="Radio4" type="radio" name="rad_houseStatus" value="0" runat="server" />
                            无
                        </label>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">有无车产：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <label>
                            <input id="Radio5" type="radio" name="rad_carStatus" checked="true" value="1" runat="server" />
                            有
                        </label>
                        <label>
                            <input id="Radio6" type="radio" name="rad_carStatus" value="0" runat="server" />
                            无
                        </label>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">工作年限：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="text_workDuration" type="text" name="text_workDuration" value="" class="input_text fl" maxlength="50" style="width: 100px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">职业状态：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <label>
                            <input type="radio" name="rad_jobStatus" checked="true" value="在职员工" runat="server" />
                            在职员工
                        </label>
                        <label>
                            <input type="radio" name="rad_jobStatus" value="私企老板" runat="server" />
                            私企老板
                        </label>
                    </div>
                </td>
            </tr>
        </table>
        <table id="enterpriseInfo" border="0" cellspacing="0" cellpadding="0" class="editTable" style="min-width: 500px; width: 40%; float: left; margin-left: 10px; display: none;">
            <tr>
                <td id="enterpriseTile" style="padding-top: 5px; text-align: center; font-weight: bold;" colspan="2">企业信息</td>
            </tr>
            <tr>
                <td style="text-align: right">公司性质：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <select id="sel_companyNature" class="fl input_text" style="margin: 0; width: 208px;" runat="server">
                            <option value="0" selected="selected">--请选择--</option>
                            <option value="国有企业">国有企业</option>
                            <option value="集体企业">集体企业</option>
                            <option value="有限责任公司">有限责任公司</option>
                            <option value="股份有限公司">股份有限公司</option>
                            <option value="私营企业">私营企业</option>
                            <option value="中外合资企业">中外合资企业</option>
                            <option value="外商投资企业">外商投资企业</option>
                        </select>
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">所在城市：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <select id="sel_liveProvince" class="fl input_text" style="margin: 0; width: 100px;" runat="server">
                            <option value="0" selected="selected">--请选择--</option>
                        </select>
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <select id="sel_liveCity" class="fl input_text" style="margin: 0; width: 100px;" runat="server">
                            <option value="0" selected="selected">--请选择--</option>
                        </select>
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 150px;">行业类别：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <select id="sel_industry" class="fl input_text" style="margin: 0; width: 208px;" runat="server">
                            <option value="0" selected="selected">--请选择--</option>
                            <option value="计算机/互联网/通信/电子">计算机/互联网/通信/电子</option>
                            <option value="会计/金融/银行/保险">会计/金融/银行/保险</option>
                            <option value="贸易/消费/制造/营运">贸易/消费/制造/营运</option>
                            <option value="制药/医疗">制药/医疗</option>
                            <option value="广告/媒体">广告/媒体</option>
                            <option value="房地产/建筑">房地产/建筑</option>
                            <option value="专业服务/教育/培训">专业服务/教育/培训</option>
                            <option value="物流/运输">物流/运输</option>
                            <option value="能源/原材料">能源/原材料</option>
                            <option value="政府/非赢利机构">政府/非赢利机构</option>
                            <option value="其他">其他</option>
                        </select>
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">注册资本：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="text_regCapital" type="text" name="text_regCapital" value="" class="input_text fl" maxlength="50" style="width: 100px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">主营产品：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="text_mainBusiness" type="text" name="text_mainBusiness" value="" class="input_text fl" maxlength="50" style="width: 208px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">成立年限：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="text_companyAge" type="text" name="text_companyAge" value="" class="input_text fl" maxlength="50" style="width: 100px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl" style="margin: 5px 0 0 5px;">年</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">经营范围：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="text_businessScope" type="text" name="text_businessScope" value="" class="input_text fl" maxlength="50" style="width: 208px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">员工人数：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 5px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="text_employeeNum" type="text" name="text_employeeNum" value="" class="input_text fl" maxlength="50" style="width: 100px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl" style="margin: 5px 0 0 5px;">人</span>
                    </div>
                </td>
            </tr>
        </table>
        <table id="tabAppointmentList" border="0" cellspacing="0" cellpadding="0" class="editTable" style="min-width: 500px; display:none; width: 40%; float: left; margin-left: 10px; margin-top:10px;">
            <tr>
                <td style="padding-top: 5px; text-align: center; font-weight: bold;" colspan="2">预约信息</td>
            </tr>
            <tr>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px; margin-left:20px;"> 
                        <span class="fl" style="line-height: 29px;">预约用户余额：<span id="txtAppointmentAmount">0</span> 元</span>
                        <span class="fl" style="line-height: 29px; margin-left: 3px; margin-bottom:3px;">
                            <input id="Button2" type="button" value="选择会员" class="inputButton" style="width: 62px; font-weight: normal; margin-left:10px;"  onclick="MessageWindow(640, 360, '/MemberManage/AppointmentMemberSelect.aspx')" />
                        </span>
                    </div>                        
                </td>
            </tr>
            <tr>
                <td style="text-align: left; padding-left: 5px;">
                    <div class="selectDiv" style="margin-top: 3px; margin-bottom:3px; height: 110px; overflow-y: scroll;">
                        <table class="editTable" id="tabAppointment" style="min-width: 388px;">
                            <tr>
                                <td>会员名</td>
                                <td>预约电话</td>
                                <td>预约金额</td>
                                <td>可用余额</td>
                                <td>申请时间</td>
                            </tr>                            
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
