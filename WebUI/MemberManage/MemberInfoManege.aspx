<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberInfoManege.aspx.cs" Inherits="WebUI.MemberManage.MemberInfoManege" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">


.editTable { text-align: center; border-collapse: collapse; border: 1px solid #AFC9EA; max-width: 1500px; min-width: 700px; width: 100%; *width: expression(((document.documentElement.clientWidth||document.body.clientWidth)<=700? "700px":"100%")); margin: 0; padding: 0; }

    .editTable td { border-collapse: collapse; border: 1px solid #AFC9EA; word-break: break-all; height: 35px; }


.fl { float: left; }
.fl{ float:left} 
input, label, select, option, textarea, button, fieldset, legend { font: 12px/18px Verdana, Simsun, Helvetica, Arial, sans-serif; /*width: 20px;*/ }

input, label, select, option, textarea, button, fieldset, legend{font:12px/18px Verdana, Simsun, Helvetica, Arial, sans-serif;}


.input_text { float: left; height: 29px; background: url(/images/input_bg.png) repeat-x; border: 0; text-indent: 3px; line-height: 29px; color: #1B759F; }
        .auto-style1
        {
            margin: 0;
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table id="memberInfo" border="0" cellspacing="0" cellpadding="0" class="editTable" style="min-width: 500px; width: 40%; float: left; margin-left: 10px;">
            <tr>
                <td colspan="2" class="auto-style1">用户信息</td>
            </tr>
            <tr>
                <td class="auto-style1">年龄：</td>
                <td class="auto-style1">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        &nbsp;<span class="fl"><img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl" style="margin: 5px 0 0 5px;">
                        <span class="fl">
                        <input id="txt_memberAge" type="text" value="" class="auto-style1" maxlength="50" runat="server" /></span>岁</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">婚姻状况：</td>
                <td class="auto-style1">
                    <div class="auto-style1">
                        <label>
                            <input type="radio" name="rad_maritalStatus" value="1" checked="true" runat="server" class="auto-style1" />
                            已婚
                            <input type="radio" name="rad_maritalStatus" value="2" runat="server" class="auto-style1" />
                            未婚
                        </label>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">性别：</td>
                <td class="auto-style1">
                    <div class="auto-style1">
                        <label>
                            <input id="Radio1" type="radio" name="rad_sexStatus" checked="true" value="男" runat="server" class="auto-style1" />
                            男
                            <input id="Radio2" type="radio" name="rad_sexStatus" value="女" runat="server" class="auto-style1" />
                            女
                        </label>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">籍贯：</td>
                <td class="auto-style1">
                    <div class="auto-style1">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <select id="sel_nativeProvince" class="fl input_text" style="margin: 0; width: 100px;" runat="server" name="D1">
                            <option value="0" selected="selected">选择省份</option>
                        </select>
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <select id="sel_nativeCity" class="fl input_text" style="margin: 0; width: 100px;" runat="server" name="D2">
                            <option value="0" selected="selected">选择城市</option>
                        </select>
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">家庭人数：</td>
                <td class="auto-style1">
                    <div class="auto-style1">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        &nbsp;<span class="fl"><img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span class="fl" style="margin: 5px 0 0 5px;"><input id="txt_familyNum" type="text" name="txt_familyNum" value="" class="auto-style1" maxlength="50" runat="server" />人</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">月收入水平：</td>
                <td class="auto-style1">
                    <div class="auto-style1">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        &nbsp;<span class="fl"><img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        <input id="txt_monthlyIncome" type="text" name="txt_monthlyIncome" value="" class="auto-style1" maxlength="50" runat="server" /></span></div>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">有无房产：</td>
                <td class="auto-style1">
                    <div class="auto-style1">
                        <label>
                            <input id="Radio3" type="radio" name="rad_houseStatus" checked="true" value="1" runat="server" class="auto-style1" />
                            有
                            <input id="Radio4" type="radio" name="rad_houseStatus" value="0" runat="server" class="auto-style1" />
                            无
                        </label>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">有无车产：</td>
                <td class="auto-style1">
                    <div class="auto-style1">
                        <label>
                            <input id="Radio5" type="radio" name="rad_carStatus" checked="true" value="1" runat="server" class="auto-style1" />
                            有
                            <input id="Radio6" type="radio" name="rad_carStatus" value="0" runat="server" class="auto-style1" />
                            无
                        </label>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">工作年限：</td>
                <td class="auto-style1">
                    <div class="auto-style1">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        &nbsp;<span class="fl"><img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        <input id="text_workDuration" type="text" name="text_workDuration" value="" class="auto-style1" maxlength="50" runat="server" /></span></div>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">职业状态：</td>
                <td class="auto-style1">
                    <div class="auto-style1">
                        <label>
                            <input type="radio" name="rad_jobStatus" checked="true" value="在职员工" runat="server" class="auto-style1" />
                            在职员工
                            <input type="radio" name="rad_jobStatus" value="私企老板" runat="server" class="auto-style1" />
                            私企老板
                        </label>
                    </div>
                </td>
            </tr>
        </table>
    </div>
             <asp:Button ID="button1" runat="server" Text="立即申请" OnClick="Button1_Click" Width="60" CssClass="inputButton" />
    </form>
</body>
</html>
