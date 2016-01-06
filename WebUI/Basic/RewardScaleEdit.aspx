<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RewardScaleEdit.aspx.cs" Inherits="WebUI.Basic.RewardScaleEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            var txtInterest = $("#txtInterest");
            var txtRewardScale = $("#txtRewardScale");
            var txtRankLevel = $("#txtRankLevel");
            var txtLevelDesc = $("#txtLevelDesc");
            if ($.trim(txtInterest.val()) == "") {
                MessageTips('请输入利息收益指标', 'warning');
                txtInterest.focus();
                return false;
            }
            if ($.trim(txtRewardScale.val()) == "") {
                MessageTips('请输入奖励比例', 'warning');
                txtRewardScale.focus();
                return false;
            }
            if ($.trim(txtRankLevel.val()) == "") {
                MessageTips('请输入排序等级', 'warning');
                txtRankLevel.focus();
                return false;
            }
            if ($.trim(txtLevelDesc.val()) == "") {
                MessageTips('请输入等级描述', 'warning');
                txtLevelDesc.focus();
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
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">利息收益指标：</li>
                    <li style="margin: 3px; padding: 0; width:260px; float: left;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtInterest" type="text" runat="server" style="width: 150px;" class="input_text" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span><span style="font:12px/18px Verdana, Simsun, Helvetica, Arial, sans-serif">元</span>
                    </li>
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">奖励比例：</li>
                    <li style="margin: 3px; padding: 0; width:260px; float: left;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtRewardScale" type="text" runat="server" style="width: 80px;" class="input_text" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span><span style="font:12px/18px Verdana, Simsun, Helvetica, Arial, sans-serif">%</span>
                    </li>
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">等级排序：</li>
                    <li style="margin: 3px; padding: 0; width:260px; float: left;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtRankLevel" type="text" runat="server" style="width: 100px;" class="input_text" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </li>
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">等级描述：</li>
                    <li style="margin: 3px; padding: 0; width:260px; float: left;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtLevelDesc" type="text" runat="server" style="width: 230px;" class="input_text" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </li>
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">是否启用：</li>
                    <li style="margin: 3px; padding-top:7px; width:260px; float: left;">
                        <input id="ckbEnable" name="ckbEnable" type="checkbox" runat="server" />
                    </li>
                </ul>
                <div style="text-align: right; width: 400px;border-top: dashed 1px #ff6347; float: right;padding-top: 5px;">
                    <%--<input id="btnOperate" type="button" value="提交" runat="server" onserverclick="Operate_Click" onclick="return validate();" style="margin-right:5px; " class="inputButton" />--%>
                    <asp:Button Text="提交" OnClick="Operate_Click" runat="server" OnClientClick="return validate();" CssClass="inputButton" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
