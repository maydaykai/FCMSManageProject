<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuickBankCardEdit.aspx.cs" Inherits="WebUI.MemberManage.QuickBankCardEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../css/global.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <script src="/js/select2css.js"></script>
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="../js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#ckbEnable').tzCheckbox({ labels: ['Enable', 'Disable'] });
        });
    </script>

</head>
<body style="margin: 0; padding: 0;">
    <form id="form1" runat="server">
        <div style="float: left;">
            <div style="width: 400px; margin: 0 auto;">
                <ul style="float: left; list-style: none; width: 400px;padding: 0;margin: 0;">
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">会员名：</li>
                    <li style="margin: 3px; padding: 0; width:260px; float: left;">
                        <span class="fl">
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtMemberName" disabled="disabled" type="text" runat="server" style="width: 150px;" class="input_Disabled" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </li>
                    <li style="width: 120px;  margin: 3px; padding-top:7px; float: left; text-align: right;" >开户名：</li>
                    <li style="margin: 3px; padding: 0; width:260px; float: left;">
                        <span class="fl">
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtRealName" type="text" disabled="disabled"  runat="server" style="width: 80px;" class="input_Disabled" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </li>
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">银行名称：</li>
                    <li style="margin: 3px; padding: 0; width:260px; float: left;">
                        <div class="selectDiv">
                            <div class="fl">
                                <img src="/images/gray_left.png" width="4" height="29" alt="" />
                            </div>

                            <asp:DropDownList ID="selCurrStatus" CssClass="fl" runat="server"></asp:DropDownList>

                            <%--<select name="selCurrStatus" id="selCurrStatus" class="fl" runat="server">
                                <option value="0">回访成功</option>
                                <option value="1">回访失败</option>
                                <option value="2">回访跟进</option>
                            </select>--%>
                            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                                <img src="/images/select_right.png" width="31" height="29" alt="" id="img_selCurrStatus" />
                            </div>
                        </div>
                    </li>                    
                  <!-- 
                       <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">银行名称：</li>
                    <li style="margin: 3px; padding: 0; width:260px; float: left;">
                        <span class="fl">
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtBankName" type="text" runat="server" style="width: 100px;" class="input_Disabled" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </li>
                      -->
                     
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">银行卡号：</li>
                    <li style="margin: 3px; padding: 0; width:260px; float: left;">
                        <span class="fl">
                            <img src="/images/gray_left.png" width="4" height="29" alt="" />
                        </span>
                        <input id="txtBankCardNo" type="text" disabled="disabled"  runat="server" style="width: 230px;" class="input_Disabled" />
                        <span class="fl">
                            <img src="../images/gray_right.png" style="width: 4px; height: 29px;" alt="" />
                        </span>
                    </li>
                    <li style="width: 120px; margin: 3px; padding-top:7px; float: left; text-align: right;">是否启用：</li>
                    <li style="margin: 3px; padding-top:7px; width:260px;  float: left;">
                        <input id="ckbEnable" name="ckbEnable" type="checkbox" runat="server"  />
                    </li>
                </ul> 
                <div style="text-align: right; width: 400px;border-top: dashed 1px #ff6347; float: right;padding-top: 5px; ">
                    <asp:Button ID="Button1" Text="修改" OnClick="Operate_Click"  runat="server"  CssClass="inputButton" />

                   <asp:Button ID="Button2" Text="禁用" OnClick="Bear_Click"  runat="server"  CssClass="inputButton" />
                </div> 
               </div>
        </div>
    </form>
    <script type="text/javascript">
        $(function () {
            $("#select_selCurrStatus").css("width", "77px");
        });
    </script>
</body>
</html>
