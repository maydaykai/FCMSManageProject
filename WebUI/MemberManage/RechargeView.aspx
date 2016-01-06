<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RechargeView.aspx.cs" Inherits="WebUI.MemberManage.RechargeView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="zh-CN" />
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <title></title>
</head>
<body>
    <table class="table_box" width="90%" border="1">
        <tr class="tit_bar">
            <td colspan="2" class="tit_bar">
                <span style="font-size: 14pt"><strong>单笔订单查询请求参数</strong></span></td>
        </tr>
        <tr>
            <td>1</td>
            <td>商户号: <%=merchantId%></td>
        </tr>
        <tr>
            <td>2</td>
            <td>接口版本号: <%=version%></td>
        </tr>
        <tr>
            <td>3</td>
            <td>签名方式: <%=signType%></td>
        </tr>
        <tr>
            <td style="height: 20px">4</td>
            <td style="height: 20px">商户订单号: <%=orderNo%></td>
        </tr>
        <tr>
            <td>5</td>
            <td>商户订单提交时间: <%=orderDatetime %></td>
        </tr>
        <tr>
            <td>6</td>
            <td>商户提交查询时间: <%=queryDatetime%></td>
        </tr>

    </table>
    <p>签名字符串：<%=querySignMsg%></p>


    <!-- 同步请求响应方式提交查询请求 -->
    <hr />
    <center><h4><strong>同步响应单笔订单查询结果</strong></h4></center>

    <div>
        <table border="1" width="90%">
            <tr>
                <td>验签是否成功：</td>
                <td><%=verifyResult%></td>
            </tr>
            <tr>
                <td>订单是否成功：</td>
                <td><%=payResult%></td>
            </tr>
            <tr>
                <td>查询返回数据：</td>
                <td>
                    <textarea rows="10" cols="100"><%=responseString%></textarea></td>
            </tr>
            <tr>
                <td style="text-align: right; width: 200px;"></td>
                <td style="text-align: left; padding-left: 5px;">
                    <%--<input type="button" id="btnSaveDetailInfo" value="保 存" class="inputButton" />--%>&nbsp;&nbsp<input type="button" onclick="javascript: history.go(-1);" value="返 回" class="inputButton" />
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
