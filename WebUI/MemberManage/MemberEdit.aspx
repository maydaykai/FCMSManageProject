<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberEdit.aspx.cs" Inherits="WebUI.MemberManage.MemberEdit" %>

<%@ Register Assembly="ManageFcmsCommon" Namespace="ManageFcmsCommon" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxtabs.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="/js/Area.js"></script>
    <script src="../js/loan/init.js"></script>
    <script src="../js/jquery.query.js"></script>
    <link href="../js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" rel="stylesheet" />
    <link href="../js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <style type="text/css">
        .selectDiv .select_box {
            width: 150px;
        }

        .TableSelect {
            background-color: #fcefa1;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            var type = $.query.get("type");
            $('#<%=cbIsLocked.ClientID%>,#<%=cbIsAllowWithdraw.ClientID%>,#<%=cbCashSetting.ClientID%>').tzCheckbox({ labels: ['Enable', 'Disable'] });
            getProvinceListByHandler($("#ulProvince"), $("#divProvince"), $("#<%=hdProvince.ClientID%>"), $("#ulCity"), $("#divCity"), $("#<%=hdCity.ClientID%>"));
            getProvinceListByHandler($("#ulNativePlaceProvince"), $("#divNativePlaceProvince"), $("#<%=hdNativePlaceProvince.ClientID%>"), $("#ulNativePlaceCity"), $("#divNativePlaceCity"), $("#<%=hdNativePlaceCity.ClientID%>"));
            getProvinceListByHandler($("#ulDomicilePlaceProvince"), $("#divDomicilePlaceProvince"), $("#<%=hdDomicilePlaceProvince.ClientID%>"), $("#ulDomicilePlaceCity"), $("#divDomicilePlaceCity"), $("#<%=hdDomicilePlaceCity.ClientID%>"));
            getProvinceListByHandler($("#ulWorkCityProvince"), $("#divWorkCityProvince"), $("#<%=hdWorkCityProvince.ClientID%>"), $("#ulWorkCityCity"), $("#divWorkCityCity"), $("#<%=hdWorkCityCity.ClientID%>"));
            getProvinceListByHandler($("#ulWorkCityProvince1"), $("#divWorkCityProvince1"), $("#<%=hdWorkCityProvince1.ClientID%>"), $("#ulWorkCityCity1"), $("#divWorkCityCity1"), $("#<%=hdWorkCityCity1.ClientID%>"));
            getProvinceListByHandler($("#ulWorkCityProvince2"), $("#divWorkCityProvince2"), $("#<%=hdWorkCityProvince2.ClientID%>"), $("#ulWorkCityCity2"), $("#divWorkCityCity2"), $("#<%=hdWorkCityCity2.ClientID%>"));
            $("#sel_nativeProvince,sel_liveProvince").live("click", function () {
                getCityListFromHandlerByParentID($(this).val(), $(this).attr("id").replace("Province", "City").replace("province", "city"));
            });
            $('#jqxTabs').jqxTabs({ position: 'top', theme: "arctic" });
            $("input[name=btnBack]").click(function () {
                window.location.href = type == 1 ? "/BranchCompanyManage/BranchCompanyEmployee.aspx?columnId=<%=ColumnId%>" : "MemberManage.aspx?columnId=<%=ColumnId%>";
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id='jqxWidget'>
            <div id='jqxTabs'>
                <ul>
                    <li style="margin-left: 30px;">会员投资记录</li>
                    <li>会员注册信息</li>
                    <li>会员基本信息</li>
                    <li>会员详细信息</li>
                    <li id="setting" runat="server">会员线下提现设置</li>
                    <li>用户信息管理</li>
                </ul>
                <div>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <HeaderTemplate>
                            <table border="0" cellspacing="0" cellpadding="0" class="editTable" id="AuditRecord">
                                <tr style="font-weight: bold; background-color: #99BBE8">
                                    <td width="100">借款人</td>
                                    <td width="65">真实姓名</td>
                                    <td width="100">借款编号</td>
                                    <td width="80">借款金额（元）</td>
                                    <td width="80">投资金额（元）</td>
                                    <td width="60">借款期限</td>
                                    <td width="120">申请时间</td>
                                    <td width="120">满标时间</td>
                                    <td width="76">借款用途</td>
                                    <td width="75">审核状态</td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr onmouseover="this.className = 'TableSelect';" onmouseout="this.className = '';" class="">
                                <td><%#Eval("MemberName") %></td>
                                <td><%#Eval("RealName") %></td>
                                <td><%#Eval("LoanNumber") %></td>
                                <td><%#string.Format("{0:C}",Eval("LoanAmount")) %></td>
                                <td><%#string.Format("{0:C}",Eval("BidAmount")) %></td>
                                <td><%#Eval("LoanTerm").ToString() + (Convert.ToInt32(Eval("BorrowMode")) == 1 ? "个月" : "天") %></td>
                                <td><%#Eval("LoanCreateTime") %></td>
                                <td><%#Eval("FullScaleTime") %></td>
                                <td><%#Eval("LoanUseName") %></td>
                                <td><%#Eval("ExamStatusName") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <div>
                        <input type="button" name="btnBack" value="返 回" class="inputButton fl" />
                        <cc1:Pagination ID="Pagination1" runat="server" OnPageChanged="Pagination1_PageChanged" />
                    </div>
                </div>
                <div>
                    <table cellpadding="0" cellspacing="0" class="editTable" border="0" style="min-width: 1000px;">
                        <tr>
                            <td style="text-align: right; width: 200px;">会员名：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtMemberName" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 200px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">手机号：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtMobile" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 240px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">电子邮件：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtEmail" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 240px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">信用积分：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtCreditPoint" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 30px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">会员积分：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtMemberPoint" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 30px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">可用余额(元)：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtBalance" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">会员类型：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <input type="radio" runat="server" id="rdTypeIdentity" disabled="True" name="type" />个人用户 &nbsp;&nbsp;
                                    <input type="radio" runat="server" id="rdTypeEntprise" disabled="True" name="type" />企业用户
                                </div>
                            </td>
                        </tr>

                        
                        <tr>
                            <td style="text-align: right; width: 200px;">会员等级：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtMemberLevel" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 20px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">最后登录IP：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtLastIP" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 180px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">最后登录时间：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtLastLoginTime" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 180px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">登录次数：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtTimes" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 30px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">注册时间：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtRegTime" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 180px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">VIP开始时间：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtVIPStartTime" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 180px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">VIP结束时间：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtVIPEndTime" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 180px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">是否锁定：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <input type="checkbox" id="cbIsLocked" runat="server" disabled="True" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">是否允许提现：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <input type="checkbox" id="cbIsAllowWithdraw" runat="server" disabled="True" />
                                </div>
                            </td>
                        </tr>
                         <tr>
                            <td style="text-align: right;">是否为营销人员：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="selectDiv" style="margin-top: 5px;">
                                    <label>
                                        <input id="Radio3" type="radio" name="yx_Marketing"  value="1" runat="server" />
                                        是
                                    </label>
                                    <label>
                                        <input id="Radio4" type="radio" name="yx_Marketing"  value="0" runat="server" />
                                        否
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;"></td>
                            <td style="text-align: left; padding-left: 5px;">
                                <input type="button" id="btnSaveRegInfo" value="保 存" class="inputButton" runat="server" />&nbsp;&nbsp;<input type="button" name="btnBack" value="返 回" class="inputButton" />
                            </td>
                        </tr>
                        
                    </table>
                </div>
                <div>
                    <table cellpadding="0" cellspacing="0" class="editTable" border="0">
                        <tr>
                            <td style="text-align: right; width: 200px;">真实姓名：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtRealName" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 200px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">证件号码：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <span class="fl">
                                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtIdentityCard" type="text" value="" class="input_Disabled fl" maxlength="20" style="width: 250px;" runat="server" disabled="True" />
                                    <span class="fl">
                                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <%--<tr>
                        <td  style="text-align: right; width: 200px;">性别：</td>
                        <td  style="text-align: left; padding-left: 5px;">
                             <div style="float: left; margin-bottom: -3px;">
                                <input type="radio" runat="server" id="rdSexMale" disabled="True" name="sex" />男 &nbsp;&nbsp;
                                <input type="radio" runat="server" id="rdSexFemale" disabled="True" name="sex" />女
                            </div>
                        </td>
                    </tr>--%>
                        <tr>
                            <td style="text-align: right; width: 200px;">居住地址：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="diy_select">
                                    <input id="hdProvince" name="" class="diy_select_input" type="hidden" runat="server" />
                                    <div style="width: 4px; float: left;">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <div id="divProvince" class="diy_select_txt" style="width: 70px; float: left;">--省份--</div>
                                    <div class="diy_select_btn">&nbsp;</div>
                                    <ul id="ulProvince" style="display: none;" class="diy_select_list">
                                    </ul>
                                </div>
                                <div class="diy_select">
                                    <input id="hdCity" name="" class="diy_select_input" type="hidden" runat="server" />
                                    <div style="width: 4px; float: left;">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <div id="divCity" class="diy_select_txt" style="width: 70px; float: left;">--城市--</div>
                                    <div class="diy_select_btn"></div>
                                    <ul id="ulCity" style="display: none;" class="diy_select_list">
                                    </ul>
                                </div>
                                <div class="inputText">
                                    <span class="fl">
                                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtAddress" type="text" value="" class="input_text fl" maxlength="200" style="width: 250px;" runat="server" />
                                    <span class="fl">
                                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">电话号码：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="inputText">
                                    <span class="fl">
                                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtTelephone" type="text" value="" class="input_text fl" maxlength="200" style="width: 250px;" runat="server" />
                                    <span class="fl">
                                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">传真：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="inputText">
                                    <span class="fl">
                                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtFax" type="text" value="" class="input_text fl" maxlength="200" style="width: 250px;" runat="server" />
                                    <span class="fl">
                                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                         
                        <tr>
                            <td style="text-align: right; width: 200px;"></td>
                            <td style="text-align: left; padding-left: 5px;">
                                <input type="button" id="btnSaveMemberInfo" value="保 存" class="inputButton" runat="server" />&nbsp;&nbsp<input type="button" name="btnBack" value="返 回" class="inputButton" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table cellpadding="0" cellspacing="0" class="editTable" border="0">
                        <tr>
                            <td style="text-align: right; width: 200px;">婚姻状况：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <input type="radio" runat="server" id="rdMaritalStatusYes" name="sex" />已婚 &nbsp;&nbsp;
                                <input type="radio" runat="server" id="rdMaritalStatusNo" name="sex" />未婚
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">有无子女：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <input type="radio" runat="server" id="rdChildrenYes" name="children" />有 &nbsp;&nbsp;
                                <input type="radio" runat="server" id="rdChildrenNo" name="children" />无
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">是否有房：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <input type="radio" runat="server" id="rdHouseYes" name="house" />有 &nbsp;&nbsp;
                                <input type="radio" runat="server" id="rdHouseNo" name="house" />无
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">有无房贷：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <input type="radio" runat="server" id="rdHouseLoanYes" name="houseLoan" />有 &nbsp;&nbsp;
                                <input type="radio" runat="server" id="rdHouseLoanNo" name="houseLoan" />无
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">是否有车：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <input type="radio" runat="server" id="rdCarYes" name="car" />有 &nbsp;&nbsp;
                                <input type="radio" runat="server" id="rdCarNo" name="car" />无
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">有无车贷：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <input type="radio" runat="server" id="rdCarLoanYes" name="carLoan" />有 &nbsp;&nbsp;
                                <input type="radio" runat="server" id="rdCarLoanNo" name="carLoan" />无
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">籍贯：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="diy_select">
                                    <input id="hdNativePlaceProvince" name="" class="diy_select_input" type="hidden" runat="server" />
                                    <div style="width: 4px; float: left;">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <div id="divNativePlaceProvince" class="diy_select_txt" style="width: 70px; float: left;">--省份--</div>
                                    <div class="diy_select_btn">&nbsp;</div>
                                    <ul id="ulNativePlaceProvince" style="display: none;" class="diy_select_list">
                                    </ul>
                                </div>
                                <div class="diy_select">
                                    <input id="hdNativePlaceCity" name="" class="diy_select_input" type="hidden" runat="server" />
                                    <div style="width: 4px; float: left;">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <div id="divNativePlaceCity" class="diy_select_txt" style="width: 70px; float: left;">--城市--</div>
                                    <div class="diy_select_btn"></div>
                                    <ul id="ulNativePlaceCity" style="display: none;" class="diy_select_list">
                                    </ul>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">户口所在地：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="diy_select">
                                    <input id="hdDomicilePlaceProvince" name="" class="diy_select_input" type="hidden" runat="server" />
                                    <div style="width: 4px; float: left;">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <div id="divDomicilePlaceProvince" class="diy_select_txt" style="width: 70px; float: left;">--省份--</div>
                                    <div class="diy_select_btn">&nbsp;</div>
                                    <ul id="ulDomicilePlaceProvince" style="display: none;" class="diy_select_list">
                                    </ul>
                                </div>
                                <div class="diy_select">
                                    <input id="hdDomicilePlaceCity" name="" class="diy_select_input" type="hidden" runat="server" />
                                    <div style="width: 4px; float: left;">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <div id="divDomicilePlaceCity" class="diy_select_txt" style="width: 70px; float: left;">--城市--</div>
                                    <div class="diy_select_btn"></div>
                                    <ul id="ulDomicilePlaceCity" style="display: none;" class="diy_select_list">
                                    </ul>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">最高学历：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="selectDiv inputText">
                                    <div class="fl">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <select id="selHighestDegree" class="fl" runat="server">
                                        <option value="0" selected="selected">--最高学历--</option>
                                        <option value="1">小学</option>
                                        <option value="2">中学</option>
                                        <option value="3">专科</option>
                                        <option value="4">本科</option>
                                        <option value="5">硕士</option>
                                        <option value="6">博士</option>
                                    </select>
                                    <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                        <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selHighestDegree" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">职业状态：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="selectDiv inputText">
                                    <div class="fl">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <select id="selJobStatus" class="fl" runat="server">
                                        <option value="0" selected="selected">--职业状态--</option>
                                        <option value="1">在职</option>
                                        <option value="2">离职</option>
                                    </select>
                                    <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                        <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selJobStatus" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">月收入：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="selectDiv inputText">
                                    <div class="fl">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <select id="selMonthlyIncome" class="fl" runat="server">
                                        <option value="0" selected="selected">--月收入--</option>
                                        <option value="1">1500以下</option>
                                        <option value="2">1500~3499</option>
                                        <option value="3">3500~5999</option>
                                        <option value="4">6000~10000</option>
                                        <option value="5">10000以上</option>
                                    </select>
                                    <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                        <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selMonthlyIncome" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">职业类型：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="selectDiv inputText">
                                    <div class="fl">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <select id="selJobType" class="fl" runat="server">
                                        <option value="0" selected="selected">--职业类型--</option>
                                        <option value="1">金融</option>
                                        <option value="2">IT</option>
                                        <option value="3">其他</option>
                                    </select>
                                    <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                        <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selJobType" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">单位名称：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="inputText">
                                    <span class="fl">
                                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtCompanyName" type="text" value="" class="input_text fl" maxlength="200" style="width: 250px;" runat="server" />
                                    <span class="fl">
                                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">工作城市：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="diy_select">
                                    <input id="hdWorkCityProvince" name="" class="diy_select_input" type="hidden" runat="server" />
                                    <div style="width: 4px; float: left;">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <div id="divWorkCityProvince" class="diy_select_txt" style="width: 70px; float: left;">--省份--</div>
                                    <div class="diy_select_btn">&nbsp;</div>
                                    <ul id="ulWorkCityProvince" style="display: none;" class="diy_select_list">
                                    </ul>
                                </div>
                                <div class="diy_select">
                                    <input id="hdWorkCityCity" name="" class="diy_select_input" type="hidden" runat="server" />
                                    <div style="width: 4px; float: left;">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <div id="divWorkCityCity" class="diy_select_txt" style="width: 70px; float: left;">--城市--</div>
                                    <div class="diy_select_btn"></div>
                                    <ul id="ulWorkCityCity" style="display: none;" class="diy_select_list">
                                    </ul>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">公司类别：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="selectDiv inputText">
                                    <div class="fl">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <select id="selCompanyCategory" class="fl" runat="server">
                                        <option value="0" selected="selected">--公司类别--</option>
                                        <option value="1">计算机/互联网/通信/电子</option>
                                        <option value="2">会计/金融/银行/保险</option>
                                        <option value="3">贸易/消费/制造/营运</option>
                                        <option value="4">制药/医疗</option>
                                        <option value="5">广告/媒体</option>
                                        <option value="6">房地产/建筑</option>
                                        <option value="7">专业服务/教育/培训</option>
                                        <option value="8">物流/运输</option>
                                        <option value="9">能源/原材料</option>
                                        <option value="10">政府/非赢利机构</option>
                                        <option value="11">其他</option>
                                    </select>
                                    <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                        <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selCompanyCategory" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">公司规模：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="selectDiv inputText">
                                    <div class="fl">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <select id="selCompanySize" class="fl" runat="server">
                                        <option value="0" selected="selected">--公司规模--</option>
                                        <option value="1">50人以下</option>
                                        <option value="2">51-100人</option>
                                        <option value="3">101-300人</option>
                                        <option value="4">301-1000人</option>
                                        <option value="5">1000人以上</option>
                                    </select>
                                    <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                        <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selCompanySize" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">在现单位工作年限：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="selectDiv inputText">
                                    <div class="fl">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <select id="selWorkTerm" class="fl" runat="server">
                                        <option value="0" selected="selected">--在现单位工作年限--</option>
                                        <option value="1">不到1年</option>
                                        <option value="2">1-3年</option>
                                        <option value="3">3年以上</option>
                                    </select>
                                    <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                        <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selWorkTerm" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">公司电话：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="inputText">
                                    <span class="fl">
                                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtCompanyPhone" type="text" value="" class="input_text fl" maxlength="200" style="width: 250px;" runat="server" />
                                    <span class="fl">
                                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">公司地址：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="inputText">
                                    <span class="fl">
                                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtCompanyAddress" type="text" value="" class="input_text fl" maxlength="200" style="width: 250px;" runat="server" />
                                    <span class="fl">
                                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">直系家属联系人姓名：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="inputText">
                                    <span class="fl">
                                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtContactName" type="text" value="" class="input_text fl" maxlength="200" style="width: 250px;" runat="server" />
                                    <span class="fl">
                                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">直系家属联系人关系：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="inputText">
                                    <span class="fl">
                                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtContactRelation" type="text" value="" class="input_text fl" maxlength="200" style="width: 250px;" runat="server" />
                                    <span class="fl">
                                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">直系家属联系人手机：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="inputText">
                                    <span class="fl">
                                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtContactPhone" type="text" value="" class="input_text fl" maxlength="200" style="width: 250px;" runat="server" />
                                    <span class="fl">
                                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;"></td>
                            <td style="text-align: left; padding-left: 5px;">
                                <input type="button" id="btnSaveDetailInfo" value="保 存" class="inputButton" runat="server" />&nbsp;&nbsp<input type="button" name="btnBack" value="返 回" class="inputButton" />
                            </td>
                        </tr>
                    </table>
                </div>

                <div>
                    <table cellpadding="0" cellspacing="0" class="editTable" border="0">
                        <tr>
                            <td style="text-align: right; width: 200px;">是否开启：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-bottom: -3px;">
                                    <input type="checkbox" id="cbCashSetting" runat="server" disabled="True" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;"></td>
                            <td style="text-align: left; padding-left: 5px;">
                                <input type="button" id="btnSaveCashSetting" value="保 存" class="inputButton" runat="server"/>&nbsp;&nbsp<input type="button" name="btnBack" value="返 回" class="inputButton" />
                            </td>
                        </tr>
                    </table>
                </div>

                <div>
                    <table id="memberInfo" runat="server" border="0" cellspacing="0" cellpadding="0" class="editTable" style="min-width: 500px; width: 40%; float: left; margin-left: 10px;">
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
                                        <input id="Marriage1" type="radio" name="rad_maritalStatus" value="1" runat="server" />
                                        已婚
                                    </label>
                                    <label>
                                        <input id="Marriage" type="radio" name="rad_maritalStatus" value="2" runat="server" />
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
                                        <input id="RadioMan" type="radio" name="rad_sexStatus" value="男" runat="server" />
                                        男
                                    </label>
                                    <label>
                                        <input id="RadioWoman" type="radio" name="rad_sexStatus" value="女" runat="server" />
                                        女
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">籍贯：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="diy_select">
                                    <input id="hdWorkCityProvince1" name="" class="diy_select_input" type="hidden" runat="server" />
                                    <div style="width: 4px; float: left;">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <div id="divWorkCityProvince1" class="diy_select_txt" style="width: 70px; float: left;">--省份--</div>
                                    <div class="diy_select_btn">&nbsp;</div>
                                    <ul id="ulWorkCityProvince1" style="display: none;" class="diy_select_list">
                                    </ul>
                                </div>
                                <div class="diy_select">
                                    <input id="hdWorkCityCity1" name="" class="diy_select_input" type="hidden" runat="server" />
                                    <div style="width: 4px; float: left;">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <div id="divWorkCityCity1" class="diy_select_txt" style="width: 70px; float: left;">--城市--</div>
                                    <div class="diy_select_btn"></div>
                                    <ul id="ulWorkCityCity1" style="display: none;" class="diy_select_list">
                                    </ul>
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
                                        <input id="txt_Radioyse" type="radio" name="rad_houseStatus" checked="true" value="1" runat="server" />
                                        有
                                    </label>
                                    <label>
                                        <input id="txt_Radiono" type="radio" name="rad_houseStatus" value="0" runat="server" />
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
                                        <input id="txt_Caryes" type="radio" name="rad_carStatus" checked="true" value="1" runat="server" />
                                        有
                                    </label>
                                    <label>
                                        <input id="txt_Carno" type="radio" name="rad_carStatus" value="0" runat="server" />
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
                                        <input id="txt_Employee" type="radio" name="rad_jobStatus" checked="true" value="在职员工" runat="server" />
                                        在职员工
                                    </label>
                                    <label>
                                        <input id="txt_Haveboss" type="radio" name="rad_jobStatus" value="私企老板" runat="server" />
                                        私企老板
                                    </label>
                                </div>
                            </td>
                        </tr>
                       
                        <tr>
                            <td style="text-align: right; width: 200px;"></td>
                            <td style="text-align: left; padding-left: 5px;">
                                <input type="button" id="btnOK" value="保 存" class="inputButton" runat="server" />&nbsp;&nbsp;<input type="button" name="btnBack" value="返 回" class="inputButton" />
                            </td>
                        </tr>
                    </table>
                    <table id="enterpriseInfo" border="0" cellspacing="0" cellpadding="0" runat="server" class="editTable" style="min-width: 500px; width: 40%; float: left; margin-left: 10px; display: none;">
                        <tr>
                            <td id="enterpriseTile" style="padding-top: 5px; text-align: center; font-weight: bold;" colspan="2">企业信息</td>
                        </tr>
                        <tr>
                            <td style="text-align: right">行业类别：</td>
                            <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                                <div class="selectDiv inputText">
                                    <div class="fl">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
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
                                    <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                        <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_companyNature" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">所在城市：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="diy_select">
                                    <input id="hdWorkCityProvince2" name="" class="diy_select_input" type="hidden" runat="server" />
                                    <div style="width: 4px; float: left;">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <div id="divWorkCityProvince2" class="diy_select_txt" style="width: 70px; float: left;">--省份--</div>
                                    <div class="diy_select_btn">&nbsp;</div>
                                    <ul id="ulWorkCityProvince2" style="display: none;" class="diy_select_list">
                                    </ul>
                                </div>
                                <div class="diy_select">
                                    <input id="hdWorkCityCity2" name="" class="diy_select_input" type="hidden" runat="server" />
                                    <div style="width: 4px; float: left;">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
                                    <div id="divWorkCityCity2" class="diy_select_txt" style="width: 70px; float: left;">--城市--</div>
                                    <div class="diy_select_btn"></div>
                                    <ul id="ulWorkCityCity2" style="display: none;" class="diy_select_list">
                                    </ul>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 150px;">行业类别：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="selectDiv inputText">
                                    <div class="fl">
                                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                                    </div>
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
                                    <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                        <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_industry" />
                                    </div>
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
                                    <input id="txt_regCapital" type="text" name="txt_regCapital" value="" class="input_text fl" maxlength="50" style="width: 100px;" runat="server" />
                                    <span class="fl">
                                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <span class="fl" style="margin: 5px 0 0 5px;"></span>
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
                                    <span class="fl" style="margin: 5px 0 0 5px;"></span>
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
                                    <span class="fl" style="margin: 5px 0 0 5px;"></span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;"></td>
                            <td style="text-align: left; padding-left: 5px;">
                                <input type="button" id="btnCorporate" value="保 存" onclick="loanEdit();" class="inputButton" runat="server"/>&nbsp;&nbsp<input type="button" name="btnBack" value="返 回" class="inputButton" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
    <script src="/js/selectDiv.js"></script>
    <script src="../js/select2css.js"></script>
    <script type="text/javascript">
        //参数组装
        function buildQueryString(data) {
            var str = ''; for (var prop in data) {
                if (data.hasOwnProperty(prop)) {
                    str += prop + '=' + data[prop] + '&';
                }
            }
            return str.substr(0, str.length - 1);
        }

        var formatedData = '';

        function requestSubmit() {
            $.ajax({
                type: "POST",
                url: "../HanderAshx/MemberManage/MemberEditHandler.ashx",
                data: formatedData,
                success: function (data) {
                    if (data == "success") {
                        MessageAlert("保存成功", "success", "");
                    }
                    else if (data == "error") {
                        MessageAlert("保存失败，发生过借款的用户才能开启线下提现功能！！", "error", location.href);
                    }
                },
                error: function (xmlHttpRequest) {
                    alert(xmlHttpRequest.innerText);
                },
            });
        }

        //注册信息保存
        $("#btnSaveRegInfo").click(function () {
            var obj = new Object();
            obj.IsLocked = $("#<%=cbIsLocked.ClientID%>").attr("checked");
            obj.IsAllowWithdraw = $("#<%=cbIsAllowWithdraw.ClientID%>").attr("checked");
            obj.MemberID = "<%=ManageFcmsCommon.ConvertHelper.QueryString(Request, "ID", 0)%>";
            obj.sign = "1";
            obj.Marketing = $("input[name='yx_Marketing']:checked").val();
            formatedData = buildQueryString(obj);
            requestSubmit();
        });

        //基本信息保存
        $("#btnSaveMemberInfo").click(function () {
            var obj = new Object();
            obj.Province = $("#<%=hdProvince.ClientID%>").val();
            obj.City = $("#<%=hdCity.ClientID%>").val();
            obj.Address = $("#<%=txtAddress.ClientID%>").val();
            obj.Telephone = $("#<%=txtTelephone.ClientID%>").val();
            obj.Fax = $("#<%=txtFax.ClientID%>").val();
            obj.MemberID = "<%=ManageFcmsCommon.ConvertHelper.QueryString(Request, "ID", 0)%>";
            obj.sign = "2";
            formatedData = buildQueryString(obj);
            requestSubmit();
        });

        //详细信息保存
        $("#btnSaveDetailInfo").click(function () {
            var obj = new Object();
            obj.sign = "3";
            obj.MemberID = "<%=ManageFcmsCommon.ConvertHelper.QueryString(Request, "ID", 0)%>";
            if ($("#<%=rdMaritalStatusYes.ClientID%>").attr("checked")) {
                obj.MaritalStatus = 1;
            } else if ($("#<%=rdMaritalStatusNo.ClientID%>").attr("checked")) {
                obj.MaritalStatus = 2;
            }
            obj.Children = $("#<%=rdChildrenYes.ClientID%>").attr("checked");
            obj.House = $("#<%=rdHouseYes.ClientID%>").attr("checked");
            obj.HouseLoan = $("#<%=rdHouseLoanYes.ClientID%>").attr("checked");
            obj.Car = $("#<%=rdCarYes.ClientID%>").attr("checked");
            obj.CarLoan = $("#<%=rdCarLoanYes.ClientID%>").attr("checked");
            obj.NativePlace = $("#<%=hdNativePlaceCity.ClientID%>").val();
            obj.DomicilePlace = $("#<%=hdDomicilePlaceCity.ClientID%>").val();
            obj.HighestDegree = $("#<%=selHighestDegree.ClientID%>").val();
            obj.JobStatus = $("#<%=selJobStatus.ClientID%>").val();
            obj.MonthlyIncome = $("#<%=selMonthlyIncome.ClientID%>").val();
            obj.JobType = $("#<%=selJobType.ClientID%>").val();
            obj.CompanyName = $("#<%=txtCompanyName.ClientID%>").val();
            obj.WorkCity = $("#<%=hdWorkCityCity.ClientID%>").val();
            obj.CompanyCategory = $("#<%=selCompanyCategory.ClientID%>").val();
            obj.CompanySize = $("#<%=selCompanySize.ClientID%>").val();
            obj.WorkTerm = $("#<%=selWorkTerm.ClientID%>").val();
            obj.CompanyPhone = $("#<%=txtCompanyPhone.ClientID%>").val();
            obj.CompanyAddress = $("#<%=txtCompanyAddress.ClientID%>").val();
            obj.ContactName = $("#<%=txtContactName.ClientID%>").val();
            obj.ContactRelation = $("#<%=txtContactRelation.ClientID%>").val();
            obj.ContactPhone = $("#<%=txtContactPhone.ClientID%>").val();
            formatedData = buildQueryString(obj);
            requestSubmit();
        });

        $("#btnSaveCashSetting").click(function () {
            var obj = new Object();
            obj.sign = "4";
            obj.MemberID = "<%=ConvertHelper.QueryString(Request, "ID", 0)%>";
            obj.CashSetting = $("#<%=cbCashSetting.ClientID%>").attr("checked");
            formatedData = buildQueryString(obj);
            requestSubmit();
        });

        function resSubmit() {
            if (porate()) {
                $.ajax({
                    type: "POST",
                    url: "../HanderAshx/MemberManage/MemberEditHandler.ashx?sign=5",
                    data: formatedData,
                    success: function (data) {
                        if (data == "success") {
                            MessageAlert("保存成功", "success", "");
                        }
                        else if (data == "error") {
                            MessageAlert("保存失败", "error", location.href);
                        }
                    },
                    error: function (xmlHttpRequest) {
                        alert(xmlHttpRequest.innerText);

                    },
                });
            }
        }

        function porate() {
            var Age = $("#sel_companyNature").val();
            if (!Age) {
                MessageAlert("请输入年龄", "error", "");
                return false;
            }
            var MaritalStatus = $("input[name='rad_maritalStatus']:checked").val();
            if (!MaritalStatus) {
                MessageAlert("请选择婚姻状况", "error", "");
                return false;
            }
            var Sex = $("input[name='rad_sexStatus']:checked").val();
            if (!Sex) {
                MessageAlert("请选择性别", "error", "");
                return false;
            }
            var DomicilePlace = $("#hdWorkCityCity1").val();
            if (!DomicilePlace) {
                MessageAlert("请选择城市", "error", "");
                return false;
            }
            var FamilyNum = $("#txt_familyNum").val();
            if (!FamilyNum) {
                MessageAlert("请填写家庭人数", "error", "");
                return false;
            }
            var MonthlyPay = $("#txt_monthlyIncome").val();
            if (!MonthlyPay) {
                MessageAlert("请填写月收入水平", "error", "");
                return false;
            }
            var HaveHouse = $("input[name='rad_houseStatus']:checked").val();
            if (!HaveHouse) {
                MessageAlert("请选择有无房产", "error", "");
                return false;
            }
            var HaveCar = $("input[name='rad_carStatus']:checked").val();
            if (!HaveCar) {
                MessageAlert("请选择有无车产", "error", "");
                return false;
            }
            var WorkingLife = $("#text_workDuration").val();
            if (!WorkingLife) {
                MessageAlert("请填写工作年限", "error", "");
                return false;
            }
            var JobStatus = $("input[name='rad_jobStatus']:checked").val();
            if (!JobStatus) {
                MessageAlert("请选择职业状态", "error", "");
                return false;
            }
            return true;
        }

        $(function() {
            $("#btnOK").click(function() {
                var obj = new Object();
                obj.MemberID = "<%=ManageFcmsCommon.ConvertHelper.QueryString(Request, "ID", 0)%>";
                obj.Age = $("#txt_memberAge").val();
                obj.MaritalStatus = $("input[name='rad_maritalStatus']:checked").val();
                obj.Sex = $("input[name='rad_sexStatus']:checked").val();
                obj.DomicilePlace = $("#hdWorkCityCity1").val();
                obj.FamilyNum = $("#txt_familyNum").val();
                obj.MonthlyPay = $("#txt_monthlyIncome").val();
                obj.HaveHouse = $("input[name='rad_houseStatus']:checked").val();
                obj.HaveCar = $("input[name='rad_carStatus']:checked").val();
                obj.WorkingLife = $("#text_workDuration").val();
                obj.JobStatus = $("input[name='rad_jobStatus']:checked").val();
              
                formatedData = buildQueryString(obj);
                resSubmit();
            });

            $.ajax({
                type: "POST",
                async: false,
                url: "../HanderAshx/p2p/LoanUserInfo.ashx?memberId=" + '<%=model.ID %>' + "&type=" + '<%=model.Type%>',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function(result) {
                    if (result == null)
                        return;
                    else
                        initUserInfo('<%=model.Type%>', result[0]);
                }
            });
        });
        





        function initUserInfo(type, data) {
            if (type == 0) {
                $("#txt_memberAge").val(data.Age);
                $("input[name='rad_maritalStatus'][value='" + data.MaritalStatus + "']").attr("checked", "checked");
                $("input[name='rad_sexStatus'][value='" + data.Sex + "']").attr("checked", "checked"); 
                $("#hdWorkCityProvince1").val(data.ParentID);
                $("#hdWorkCityCity1").val(data.DomicilePlace);
                $("#txt_monthlyIncome").val(data.MonthlyPay);
                $("input[name='rad_houseStatus'][value='" + (data.HaveHouse ? 1 : 0) + "']").attr("checked", "checked");
                $("input[name='rad_carStatus'][value='" + (data.HaveCar ? 1 : 0) + "']").attr("checked", "checked");
                $("#text_workDuration").val(data.WorkingLife);
                $("#txt_familyNum").val(data.FamilyNum);
                $("input[name='rad_jobStatus'][value='" + data.JobStatus + "']").attr("checked", "checked");
            } else {
                $("#sel_companyNature").val(data.Nature);
                $("#sel_industry").val(data.IndustryCategory);
                $("#hdWorkCityProvince2").val(data.ParentID);
                $("#hdWorkCityCity2").val(data.CityId);
                $("#txt_regCapital").val(data.RegisteredCapital);
                $("#text_mainBusiness").val(data.MainProducts);
                $("#text_companyAge").val(data.SetUpyear);
                $("#text_businessScope").val(data.BusinessScope);
                $("#text_employeeNum").val(data.Size);

            }
        }

        function Corporate() {
            if (loanEdit()) {
                $.ajax({
                    type: "POST",
                    url: "../HanderAshx/MemberManage/MemberEditHandler.ashx?sign=6",
                    data: formatedData,
                    success: function (data) {
                        if (data == "success") {
                            MessageAlert("保存成功", "success", "");
                        }
                        else if (data == "error") {
                            MessageAlert("保存失败", "error", location.href);
                        }
                    },
                    error: function (xmlHttpRequest) {
                        alert(xmlHttpRequest.innerText);
                    },
                });
            }
        }

        function loanEdit() {
            var Nature = $("#<%=sel_companyNature.ClientID%>").val();
            if (!Nature) {
                MessageAlert("请选择公司性质", "error", "");
                return false;
            }
            var IndustryCategory = $("#<%=sel_industry.ClientID%>").val();
            if (!IndustryCategory) {
                MessageAlert("请选择行业类别", "error", "");
                return false;
            }
            var CityId = $("#hdWorkCityCity2").val();
            if (!CityId) {
                MessageAlert("请选择城市", "error", "");
                return false;
            }
            var Size = $("#text_employeeNum").val();
            if (!Size) {
                MessageAlert("请填写员工人数", "error", "");
                return false;
            }
            var MainProducts = $("#text_mainBusiness").val();
            if (!MainProducts) {
                MessageAlert("请填写主营产品", "error", "");
                return false;
            }
            var RegisteredCapital = $("#txt_regCapital").val();
            if (!RegisteredCapital) {
                MessageAlert("请填写注册资本", "error", "");
                return false;
            }
            var BusinessScope = $("#text_businessScope").val();
            if (!BusinessScope) {
                MessageAlert("请填写注册资本", "error", "");
                return false;
            }
            var SetUpyear = $("#text_companyAge").val();
            if (!SetUpyear) {
                MessageAlert("请填写成立年限", "error", "");
                return false;
            }
            return true;
            Corporate();
        }

               $("#btnCorporate").click(function () {
                 var obj = new Object();
                   obj.MemberID = "<%=ManageFcmsCommon.ConvertHelper.QueryString(Request, "ID", 0)%>";
                   obj.Nature = $("#<%=sel_companyNature.ClientID%>").val();
                   obj.IndustryCategory = $("#<%=sel_industry.ClientID%>").val();
                   obj.CityId = $("#hdWorkCityCity2").val();
                   obj.Size = $("#text_employeeNum").val();
                   obj.MainProducts = $("#text_mainBusiness").val();
                   obj.RegisteredCapital = $("#txt_regCapital").val();
                   obj.BusinessScope = $("#text_businessScope").val();
                   obj.SetUpyear = $("#text_companyAge").val();
                  formatedData = buildQueryString(obj);
                Corporate();
              });

    </script>
</body>
</html>
