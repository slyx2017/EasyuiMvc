﻿$(function () {
    $("#ui_menu_dg").datagrid({
        url: "/Menu/GetJson",
        striped: true, rownumbers: true, pagination: true, pageSize: 20, singleSelect: true, multiSort: false,
        pageList: [20, 40, 60, 80, 100],
        fitColumns: true,
        frozenColumns: [[
                        //{ field: 'ck', checkbox: true },
                        { field: 'Id', title: '菜单ID', width: 60 },
                        { field: 'Name', title: '菜单名称', width: 150 }]],
        columns: [[
                   { field: 'ParentId', title: '父节点ID', width: 80 },
                   { field: 'Code', title: '标识码', width: 120 },
                   { field: 'LinkAddress', title: '链接地址', width: 200 },
                   { field: 'Icon', title: '图标', width: 150 },
                   { field: 'Sort', title: '排序', width: 80 },
                   { field: 'UpdateTime', title: '最后更新时间', width: 130 },
                   { field: 'UpdateBy', title: '最后更新人', width: 100 }
        ]]
    });


    //回车搜索
    $("#ui_menu_search").find('input').on('keyup', function (event) {
        if (event.keyCode == '13') {
            searchdata();
        }
    })
});

//新增
function AddMenu() {
    $("<div/>").dialog({
        id: "ui_menu_add_dialog",
        href: "/Menu/MenuAdd",
        title: "添加菜单",
        height: 300,
        width: 400,
        modal: true,
        buttons: [{
            id: "ui_menu_add_btn",
            text: '添 加',
            handler: function () {
                $("#MenuAddForm").form("submit", {
                    url: "/Menu/AddMenu",
                    onSubmit: function (param) {
                        var parentid = $("#treeMenuParentId").combotree("getValues").toString();
                        param.MenuName = $("#txtMenuName").val()
                        param.MenuCode = $("#txtMenuCode").val();
                        param.MenuIcon = $("#comboxMenuIconTree").combotree("getValues").toString(); //$("#txtMenuIcon").val();
                        param.MenuSort = $('#txtMenuSort').val();
                        param.MenuLinkAddress = $("#txtMenuLinkAddress").val();
                        param.MenuParentId = parentid

                        if ($(this).form('validate')) {
                            //if (parentid != "0" && parentid != "") {
                            //    if ($.trim($("#txtMenuCode").val()) == "") {
                            //        $.show_alert("提示", "选择了父节点则必须填写标识码");
                            //        return false;
                            //    }
                            //    if ($.trim($("#txtMenuLinkAddress").val()) == "") {
                            //        $.show_alert("提示", "选择了父节点则必须填写链接地址");
                            //        return false;
                            //    }
                            //}
                            $('#ui_menu_edit_btn').linkbutton('disable');
                            return true;
                        }
                        else {
                            $('#ui_menu_add_btn').linkbutton('enable');
                            return false;
                        }
                    },
                    success: function (data) {
                        var dataJson = eval('(' + data + ')');
                        if (dataJson.success) {
                            $("#ui_menu_add_dialog").dialog('destroy');
                            $.show_alert("提示", dataJson.msg);
                            $("#ui_menu_dg").datagrid("reload");
                        } else {
                            $('#ui_menu_add_btn').linkbutton('enable');
                            $.show_alert("提示", dataJson.msg);
                        }
                    }
                });
            }
        }, {
            text: '取 消',
            handler: function () {
                $("#ui_menu_add_dialog").dialog('destroy');
            }
        }],
        onLoad: function () {
            $("#txtMenuName").focus();
        },
        onClose: function () {
            $("#ui_menu_add_dialog").dialog('destroy');  //销毁dialog对象
        }
    });
}

