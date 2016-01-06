<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocalCreditDetail.aspx.cs" ValidateRequest="false" Inherits="WebUI.p2p.LocalCreditDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

    <script src="../js/My97DatePicker/WdatePicker.js"></script>

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
        

        $(function(){  
            
            $("#txt_ApplyAmount").bind("input propertychange", function() {
                $("#BidAmountMax").val($(this).val());
            });
        })  

        


    </script>
    <style type="text/css">
        .auto-style1 {
            height: 29px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <fieldset>
            <legend>会员信息</legend>
            <p>&nbsp;</p>
            <table cellpadding="0" cellspacing="0" class="editTable">
                <tr>
                    <td style="text-align: right; width: 15%">会员帐号：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <label id="lab_MemberName" runat="server"></label>
                    </td>
                    <td style="text-align: right; width: 15%">姓名：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <label id="lab_RealName" runat="server"></label>

                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">身份证号：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_IdentityCard" runat="server"></label>
                    </td>
                    <td style="text-align: right;">性别：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Sex" runat="server"></label>
                    </td>
                </tr>
            </table>


        </fieldset>
        <p>&nbsp;</p>
        <fieldset>
            <legend>基本信息</legend>
            <p>&nbsp;</p>
            <table cellpadding="0" cellspacing="0" class="editTable">
                <tr>
                    <td style="text-align: right; width: 15%">学历：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <label id="lab_EducationName" runat="server"></label>
                    </td>
                    <td style="text-align: right; width: 15%">社保卡号：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <label id="lab_SocialCard" runat="server" />
                    </td>
                </tr>


                <tr>
                    <td style="text-align: right;">住宅地址：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Residence" runat="server"></label>
                    </td>
                    <td style="text-align: right">住宅电话：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_ResidenceTelephone" runat="server" />

                    </td>
                </tr>


                <tr>
                    <td style="text-align: right;">移动电话：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Mobile" runat="server"></label>
                    </td>
                    <td style="text-align: right">单位性质：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_CompanyNatureName" runat="server" />

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">工作单位名称：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_CompanyName" runat="server"></label>
                    </td>
                    <td style="text-align: right">单位地址：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_CompanyAddress" runat="server" />

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">单位电话：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_CompanyTelephone" runat="server"></label>
                    </td>
                    <td style="text-align: right">工作职务：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Duties" runat="server" />

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">应急联系人1：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_EmergencyName1" runat="server"></label>
                    </td>
                    <td style="text-align: right">应急联系人关系1：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Relationship1" runat="server" />

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">应急联系人电话1：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_ContactNum1" runat="server"></label>
                    </td>
                    <td style="text-align: right">应急联系人2：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_EmergencyName2" runat="server" />

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">应急联系人关系2：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Relationship2" runat="server"></label>
                    </td>
                    <td style="text-align: right">应急联系人电话2：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_ContactNum2" runat="server" />

                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">应急联系人3：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_EmergencyName3" runat="server"></label>
                    </td>
                    <td style="text-align: right">应急联系人关系3：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Relationship3" runat="server" />

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">应急联系人电话3：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_ContactNum3" runat="server"></label>
                    </td>
                    <td style="text-align: right">配偶姓名：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_WifeName" runat="server" />

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">配额偶身份证号码：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_IdCard" runat="server"></label>
                    </td>
                    <td style="text-align: right">配偶移动电话：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_WifeMobile" runat="server" />

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">配偶月均收入：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_WifeIncome" runat="server"></label>
                    </td>
                    <td style="text-align: right">配偶单位名称：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_CompanyName2" runat="server"></label>

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">配偶单位地址：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_CompanyAddress2" runat="server"></label>
                    </td>
                    <td style="text-align: right">配偶单位电话：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_CompanyTelephone2" runat="server"></label>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">配偶单位性质：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_CompanyNature2" runat="server"></label>
                    </td>
                    <td style="text-align: right">配偶工作职务：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_WifeDuties" runat="server"></label>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">房产权利人：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_HouseOwner" runat="server"></label>
                    </td>
                    <td style="text-align: right">产权编号：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_HouseNumber" runat="server"></label>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">房产地址：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_HouseAddress" runat="server"></label>
                    </td>
                    <td style="text-align: right">车辆权利人：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_CarOwner" runat="server"></label>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">车辆号码：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_CarNumber" runat="server"></label>
                    </td>
                    <td style="text-align: right">车辆类型：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_CarBrand" runat="server"></label>
                    </td>
                </tr>

            </table>
        </fieldset>
        <p>&nbsp;</p>
        <fieldset>
            <legend>上传资料</legend>
            <p>&nbsp;</p>
            <table cellpadding="0" cellspacing="0" class="editTable">
                <tr>
                    <td style="text-align: right; width: 15%">身份证：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <label id="lab_IDCardAuthen1" runat="server" />
                        &nbsp;&nbsp;
                        <label id="lab_IDCardAuthen2" runat="server" />
                        &nbsp;&nbsp;
                        <label id="lab_IDCardAuthen3" runat="server" />
                        &nbsp;&nbsp;
                    </td>
                    <td style="text-align: right; width: 15%">户口本：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <label id="lab_BookAuthen1" runat="server" />
                        &nbsp;&nbsp;
                        <label id="lab_BookAuthen2" runat="server" />
                        &nbsp;&nbsp;
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">社保：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_SocialAuthen" runat="server" />
                    </td>
                    <td style="text-align: right">住址证明：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_ResidenceAuthen" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">工作证明：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_WorkAuthen" runat="server" />
                    </td>
                    <td style="text-align: right">收入证明：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_BankCard1" runat="server" />
                        &nbsp;&nbsp;
                        <label id="lab_BankCard2" runat="server" />
                        &nbsp;&nbsp;
                        <label id="lab_BankStreamLine" runat="server" />
                        &nbsp;&nbsp;
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">征信报告：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_CreditReport" runat="server" />
                    </td>
                    <td style="text-align: right">信用卡对账单：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_CreditCard" runat="server" />
                        &nbsp;&nbsp;
                        <label id="lab_CreditCardBill" runat="server" />
                        &nbsp;&nbsp;
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">学历、学位证书：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_EducationAuthen" runat="server" />
                    </td>
                    <td style="text-align: right">结婚证：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_MarryAuthen" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">房产证：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_HouseAuthen" runat="server" />
                        &nbsp;&nbsp;
                        <label id="lab_HouseAuthenPage" runat="server" />
                        &nbsp;&nbsp;
                    </td>
                    <td style="text-align: right"></td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="Label12" runat="server" />
                    </td>
                </tr>

            </table>
        </fieldset>

        <p>&nbsp;</p>
        <fieldset>
            <legend>调查信息</legend>
            <p>&nbsp;</p>
            <table cellpadding="0" cellspacing="0" class="editTable">
                <tr>
                    <td style="text-align: right; width: 15%">月收入：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <asp:TextBox runat="server" ID="txt_MonthlyProfit"></asp:TextBox>
                    </td>
                    <td style="text-align: right; width: 15%">负债总额：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <asp:TextBox runat="server" ID="txt_LiabilitiesAmount"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 15%">负债比：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <asp:TextBox runat="server" ID="txt_LiabilitiesRatio"></asp:TextBox>
                    </td>
                    <td style="text-align: right; width: 15%">月可支配收入：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <asp:TextBox runat="server" ID="txt_MonthlyControlIncome"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 15%">是否优质职业：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <asp:RadioButtonList ID="rdo_QualityProfessional" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="否" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align: right; width: 15%">是否有房产：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <asp:RadioButtonList ID="rdo_HouseProperty" runat="server" RepeatDirection="Horizontal" >
                            <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="否" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 15%">诉讼查询：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%" colspan="3">
                        <asp:TextBox runat="server" ID="txt_LitigationSeach" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 15%">社保查询：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%" colspan="3">
                        <asp:TextBox runat="server" ID="txt_SocialSecuritySeach" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 15%">信用记录查询：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%" colspan="3">
                        <asp:TextBox runat="server" ID="txt_CreditRecordSeach" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 15%">联系人情况说明：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%" colspan="3">
                        <asp:TextBox runat="server" ID="txt_ContactSituation" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 15%">其他情况说明：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%" colspan="3">
                        <asp:TextBox runat="server" ID="txt_OtherSituation" Width="98%" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>

        <p>&nbsp;</p>
        <fieldset>
            <legend>借款信息</legend>
            <p>&nbsp;</p>
            <table cellpadding="0" cellspacing="0" class="editTable">
                <tr>

                    <td style="text-align: right; width: 15%">申请贷款金额：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <label id="lab_LoanAmount" runat="server" />
                    </td>

                    <td style="text-align: right; width: 15%">批贷金额：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <asp:TextBox runat="server" ID="txt_ApplyAmount"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">还款方式：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_RepaymentMethod" runat="server">按月等额本息</label>
                    </td>

                    <td style="text-align: right;">借款期限（按月）：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">

                        <div class="selectDiv" style="margin-top: 5px;">
                            <div class="fl">
                                <img src="/images/input_left.png" width="4" height="29" alt="" />
                            </div>
                            <select id="lab_LoanTerm" runat="server">
                            </select>
                            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_lab_LoanTerm" />
                            </div>
                        </div>

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">年化利率：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <div class="selectDiv" style="margin-top: 5px;">
                            <div class="fl">
                                <img src="/images/input_left.png" width="4" height="29" alt="" />
                            </div>
                            <select id="lab_LoanRate" runat="server">
                            </select>
                            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_lab_LoanRate" />
                            </div>
                        </div>
                    </td>
                    <td style="text-align: right">申请时间：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_createTime" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">审批结果：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <span class="selectDiv" style="margin-top: 5px; display: inline-block;">
                            <input type="radio" id="radio_audit" name="audit" runat="server" checked="True" class="fl" style="margin-top: 5px;" /><span class="fl" style="margin-top: 5px; margin-right: 5px;">审批通过</span>
                            <input type="radio" id="radio_Audit_loading" name="audit" runat="server" class="fl" style="margin-top: 5px;" /><span class="fl" style="margin-top: 5px;">审批不通过</span>
                            <span class="fl" style="margin-top: 5px; display: none">&nbsp不通过理由：</span>

                            <span style="width: 4px; float: left; display: none">
                                <img src="/images/input_left.png" width="4" height="29" alt="" />
                            </span>
                            <input type="text" id="textReason" runat="server" class="fl input_text" style="width: 250px; display: none" maxlength="20" />
                            <span class="fl">
                                <img src="../images/input_right.png" style="width: 4px; height: 29px; display: none" alt="" />
                            </span>

                        </span>
                    </td>

                    <td style="text-align: right;">借款用途：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">


                        <div class="selectDiv" style="margin-top: 5px;">
                            <div class="fl">
                                <img src="/images/input_left.png" width="4" height="29" alt="" />
                            </div>
                            <select id="sel_loanUseName" runat="server">
                            </select>
                            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_loanUseName" />
                            </div>
                        </div>
                    </td>

                </tr>


                <tr>
                    <td style="text-align: right;">借款描述：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">
                        <textarea id="content1" name="content1" rows="10" style="width: 100%;" runat="server"></textarea>
                    </td>
                </tr>


                <tr>
                    <td style="text-align: right;">申请人的借款审核结果：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">
                        <label id="lab_AuditRecords" runat="server"></label>

                    </td>
                </tr>

            </table>

        </fieldset>
        <p>&nbsp;</p>
        <fieldset>
            <legend>评分</legend>
            <p>&nbsp;</p>
            <table cellpadding="0" cellspacing="0" class="editTable">
                <tr>
                    <td style="text-align: right; width: 15%">资料认证：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="audit" id="AuthList">
                            <tr class="weight">
                                <td width="150">审核项目</td>
                                <td width="80">状态</td>
                                <td width="200">通过日期</td>
                            </tr>
                        </table>
                        <label id="Label34" runat="server">
                            <asp:Button ID="BtnAuth" runat="server" Text="认证" Width="60" CssClass="inputButton" OnClick="BtnAuth_Click" /></label>

                        <asp:CheckBoxList ID="ckbAuthList" runat="server" RepeatDirection="Horizontal" RepeatColumns="7" BorderColor="White" RepeatLayout="Flow" CssClass="checkList">
                        </asp:CheckBoxList>
                    </td>
                    <td style="text-align: right; width: 15%">信用评分：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <label id="lab_SumScore" class="fl" runat="server"></label>
                        <span class="fl">&nbsp&nbsp等级：</span>
                        <label id="lab_ScoreLevel" class="fl" runat="server"></label>
                        <asp:Button ID="BtnScore" runat="server" Text="评分" OnClick="BtnScore_Click" Width="60" CssClass="inputButton" />
                    </td>
                </tr>

            </table>

        </fieldset>
        <p>&nbsp;</p>
        <fieldset>
            <legend>设置借款信息</legend>
            <p>&nbsp;</p>
            <table cellpadding="0" cellspacing="0" class="editTable">
                <tr>
                    <td style="text-align: right; width: 15%">最低投资金额：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <asp:TextBox runat="server" ID="txtBidAmountMin"></asp:TextBox>
                    </td>
                    <td style="text-align: right; width: 15%">最高投资金额：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <asp:TextBox runat="server" ID="BidAmountMax" ReadOnly="true"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td style="text-align: right;">竞标开始时间：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <asp:TextBox runat="server" ID="WdataBidStartTime"></asp:TextBox>
                    </td>
                    <td style="text-align: right;">竞标截止时间：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <asp:TextBox runat="server" ID="WdataBidEndTime"></asp:TextBox>
                    </td>

                </tr>
                <tr>

                    <td style="text-align: right;">自动投标比例：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">
                        <input type="text" id="txtAutoBidScale" name="rate" runat="server" />%
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">上传附件：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">
                        <div id="fileQueue"></div>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:Button ID="Button4" runat="server" OnClick="Button3_Click" Text="上传附件" CssClass="inputButton" Width="100" /><br />
                        文件列表：<br />
                        <asp:Table ID="tbDirInfo" runat="server" />
                        <br />
                        <input type="text" id="text_FileName" runat="server" class="fl input_text" />
                        <asp:Button ID="BtnDeleteFile" runat="server" Text="删除文件" CssClass="inputButton" Width="100" OnClick="BtnDeleteFile_Click" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">担保公司：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">



                        <div class="selectDiv" style="margin-top: 5px;">
                            <div class="fl">
                                <img src="/images/input_left.png" width="4" height="29" alt="" />
                            </div>
                            <select id="sel_Guarantee" runat="server">
                                <option value="0" selected="selected">选择担保公司</option>
                            </select>
                            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_Guarantee" />
                            </div>
                        </div>
                    </td>
                    <td style="text-align: right;">担保费：</td>
                    <td style="text-align: left;">
                        <input type="text" id="txtGuaranteeFee" name="rate" runat="server" />
                        %
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">收取借款人居间服务费：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <input type="text" id="txt_server1" name="rate" runat="server" />
                        %(按年)
                    </td>
                    <td style="text-align: right;">收取投资人居间服务费：</td>
                    <td style="text-align: left;">
                        <input type="text" id="txt_server2" name="rate" runat="server" />
                        %
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
                    <td style="text-align: right; display: none">借款合同号：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; display: none">
                        <input type="text" id="text_ContractNo" name="rate" runat="server" />

                    </td>
                    <td style="text-align: right; display: none">分支机构：</td>
                    <td style="text-align: left; display: none">
                        <input type="text" id="text_Agency" name="rate" runat="server" />

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right; display: none">项目负责人一：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; display: none">
                        <input type="text" id="text_LinkmanOne" name="rate" runat="server" />

                    </td>
                    <td style="text-align: right; display: none">电话：</td>
                    <td style="text-align: left; display: none">
                        <input type="text" id="txt_TelOne" name="rate" runat="server" />

                    </td>
                </tr>


                <tr>
                    <td style="text-align: right; display: none">项目负责人二：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; display: none">
                        <input type="text" id="text_LinkmanTwo" name="rate" runat="server" />

                    </td>
                    <td style="text-align: right; display: none">电话：</td>
                    <td style="text-align: left; display: none">
                        <input type="text" id="txt_TelTwo" name="rate" runat="server" />

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
            </table>
        </fieldset>
        <p>&nbsp;</p>
        <asp:Button ID="button1" runat="server" Text="审核" Width="100" CssClass="inputButton" OnClick="button1_Click" />
        <input type="button" value="返回列表" style="width: 100px;" class="inputButton" onclick="window.location.href = 'LocalCreditManage.aspx?<%=HttpContext.Current.Request.QueryString.ToString()%>    ';" />
        <asp:HiddenField runat="server" ID="hidd_loanId" />
        <asp:HiddenField runat="server" ID="hidd_memberId" />
        <asp:HiddenField runat="server" ID="hidd_LoanAmount" />
        <asp:HiddenField runat="server" ID="hidd_LoanTerm" />
        <asp:HiddenField runat="server" ID="hidd_loanUse" />
        <asp:HiddenField runat="server" ID="hidd_mLoanAount" />

    </form>
    <script src="../js/select2css.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var loanid = getQueryString("LoanId");
            if(loanid==0 || loanid=="0" || loanid==null)
            {
                loanid=$("#hidd_loanId").val();
            }
            loadInfo(loanid);
            sumScore(loanid);
            
        });

        /*获取URL参数*/
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

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
                    success: function (result) {
                        var jsondatas = eval(result);
                        var temphtml = "";
                        $.each(jsondatas, function (i, n) {
                            temphtml = "<tr>" +
                                "<td width=\"100\">" + n.ProcessName + "</td>";
                            temphtml += "<td width=\"65\"><img src=\"../images/" + (n.Result == true ? "ok_icon.png" : "no_icon.png") + "\" width=\"15\" height=\"15\" class=\"ok_icon\"/></td>";
                            temphtml += "<td width=\"70\">" + n.UserName + "</td>" +
                                "<td width=\"160\">" + $.FormatDateTime(n.AuditTime, 2) + "</td>" +
                                "<td width=\"177\">" + n.Reason + "</td>" +
                                "<td width=\"176\">" + n.ReviewComments + "</td>" +
                                "</tr>";
                            $("#AuditRecord").append(temphtml);

                        });
                    }
                });
            AuthInfoList(loanid, 2);
        }

        function AuthInfoList(loanid, sign) {

            var params = { "loanid": "" + loanid + "", "sign": "" + sign + "" };
            $.ajax(
                 {
                     type: "get",
                     url: "../HanderAshx/p2p/LoanAuthInfo.ashx",
                     async: false,//true表示异步 false表示同步  
                     contentType: "application/json; charset=utf-8",
                     data: params,
                     dataType: 'json',
                     success: function (result) {
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



        function sumScore(loanId) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/HanderAshx/p2p/LoanScore.ashx?sign=2&loanId=" + loanId,
                data: "text",
                dataType: "json",
                async:false,
                success: function (result) {
                    $("#lab_SumScore").text(result[0].SumScore);
                    $("#lab_ScoreLevel").text(result[0].ScoreLevel);
                }
            });
        }

        
    </script>
</body>


<script src="../js/select2css.js"></script>

</html>
