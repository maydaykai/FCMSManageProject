<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsMass.aspx.cs" Inherits="WebUI.SmsManage.SmsMass" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>理财短信群发</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxlistbox.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdropdownlist.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxmenu.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.pager.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.sort.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.filter.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.columnsresize.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.selection.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxpanel.js"></script>
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/icon.css" rel="stylesheet" />
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <link href="../js/Uploadify/uploadify.css" rel="stylesheet" />
    <script src="../js/Uploadify/swfobject.js"></script>
    <script src="../js/Uploadify/jquery.uploadify.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div style="width: 700px;" class="fl">

            <div style="width: 65px; margin-top: 10px;" class="fl">
                <span style="float: left;">下载模版：</span>
            </div>
            <div style="width: 635px; margin-top: 10px;" class="fl">
                <a href="smsTemp/smsTemp.rar" style="font-size: 1.2em;" class="fl">点击下载Excel模版</a><span style="color: #ff0000;">（温馨提示：建议每次数据量控制在1万条以内）</span>
            </div>
            <div style="width: 65px; margin-top: 10px;" class="fl">
                <span style="float: left; line-height: 24px;">选择文件：</span>
            </div>
            <div style="width: 635px; margin-top: 10px;" class="fl">
                <span id="file_upload"></span>
                <span id="uploadfileQueue" style="padding: 3px;" class="fl"></span>
                <input id="hfFilePath" type="hidden" />
            </div>
            <div style="width: 100%;" class="fl">
                <span style="float: left; height: 100px;">短信内容：</span>
                <span style="float: left; width: 458px;">
                    <b class="xtop">
                        <b class="xb1"></b>
                        <b class="xb2"></b>
                    </b>
                    <textarea id="txtSmsContent" cols="50" rows="5" class="xboxcon" style="width: 450px; height: 100px;"></textarea>
                    <b class="xbottom">
                        <b class="xb2"></b>
                        <b class="xb1"></b>
                    </b>
                </span>
            </div>
            <div class="fl" style="width: 100%; margin-top: 10px;">
                <span class="fl" style="height: 30px; line-height: 30px;">定时发送：</span>
                <div style="width: 4px; float: left;">
                    <img src="/images/input_left.png" width="4" height="29" alt="" />
                </div>
                <input id="sendTime" class="fl input_text" type="text" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" style="width: 182px" />
                <div class="fl" style="margin-right: 5px;">
                    <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                </div>
                <input id="sendSms_Btn" type="button" value="发送短信" class="inputButton" style="width: 80px;" />
            </div>
        </div>

        <script type="text/javascript">
            $(function () {
                $("#file_upload").uploadify({
                    //开启调试
                    'debug': false,
                    //是否自动上传
                    'auto': true,
                    'buttonText': '选择Excel文件并上传',
                    //flash
                    'swf': "../js/Uploadify/uploadify.swf",
                    //文件选择后的容器ID
                    'queueID': 'uploadfileQueue',
                    'uploader': '../js/Uploadify/UploadifyHandler.ashx',
                    'width': '160',
                    'height': '24',
                    'multi': false,
                    'fileTypeDesc': '支持的格式：',
                    'fileTypeExts': '*.xls;*.xlsx;',
                    'fileSizeLimit': '5MB',
                    'removeTimeout': 1,
                    //返回一个错误，选择文件的时候触发
                    'onSelectError': function (file, errorCode, errorMsg) {
                        switch (errorCode) {
                            case -100:
                                MessageAlert("上传的文件数量已经超出系统限制的" + $('#file_upload').uploadify('settings', 'queueSizeLimit') + "个文件！", "warning", "");
                                break;
                            case -110:
                                MessageAlert("文件 [<span style='color:#ff0000;'>" + file.name + "</span>] 大小超出系统限制的<span style='color:#ff0000;'>" + $('#file_upload').uploadify('settings', 'fileSizeLimit') + "</span>大小！", "warning", "");
                                break;
                            case -120:
                                MessageAlert("文件 [<span style='color:#ff0000;'>" + file.name + "</span>] 大小异常！", "warning", "");
                                break;
                            case -130:
                                MessageAlert("文件 [<span style='color:#ff0000;'>" + file.name + "</span>] 类型不正确！", "warning", "");
                                break;
                        }
                    },
                    //检测FLASH失败调用
                    'onFallback': function () {
                        MessageAlert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
                    },
                    //上传到服务器，服务器返回相应信息到data里
                    'onUploadSuccess': function (file, data, response) {
                        $("#hfFilePath").val(data);
                        LHG_Tips("smsTipA", "恭喜您，文件上传成功！", 3, 'success.png', '');
                    }
                });

                $("#sendSms_Btn").click(function () {
                    if ($("#hfFilePath").val() == "") {
                        LHG_Tips("smsTipA", "非常抱歉，请先上传Excel文件！", 3, 'warning.png', '');
                        return;
                    }
                    if ($.trim($("#txtSmsContent").val()) == "") {
                        LHG_Tips("smsTipA", "请填写短信内容！", 3, 'warning.png', '');
                        return;
                    }
                    SmsSendByExcel();
                });
            });

            //上传
            function doUplaod() {
                $('#file_upload').uploadify('upload', '*');
            }
            //取消上传
            function closeLoad() {
                $('#file_upload').uploadify('cancel', '*');
            }
        </script>

        <script type="text/javascript">
            function SmsSendByExcel() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/HanderAshx/UserManage/SmsSendByExcelHandler.ashx?filePath=" + $("#hfFilePath").val() + "&smsContent=" + encodeURIComponent($.trim($("#txtSmsContent").val())) + "&sendTime=" + $("#sendTime").val(),
                    dataType: "json",
                    beforeSend: function () { LHG_Tips("smsTipLoad", '正在加载数据至短信队列，请稍等...', '', 'tip_loading.gif', ''); },
                    success: function (jsonData) {
                        tip_close("smsTipLoad");
                        LHG_Tips("smsTipMsg", jsonData.msg, 4, jsonData.icon + '.png', '');
                        if (jsonData.icon == "success") {
                            $("#hfFilePath").val("");
                        }
                    },
                    error: function (responseText) {
                        tip_close("smsTipLoad");
                        alert(responseText.responseText);
                    }
                });
            }
        </script>
    </form>
</body>
</html>
