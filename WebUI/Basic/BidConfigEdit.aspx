<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BidConfigEdit.aspx.cs" Inherits="WebUI.Basic.BidConfigEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>投资额度上下限设置</title>
    <link href="../css/global.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#ckbEnable').tzCheckbox({ labels: ['Enable', 'Disable'] });
        });
    </script>
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>

</head>
<body style="margin: 0; padding: 0;">
    <form id="form1" runat="server">
        <div style="float: left;">
            <div style="width: 300px; margin: 0 auto;">
                <ul style="float: left; list-style: none; width: 300px;padding: 0;margin: 0;">
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">借款金额：</li>
                    <li style="margin: 3px; padding: 0; float: left;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtLoanAmount" type="text" runat="server" style="width: 80px;" class="input_text" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </li>
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">最小投资金额：</li>
                    <li style="margin: 3px; padding: 0; float: left;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtMinBidAmount" type="text" runat="server" style="width: 80px;" class="input_text" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </li>
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">最大投资金额：</li>
                    <li style="margin: 3px; padding: 0; float: left;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtMaxBidAmount" type="text" runat="server" style="width: 80px;" class="input_text" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </li>
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">是否启用：</li>
                    <li style="margin: 3px; padding-top:7px; float: left;">
                        <input id="ckbEnable" name="ckbEnable" type="checkbox" runat="server" />
                    </li>
                </ul>
                <div style="text-align: right; width: 300px;border-top: dashed 1px #ff6347; float: right;padding-top: 5px;">
                    <input id="btnOperate" type="button" value="提交" runat="server" onserverclick="Operate_Click" style="margin-right:5px; " class="inputButton" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
