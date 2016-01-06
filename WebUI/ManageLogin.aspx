<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageLogin.aspx.cs" Inherits="WebUI.ManageLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />

    <title>管理登录</title>

    <style type="text/css">
        body { padding: 0; margin: 0; font-size: 75%; line-height: 140%; font-family: Arial, Helvetica, sans-serif; }
        body, html { height: 100%; }
        .clear { clear: both; }
        #outer { height: 100%; overflow: hidden; position: relative; width: 100%; background: #016aa9; }
            #outer[id] { display: table; position: static; }
        #middle { position: absolute; top: 50%; text-align: center; }
            /* for explorer only*/
            #middle[id] { display: table-cell; vertical-align: middle; position: static; }
        #inner { position: relative; top: -50%; *top: 230px; width: 923px; height: 400px; margin: -30px auto 0 auto; text-align: left; }
        /*for explorer only */
        table.greenBorder { width: 923px; display: block; }
        .bg_top { background: url(images/bg_top.png); height: 78px; width: 923px; display: block; }
        .bg_bottom { background: url(images/bg_bottom.png); height: 230px; width: 923px; }
        .bg_center { background: url(images/bg_center.png); height: 92px; width: 923px; display: block; }

        .con_top { background: url(images/con_top.png) no-repeat; display: block; height: 92px; width: 490px; margin: 0 auto; }
        .con_center { background: url(images/con_center.png) no-repeat; padding-top: 10px; height: 82px; margin: 0 auto; display: block; width: 490px; }
        .con_bottom { background: url(images/con_bottom.png) no-repeat; height: 94px; width: 490px; display: block; margin: 0 auto 44px auto; }
        .log_box { width: 145px; height: 18px; margin: 0 auto 8px auto; }
        .log_title { width: 40px; display: block; float: left; text-align: left; height: 18px; line-height: 18px; color: #000; margin-top: 2px; }
        .log_input { border: 1px solid #7fbed9; background: #292929; height: 16px; width: 95px; line-height: 16px; padding-left: 3px; color: #fff; }
        .log_button { width: 49px; height: 18px; float: right; margin-right: 4px; background: url(images/button.png) no-repeat; border: 0; cursor: pointer; color: #fff; text-align: center; line-height: 18px; }
        a:focus, input[type=button], input[type=submit] { outline: none; }
    </style>
    <script type="text/javascript">
        // <![CDATA[

        function toggleContent(name, n) {
            var i, t = '', el = document.getElementById(name);
            if (!el.origCont) el.origCont = el.innerHTML;

            for (i = 0; i < n; i++) t += el.origCont;
            el.innerHTML = t;
        }

        // ]]>
    </script>
    <script src="js/lhgdialog/lhgcore.lhgdialog.min.js"></script>

    <script src="js/lhgdialog/ShowDialog.js"></script>
</head>

<body>
    <div id="outer">
        <div id="middle">
            <table width="923px" id="inner" class="greenBorder" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td class="bg_top"></td>
                </tr>
                <tr>
                    <td class="bg_center">
                        <span class="con_top"></span>
                    </td>
                </tr>
                <tr>
                    <td class="bg_bottom">
                        <div class="con_center">
                            <form runat="server" id="form1">
                                <div class="log_box">
                                    <span class="log_title fl">用户名</span>
                                    <input id="txtUserName" type="text" name="txtUserName" value="" runat="server" class="log_input fl" />
                                    <div class="clear"></div>
                                </div>
                                <div class="log_box">
                                    <span class="log_title fl">密　码</span>
                                    <input id="txtPassWord" type="password" name="txtPassWord" value="" runat="server" class="log_input fl" />
                                    <div class="clear"></div>
                                </div>
                                <div class="log_box">
                                    <input id="Button1" type="button" value="登 录" runat="server" onserverclick="Login_Click" class="log_button" hidefocus="true" />
                                    <div class="clear"></div>
                                </div>
                            </form>
                        </div>
                        <span class="con_bottom"></span>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</body>
</html>