//修改
function EditMenu() {
    var row = $("#ui_menu_dg").datagrid("getChecked");
    if (row.length < 1) {
        $.show_alert("提示", "请先选择要修改的菜单");
        return;
    }
    $("<div/>").dialog({
        id: "ui_menu_edit_dialog",
        href: "/Menu/MenuEdit",
        title: "修改菜单",
        height: 300,
        width: 400,
        modal: true,
        buttons: [{
            id: "ui_menu_edit_btn",
            text: '修 改',
            handler: function () {
                $("#MenuEditForm").form("submit", {
                    url: "/Menu/EditMenu",
                    onSubmit: function (param) {
                        var parentid = $("#treeMenuParentId").combotree("getValues").toString();
                        param.id = $("#hidid").val();
                        param.originalName = $("#hidoriginalName").val();
                        param.MenuName = $("#txtMenuName").val()
                        param.MenuCode = $("#txtMenuCode").val();
                        param.MenuIcon = $("#comboxMenuIconTree").combotree("getValues").toString(); //$("#txtMenuIcon").val();
                        param.MenuSort = $('#txtMenuSort').val();
                        param.MenuLinkAddress = $("#txtMenuLinkAddress").val();
                        param.MenuParentId = parentid
                        if ($(this).form('validate')) {
                            $('#ui_menu_add_btn').linkbutton('disable');
                            return true;
                        }
                        else {
                            $('#ui_menu_edit_btn').linkbutton('enable');
                            return false;
                        }
                    },
                    success: function (data) {
                        var dataJson = eval('(' + data + ')');
                        if (dataJson.success) {
                            $("#ui_menu_edit_dialog").dialog('destroy');
                            $.show_alert("提示", dataJson.msg);
                            $("#ui_menu_dg").datagrid("reload");
                        } else {
                            $('#ui_menu_edit_btn').linkbutton('enable');
                            $.show_alert("提示", dataJson.msg);
                        }
                    }
                });
            }
        }, {
            text: '取 消',
            handler: function () {
                $("#ui_menu_edit_dialog").dialog('destroy');
            }
        }],
        onLoad: function () {
            $("#hidid").val(row[0].Id);
            $("#hidoriginalName").val(row[0].Name);
            $("#txtMenuName").val(row[0].Name);
            $("#txtMenuCode").val(row[0].Code);
            $('#comboxMenuIconTree').combotree('setValue', row[0].Icon);
            //$("#txtMenuIcon").val(row[0].Icon);
            $('#txtMenuSort').numberspinner('setValue', row[0].Sort);
            $("#txtMenuLinkAddress").val(row[0].LinkAddress);
            if (row[0].ParentId != "0") {
                $("#treeMenuParentId").combotree("setValue", row[0].ParentId);
            }
        },
        onClose: function () {
            $("#ui_menu_edit_dialog").dialog('destroy');
        }
    });
}

//删除
function DelMenu() {
    var rows = $("#ui_menu_dg").datagrid("getChecked");
    if (rows.length < 1) {
        $.show_alert("提示", "请先勾选要删除的菜单");
        return;
    }
    $.messager.confirm('提示', '确定删除选中行吗？', function (r) {
        if (r) {
            var MenuIDs = "";
            $.each(rows, function (i, row) {
                MenuIDs += row.Id + ",";
            });
            MenuIDs = MenuIDs.substring(0, MenuIDs.length - 1);
            $.ajax({
                url: "/Menu/DelMenuByIDs",
                data: {
                    IDs: MenuIDs
                },
                type: "POST",
                dataType: "json",
                success: function (data) {
                    if (data.success) {
                        $.show_alert("提示", data.msg);
                        $("#ui_menu_dg").datagrid("reload").datagrid('clearSelections').datagrid('clearChecked');
                    } else {
                        $.show_alert("提示", data.msg);
                    }
                }
            });
        }
    });
}
//搜索
function searchdata() {
    $("#ui_menu_dg").datagrid('load', {
        MenuName: $("#txtSearchMenuName").val()
    });
    $("#ui_menu_dg").datagrid('clearSelections').datagrid('clearChecked');
}