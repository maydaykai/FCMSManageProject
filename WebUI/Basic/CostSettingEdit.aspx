<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CostSettingEdit.aspx.cs" Inherits="WebUI.Basic.CostSettingEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>费用比例设置</title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="../js/select2css.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" class="editTable">
            <tr>
                <td style="text-align: right; width: 200px;">设置费用类型：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <div class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </div>
                        <select name="selFeeType" id="selFeeType" class="fl" runat="server">
                        </select>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <img src="/images/select_right.png" width="31" height="29" alt="" id="img_selFeeType" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">计算方式：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <div class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </div>
                        <select name="selCalculationMode" id="selCalculationMode" class="fl" runat="server">
                            <option value="">--请选择--</option>
                            <option value="0">按天</option>
                            <option value="1">按月</option>
                            <option value="2">按金额</option>
                        </select>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <img src="/images/select_right.png" width="31" height="29" alt="" id="img_selCalculationMode" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">计算方式初始值：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtCalInitialValue" type="text" name="txtCalInitialValue" value="" class="input_text fl" maxlength="20" style="width: 80px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </div>
                </td>
            </tr>

            <tr>
                <td style="text-align: right">计算方式收费初始比例：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtCalInitialProportion" type="text" name="txtCalInitialProportion" value="" class="input_text fl" maxlength="20" style="width: 80px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span  class="fl" style="height: 25px; line-height: 25px; margin-top: 3px;">%</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">增加方式：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div class="selectDiv">
                        <div class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </div>
                        <select name="selIncreasingMode" id="selIncreasingMode" class="fl" runat="server">
                            <option value="">--请选择--</option>
                            <option value="0">固定比例</option>
                            <option value="1">以后按固定比例</option>
                            <option value="2">以后按递增比例</option>
                        </select>
                        <div class="fl" style="margin-left: -5px; cursor: pointer;">
                            <img src="/images/select_right.png" width="31" height="29" alt="" id="img_selIncreasingMode" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">增加初始值：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtIncreasUnit" type="text" name="txtIncreasUnit" value="" class="input_text fl" maxlength="20" style="width: 80px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span  class="fl" style="height: 25px; line-height: 25px; margin-top: 3px;">%</span>
                    </div>
                </td>
            </tr>

            <tr>
                <td style="text-align: right">增加初始比例：</td>
                <td style="text-align: left; padding-left: 5px; padding-top: 5px;">
                    <div style="float: left; margin-bottom: -3px;">
                        <span class="fl">
                            <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <input id="txtIncreasProportion" type="text" name="txtIncreasProportion" value="" class="input_text fl" maxlength="20" style="width: 80px;" runat="server" />
                        <span class="fl">
                            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                        <span  class="fl" style="height: 25px; line-height: 25px; margin-top: 3px;">%</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">启用状态：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <input id="chkEnableStatus" name="chkEnableStatus" type="checkbox" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left; padding-left: 5px;">
                    <input id="Operate_Btn" type="button" value="提交" runat="server" class="inputButton" onserverclick="Operate_Btn_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<script type="text/javascript">
    $(function () {
        $(':checkbox').tzCheckbox({ labels: ['Enable', 'Disable'] });
    });
</script>
<style type="text/css">
    .selectDiv #select_selCalculationMode { width: 65px; }
    .selectDiv #select_selIncreasingMode { width: 85px; }
</style>
