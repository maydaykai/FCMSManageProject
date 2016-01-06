<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnterpriseAuthtEdit.aspx.cs" Inherits="WebUI.MemberManage.EnterpriseAuthtEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>企业信息认证管理</title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="/js/Area.js"></script>
    <script type="text/javascript">
        $(function () {
            getProvinceListByHandler($("#ulProvince"), $("#divProvince"), $("#hdProvince"), $("#ulCity"), $("#divCity"), $("#hdCity"));
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" class="editTable">
            <tr>
                <td style="text-align: right; width: 200px;">会员名：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtMemberName" type="text" name="txtMemberName" value="" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" disabled="True" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">企业名称：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtEnterpriseName" type="text" name="txtEnterpriseName" value="" class="input_Disabled fl" maxlength="20" style="width: 250px;" runat="server" disabled="True" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">营业执照注册号：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtRegistrationNo" type="text" name="txtRegistrationNo" value="" class="input_Disabled fl" maxlength="20" style="width: 150px;" runat="server" disabled="True" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">组织机构代码：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtOrganizationCode" type="text" name="txtOrganizationCode" value="" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" disabled="True" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                    <div class='tip_wrap'>
                        <div class="tip_content" style="color: #DC143C">
                            <asp:Literal ID="litAnthStatus" runat="server"></asp:Literal>&nbsp;
                        </div>
                        <span class='arrow_left' style='top: 1px;'></span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">法人姓名：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtLegalName" type="text" name="txtLegalName" value="" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" disabled="True" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">经办人姓名：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtOperatorRealName" type="text" name="txtOperatorRealName" value="" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" disabled="True" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">经办人身份证号码：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtOperatorIdentityCard" type="text" name="txtOperatorIdentityCard" value="" class="input_Disabled fl" maxlength="20" style="width: 150px;" runat="server" disabled="True" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">经办人手机号码：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtOperatorPhone" type="text" name="txtOperatorPhone" value="" class="input_Disabled fl" maxlength="20" style="width: 100px;" runat="server" disabled="True" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">经营场所所在地：</td>
                <td style="text-align: left;">
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
                        <input id="txtAddress" type="text" name="txtAddress" value="" class="input_text fl" maxlength="200" style="width: 250px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">认证次数：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtAuthNumber" type="text" name="txtAuthNumber" value="" class="input_text fl" maxlength="20" style="width: 30px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">认证有效期：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtExpireTime" type="text" name="txtExpireTime" value="" disabled="True" class="input_Disabled fl" maxlength="20" style="width: 145px;" runat="server" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left; padding-left: 5px;">
                    <input type="button" id="btn_Operater" value="保存" class="inputButton" runat="server" onserverclick="btn_Operater_Click" />
                    <input type="button" class="inputButton" onclick="window.history.go(-1);" value="返回" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
<script src="/js/selectDiv.js"></script>
<script type="text/javascript">
    $(function () {
        $(':checkbox').tzCheckbox({ labels: ['Enable', 'Disable'] });
    });
</script>
