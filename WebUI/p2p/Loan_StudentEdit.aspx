<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Loan_StudentEdit.aspx.cs" Inherits="WebUI.p2p.Loan_StudentEdit" ValidateRequest="false" %>

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
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <fieldset>
            <legend>会员信息</legend>
            <table cellpadding="0" cellspacing="0" class="editTable">
                <tr>
                    <td style="text-align: right; width: 15%">会员帐号：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <label id="lab_MemberName" runat="server"></label>
                        <%-- <asp:Button ID="btnLookMemberInfo" runat="server" Text="查看会员信息" Width="120" CssClass="inputButton"  />--%>
                    </td>
                    <td style="text-align: right; width: 10%">姓名：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 40%">
                        <label id="lab_RealName" runat="server"></label>

                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">身份证号：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_IdentityCard" runat="server"></label>
                    </td>
                    <td style="text-align: right">性别：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Sex" runat="server" />
                    </td>
                </tr>
            </table>


        </fieldset>
        <fieldset>
            <legend>学生基本信息</legend>
            <table cellpadding="0" cellspacing="0" class="editTable">
                <tr>
                    <td style="text-align: right;">院校名称：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_UniversityName" runat="server"></label>
                    </td>
                    <td style="text-align: right">院校省市：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="Label8" runat="server" />
                        广东省深圳市
                    </td>
                </tr>


                <tr>
                    <td style="text-align: right;">专业：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Professional" runat="server"></label>
                    </td>
                    <td style="text-align: right">学历：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Education" runat="server" />

                    </td>
                </tr>


                <tr>
                    <td style="text-align: right;">学号：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_StudentID" runat="server"></label>
                    </td>
                    <td style="text-align: right">入学时间：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_EnrollmentYear" runat="server" />

                    </td>
                </tr>

                <tr>

                    <td style="text-align: right">手机号码：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Mobile" runat="server" />

                    </td>
                    <td style="text-align: right;">通讯地址：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Mailingaddress" runat="server"></label>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">家庭住址：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_AccountLocationCity" runat="server"></label>
                    </td>
                    <td style="text-align: right">直系亲属父亲姓名：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Lineal_one" runat="server" />

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">联系电话1：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Lineal_one_mobile" runat="server"></label>
                    </td>
                    <td style="text-align: right">直系亲属母亲姓名：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Lineal_two" runat="server" />

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">直系亲属父亲工作单位：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_one_job" runat="server"></label>
                    </td>
                    <td style="text-align: right">直系亲属母亲工作单位：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_two_job" runat="server" />

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">直系亲属父亲联系电话：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_one_tel" runat="server"></label>
                    </td>
                    <td style="text-align: right">直系亲属母亲联系电话：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_two_tel" runat="server" />

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">直系亲属父亲家庭住址：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_one_adress" runat="server"></label>
                    </td>
                    <td style="text-align: right">直系亲属母亲家庭住址：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_two_adress" runat="server" />

                    </td>
                </tr>



                <tr>

                    <td style="text-align: right">好友 1：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Friend_one" runat="server" />

                    </td>
                    <td style="text-align: right;">好友1联系电话：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Friend_one_mobile" runat="server"></label>
                    </td>
                </tr>

                <tr>

                    <td style="text-align: right">好友 2：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_Friend_two" runat="server" />
                        <td style="text-align: right;">好友2联系电话：</td>
                        <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                            <label id="lab_Friend_two_mobile" runat="server"></label>
                        </td>

                    </td>
                </tr>

                <tr>

                    <td style="text-align: right">身份证正面照：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <asp:HyperLink runat="server" ID="lab_PositiveIdentityCard"></asp:HyperLink>

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">身份证反面照：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <asp:HyperLink runat="server" ID="lab_NegativeIdentityCard"></asp:HyperLink>
                    </td>
                    <td style="text-align: right">学生证正面照：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <asp:HyperLink runat="server" ID="lab_StudentIDCard"></asp:HyperLink>
                    </td>
                </tr>

                <tr>
                    <%--<td style="text-align: right;">近6个月银行流水：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <asp:HyperLink runat="server" ID="lba_Bankwater"></asp:HyperLink>
                    </td>--%>
                    <td style="text-align: right;">学籍信息：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <asp:HyperLink runat="server" ID="lab_StudentInformationScreenshot"></asp:HyperLink>
                    </td>
                    <td style="text-align: right">支付宝截图：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <asp:HyperLink runat="server" ID="lab_Alipay"></asp:HyperLink>
                    </td>
                </tr>
                <tr>


                    <td style="text-align: right">手持身份证图：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <asp:HyperLink runat="server" ID="lab_HeadsetIdentityCard"></asp:HyperLink>
                    </td>

                    <td style="text-align: right">支付宝上一年对账单：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <asp:HyperLink runat="server" ID="lab_StudentInformationlastYearAlipay"></asp:HyperLink>
                    </td>
                </tr>
            </table>
        </fieldset>

        <fieldset>
            <legend>借款信息</legend>
            <table cellpadding="0" cellspacing="0" class="editTable">
                <tr>

                    <td style="text-align: right">借款金额：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">
                        <%-- <label id="lab_LoanAmount" runat="server" />--%>
                        <asp:TextBox runat="server" ID="lab_LoanAmount"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">借款期限（按月）：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">


                        <%--    <input type="text" id="lab_LoanTerm" name="rate" runat="server" />img_sel_repaymentMethod--%>

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
                    <td style="text-align: right;">还款方式：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <%--  <asp:TextBox runat="server" ID="lab_remake"></asp:TextBox>--%>
                        <label id="lab_remake" runat="server">按月等额本息</label>
                    </td>
                    <td style="text-align: right">申请时间：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_createTime" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">利率：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">

                        <div class="selectDiv" style="margin-top: 5px;">
                            <div class="fl">
                                <img src="/images/input_left.png" width="4" height="29" alt="" />
                            </div>
                            <select id="lab_LoanRate" runat="server">
                                <option id="Option14" value="9.00">9%</option>
                                <option id="Option17" value="10.00" selected="selected">10%</option>
                                <option id="Option18" value="11.00">11%</option>
                                <option id="Option19" value="12.00">12%</option>
                                <option id="Option15" value="13.00">13%</option>
                                <option id="Option16" value="14.00">14%</option>
                                <option id="Option20" value="15.00">15%</option>
                                <option id="Option21" value="16.00">16%</option>
                                <option id="Option22" value="17.00">17%</option>
                                <option id="Option23" value="18.00">18%</option>
                            </select>
                            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_lab_LoanRate" />
                            </div>
                        </div>
                        <%--   <asp:TextBox runat="server" ID="lab_LoanRate"></asp:TextBox>--%>
                    </td>

                </tr>
                <tr style="display: none">
                    <td style="text-align: right; width: 15%">审批状态：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%">
                        <div class="selectDiv" style="margin-top: 5px;">
                            <div class="fl">
                                <img src="/images/input_left.png" width="4" height="29" alt="" />
                            </div>
                            <select id="sel_repaymentMethod" runat="server">
                                <option id="repayment0" value="0" selected="selected">请选择</option>
                                <option id="repayment1" value="1">已审核</option>
                                <option id="Option1" value="2">未审核</option>
                            </select>
                            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_repaymentMethod" />
                            </div>
                        </div>
                    </td>

                </tr>

                <tr>
                    <td style="text-align: right; width: 15%">借款用途：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px; width: 35%" colspan="1">


                        <div class="selectDiv" style="margin-top: 5px;">
                            <div class="fl">
                                <img src="/images/input_left.png" width="4" height="29" alt="" />
                            </div>
                            <select id="sel_loanUseName" runat="server">
                                <option value="0" selected="selected">选择借款用途</option>
                            </select>
                            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_loanUseName" />
                            </div>
                        </div>
                    </td>

                </tr>


                <tr>
                    <td style="text-align: right;">审批结果：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">
                        <span class="selectDiv" style="margin-top: 5px; display: inline-block;">
                            <input type="radio" id="radio_audit" name="audit" runat="server" checked="True" class="fl" style="margin-top: 5px;" /><span class="fl" style="margin-top: 5px; margin-right: 5px;">审批通过</span>
                            <input type="radio" id="radio_Audit_loading" name="audit" runat="server" class="fl" style="margin-top: 5px;" /><span class="fl" style="margin-top: 5px;">审核不通过</span>
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
                </tr>

                <tr>
                    <td style="text-align: right;">申请人借款描叙：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">
                        <label id="lab_UseDescription" runat="server"></label>

                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">借款描叙：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;" colspan="3">
                        <textarea id="content1" name="content1" rows="10" style="width: 100%; visibility: hidden;" runat="server"></textarea>

                    </td>
                </tr>

                <!--新增项目描叙和平台意见-->

                <tr>
                    <td style="border-bottom: 0; text-align: right;">&nbsp;</td>
                    <td style="border-bottom: 0; text-align: left; padding-left: 5px;" colspan="3">
                        <div class="selectDiv">
                            <asp:Button ID="button2" runat="server" Text="保存借款信息" Width="100px" CssClass="inputButton" OnClick="button2_Click" />
                            <input type="button" value="返回列表" style="width: 60px;" class="inputButton" onclick="window.location.href = 'LoanStudent_Credits.aspx?<%=HttpContext.Current.Request.QueryString.ToString()%>    ';" />
                        </div>
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

        <fieldset>
            <legend>评分</legend>
            <table cellpadding="0" cellspacing="0" class="editTable">
                <tr>
                    <td style="text-align: right;">资料认证：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
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
                    <td style="text-align: right">信用评分：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <label id="lab_SumScore" class="fl" runat="server"></label>
                        <span class="fl">&nbsp&nbsp等级：</span>
                        <label id="lab_ScoreLevel" class="fl" runat="server"></label>
                        <asp:Button ID="BtnScore" runat="server" Text="评分" OnClick="BtnScore_Click" Width="60" CssClass="inputButton" />
                    </td>
                </tr>

            </table>

        </fieldset>

        <fieldset>
            <legend>设置借款信息</legend>
            <table cellpadding="0" cellspacing="0" class="editTable">
                <tr>
                    <td style="text-align: right;">最低投资金额：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <asp:TextBox runat="server" Text="100" ID="txtBidAmountMin"></asp:TextBox>
                    </td>
                    <td style="text-align: right;">最高投资金额：</td>
                    <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                        <asp:TextBox runat="server" ID="BidAmountMax"></asp:TextBox>
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
                        <input type="text" id="txtAutoBidScale" name="rate"  value="0" runat="server" />%
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
                        <input type="text" id="txtGuaranteeFee" name="rate" value="4.00" runat="server" />
                        %
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">平台借款管理费：</td>
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
                <tr>
                    <td style="border-bottom: 0; text-align: right;">&nbsp;</td>
                    <td style="border-bottom: 0; text-align: left; padding-left: 5px;" colspan="3">
                        <div class="selectDiv">
                            <asp:Button ID="button1" runat="server" Text="审核" Width="100" CssClass="inputButton" OnClick="button1_Click" />
                            <asp:HiddenField runat="server" ID="hidd_loanId" />

                            <asp:HiddenField runat="server" ID="hidd_start" />
                            <asp:HiddenField runat="server" ID="hidd_end" />
                        </div>
                    </td>

                </tr>

            </table>
        </fieldset>

    </form>
    <script src="../js/select2css.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var loanid = getQueryString("LoanId");
            if(loanid==0 || loanid=="0")
            {
                loanid=$("#hidd_loanId").val();
            }

            var date=new Date();
            var year = date.getFullYear();
            var month = ("0" + (date.getMonth() + 1)).slice(-2);

            var  day = new Date(year,month,0);   
            
            var lastdate = year + '-' + month + '-' + day.getDate();//获取当月最后一天日

            // $("#txtDate1").val(year + "-" + month + "-" + day);
            // $("#txtDate2").val(lastdate);
      
            //jQuery("#select_id").append("<option value='Value'>Text</option>"); 
            $("#WdataBidStartTime").val($("#hidd_start").val());
            $("#WdataBidEndTime").val($("#hidd_end").val());
            
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
