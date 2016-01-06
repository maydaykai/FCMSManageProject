<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectTest.aspx.cs" Inherits="WebUI.MemberManage.SelectTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>AAA</title>
    <script src="/js/jQuery/jquery-1.7.2.min.js"></script>
    <script src="/js/Area.js"></script>
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            getProvinceListByHandler($("#ulProvince"), $("#divProvince"), $("#hdProvince"), $("#ulCity"), $("#divCity"), $("#hdCity"));
        });
    </script>
</head>
<body>

    <form id="form1" runat="server">
        <div class="diy_select">
            <input id="hdProvince" name="" class="diy_select_input" type="hidden" runat="server" />
            <div style="width: 4px; float: left;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <div id="divProvince" class="diy_select_txt" style="width: 70px; float: left;">--请选择--</div>
            <div class="diy_select_btn">&nbsp;</div>
            <ul id="ulProvince" style="display: none;" class="diy_select_list">
            </ul>
        </div>
        <div class="diy_select">
            <input id="hdCity" name="" class="diy_select_input" type="hidden" runat="server" />
            <div style="width: 4px; float: left;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <div id="divCity" class="diy_select_txt" style="width: 70px; float: left;">--请选择--</div>
            <div class="diy_select_btn"></div>
            <ul id="ulCity" style="display: none;" class="diy_select_list">
            </ul>
        </div>
    </form>
    <script src="/js/selectDiv.js"></script>
</body>
</html>
