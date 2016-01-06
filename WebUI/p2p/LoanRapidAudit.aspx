<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanRapidAudit.aspx.cs" Inherits="WebUI.p2p.LoanRapidAudit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../css/table.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <script src="../js/jqgrid/js/jquery-1.9.0.min.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <style type="text/css">
        .table_left {
            width: 220px; text-align: right; background-color: #E0ECFF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table width="100%" border="0" cellspacing="0" cellpadding="0" class="editTable">
          <tr>
             <td class="table_left">会员名字：</td>
             <td>
                <label id ="lab_RealName" runat="server"></label>
             </td>
          </tr>
          <tr>
             <td class="table_left">贷款会员：</td>
             <td>
                <label id ="lab_MemberName" runat="server"></label>
             </td>
          </tr>
          <tr>
             <td class="table_left">手机号码：</td>
             <td>
                <label id ="lab_memberPhone" runat="server"></label>
             </td>
          </tr>
          <tr>
            <td class="table_left">所在地区：</td>
            <td>
                <label id ="lab_province" runat="server"></label>
                <label id ="lab_city" runat="server"></label>
            </td>
          </tr>
          <tr>
            <td class="table_left">贷款用途：</td>
            <td>
                <label id ="lab_loanUseName" runat="server"></label>
            </td>
          </tr>
          <tr>
              <td class="table_left">贷款期限：</td>
            <td>
                <label id ="lab_loanTerm" runat="server"></label>
                <label id="lab_loanModel" runat="server"></label>
            </td>
          </tr>
           <tr>
            <td class="table_left">贷款额度：</td>
            <td>
                <label id ="lab_loanAmount" runat="server"></label> 元
            </td>
          </tr>
          <tr>
            <td class="table_left">当前状态：</td>
            <td>
                <label id ="lab_status" runat="server"></label>
            </td>
          </tr>
          <tr>
               <td class="table_left">申请时间：</td>
              <td>
                   <label id ="lab_createTime" runat="server"></label>
              </td>
          </tr>
          <tr>
               <td class="table_left">描述：</td>
              <td>
                   <label id ="lab_Describe" runat="server"></label>
              </td>
          </tr>
          <tr>
            <td class="table_left">审核原因：</td>
            <td>
                <input type="radio" id ="radio_audit" name="audit" runat="server" checked="True"/>审核通过
                <input type="radio" id ="radio_noAudit" name="audit" runat="server" />审核不通过
            </td>
          </tr>
           <tr>                                               
            <td class="table_left" style="border-bottom:0;">&nbsp;</td>
            <td style="border-bottom:0;"><input type="button" id="button1" runat="server" value="审核" onserverclick="button1_ServerClick" class="inputButton"/>&nbsp;&nbsp<input type="button" value="返回" onclick="javascript:history.go(-1)" class="inputButton" /></td>
          </tr>
      </table>
    </div>
    </form>
</body>
</html>
