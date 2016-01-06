<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanMemberInfo.aspx.cs" Inherits="WebUI.p2p.LoanMemberInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxtabs.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="/js/Area.js"></script>
    <script src="../js/loan/init.js"></script>
    <link href="../js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" rel="stylesheet" />
    <link href="../js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    
    <script type="text/javascript">
        var formatedData = '';
        $(document).ready(function () {
            $("#btnSaveDetailInfo").click(function () {
            var obj = new Object();
            obj.ID = "<%=MemberId%>";
            obj.CreditLine = $("#txtCompanyAddress").val();
            obj.CardNumber = $("#txtCreditnumber").val();
            obj.CreditNumber = $("#txtCard").val();
            obj.IdentityCard = $("#txt_identitycard").val();
            formatedData = buildQueryString(obj);
            Corporate();
            });
        });
        //参数组装
        function buildQueryString(data) {
            var str = ''; for (var prop in data) {
                 if (data.hasOwnProperty(prop)) {
                 str += prop + '=' + data[prop] + '&';
                              }
                          }
            return str.substr(0, str.length - 1);
        }

        
        function Corporate() {
            if (loanEdit()) {
                $.ajax({
                    type: "POST",
                    url: "../HanderAshx/p2p/LoanMemberInfo.ashx",
                    data: formatedData,
                    dataType: "text",
                    success: function (data) {
                        if (data == "success") {
                            MessageAlert("保存成功", "success", "");
                        }
                        else if (data == "error") {
                            MessageAlert("保存失败", "error", location.href);
                        }
                    },
                    error: function (xmlHttpRequest) {
                        alert(xmlHttpRequest.innerText);
                    }
                });
            }
        }


        function loanEdit() {
            var CreditLine = $("#txtCompanyAddress").val();
            if (!CreditLine) {
                MessageAlert("请输入授信额度", "error", "");
                return false;
            }
            var CardNumber = $("#txtCreditnumber").val();
            if (!CardNumber) {
                MessageAlert("请输入授信编号", "error", "");
                return false; 
            }
            var CreditNumber = $("#txtCard").val();
            if (!CreditNumber) {
                MessageAlert("请输入卡号", "error", "");
                return false;
            }
            var IdentityCard = $("#txt_identitycard").val();
            if (!IdentityCard) {
                MessageAlert("请填写身份证号码", "error", "");
                return false;
            }
            return true;
            Corporate();
        }

    </script>
</head>

<body>
       <form id="form1" runat="server">
                         <table cellpadding="0" cellspacing="0" class="editTable" border="0">
                           <tr>
                            <td style="text-align: right; width: 200px;">授信额度：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="inputText">
                                    <span class="fl">
                                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtCompanyAddress" type="text" value="" class="input_text fl" maxlength="200" style="width: 250px;" runat="server" />
                                    <span class="fl">
                                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                            <tr>
                            <td style="text-align: right; width: 200px;">授信编号：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="inputText">
                                    <span class="fl">
                                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtCreditnumber" type="text"  class="input_text fl" maxlength="200" style="width: 250px;" runat="server" />
                                    <span class="fl">
                                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">卡号：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="inputText">
                                    <span class="fl">
                                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                    <input id="txtCard" type="text" value="" class="input_text fl" maxlength="200" style="width: 250px;" runat="server" />
                                    <span class="fl">
                                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 200px;">身份证号码：</td>
                            <td style="text-align: left; padding-left: 5px;">
                                <div class="inputText">
                                    <span class="fl">
                                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                                    </span> 
                                    <input id="txt_identitycard" type="text"  class="input_text fl" maxlength="200" style="width: 250px;" runat="server" />
                                    <span class="fl">
                                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                                    </span>
                                </div>
                            </td>
                        </tr>
                          <tr>
                            <td style="text-align: right; width: 200px;"></td>
                            <td style="text-align: left; padding-left: 5px;">
                            <input type="button" id="btnSaveDetailInfo" value="保 存" class="inputButton" runat="server" />&nbsp;&nbsp;<input type="button" name="btnBack" value="返 回" class="inputButton" />
                            </td>
                        </tr>
                  </table>
          </form>
</body>
</html>
