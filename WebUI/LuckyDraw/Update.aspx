<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update.aspx.cs" Inherits="WebUI.LuckyDraw.Update" %>

<%@ Import Namespace="LuckyDraw.Common" %>


<% int prizeNumber = CommonData.PrizeKindCount; %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.ui-start.css" type="text/css" />
    <link href="/css/icon.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"> </script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqx-all.js"> </script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/"> </script>
    <link href="../css/select.css" rel="stylesheet" />
    <script src="../js/My97DatePicker/WdatePicker.js"> </script>
    <link href="../css/global.css" rel="stylesheet" />

    <style>
        .editTable {
            max-width: 3000px;
        }

        .required {
            color: red;
        }

        .number {
        }

        .readOnly {
            background-color: #d6e4ea;
            /*background-image: none;*/
        }

        .rdPrize {
        }
    </style>

</head>
<body style="min-width: 800px; overflow-y: scroll;">
    <div id='dvComment' style="display: none">
        <div>点击查看规则说明</div>
        <div>
            <div style="color: rgb(60, 123, 3); float: left; font-size: 14px; padding-left: 20px; text-align: left; width: 70%;">
                <div style="padding-top: 10px;">一、新建抽奖活动的奖品数目应当为6个。</div>
                <div style="padding-top: 10px;">二、如有尚未结束的抽奖活动，则无法建立新的抽奖活动。</div>
                <div style="padding-bottom: 10px; padding-top: 10px;">三、标记为默认奖的奖项，不受总数限制。</div>
            </div>
        </div>
    </div>

    <div style="padding-top: 10px;">
        <form method="post" id="frmOne">
            <input type="hidden" value="" name="txtDrawId" id="txtDrawId" />
            <div style="background-color: gray; padding-right: 10px; width: 100%;" id="frm">
                <table border="0" cellspacing="0" cellpadding="0" class="editTable" style="float: left; min-width: 600px;">
                    <tr>
                        <td style="text-align: right;"><span class="required">*</span>抽奖活动名称：</td>
                        <td style="padding-left: 5px; padding-top: 5px; text-align: left;" colspan="9">
                            <div style="float: left; margin-bottom: -3px;">
                                <div style="float: left; width: 4px;">
                                    <img src="/images/input_left.png" width="4" height="29" alt="" />
                                </div>
                                <input type="text" id="txtSweepstakeName" name="txtSweepstakeName" class="fl input_text" />
                                <div class="fl">
                                    <img src="../images/input_right.png" style="height: 29px; width: 4px;" alt="" />
                                </div>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;"><span class="required">*</span>活动时间：</td>
                        <td style="padding-left: 5px; padding-top: 5px; text-align: left;" colspan="9">
                            <div style="float: left; width: 4px;">
                                <img src="/images/input_left.png" width="4" height="29" alt="" />
                            </div>
                            <input name="startDate" type="text" id="startDate" class="fl input_text" onclick=" WdatePicker() " style="width: 140px" />
                            <div class="fl" style="margin-right: 5px;">
                                <img src="../images/input_right.png" style="height: 29px; width: 4px;" alt="" />
                            </div>
                            <div style="float: left; width: 14px;">--</div>
                            <div style="float: left; width: 4px;">
                                <img src="/images/input_left.png" width="4" height="29" alt="" />
                            </div>
                            <input name="endDate" type="text" id="endDate" class="fl input_text" onclick=" WdatePicker(); " style="width: 140px" />
                            <div class="fl" style="margin-right: 5px;">
                                <img src="../images/input_right.png" style="height: 29px; width: 4px;" alt="" />
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;"><span class="required">*</span>活动最低参与金额：</td>
                        <td style="padding-left: 5px; padding-top: 5px; text-align: left;" colspan="7">
                            <div style="float: left; width: 4px;">
                                <img src="/images/input_left.png" width="4" height="29" alt="" />
                            </div>
                            <input name="Unit" type="text" id="Unit" class="fl input_text" style="width: 140px" />
                            <div class="fl" style="margin-right: 5px;">
                                <img src="../images/input_right.png" style="height: 29px; width: 4px;" alt="" />
                            </div>
                            <div style="float: left; width: 100px;">
                                排除的抽奖金额：
                            </div>
                            <div style="float: left; width: 4px;">
                                <img src="/images/input_left.png" width="4" height="29" alt="" />
                            </div>
                            <input name="OutUnit" type="text" id="OutUnit" class="fl input_text" style="width: 140px" value="0" />
                            <div class="fl" style="margin-right: 5px;">
                                <img src="../images/input_right.png" style="height: 29px; width: 4px;" alt="" />
                            </div>
                            （5k和5w两个活动并行时，在5k的活动中填写50000，<br />
                            计算抽奖次数会排除5w）
                        </td>
                        <td colspan="2">
                            <div id="dateMessage" style="color: rgb(255, 121, 121);"></div>
                        </td>
                    </tr>

                    <% for (int i = 1; i <= prizeNumber; i++)
                       { %>
                    <tr>
                        <td style="text-align: right;"><span class="required">*</span>奖品（<%= i %>）名称：</td>
                        <td style="padding-left: 5px; padding-top: 5px; text-align: left;">
                            <div style="float: left; margin-bottom: -3px;">
                                <div style="float: left; width: 4px;">
                                    <img src="/images/input_left.png" width="4" height="29" alt="" />
                                </div>
                                <input type="text" id="txtPrize<%= i %>" name="txtPrize<%= i %>" class="fl input_text" />
                                <div class="fl">
                                    <img src="../images/input_right.png" style="height: 29px; width: 4px;" alt="" />
                                </div>
                            </div>
                            <input type="hidden" value="" name="txtPrize<%= i %>Id" id="txtPrize<%= i %>Id" />
                        </td>
                        <td style="text-align: right;"><span class="required">*</span>总数：</td>
                        <td style="padding-left: 5px; padding-top: 5px; text-align: left;">
                            <div style="float: left; margin-bottom: -3px;">
                                <div style="float: left; width: 4px;">
                                    <img src="/images/input_left.png" width="4" height="29" alt="" />
                                </div>
                                <input type="text" id="txtPrize<%= i %>Number" name="txtPrize<%= i %>Number" class="fl input_text number" value="0" maxlength="8" style="width: 80px" />
                                <div class="fl">
                                    <img src="../images/input_right.png" style="height: 29px; width: 4px;" alt="" />
                                </div>
                            </div>
                        </td>
                        <td style="text-align: right;"><span class="required">*</span>默认奖：</td>
                        <td style="padding-left: 5px; padding-top: 5px; text-align: left;">
                            <div style="float: left;">
                                <input class="rdPrize" style="cursor: pointer;" type="radio" value="<%= i %>" name="rdPrizeDefault" id="rdPrize<%= i %>Default" <%= (i == prizeNumber) ? "checked" : "" %> />
                            </div>
                        </td>
                        <td style="text-align: right;"><span class="required"></span>参考中奖率：</td>
                        <td style="padding-left: 5px; text-align: left;">
                            <div style="float: left; margin-bottom: -3px;">
                                <div style="float: left; width: 4px;">
                                    <img src="/images/input_left.png" width="4" height="29" alt="" />
                                </div>
                                <input type="text" id="SweepstakeOdds<%= i %>" name="SweepstakeOdds<%= i %>" class="fl input_text" value="0" maxlength="12" />
                                <div class="fl">
                                    <img src="../images/input_right.png" style="height: 29px; width: 4px;" alt="" />
                                </div>
                            </div>
                            <%--<div id="SweepstakeOdds<%= i %>" style="color: rgb(60, 123, 3);">
                                0.00
                            </div>--%>
                        </td>
                        <td style="text-align: right;">间隔时间：</td>
                        <td style="padding-left: 5px; padding-top: 5px; text-align: left;">
                            <div style="float: left; margin-bottom: -3px;">
                                <div style="float: left; width: 4px;">
                                    <img src="/images/input_left.png" width="4" height="29" alt="" />
                                </div>
                                <input type="text" id="txtPrize<%= i %>TimeSpan" name="txtPrize<%= i %>TimeSpan" class="fl input_text" style="width: 80px" value="0" /><span style="padding-left: 5px;">(分钟)</span>
                                <div class="fl">
                                    <img src="../images/input_right.png" style="height: 29px; width: 4px;" alt="" />
                                </div>
                            </div>
                        </td>
                    </tr>
                    <% } %>

                    <tr style="display: none">
                        <td style="text-align: right;">中奖系数：</td>
                        <td style="padding-left: 5px; padding-top: 5px; text-align: left;">
                            <div style="float: left; margin-bottom: -3px;">
                                <div style="float: left; width: 4px;">
                                    <img src="/images/input_left.png" width="4" height="29" alt="" />
                                </div>
                                <input type="text" id="SweepstakeFactor" name="SweepstakeFactor" class="fl input_text number readOnly" value="1" readonly="readonly" />
                                <div class="fl">
                                    <img src="../images/input_right.png" style="height: 29px; width: 4px;" alt="" />
                                </div>
                            </div>
                        </td>
                        <td colspan="8">
                            <div style="padding: 15px;">
                                <div id='slSweepstakeFactor'></div>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">操作：</td>
                        <td colspan="9" style="text-align: right;">
                            <div id="ops">
                                <input id="btnPreview" type="button" value="预览抽奖活动" style="cursor: pointer; display: none;" />
                                <input id="btnStart" type="button" value="保存" style="cursor: pointer" />
                            </div>
                        </td>
                    </tr>

                </table>
            </div>

            <div style="float: left; font-family: Verdana; font-size: 13px; padding-top: 15px; width: 100%;">
                <div id="SweepstakeHistory"></div>
            </div>
        </form>
    </div>
