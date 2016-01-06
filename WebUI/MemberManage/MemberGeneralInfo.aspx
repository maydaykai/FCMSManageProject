<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberGeneralInfo.aspx.cs" Inherits="WebUI.MemberManage.MemberGeneralInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <script src="../js/jQuery/jquery-1.7.2.min.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="/js/Area.js"></script>
    <style type="text/css">
        .selectDiv .select_box {
         width: 150px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            getProvinceListByHandler($("#ulProvince"), $("#divProvince"), $("#<%=hdProvince.ClientID%>"), $("#ulCity"), $("#divCity"), $("#<%=hdCity.ClientID%>"));
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <table cellpadding="0" cellspacing="0" class="editTable" border="0">
        <tr>
            <td style="text-align: right; width: 100px;">真实姓名：</td>
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
            <td style="text-align: right; width: 100px;">证件号码：</td>
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
        <tr>
            <td  style="text-align: right; width: 100px;">居住地址：</td>
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
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtAddress" type="text" value="" class="input_Disabled fl" maxlength="200" style="width: 250px;" runat="server" disabled="True" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 100px;">电话号码：</td>
            <td style="text-align: left; padding-left: 5px;">
                <div class="inputText">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtTelephone" type="text" value="" class="input_Disabled fl" maxlength="200" style="width: 250px;" runat="server" disabled="True" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 100px;">传真：</td>
            <td style="text-align: left; padding-left: 5px;">
                <div class="inputText">
                    <span class="fl">
                        <img src="../images/gray_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input id="txtFax" type="text" value="" class="input_Disabled fl" maxlength="200" style="width: 250px;" runat="server" disabled="True" />
                    <span class="fl">
                        <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </div>
            </td>
        </tr>
    </table>
    </form>
    <script src="../js/selectDiv.js"></script>
</body>
</html>
