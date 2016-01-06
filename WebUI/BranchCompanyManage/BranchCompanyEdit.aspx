<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BranchCompanyEdit.aspx.cs" Inherits="WebUI.BranchCompanyManage.BranchCompanyEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>分公司设置</title>
    <link href="../css/global.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
</head>
<body style="margin: 0; padding: 0;">
    <form id="form1" runat="server">
        <div style="float: left;">
            <div style="width: 500px; margin: 0 auto;">
                <ul style="float: left; list-style: none; width: 500px;padding: 0;margin: 0;">
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">公司名称：</li>
                    <li style="margin: 3px; padding: 0; float: left;width: 360px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtCompanyName" type="text" runat="server" style="width: 180px;" class="input_text" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </li>
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">成立日期：</li>
                    <li style="margin: 3px; padding: 0; float: left;width: 360px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtSetUpDate" type="text" runat="server" style="width: 120px;" onclick="WdatePicker()" class="input_text" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </li>
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">备注：</li>
                    <li style="margin: 3px; padding: 0; float: left;width: 360px;">
                        <textarea rows="3" cols="40" id="txtRemark" runat="server"></textarea>
                    </li>
                </ul>
                <div style="text-align: right; width: 500px;border-top: dashed 1px #ff6347; float: right;padding-top: 5px;">
                    <input id="btnOperate" type="button" value="提交" runat="server" onserverclick="Operate_Click" style="margin-right:5px; " class="inputButton" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