</body>
</html>

<%--初始化表单相关事件--%>
<script>
    function ChangeDateFormat(val) {
        if (val != null) {
            var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
            //月份为0-11，所以+1，月份小于10时补个0
            var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
            var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
            return date.getFullYear() + "-" + month + "-" + currentDate;
        }

        return "";
    }
    var theme = "ui-start";
    var totalPrizeNumber = <%= prizeNumber %> +0;

    var ComputeTimeDiff = function () {
        var startDate = $("#startDate").val();
        var endDate = $("#endDate").val();

        if (!startDate && !endDate) return;

        if (!startDate) {
            $("#startDate").val(endDate);
            startDate = endDate;
        } else if (!endDate) {
            $("#endDate").val(startDate);
            endDate = startDate;
        }

        startDate = StrToDate(startDate);
        endDate = StrToDate(endDate);

        if (endDate.getTime() < startDate.getTime()) {
            $("#endDate").val($("#startDate").val());
            $("#dateMessage").html("活动持续0小时0分钟0秒");
            return;
        }

        var totalMs = endDate.getTime() + 1000 * 60 * 60 * 24 - startDate.getTime(); //时间差的毫秒数

        var hours = Math.floor(totalMs / (3600 * 1000)); //计算小时数

        var leave1 = totalMs % (24 * 3600 * 1000); //计算除去天数后剩余的毫秒数
        var leave2 = leave1 % (3600 * 1000); //计算除去小时数后剩余的毫秒数
        debugger;
        var minutes = Math.floor(leave2 / (60 * 1000)); //计算分钟数

        var leave3 = leave2 % (60 * 1000); //计算除去分钟数后剩余的毫秒数
        var seconds = Math.round(leave3 / 1000); //计算秒数

        var res = "活动持续：" + hours + "小时" + minutes + "分钟" + seconds + "秒";
        $("#dateMessage").html(res);


        ////计算每种奖品的时间间隔
        //for (var i = 1; i <= totalPrizeNumber; i++) {
        //    var v = $("#txtPrize" + i + "Number").val();
        //    if (!isNaN(parseInt(v, 10))) {
        //        var spHour = hours * 60 / v;
        //        $("#txtPrize" + i + "TimeSpan").val(spHour.toFixed(4));
        //    }
        //}
    };

    var StrToDate = function (str) {
        var val = Date.parse(str);
        var newDate = new Date(val);
        return newDate;
    };

    var BindRule = function () {

        $('#frm').jqxValidator({
            rules: [
                { input: '#txtSweepstakeName', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                { input: '#startDate', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                { input: '#endDate', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                { input: '#txtPrize1', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize2', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize3', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize4', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize5', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize6', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize7', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize8', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize9', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize10', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                { input: '#txtPrize1Number', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize2Number', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize3Number', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize4Number', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize5Number', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize6Number', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize7Number', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize8Number', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize9Number', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize10Number', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                { input: '#SweepstakeOdds1', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#SweepstakeOdds2', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#SweepstakeOdds3', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#SweepstakeOdds4', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#SweepstakeOdds5', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#SweepstakeOdds6', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#SweepstakeOdds7', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#SweepstakeOdds8', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#SweepstakeOdds9', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#SweepstakeOdds10', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                { input: '#txtPrize1TimeSpan', message: '字段不能为空', action: 'keyup, blur', rule: 'required' }
                //,
                //{ input: '#txtPrize2TimeSpan', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize3TimeSpan', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize4TimeSpan', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize5TimeSpan', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize6TimeSpan', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize7TimeSpan', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize8TimeSpan', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize9TimeSpan', message: '字段不能为空', action: 'keyup, blur', rule: 'required' },
                //{ input: '#txtPrize10TimeSpan', message: '字段不能为空', action: 'keyup, blur', rule: 'required' }
            ],
            onSuccess: function () {
                $('#frmOne').attr('action', "UpdateLuckyDraw.aspx");
                $("#frmOne").submit();

            },
            onError: function () {

            }
        });
    };
</script>

<%--初始化按钮相关事件--%>
<script type="text/javascript">
    var btnModal;
    var modal = { preview: 0, create: 1 };
    //获取url中的参数
    function getUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }
    $(document).ready(function () {
        $.ajax({
            url: 'LoadDraw.ashx',
            type: 'post',
            data: { id: getUrlParam('id') },
            success: function (data) {
                var obj = JSON.parse(data);
                $('#txtSweepstakeName').val(obj.SweepstakeName);
                $('#txtDrawId').val(obj.SweepstakeId);
                $('#startDate').val(ChangeDateFormat(obj.StartDate));
                $('#endDate').val(ChangeDateFormat(obj.EndDate));
                $('#Unit').val(obj.Unit);
                $('#OutUnit').val(obj.OutUnit);
                $('#slSweepstakeFactor').jqxSlider({ min: 1, max: 6, ticksFrequency: 1, value: obj.SweepstakeFactor, step: 1, showButtons: false, mode: 'fixed', width: '90%' });
                var c = 0;
                $.each(obj.Prizes, function (i, item) {
                    var j = i + 1;
                    $('#txtPrize' + j).val(item.PrizeName);
                    $('#txtPrize' + j + 'Id').val(item.PrizeId);
                    $('#txtPrize' + j + 'Number').val(item.PrizeNumber);
                    $('#SweepstakeOdds' + j + '').val(item.WinningRate);
                    $('#txtPrize' + j + 'TimeSpan').val((parseFloat(item.IntervalTime) * 60).toFixed(4));
                    if (item.PrizeDefault) { c = i; }
                })
                $("input[name=rdPrizeDefault]:eq(" + c + ")").attr("checked", true);
            }
        })

        $("#btnStart").jqxButton({ theme: theme, width: '130', height: '30' });
        $("#btnPreview").jqxButton({ theme: theme, width: '130', height: '30' });
        $('#btnStart').on('click', function () {
            var count = 0;
            for (var i = 1; i <= 10; i++) {
                count += parseFloat($('#SweepstakeOdds' + i).val(), 10);
            }
            if (count.toFixed(10) != 1) {
                alert('中奖率相加不等于1,' + count);
                return false;
            }
            btnModal = modal.create;
            $('#frm').jqxValidator('validate');
        });
        //验证
        BindRule();
        //时间间隔
        ComputeTimeDiff();
        $("#dvComment").jqxExpander({ width: '100%', rtl: true, expanded: false });
    });
</script>

<%--初始化皮肤相关事件--%>
<script>
    $.jqx = $.jqx || {};
    $.jqx.theme = theme;
</script>

<%--初始化表格相关事件--%>
<script type="text/javascript">
    $(document).ready(function () {
        //$('#slSweepstakeFactor').jqxSlider({ min: 1, max: 6, ticksFrequency: 1, value: 1, step: 1, showButtons: false, mode: 'fixed', width: '90%' });
        $('#slSweepstakeFactor').on('change', function (event) { ComputerSweepstakeFactor(event.args.value); });
    });

    var ComputerSweepstakeFactor = function (value) {
        $("#SweepstakeFactor").val(value);
    };
</script>

<%--初始化中奖率显示相关事件--%>
<script type="text/javascript">
    $(document).ready(function () {
        $('#slSweepstakeFactor').on('change', function () { ComputeSweepstakeData(); });
        $(".rdPrize").change(function () {
            ComputeSweepstakeData();
        });

        $(".number").keyup(function () {
            var tmptxt = $(this).val();
            $(this).val(tmptxt.replace(/\D/g, ''));
            setTimeout(ComputeSweepstakeData, 0);
        }).bind("paste", function () {
            var tmptxt = $(this).val();
            $(this).val(tmptxt.replace(/\D|^0/g, ''));
            setTimeout(ComputeSweepstakeData, 0);
        }).css("ime-mode", "disabled");

        $("#slSweepstakeFactor").val(3);
    });

    //前台统计中奖率算法
    var ComputeSweepstakeData = function () {
        //ComputeTimeDiff();

        //var sf = $("#SweepstakeFactor").val(); //中奖系数

        //var realSf = Math.pow(Math.PI / 2, parseInt(sf, 10));

        //var points = {};
        //for (var i = 1; i <= totalPrizeNumber; i++) {
        //    var v = $("#txtPrize" + i + "Number").val();
        //    if (!isNaN(parseInt(v, 10))) {
        //        var itemPoint = parseInt(v, 10) * Math.pow(realSf, i - 1);
        //        points[i] = itemPoint;
        //    }
        //}

        //var sumPoint = 0;
        //for (var m = 1; m <= totalPrizeNumber; m++) {
        //    if (points[m]) {
        //        sumPoint += points[m];
        //    }
        //}

        //var odds = {};
        //if (sumPoint != 0) {
        //    for (var j = 1; j <= totalPrizeNumber; j++) {
        //        var vs = $("#txtPrize" + j + "Number").val();
        //        if (!isNaN(parseInt(vs, 10))) {
        //            var itemPointVs = parseInt(vs, 10) * Math.pow(realSf, j - 1);
        //            var realVs = (itemPointVs / sumPoint).toFixed(10);
        //            odds[j] = realVs;
        //        }

        //        $("#SweepstakeOdds" + j).html(odds[j]);
        //    }
        //}

        ////处理默认项
        //var selectedVal = $('input[name="rdPrizeDefault"]:checked').val();

        //var othersVal = 0;
        //for (var k = 1; k <= totalPrizeNumber; k++) {
        //    if (k != selectedVal && typeof odds[k] != "undefined") {
        //        othersVal += parseFloat(odds[k], 10);
        //    }
        //}

        //var defaultOdds = (1 - parseFloat(othersVal, 10)).toFixed(10);
        //$("#SweepstakeOdds" + selectedVal).html(defaultOdds);
    };

</script>
