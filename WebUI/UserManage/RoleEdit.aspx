<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleEdit.aspx.cs" Inherits="WebUI.UserManage.RoleEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title id="Description">角色权限分配</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdatatable.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxtreegrid.js"></script>
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <style>
        .selectDiv .select_box {
            float:left;
            width:100px;
        }
        .selectDiv ul {
            width:120px;
        }
    </style>
    <script src="../js/select2css.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            //主题
            var theme = "arctic";

            var source =
            {
                url: '/HanderAshx/UserManage/RoleRightHandler.ashx',
                dataType: "json",
                dataFields: [
                    { name: 'Name', type: 'string' },
                    { name: 'ID', type: 'int' },
                    { name: 'ParentID', type: 'int' },
                    { name: 'ckbList', type: 'string' }
                ],
                hierarchy:
                {
                    keyDataField: { name: 'ID' },
                    parentDataField: { name: 'ParentID' }
                },
                id: 'EmployeeID'
            };

            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            $("#treeGrid").jqxTreeGrid(
            {
                theme: theme,
                width: 750,
                height: 500,
                source: dataAdapter,
                sortable: true,
                ready: function () {
                    for (var i = 0; i < 150; i++) {
                        $("#treeGrid").jqxTreeGrid('expandRow', i);
                    }
                    SetCheckBox($("#hdRightList").val());
                },
                columns: [
                  { text: '栏目名称', dataField: 'Name', width: 200, cellsalign: 'left', align: 'center' },
                  { text: '栏目ID', dataField: 'ID', width: 50, cellsalign: 'center', align: 'center' },
                  { text: '权限组', dataField: 'ckbList', width: 480, cellsalign: 'left', align: 'left' }
                ]
            });

            $('#OperaterBtn').bind('click', function () {
                GetCheckBox();
            });
        });

    </script>

    <script type="text/javascript">
        function GetCheckBox() {
            var str = "";
            $("input[type=checkbox]:checked").each(function () {
                if (str == "") {
                    str += $(this).attr("name") + '|' + $(this).val();
                } else {
                    str += "," + $(this).attr("name") + '|' + $(this).val();
                }
            });
            $("#hdRightList").val(str);
        }

        function SetCheckBox(obj) {
            var array = obj.split(',');
            for (var i = 0; i < array.length; i++) {
                $("input:checkbox[id='ckb_" + array[i].split('|')[0] + "_" + array[i].split('|')[1] + "']").attr('checked', 'checked');
            }
        }
    </script>
    <link href="/css/global.css" rel="stylesheet" />
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <table cellpadding="0" cellspacing="0" class="editTable">
            <tr>
                <td style="text-align: right">角色名称：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:5px;">
                    <span class="fl">
                        <img src="/images/input_left.png" width="4" height="29" alt="" />
                    </span>
                    <input id="txtRoleName" type="text" runat="server" class="input_text" />
                    <span class="fl">
                        <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                    </span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top">角色描述：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <textarea id="txtRoleDescription" cols="50" rows="5" runat="server" style="font-size: 12px;"></textarea>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">审核状态：</td>
                <td style="text-align: left; padding-left: 5px; padding-top:5px;">
                    <div class="selectDiv">
                        <span class="fl">
                            <img src="/images/input_left.png" width="4" height="29" alt="" />
                        </span>
                        <select id="selStatus" name="selStatus" runat="server" class="fl">
                            <option value="0">复审中</option>
                            <option value="1">复审通过</option>
                            <option value="2">复审不通过</option>
                        </select>
                        <span class="fl" style="margin-left: -5px; cursor: pointer;">
                            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selStatus" />
                        </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">创建时间：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <asp:Literal ID="litCreateTime" runat="server"></asp:Literal>
                </td>
            </tr>

            <tr>
                <td style="text-align: right; vertical-align: top;">角色权限分配：</td>
                <td style="text-align: left; padding-left: 5px;">
                    <div id="treeGrid">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td style="text-align: left; padding-left: 5px;">
                    <input id="hdRightList" type="hidden" runat="server" />
                    <asp:Button ID="OperaterBtn" runat="server" Text="提交" OnClick="Operate_Click" CssClass="inputButton" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>



