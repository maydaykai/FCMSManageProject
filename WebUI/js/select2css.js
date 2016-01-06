/*#############################################################
Name: Select to CSS
Version: 0.2
Author: Utom
URL: http://utombox.com/
#############################################################*/
var selects = document.getElementsByTagName('select');

var isIE = (document.all && window.ActiveXObject && !window.opera) ? true : false;

function $Select(id) {
    return document.getElementById(id);
}

function stopBubbling(ev) {
    ev.stopPropagation();
}

function rSelects() {

    for (var i = 0; i < selects.length; i++) {
        selects[i].style.display = 'none';
        var selectTag = document.createElement('div');
        selectTag.id = 'select_' + selects[i].name;
        selectTag.className = 'select_box';
        selects[i].parentNode.insertBefore(selectTag, selects[i]);

        var selectInfo = document.createElement('div');
        selectInfo.id = 'select_info_' + selects[i].name;
        if (selects[i].disabled) {
            selectInfo.className = 'tag_select_disabled';
        }
        else {
            selectInfo.className = 'tag_select';
        }
        selectInfo.style.cursor = 'pointer';
        selectTag.appendChild(selectInfo);

        var selectUl = document.createElement('ul');
        selectUl.id = 'options_' + selects[i].name;
        selectUl.className = 'tag_options';
        selectUl.style.position = 'absolute';
        selectUl.style.display = 'none';
        selectUl.style.zIndex = '999';
        selectTag.appendChild(selectUl);

        rOptions(i, selects[i].name);

        mouseSelects(selects[i].name);

    }
}


function rOptions(i, name) {

    var options = selects[i].getElementsByTagName('option');
    var optionsUl = 'options_' + name;
    for (var n = 0; n < selects[i].options.length; n++) {
        var optionLi = document.createElement('li');
        optionLi.style.cursor = 'pointer';
        optionLi.className = 'open';
        $Select(optionsUl).appendChild(optionLi);

        var optionText = document.createTextNode(selects[i].options[n].text);
        optionLi.appendChild(optionText);

        var optionSelected = selects[i].options[n].selected;

        if (optionSelected) {
            optionLi.className = 'open_selected';
            optionLi.id = 'selected_' + name;
            $Select('select_info_' + name).appendChild(document.createTextNode(optionLi.innerHTML));
        }

        optionLi.onmouseover = function () { this.className = 'open_hover'; };
        optionLi.onmouseout = function () {
            if (this.id == 'selected_' + name) {
                this.className = 'open_selected';
            } else {
                this.className = 'open';
            }
        };

        optionLi.onclick = new Function("clickOptions(" + i + "," + n + ",'" + selects[i].name + "')");
    }
}

function mouseSelects(name) {
    var sincn = 'select_info_' + name;
    var imgSel = 'img_' + name;
    $Select(sincn).onmouseover = function () { if (this.className == 'tag_select') this.className = 'tag_select_hover'; };
    $Select(sincn).onmouseout = function () { if (this.className == 'tag_select_hover') this.className = 'tag_select'; };
    $Select(imgSel).onmouseover = function () { if ($Select(sincn).className == 'tag_select') $Select(sincn).className = 'tag_select_hover'; };
    $Select(imgSel).onmouseout = function () { if ($Select(sincn).className == 'tag_select_hover') $Select(sincn).className = 'tag_select'; };

    if (isIE) {
        $Select(sincn).onclick = new Function("clickSelects('" + name + "');window.event.cancelBubble = true;");
        $Select(imgSel).onclick = new Function("clickSelects('" + name + "');window.event.cancelBubble = true;");
    } else if (!isIE) {
        $Select(imgSel).onclick = new Function("clickSelects('" + name + "');");
        $Select(sincn).onclick = new Function("clickSelects('" + name + "');");
        $Select('select_info_' + name).addEventListener("click", stopBubbling, false);
        $Select(imgSel).addEventListener("click", stopBubbling, false);
    }

}

function clickSelects(name) {

    var sincn = 'select_info_' + name;
    var sinul = 'options_' + name;

    for (var i = 0; i < selects.length; i++) {
        if (selects[i].name == name) {
            if ($Select(sincn).className == 'tag_select_hover') {
                $Select(sincn).className = 'tag_select_open';
                $Select(sinul).style.display = '';
            }
            else if ($Select(sincn).className == 'tag_select_open') {
                $Select(sincn).className = 'tag_select_hover';
                $Select(sinul).style.display = 'none';
            }
        }
        else {
            if ($Select(selects[i].id).disabled) {
                $Select('select_info_' + selects[i].name).className = 'tag_select_disabled';
            } else {
                $Select('select_info_' + selects[i].name).className = 'tag_select';
            }
            $Select('options_' + selects[i].name).style.display = 'none';
        }
    }

}

function clickOptions(i, n, name) {

    var li = $Select('options_' + name).getElementsByTagName('li');
    $Select('selected_' + name).className = 'open';
    $Select('selected_' + name).id = '';
    li[n].id = 'selected_' + name;
    li[n].className = 'open_hover';
    $Select('select_' + name).removeChild($Select('select_info_' + name));

    var selectInfo = document.createElement('div');
    selectInfo.id = 'select_info_' + name;
    selectInfo.className = 'tag_select';
    selectInfo.style.cursor = 'pointer';
    $Select('options_' + name).parentNode.insertBefore(selectInfo, $Select('options_' + name));

    mouseSelects(name);

    $Select('select_info_' + name).appendChild(document.createTextNode(li[n].innerHTML));
    $Select('options_' + name).style.display = 'none';
    $Select('select_info_' + name).className = 'tag_select';
    selects[i].options[n].selected = 'selected';

}

window.onload = function (e) {
    var bodyclick = document.getElementsByTagName('body').item(0);
    rSelects();
    bodyclick.onclick = function () {
        for (var i = 0; i < selects.length; i++) {
            if ($Select(selects[i].id).disabled) {
                $Select('select_info_' + selects[i].name).className = 'tag_select_disabled';
            } else {
                $Select('select_info_' + selects[i].name).className = 'tag_select';
            }
            $Select('options_' + selects[i].name).style.display = 'none';
        }
    };
};
