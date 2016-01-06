<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BigEvent.aspx.cs" Inherits="WebUI.Information.BigEvent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <link href="/js/jquery.tzCheckbox/jquery.tzCheckbox.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/kindeditor-4.1.10/themes/default/default.css" />
    <link rel="stylesheet" href="/js/kindeditor-4.1.10/plugins/code/prettify.css" />
    <script src="/js/jquery.tzCheckbox/jquery.min.js"></script>
    <script src="/js/jquery.tzCheckbox/jquery.tzCheckbox.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="/js/select2css.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <script charset="utf-8" src="/js/kindeditor-4.1.10/kindeditor.js"></script>
    <script charset="utf-8" src="/js/kindeditor-4.1.10/lang/zh_CN.js"></script>
    <script charset="utf-8" src="/js/kindeditor-4.1.10/plugins/code/prettify.js"></script>
    <script src="../js/juploader-1.0/jquery.jUploader-1.0.js"></script>
    <style type="text/css">
        .selectDiv .select_box {
            width: 175px;
        }

        .selectDiv ul {
            width: 190px;
        }

        .noBorderTable {
            border: none;
        }

            .noBorderTable td {
                border: none;
            }

        .imgS {
            max-width: 400px;
            width: expression(document.body.clientWidth > 400?"400px":"auto" );
        }
    </style>    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" class="editTable" border="0" style="min-width: 800px;">
                <tr>
                    <td style="text-align: right; width: 150px;">事件标题：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <div style="float: left; margin-bottom: -3px;">
                            <span class="fl">
                                <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                            <input id="txtTitle" type="text" value="" class="input_text fl" style="width: 400px;" runat="server" />
                            <span class="fl">
                                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                        </div>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">资讯栏目：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <div style="float: left; margin-bottom: -3px;" class="selectDiv">
                            <span class="fl">
                                <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                            <select id="selSections" name="selSections" runat="server"></select>
                            <span class="fl" style="margin-left: -5px; cursor: pointer;">
                                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selSections" />
                            </span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 150px;">URL：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <div style="float: left; margin-bottom: -3px;">
                            <span class="fl">
                                <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                            <input id="txtUrl" type="text" value="javascript:;" class="input_text fl" style="width: 400px;" runat="server" />
                            <span class="fl">
                                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">发布时间：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <div style="float: left; margin-bottom: -3px;">
                            <span class="fl">
                                <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                            <input id="txtPubTime" type="text" value="" class="input_text fl" maxlength="20" style="width: 200px;" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" />
                            <span class="fl">
                                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                        </div>
                    </td>
                </tr>
                <tr id="SummaryCountId" runat="server">
                    <td style="text-align: right;">摘要：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <textarea id="txt_SummaryCount" runat="server" style="width: 700px; height: 400px;" name="content"></textarea>
                    </td>
                </tr>

                <tr id="Tr1" runat="server">
                    <td style="text-align: right;">是否为推荐资讯：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <input type="checkbox" id="cbRecommend" runat="server" />
                    </td>
                </tr>
                
                <tr id="Tr2" runat="server">
                    <td style="text-align: right;">排序值：</td>
                    <td style="text-align: left;">
                        <input type="text" id="txt_ShowDesc" runat="server" />(排序值,默认为0  如果为0则不会按照此条件)</td>
                </tr>


                <tr runat="server" id="trAudit">
                    <td style="text-align: right;">审核状态：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <input type="radio" value="1" id="rdStatusY" runat="server" name="status" />审核通过
                        <input type="radio" value="2" id="rdStatusN" runat="server" name="status" />审核不通过
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">操作：</td>
                    <td style="text-align: left; padding-left: 5px;">
                         <input type="hidden" id="hie_Image_value" runat="server" />
                        <asp:Button ID="btnSave1" runat="server" Text="提交" CssClass="inputButton" OnClick="btnSave_ServerClick" />&nbsp;&nbsp;<input type="button" class="inputButton" value="返回" onclick="location.href = 'BigEventManage.aspx?columnId=<%=ColumnId%>    ';" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
