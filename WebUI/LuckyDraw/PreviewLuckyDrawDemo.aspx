<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewLuckyDrawDemo.aspx.cs" Inherits="WebUI.LuckyDraw.Preview" %>
<%@ Import Namespace="LuckyDraw.Common" %>
<!DOCTYPE>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>转盘抽奖</title>
        <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"> </script>
        <script src="/js/Rotate.js"> </script>
        <style>
            * {
                margin: 0;
                padding: 0;
            }

            .lotteryMain {
                padding: 0px 0;
                width: 100%;
            }

            .lotteryBg {
                background: url(/images/lotteryBg.gif) no-repeat;
                height: 520px;
                margin: 0 auto;
                overflow: hidden;
                position: relative;
                width: 520px;
            }

            #run {
                -ms-transform: rotate(0deg);
                height: 214px;
                left: 50%;
                margin-left: -76px;
                margin-top: -107px;
                position: absolute;
                top: 50%;
                transform: rotate(0deg);
                width: 153px;
                z-index: 1;
            }

            #btn_run {
                background: url(/images/btn_start.png) no-repeat;
                border: none;
                cursor: pointer;
                height: 125px;
                left: 50%;
                margin-left: -62px;
                margin-top: -62px;
                outline: none;
                position: absolute;
                top: 50%;
                width: 125px;
                z-index: 2;
            }
        </style>
    </head>

    <body>
        <section class="lotteryMain">
            <div class="lotteryBg">
                <img id="run" src="/images/start.png" />
                <input id="btn_run" type="button" value="" />
                <div id="Prize01" style="color: #79140c; font-weight: bold; left: 230px; position: absolute; top: 120px;width: 50px; ">一等奖：</div>
                <div id="Prize02" style="color: #79140c; font-weight: bold; left: 340px; position: absolute; top: 180px;width: 50px; ">二等奖：</div>
                <div id="Prize03" style="color: #79140c; font-weight: bold; left: 340px; position: absolute; top: 300px;width: 50px; ">三等奖：</div>
                <div id="Prize04" style="color: #79140c; font-weight: bold; left: 230px; position: absolute; top: 360px;width: 50px; ">四等奖：</div>
                <div id="Prize05" style="color: #79140c; font-weight: bold; left: 130px; position: absolute; top: 300px; width: 50px; text-align: left;">五等奖：</div>
                <div id="Prize06" style="color: #79140c; font-weight: bold; left: 130px; position: absolute; top: 180px; width: 50px; text-align: left;">六等奖：</div>
            </div>
        </section>  
        <script>
            $(function() {
                $("#btn_run").click(function() {
                    $("#btn_run").attr('disabled', true).css("cursor", "default");
                    lottery();
                });

                <% for (int i = 0; i < CommonData.PrizeKindCount; i++)
                   { %>;
                $("#Prize0<%= (i + 1) %>").html("奖品：<br><%= Prizes[i] %>");
                <% } %>;
            });

            function lottery() {
                $.ajax({
                    type: 'get',
                    url: 'PreviewData.aspx',
                    dataType: 'json',
                    cache: false,
                    error: function() { return false; },
                    success: function(obj) {
                        $("#run").rotate({
                            duration: 3000, //转动时间 
                            angle: 0, //默认角度
                            animateTo: 360 * 6 + obj.rotate, //转动角度 
                            easing: $.easing.easeOutSine,
                            callback: function() {
                                alert("恭喜您获得了奖品：" + obj.results);
                                $("#btn_run").attr('disabled', false).css("cursor", "pointer");
                            }
                        });
                    }
                });
            }
        </script>  
    </body>
</html>