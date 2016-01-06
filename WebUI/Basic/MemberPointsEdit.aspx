<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberPointsEdit.aspx.cs" Inherits="WebUI.Basic.MemberPointsEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../css/global.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="../js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#ckbEnable').tzCheckbox({ labels: ['Enable', 'Disable'] });
        });
        function validate() {
            var txtName = $("#txtName");
            var txtInterestManageFee = $("#txtInterestManageFee");
            var txtMinScore = $("#txtMinScore");
            if ($.trim(txtName.val()) == "") {
                MessageTips('请输入会员积分等级名称', 'warning');
                txtName.focus();
                return false;
            }
            if ($.trim(txtInterestManageFee.val()) == "") {
                MessageTips('请输入利息管理费率', 'warning');
                txtInterestManageFee.focus();
                return false;
            }
            if ($.trim(txtMinScore.val()) == "") {
                MessageTips('请输入对应积分', 'warning');
                txtMinScore.focus();
                return false;
            }
            return true;
        }
    </script>

</head>
<body style="margin: 0; padding: 0;">
    <form id="form1" runat="server">
        <div style="float: left;">
            <div style="width: 400px; margin: 0 auto;">
                <ul style="float: left; list-style: none; width: 400px;padding: 0;margin: 0;">
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">会员积分等级名称：</li>
                    <li style="margin: 3px; padding: 0; width:260px; float: left;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtName" type="text" runat="server" style="width: 150px;" class="input_text" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </li>
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">利息管理费率：</li>
                    <li style="margin: 3px; padding: 0; width:260px; float: left;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtInterestManageFee" type="text" runat="server" style="width: 80px;" class="input_text" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span><span style="font:12px/18px Verdana, Simsun, Helvetica, Arial, sans-serif">%</span>
                    </li>
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">对应积分(>=)：</li>
                    <li style="margin: 3px; padding: 0; width:260px; float: left;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtMinScore" type="text" runat="server" style="width: 100px;" class="input_text" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </li>
                </ul>
                <div style="text-align: right; width: 400px;border-top: dashed 1px #ff6347; float: right;padding-top: 5px;">
                    <asp:Button ID="Button1" Text="提交" OnClick="Operate_Click" runat="server" OnClientClick="return validate();" CssClass="inputButton" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>