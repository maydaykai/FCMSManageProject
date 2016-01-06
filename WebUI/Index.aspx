<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebUI.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>融金宝平台管理系统</title>
    <link href="css/icon.css" rel="stylesheet" />
    <link href="js/easyUi/themes/icon.css" rel="stylesheet" />
    <link href="js/easyUi/themes/default/easyui.css" rel="stylesheet" />
    <script src="js/jQuery/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="js/easyUi/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="js/easyUi/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="js/lhgdialog/lhgdialog.min.js" type="text/javascript"></script>
    <script src="js/lhgdialog/ShowDialog.js" type="text/javascript"></script>
</head>
<body class="easyui-layout" style="overflow-y: hidden" fit="true" scroll="no">
    <noscript>
        <div style="position: absolute; z-index: 100000; height: 2046px; top: 0px; left: 0px; width: 100%; background: white; text-align: center;">
            <img src="images/noscript.gif" style="border: 0;" />
        </div>
    </noscript>
    <!--头部信息start-->
    <div region="north" split="false" border="false" style="overflow: hidden; height: 40px; line-height: 40px; background: url(images/manage__top_bg.jpg) #D2E0F2 repeat-x center 50%;">
        <span id="UserNameSpan" style="height: 32px; float: left; margin-left: 200px; margin-top: 4px; font-family: Verdana;">
            <asp:Label ID="lblWelCome" runat="server" Text=""></asp:Label></span>

        <span style="width: 32px; height: 32px; float: right; margin-right: 30px; margin-top: 4px;">

            <img id="loginOut" src="images/LoginOut.png" alt="注销登录" title="注销登录" style="border: 0; width: 32px; height: 32px; cursor: pointer;" onmouseover="this.src='images/LoginOut_Hover.png'" onmouseout="this.src='images/LoginOut.png'" />
        </span>
        <span style="width: 32px; height: 32px; float: right; margin-right: 20px; margin-top: 4px;">
            <img id="chanagePwd" src="images/chanage_pwd.png" alt="修改密码" title="修改密码" style="border: 0; width: 32px; height: 32px; cursor: pointer;" />
        </span>
    </div>
    <!--头部信息end-->
    <!--底部信息start-->

    <!--    <div region="south" split="true" style="height: 30px; background: #D2E0F2;">
        <div class="footer">
            网站底部
        </div>
    </div>-->

    <!--底部信息end->
        <!--导航栏start-->
    <div region="west" split="true" title="导航菜单" style="width: 180px;" id="west">
        <div id="nav">
            <!--  导航内容 -->
        </div>
    </div>
    <!--导航栏end-->
    <!--内容页start-->
    <div id="mainPanle" region="center" style="overflow-y: hidden">
        <div id="tabs" class="easyui-tabs" fit="true" border="false">
            <!--            <div id="home" class="panel-body panel-body-noheader panel-body-noborder" title="欢迎使用"
                style="overflow: hidden;">
                <iframe id="Index" scrolling="auto" frameborder="0" width="100%" height="100%" src="http://www.he-pai.cn"></iframe>
            </div>-->
        </div>
    </div>
    <!--内容页end-->
    <!--右键菜单start-->
    <div id="mm" class="easyui-menu" style="width: 150px; display: none;">
        <div id="tabupdate">
            刷新
        </div>
        <div class="menu-sep">
        </div>
        <div id="close">
            关闭
        </div>
        <div id="closeall">
            全部关闭
        </div>
        <div id="closeother">
            除此之外全部关闭
        </div>
        <div class="menu-sep">
        </div>
        <div id="exit">
            退出
        </div>
    </div>
    <!--右键菜单end-->
</body>
</html>

