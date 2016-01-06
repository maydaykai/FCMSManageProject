<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuaranteeNoAdd.aspx.cs" Inherits="WebUI.p2p.GuaranteeNoAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户信息编辑</title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <style type="text/css">
         .selectDiv .select_box {
         width: 85px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" class="editTable">
            <tr>
                <td style="text-align: right">所属担保公司：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:5px;">
                    <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <select id="sel_guaranteeId" class="fl input_text" style="margin: 0; width: 208px;" runat="server">
                        </select>
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width:150px;">保函号：</td>
                <td style="text-align: left; padding-left: 5px; color: #f00; padding-top:5px;">
                    <span class="fl">
                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" runat="server" />
                    </span>
                    <input id="txtGuaranteeNo" type="text" runat="server" style="width: 200px;" class="input_text" />
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" runat="server" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left; padding-left: 5px;">
                    <input id="Button1" type="button" value="提交" runat="server" onserverclick="Button1_Click" class="inputButton" />
                    <input type="button" value="返回" class="inputButton" onclick="window.history.go(-1);" />
                </td>
            </tr>
        </table>
    </form>
    </body>
</html>