<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RightEdit.aspx.cs" Inherits="WebUI.UserManage.RightEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加权限</title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#ckbRight').tzCheckbox({ labels: ['Enable', 'Disable'] });
        });
    </script>
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>

</head>
<body style="margin:0;padding: 0;">
    <form id="form1" runat="server">
        <div style="font-size: 14px;width:250px; margin:0 auto;">
            <div>
                <span class="fl" style="margin-top:10px;">权限名称：</span>
                <span class="fl" style="margin-top:5px;">
                    <span class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </span>
                    <input id="txtRightName" type="text" runat="server" class="input_text" />
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </span>
            </div>
            <div style="clear: both; margin-top: 10px; padding-top:10px; padding-bottom:10px;">
                <span style="float: left;">是否启用：</span><span><input id="ckbRight" name="ckbRight" type="checkbox" runat="server" /></span>
            </div>
            <div style="clear: both; text-align: center;">
                <input id="btnOperate" type="button" value="提交" runat="server" OnServerClick="Operate_Click" class="inputButton" />
            </div>
        </div>
    </form>
</body>
</html>
