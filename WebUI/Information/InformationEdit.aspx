<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformationEdit.aspx.cs" Inherits="WebUI.Information.InformationEdit" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script type="text/javascript">  
        $(function () {
            $("#<%=cbRecommend.ClientID%>").tzCheckbox({ labels: ['Enable', 'Disable'] });
            var editor;
            KindEditor.ready(function (K) {
                editor = K.create('#<%=txtContent.ClientID%>', {
                    cssPath: '/js/kindeditor-4.1.10/plugins/code/prettify.css',
                    uploadJson: '/js/kindeditor-4.1.10/asp.net/upload_json.ashx',
                    items: [
		                'source', '|', 'undo', 'redo', '|', 'preview', 'print', 'template', 'code', 'cut', 'copy', 'paste',
		                'plainpaste', 'wordpaste', '|', 'justifyleft', 'justifycenter', 'justifyright',
		                'justifyfull', 'insertorderedlist', 'insertunorderedlist', 'indent', 'outdent', 'subscript',
		                'superscript', 'clearhtml', 'quickformat', 'selectall', '|', 'fullscreen', '/',
		                'formatblock', 'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold',
		                'italic', 'underline', 'strikethrough', 'lineheight', 'removeformat', '|', 'image',
		                'table', 'hr', 'emoticons', 'baidumap', 'pagebreak',
		                'anchor', 'link', 'unlink', '|', 'about'
                    ],
                    afterCreate: function () {
                        var self = this;
                        K.ctrl(document, 13, function () {
                            self.sync();
                            K('form[name=form1]')[0].submit();
                        });
                        K.ctrl(self.edit.doc, 13, function () {
                            self.sync();
                            K('form[name=form1]')[0].submit();
                        });
                    }
                });
                prettyPrint();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="0" cellspacing="0" class="editTable" border="0" style="min-width: 800px;">
                <tr>
                    <td style="text-align: right; width: 150px;">资讯标题：</td>
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
                <tr style="display: none" id="Categories" runat="server">
                    <td style="text-align: right;">标题小类：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <div style="float: left; margin-bottom: -3px;" class="selectDiv">
                            <span class="fl">
                                <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                          
                            <select id="SelectMediaType" name="SelectMediaType" runat="server"  ></select> 
                            <asp:Button ID="Button1" runat="server" Text="应用" CssClass="inputButton" OnClick="Button1_Click"  />
                           <span class="fl" style="margin-left: -5px; cursor: pointer;">
                                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_SelectMediaType" />
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
                            <select id="selSections" name="selSections" runat="server" disabled="disabled"></select>
                            <span class="fl" style="margin-left: -5px; cursor: pointer;">
                                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selSections" />
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

                  <tr runat="server" id="AuthorId" style="display:none">
                    <td style="text-align: right;">作者：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <div style="float: left; margin-bottom: -3px;">
                            <span class="fl">
                                <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                            <input id="txt_Author" type="text" value="" class="input_text fl" maxlength="20" style="width: 200px;" runat="server"  />
                            <span class="fl">
                                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                        </div>
                    </td>
                </tr>

                 <tr runat="server" id="SourceId" style="display:none">
                    <td style="text-align: right;">来源连接地址：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <div style="float: left; margin-bottom: -3px;">
                            <span class="fl">
                                <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                            <input id="txt_Source" type="text" value="" class="input_text fl" maxlength="200" style="width: 200px;" runat="server"  />
                            <span class="fl">
                                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                            </span>
                        </div>
                    </td>
                </tr>

                <tr  runat="server" id="Media_logo" style="display:none">
                    <td style="text-align: right;">Logo图片：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <table class="noBorderTable">
                            <tr>
                                <td>
                                    <img class="fl imgS" src="../images/news_con_title.png" style="margin: 3px 3px 6px 0px; display: inline-block;" id="imgNewsImg_logo" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="btnUpload_logo" style="text-align: center; display:none" class="fl inputButton"><span></span></div>
                                    <div id="uploadTip_logo" style="margin-top: 10px; margin-left: 10px; display:none" class="fl">请选择文件</div>
                                    <input type="hidden" id="hiNewsImg_logo" runat="server" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">资讯图片：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <table class="noBorderTable">
                            <tr>
                                <td>
                                    <img class="fl imgS" src="../images/news_con_title.png" style="margin: 3px 3px 6px 0px; display: inline-block;" id="imgNewsImg" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="btnUpload" style="text-align: center;" class="fl inputButton"><span></span></div>
                                    <div id="uploadTip" style="margin-top: 10px; margin-left: 10px;" class="fl">请选择文件</div>
                                    <input type="hidden" id="hiNewsImg" runat="server" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">资讯正文：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <textarea id="txtContent" runat="server" style="width: 700px; height: 400px;" name="content"></textarea>
                    </td>
                </tr>
                <tr id="SummaryCountId" style="display: none"  runat="server">
                    <td style="text-align: right;">摘要：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <textarea id="txt_SummaryCount" runat="server" maxlength="100" style="width: 700px; height: 400px;" name="content"></textarea>
                    </td>
                </tr>

                <tr runat="server">
                    <td style="text-align: right;">是否为推荐资讯：</td>
                    <td style="text-align: left; padding-left: 5px;">
                        <input type="checkbox" id="cbRecommend" runat="server" />
                    </td>
                </tr>
                <tr runat="server">
                    <td style="text-align: right;">排序值：</td>
                    <td style="text-align: left;">
                        <input type="text" id="txt_ShowDesc" runat="server" />(排序值,默认为0  如果为0则不会按照此条件)</td>
                </tr>


                <tr runat="server" id="trAudit" style="display: none">
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
                        <input type="hidden" id="hie_source" runat="server" />
                        <asp:Button ID="btnSave1" runat="server" Text="提交" CssClass="inputButton" OnClick="btnSave_ServerClick" />&nbsp;&nbsp;<input type="button" class="inputButton" value="返回" onclick="location.href = 'InformationManage.aspx?columnId=<%=ColumnId%>    ';" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script type="text/javascript">
        // 设置上传配置
        $.jUploader.setDefaults({
            cancelable: true,
            allowedExtensions: ['jpg', 'png', 'gif','jpeg'],
            messages: {
                upload: '上传',
                cancel: '取消',
                emptyFile: "{file} 为空，请选择一个文件.",
                invalidExtension: "{file} 后缀名不合法. 只有 {extensions} 是允许的.",
                onLeave: "文件正在上传，如果你现在离开，上传将会被取消。"
            }
        });

        $.jUploader({
            button: 'btnUpload', // 这里设置按钮id
            action: '/js/kindeditor-4.1.10/asp.net/upload_json.ashx', // 这里设置上传处理接口

            // 开始上传事件
            onUpload: function (fileName) {
                $('#uploadTip').text('正在上传 ' + fileName + ' ...');
            },

            // 上传完成事件
            onComplete: function (fileName, response) {
                if (response.error == "0" || response.error == 0) {
                    $('#<%=imgNewsImg.ClientID%>').attr('src', response.url);
                    $('#<%=hiNewsImg.ClientID%>').val(response.fileName);
                    $('#uploadTip').text(fileName + ' 上传成功');
                } else {
                    $('#uploadTip').text('上传失败');
                }
            },

            // 取消上传事件
            onCancel: function (fileName) {
                $('#uploadTip').text(fileName + ' 上传取消');
            },

            // 系统信息显示
            showMessage: function (message) {
                $('#uploadTip').text(message);
            }
        });


        $.jUploader({
            button: 'btnUpload_logo', // 这里设置按钮id
            action: '/js/kindeditor-4.1.10/asp.net/upload_json.ashx', // 这里设置上传处理接口

            // 开始上传事件
            onUpload: function (fileName) {
                $('#uploadTip_logo').text('正在上传 ' + fileName + ' ...');
            },

            // 上传完成事件
            onComplete: function (fileName, response) {
                if (response.error == "0" || response.error == 0) {
                    $('#<%=imgNewsImg_logo.ClientID%>').attr('src', response.url);
                    $('#<%=hiNewsImg_logo.ClientID%>').val(response.fileName);
                    $('#uploadTip_logo').text(fileName + ' 上传成功');
                } else {
                    $('#uploadTip_logo').text('上传失败');
                }
            },

                // 取消上传事件
                onCancel: function (fileName) {
                    $('#uploadTip_logo').text(fileName + ' 上传取消');
                },

                // 系统信息显示
                showMessage: function (message) {
                    $('#uploadTip_logo').text(message);
                }
            });

    </script>

</body>
</html>
