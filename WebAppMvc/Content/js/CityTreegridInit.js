$(function () {
    IniDataGird();
});
//初始化表格
function IniDataGird() {
    $('#dg').treegrid({
        //title: '城市列表', //表格标题
        method: 'get',
        url: '../datagrid_data1.json', //请求数据的页面
        idField: 'code', //标识字段,主键
        treeField: 'name',
        height: "auto",
        fit: true,
        fitColumns: true,
        //lines: true,
        toolbar: '#tb',
        //queryParams: { CityName: $("#CityName").val() },
        loadMsg: '数据正在加载，请稍等..........',
        pagination: true,
        pageSize: 25,
        pageList: [10,15,20,25,30,35],
        columns: [[
            { title: 'code', field: 'code', width: 20, editor: 'text' },
            { title: '名称', field: 'name', width: 100, editor: 'text' },
            { title: 'pId', field: 'parentid', width: 100, editor: 'text' }
        ]],
        rownumbers: true, //行号，
        //onBeforeExpand: function (row, param) {
        //    if (row) {
        //        $(this).treegrid('options').url = '../Ashx/SysSetting/cityselect.ashx?action=pagelist&&flag=Child&&pId=' + row.code;
        //    }
        //    else {
        //        $(this).treegrid('options').url = '../Ashx/SysSetting/cityselect.ashx?action=pagelist&&flag=Root&&pId=0';
        //    }
        //},
        onBeforeLoad: function (row, param) {
            if (row) {
                $(this).treegrid('options').url = '../Ashx/SysSetting/cityselect.ashx?action=pagelist&&flag=Child&&pId=' + row.code;
            }
            else {
                $(this).treegrid('options').url = '../Ashx/SysSetting/cityselect.ashx?action=pagelist&&flag=Root&&pId=0';
            }
        }
    });
}
function doSearch() {
    IniTreetGird();
}
var editingId;
function edit() {
    if (editingId != undefined) {
        $('#tt').treegrid('select', editingId);
        return;
    }
    var row = $('#tt').treegrid('getSelected');
    if (row) {
        editingId = row.code
        $('#tt').treegrid('beginEdit', editingId);
    }
    else {
        $.messager.alert('提示', '请选择要修改的数据！', 'info');
        return;
    }
}
function save() {
    if (editingId != undefined) {
        var t = $('#tt');
        t.treegrid('endEdit', editingId);
        editingId = undefined;
        var persons = 0;
        var rows = t.treegrid('getChildren');
        for (var i = 0; i < rows.length; i++) {
            var p = parseInt(rows[i].persons);
            if (!isNaN(p)) {
                persons += p;
            }
        }
        var frow = t.treegrid('getFooterRows')[0];
        frow.persons = persons;
        t.treegrid('reloadFooter');
    }
}
function cancel() {
    if (editingId != undefined) {
        $('#tt').treegrid('cancelEdit', editingId);
        editingId = undefined;
    }
}