<script type="text/javascript">
    var onlyOpenTitle = "欢迎使用"; //不允许关闭的标签的标题
    var _menus;
    $(function () {
        $('#chanagePwd').click(function () {
            addTab("用户密码修改", "/UserManage/ChanagePwd.aspx", "icon icon-16");
        });

        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            dataType: "text",
            url: "HanderAshx/Basic/LoginMenuHandler.ashx",
            success: function (responseData) {
                //$("#UserNameSpan").html("欢迎您,[<span style='color:#ff0000;font-size:14px;'> " + responseData + "</span> ]登录财务管理系统！");

                //数据转换
                _menus = strToJson(responseData);
                if (_menus == "" || _menus == null) {
                    MessageAlert("对不起，会话过期或未登录，请重新登录！", "error", "ManageLogin.aspx");
                    return;
                }

                //加载左边菜单
                InitLeftMenu();

                tabClose();
                tabCloseEven();
            },
            error: function (msg) {
                $.dialog.alert("导航菜单栏加载异常，请刷新页面重试。");
            }
        });
        //$('#userRefresh').click(function () {
        //    $('#tabupdate').click();
        //})
        //$('#updatePwd').click(function () {
        //    addTab("修改密码", "#", "icon icon-16");
        //})
        $('#loginOut').click(function () {
            $.dialog.confirm('您确定要注销登录吗？', function () { top.location.href = 'ManageLogin.aspx'; }, function () { $.dialog.tips('取消注销登录'); });
        });
    });

    function strToJson(str) {
        if (str == "" || str == null) {
            return null;
        }
        var json = eval('(' + str + ')');
        return json;
    }

    //初始化左侧
    function InitLeftMenu() {
        $("#nav").accordion({ animate: false, fit: true, border: false });
        var selectedPanelname = '';
        $.each(_menus.menus, function (i, n) {
            var menulist = '';
            menulist += '<ul class="navlist">';
            $.each(n.menus, function (j, o) {
                menulist += '<li><div ><a ref="' + o.menuid + '" href="#" rel="' + o.url + "?columnId=" + o.menuid + '" ><span class="icon ' + o.icon + '" >&nbsp;</span><span class="nav" style="margin-left:2px;">' + o.menuname + '</span></a></div> ';
                if (o.menus && o.menus.length > 0) {
                    menulist += '<ul class="third_ul">';
                    $.each(o.menus, function (k, p) {
                        menulist += '<li><div><a ref="' + p.menuid + '" href="#" rel="' + p.url + "?columnId=" + p.menuid + '" ><span class="icon ' + p.icon + '" >&nbsp;</span><span class="nav" style="margin-left:2px;">' + p.menuname + '</span></a></div> </li>'
                    });
                    menulist += '</ul>';
                }
                menulist += '</li>';
            });
            menulist += '</ul>';

            $('#nav').accordion('add', {
                title: n.menuname,
                content: menulist,
                border: false,
                iconCls: 'icon ' + n.icon
            });

            if (i == 0)
                selectedPanelname = n.menuname;

        });
        $('#nav').accordion('select', selectedPanelname);

        $('.navlist li a').click(function () {
            var tabTitle = $(this).children('.nav').text();

            var url = $(this).attr("rel");
            var menuid = $(this).attr("ref");
            var icon = $(this).find('.icon').attr('class');

            var third = find(menuid);
            if (third && third.menus && third.menus.length > 0) {
                $('.third_ul').slideUp();

                var ul = $(this).parent().next();
                if (ul.is(":hidden"))
                    ul.slideDown();
                else
                    ul.slideUp();
            }
            else {
                addTab(tabTitle, url, icon);
                $('.navlist li div').removeClass("selected");
                $(this).parent().addClass("selected");
            }
        }).hover(function () {
            $(this).parent().addClass("hover");
        }, function () {
            $(this).parent().removeClass("hover");
        });
    }

    //获取左侧导航的图标
    function getIcon(menuid) {
        var icon = 'icon ';
        $.each(_menus.menus, function (i, n) {
            $.each(n.menus, function (j, o) {
                if (o.menuid == menuid) {
                    icon += o.icon;
                }
            });
        });
        return icon;
    }

    function find(menuid) {
        var obj = null;
        $.each(_menus.menus, function (i, n) {
            $.each(n.menus, function (j, o) {
                if (o.menuid == menuid) {
                    obj = o;
                }
            });
        });
        return obj;
    }

    function addTab(subtitle, url, icon) {
        if (!$('#tabs').tabs('exists', subtitle)) {
            $('#tabs').tabs('add', {
                title: subtitle,
                content: createFrame(url),
                closable: true,
                icon: icon,
                tools: [{
                    iconCls: 'icon-mini-refresh',
                    handler: function () {
                        closeTab('refresh');
                    }
                }]
            });
        } else {
            $('#tabs').tabs('select', subtitle);
            $('#mm-tabupdate').click();
        }
        tabClose();
    }

    function createFrame(url) {
        var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
        return s;
    }

    function tabClose() {
        /*双击关闭TAB选项卡*/
        $(".tabs-inner").dblclick(function () {
            var subtitle = $(this).children(".tabs-closable").text();
            $('#tabs').tabs('close', subtitle);
        });

        /*为选项卡绑定右键*/
        $(".tabs-inner").bind('contextmenu', function (e) {
            $('#mm').menu('show', {
                left: e.pageX,
                top: e.pageY
            });

            var subtitle = $(this).children(".tabs-closable").text();

            $('#mm').data("currtab", subtitle);
            $('#tabs').tabs('select', subtitle);
            return false;
        });
    }

    //绑定右键菜单事件
    function tabCloseEven() {
        $('#mm').menu({
            onClick: function (item) {
                closeTab(item.id);
            }
        });
        return false;
    }

    function closeTab(action) {
        var alltabs = $('#tabs').tabs('tabs');
        var currentTab = $('#tabs').tabs('getSelected');
        var allTabtitle = [];
        $.each(alltabs, function (i, n) {
            allTabtitle.push($(n).panel('options').title);
        });

        switch (action) {
            case "refresh":
                var iframe = $(currentTab.panel('options').content);
                var src = iframe.attr('src');
                $('#tabs').tabs('update', {
                    tab: currentTab,
                    options: {
                        content: createFrame(src)
                    }
                });
                break;
            case "tabupdate":
                var iframe = $(currentTab.panel('options').content);
                var src = iframe.attr('src');
                if (src != undefined && src != "") {
                    $('#tabs').tabs('update', {
                        tab: currentTab,
                        options: {
                            content: createFrame(src)
                        }
                    });
                }
                break;
            case "close":
                var currtab_title = currentTab.panel('options').title;
                $('#tabs').tabs('close', currtab_title);
                break;
            case "closeall":
                $.each(allTabtitle, function (i, n) {
                    if (n != onlyOpenTitle) {
                        $('#tabs').tabs('close', n);
                    }
                });
                break;
            case "closeother":
                var currtab_title = currentTab.panel('options').title;
                $.each(allTabtitle, function (i, n) {
                    if (n != currtab_title && n != onlyOpenTitle) {
                        $('#tabs').tabs('close', n);
                    }
                });
                break;
            case "closeright":
                var tabIndex = $('#tabs').tabs('getTabIndex', currentTab);

                if (tabIndex == alltabs.length - 1) {
                    alert('亲，后边没有啦 ^@^!!');
                    return false;
                }
                $.each(allTabtitle, function (i, n) {
                    if (i > tabIndex) {
                        if (n != onlyOpenTitle) {
                            $('#tabs').tabs('close', n);
                        }
                    }
                });

                break;
            case "closeleft":
                var tabIndex = $('#tabs').tabs('getTabIndex', currentTab);
                if (tabIndex == 1) {
                    alert('亲，前边那个上头有人，咱惹不起哦。 ^@^!!');
                    return false;
                }
                $.each(allTabtitle, function (i, n) {
                    if (i < tabIndex) {
                        if (n != onlyOpenTitle) {
                            $('#tabs').tabs('close', n);
                        }
                    }
                });

                break;
            case "exit":
                $('#closeMenu').menu('hide');
                break;
        }
    }

    ////弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
    //function msgShow(title, msgString, msgType) {
    //    $.messager.alert(title, msgString, msgType);
    //}
</script>
