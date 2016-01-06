function MessageAlert(mMsg, mIcon, mUrl) {
    $.dialog({
        /*防止反复弹出*/id: 'MessageAlert',
        /*标题*/
        title: '系统提示',
        /*宽度*/
        width: 'auto',
        /*高度*/
        height: 'auto',
        /*消息图标*/
        icon: mIcon + '.png',
        /*内容*/
        content: '<span style="font-size:12px;font-family:宋体;margin-left:0px; padding-left:0px;">' + mMsg + '</span>',
        /*最大化按钮*/
        max: false,
        /*最小化按钮*/
        min: false,
        /*自定义按钮 */
        button: [{
            name: '确定',
            callback: function () {
                if ($.trim(mUrl) != "" && typeof (mUrl) != "undefined") {
                    window.location.href = "" + mUrl + "";
                }
            }
        }],
        /*取消*/        //cancel: true,
        /*静止定位*/
        fixed: true,
        /*锁屏*/
        lock: true,
        /*禁止拖动*/
        drag: false,
        /*改变大小*/
        resize: false,
        close: function () {
            if ($.trim(mUrl) != "" && typeof (mUrl) != "undefined") {
                window.location.href = "" + mUrl + "";
            }
        }
        //ok:function(){if(mURL!=""){window.location.href=""+mURL+"";}}
    });
}

function MessageAlertOk(mMsg, mIcon,btnId) {
    $.dialog({
        /*防止反复弹出*/id: 'MessageAlert',
        /*标题*/
        title: '系统提示',
        /*宽度*/
        width: 'auto',
        /*高度*/
        height: 'auto',
        /*消息图标*/
        icon: mIcon + '.png',
        /*内容*/
        content: '<span style="font-size:12px;font-family:宋体;margin-left:0px; padding-left:0px;">' + mMsg + '</span>',
        /*最大化按钮*/
        max: false,
        /*最小化按钮*/
        min: false,
        /*自定义按钮 */
        button: [{
            name: '确定',
            callback: function () {
                $("#" + btnId).click();
            }
        }],
        /*取消*/
        cancel: true,
        /*静止定位*/
        fixed: true,
        /*锁屏*/
        lock: true,
        /*禁止拖动*/
        drag: false,
        /*改变大小*/
        resize: false,
        close: function () {

        }
        //ok:function(){if(mURL!=""){window.location.href=""+mURL+"";}}
    });
}

function MessageAlertChild(mMsg, mIcon, mUrl) {
    var api = frameElement.api, w = api.opener, cDg;
    cDg = w.$.dialog({
        /*防止反复弹出*/id: 'MessageAlertChild',
        /*标题*/title: '系统提示',
        /*宽度*/width: 'auto',
        /*高度*/height: 'auto',
        /*消息图标*/icon: mIcon + '.png',
        /*内容*/content: '<span style="font-size:12px;margin-left:0px; padding-left:0px;">' + mMsg + '</span>',
        /*最大化按钮*/max: false,
        /*最小化按钮*/min: false,
        /*自定义按钮    //button:[{name:'确定',callback: function(){}],*/
        /*取消*/        //cancel: true,
        /*静止定位*/fixed: true,
        /*锁屏*/lock: true,
        /*禁止拖动*/drag: false,
        /*改变大小*/resize: false,
        //close:function(){},
        ok: function () { if (mUrl != "") { w.location.href = "" + mUrl + ""; } else { if (cDg != null) cDg.close(); if (api != null) api.close(); } }

    });
}

function MessageTips(mMsg, mIcon) {
    var api = frameElement.api, w = api.opener, cDg;
    cDg = w.$.dialog({
        /*防止反复弹出*/id: 'MessageAlertChild',
        /*标题*/title: '系统提示',
        /*宽度*/width: 'auto',
        /*高度*/height: 'auto',
        /*消息图标*/icon: mIcon + '.png',
        /*内容*/content: '<span style="font-size:12px;margin-left:0px; padding-left:0px;">' + mMsg + '</span>',
        /*最大化按钮*/max: false,
        /*最小化按钮*/min: false,
        /*静止定位*/fixed: true,
        /*锁屏*/lock: true,
        parent: api,
        /*禁止拖动*/drag: false,
        /*改变大小*/resize: false,
        close: function () { },
        ok: function () { if (cDg != null) cDg.close(); }

    });
}

//打开指定窗口
function MessageWindow(w, h, url) {
    $.dialog({
        /*防止反复弹出*/id: 'MessageWindow',
        /*标题*/
        title: '<span class="icon icon-36" style="margin-top:5px;">&nbsp;</span>',
        /*宽度*/
        width: (w == 0 ? 'auto' : w),
        /*高度*/
        height: (h == 0 ? 'auto' : h),
        /*内容*/
        content: "url:" + url,
        /*最大化按钮*/
        max: false,
        /*最小化按钮*/
        min: false,
        /*取消*/        //cancel: true,
        /*静止定位*/
        fixed: true,
        /*锁屏*/
        lock: true,
        /*禁止拖动*/
        drag: true,
        /*改变大小*/
        resize: false
        //ok:function(){if(mURL!=""){window.location.href=""+mURL+"";}}
    });
}

//提示窗口
function Tips(content, time, icon) {
    $.dialog.tips(content, time, icon);
}

function LHG_Tips(id, content, times, iconImg, mUrl) {
    $.dialog({
        /*防止反复弹出*/id: id,
        /*标题*/
        title: false,
        /*宽度*/
        width: 'auto',
        /*高度*/
        height: 'auto',
        /*消息图标*/
        icon: iconImg,//'loadingTip.gif',
        /*内容*/
        content: '<span style="font-size:12px;font-family:宋体;margin-left:0px; padding-left:0px;">' + content + '</span>',
        /*最大化按钮*/
        max: false,
        /*最小化按钮*/
        min: false,
        /*取消*/        //cancel: true,
        /*静止定位*/
        fixed: true,
        /*锁屏*/
        lock: true,
        /*禁止拖动*/
        drag: true,
        /*改变大小*/
        resize: false,
        time: (times == "" ? false : times),
        close: function () {
            if ($.trim(mUrl) != "" && typeof (mUrl) != "undefined") {
                window.location.href = "" + mUrl + "";
            }
        }
        //ok:function(){if(mURL!=""){window.location.href=""+mURL+"";}}
    });
}

//关闭Tip
function tip_close(tipId) {
    $.dialog({ id: tipId }).close();
}



