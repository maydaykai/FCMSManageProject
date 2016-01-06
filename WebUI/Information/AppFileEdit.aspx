<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppFileEdit.aspx.cs" Inherits="WebUI.Information.AppFileEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <script src="../js/jQuery/jquery-1.7.2.min.js"></script>
    <script src="../js/juploader-1.0/jquery.jUploader-1.0.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <style type="text/css">
        .noBorderTable {
            border:none;
        }
            .noBorderTable td {
                border:none;
            }
        .imgL {
            max-width:600px;
            width:expression(document.body.clientWidth > 600?"600px":"auto" );
        }
        .imgS {
            max-width:400px;
            width:expression(document.body.clientWidth > 400?"400px":"auto" );
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" class="editTable" border="0" style="min-width:800px;">
            <tr>
                <td style="text-align: right; width: 150px;">APP安装包文件：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <table class="noBorderTable">
                        <%--<tr>
                            <td>
                                <img class="fl imgL" src="../images/nonepic_l.jpg" style="margin:3px 3px 6px 0px; display:inline-block;" id="imgLargePic" runat="server" />
                            </td>
                        </tr>--%>
                        <tr>
                            <td>
                                <div id="btnUpload1" style="text-align:center;" class="fl inputButton"><span></span></div><div id="upload1Tip" style="margin-top:10px; margin-left:10px;" class="fl">请选择文件</div><input type="hidden" id="hiLargePic" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">操作系统：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input type="text" class="input_text fl" id="txtOS" runat="server" style="width: 400px;" />
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">应用名称：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input type="text" class="input_text fl" id="txtAppName" runat="server" style="width: 400px;" />
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">版本号：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input type="text" class="input_text fl" id="txtVersion" runat="server" style="width: 400px;" />
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">版本编号：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input type="text" class="input_text fl" id="txtVersionNum" runat="server" style="width: 400px;" />
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">下载地址：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <span class="fl">
                        <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                    <input type="text" class="input_text fl" id="txtDownURL" runat="server" style="width: 400px;" />
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; ">操作：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <asp:Button ID="btnSave1" runat="server" Text="提交" CssClass="inputButton" OnClick="btnSave1_Click" />&nbsp;&nbsp;<input type="button" class="inputButton" value="返回" onclick="location.href = 'FocusFigureManage.aspx?columnId=<%=ColumnId%>    ';" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        // 设置上传配置
        $.jUploader.setDefaults({
            cancelable: true,
            allowedExtensions: ['apk'],
            messages: {
                upload: '上传',
                cancel: '取消',
                emptyFile: "{file} 为空，请选择一个文件.",
                invalidExtension: "{file} 后缀名不合法. 只有 {extensions} 是允许的.",
                onLeave: "文件正在上传，如果你现在离开，上传将会被取消。"
            }
        });

        $.jUploader({
            button: 'btnUpload1', // 这里设置按钮id
            action: '../HanderAshx/Information/AppUploadHandler.ashx', // 这里设置上传处理接口

            // 开始上传事件
            onUpload: function (fileName) {
                $('#upload1Tip').text('正在上传 ' + fileName + ' ...');
            },

            // 上传完成事件
            onComplete: function (fileName, response) {
                if (response.error == "0" || response.error == 0) {
                    <%--$('#<%=imgLargePic.ClientID%>').attr('src', response.url);--%>
                    $('#<%=hiLargePic.ClientID%>').val(response.fileName);
                    $('#upload1Tip').text(fileName + ' 上传成功');
                } else {
                    $('#upload1Tip').text('上传失败');
                }
            },

            // 取消上传事件
            onCancel: function (fileName) {
                $('#upload1Tip').text(fileName + ' 上传取消');
            },

            // 系统信息显示
            showMessage: function (message) {
                $('#upload1Tip').text(message);
            }
        });

        $.jUploader({
            button: 'btnUpload2', // 这里设置按钮id
            action: '/js/kindeditor-4.1.10/asp.net/upload_json.ashx', // 这里设置上传处理接口

            // 开始上传事件
            onUpload: function (fileName) {
                $('#upload2Tip').text('正在上传 ' + fileName + ' ...');
            },

            // 上传完成事件
            onComplete: function (fileName, response) {
                if (response.error == "0" || response.error == 0) {

                    $('#upload2Tip').text(fileName + ' 上传成功');
                } else {
                    $('#upload2Tip').text('上传失败');
                }
            },

            // 取消上传事件
            onCancel: function (fileName) {
                $('#upload2Tip').text(fileName + ' 上传取消');
            },

            // 系统信息显示
            showMessage: function (message) {
                $('#upload2Tip').text(message);
            }
        });
    </script>
</body>
</html>
