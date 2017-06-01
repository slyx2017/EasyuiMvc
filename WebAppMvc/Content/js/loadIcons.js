$(function () {
    $("#ui_Icons_dg").datagrid({       //初始化datagrid
        url: "/Icons/GetAllIconsInfo",
        striped: true, rownumbers: true, pagination: true, pageSize: 20, singleSelect: true, multiSort: false, fitColumns: true,
        pageList: [20, 40, 60, 80, 100],
        frozenColumns: [[
                        //{ field: 'ck', checkbox: true },
                        { field: 'Id', title: '图标ID', width: 60 },
                        { field: 'IconName', title: '图标名称', width: 200 },
                        { field: 'IconCssInfo', title: '图标信息', width: 200 }]],
        columns: [[
                   { field: 'CreateBy', title: '创建人', width: 80 },
                   { field: 'CreateTime', title: '创建时间', width: 120 },
                   { field: 'UpdateBy', title: '最后更新人', width: 130 },
                   { field: 'UpdateTime', title: '最后更新时间', width: 150 }
        ]]
    });

    //回车搜索
    $("#ui_Icons_search").find('input').on('keyup', function (event) {
        if (event.keyCode == '13') {
            searchdata();
        }
    })
});

//新增
function AddIcons() {
    $("<div/>").dialog({
        id: "ui_Icons_add_dialog",
        href: "/Icons/IconsAdd",
        title: "添加图标",
        height: 200,
        width: 500,
        modal: true,
        buttons: [{
            id: "ui_Icons_add_btn",
            text: '添 加',
            handler: function () {
                $("#IconsAddForm").form("submit", {
                    url: "/Icons/AddIcons",
                    onSubmit: function (param) {
                        param.IconName = $("#txtIconName").val();
                        param.IconCssInfo = $("#comboxIconTree").combotree("getValues").toString(); //$('#txtIconCssInfo').val();
                        if ($(this).form('validate')) {
                            $('#ui_Icons_edit_btn').linkbutton('disable');
                            return true;
                        }
                        else {
                            $('#ui_Icons_add_btn').linkbutton('enable');
                            return false;
                        }
                    },
                    success: function (data) {
                        var dataJson = eval('(' + data + ')');
                        if (dataJson.success) {
                            $("#ui_Icons_add_dialog").dialog('destroy');
                            $.show_alert("提示", dataJson.msg);
                            $("#ui_Icons_dg").datagrid("reload");
                        } else {
                            $('#ui_Icons_add_btn').linkbutton('enable');
                            $.show_alert("提示", dataJson.msg);
                        }
                    }
                });
            }
        }, {
            text: '取 消',
            handler: function () {
                $("#ui_Icons_add_dialog").dialog('destroy');
            }
        }],
        onLoad: function () {
            $("#txtTypeName").focus();
        },
        onClose: function () {
            $("#ui_Icons_add_dialog").dialog('destroy');  //销毁dialog对象
        }
    });
}

//修改
function EditIcons() {
    var row = $("#ui_Icons_dg").datagrid("getChecked");
    if (row.length < 1) {
        $.show_alert("提示", "请先选择要修改的图标");
        return;
    }
    $("<div/>").dialog({
        id: "ui_Icons_edit_dialog",
        href: "/Icons/IconsEdit",
        title: "修改图标",
        height: 200,
        width: 500,
        modal: true,
        buttons: [{
            id: "ui_Icons_edit_btn",
            text: '修 改',
            handler: function () {
                $("#IconsEditForm").form("submit", {
                    url: "/Icons/EditIcons",
                    onSubmit: function (param) {
                        param.id = $("#hidid").val();
                        param.originalName = $("#hidoriginalName").val();
                        param.IconName = $("#txtIconName").val();
                        param.IconCssInfo = $("#comboxIconTree").combotree("getValues").toString(); //$('#txtIconCssInfo').val();
                        if ($(this).form('validate')) {
                            $('#ui_Icons_add_btn').linkbutton('disable');
                            return true;
                        }
                        else {
                            $('#ui_Icons_edit_btn').linkbutton('enable');
                            return false;
                        }
                    },
                    success: function (data) {
                        var dataJson = eval('(' + data + ')');
                        if (dataJson.success) {
                            $("#ui_Icons_edit_dialog").dialog('destroy');
                            $.show_alert("提示", dataJson.msg);
                            $("#ui_Icons_dg").datagrid("reload");
                        } else {
                            $('#ui_Icons_edit_btn').linkbutton('enable');
                            $.show_alert("提示", dataJson.msg);
                        }
                    }
                });
            }
        }, {
            text: '取 消',
            handler: function () {
                $("#ui_Icons_edit_dialog").dialog('destroy');
            }
        }],
        onLoad: function () {
            $("#hidid").val(row[0].Id);
            $("#hidoriginalName").val(row[0].IconName);
            $("#txtIconName").val(row[0].IconName);
            //$("#txtIconCssInfo").val(row[0].IconCssInfo);
            $("#comboxIconTree").combotree("setValue", row[0].IconCssInfo);
        },
        onClose: function () {
            $("#ui_Icons_edit_dialog").dialog('destroy');
        }
    });
}

//删除
function DelIcons() {
    var rows = $("#ui_Icons_dg").datagrid("getChecked");
    if (rows.length < 1) {
        $.show_alert("提示", "请先勾选要删除的图标");
        return;
    }
    $.messager.confirm('提示', '确定删除选中行吗？', function (r) {
        if (r) {
            var IconsIDs = "";
            $.each(rows, function (i, row) {
                IconsIDs += row.Id + ",";
            });
            IconsIDs = IconsIDs.substring(0, IconsIDs.length - 1);
            $.ajax({
                url: "/Icons/DelIconsByIDs",
                data: {
                    IDs: IconsIDs
                },
                type: "POST",
                dataType: "json",
                success: function (data) {
                    if (data.success) {
                        $.show_alert("提示", data.msg);
                        $("#ui_Icons_dg").datagrid("reload").datagrid('clearSelections').datagrid('clearChecked');
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
    $("#ui_Icons_dg").datagrid('load', {
        IconName: $("#txtSearchIconName").val()
    });
    $("#ui_Icons_dg").datagrid('clearSelections').datagrid('clearChecked');
}