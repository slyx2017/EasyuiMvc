$(function () {
    TabsMenuRight();
    createAccrodion();
});
/** 
 * 更换EasyUI主题的方法 
 * @param themeName 
 * 主题名称 
 */
changeTheme = function (themeName) {
    var $easyuiTheme = $('#easyuiTheme');
    var url = $easyuiTheme.attr('href');
    var href = url.substring(0, url.indexOf('themes')) + 'themes/' + themeName + '/easyui.css';
    $easyuiTheme.attr('href', href);
    var $iframe = $('iframe');
    if ($iframe.length > 0) {
        for (var i = 0; i < $iframe.length; i++) {
            var ifr = $iframe[i];
            $(ifr).contents().find('#easyuiTheme').attr('href', href);
        }
    }
    $.cookie('easyuiThemeName', themeName, {
        expires: 7
    });
};

function createFrame(url) {
    var s = '<iframe frameborder="0" scrolling="auto" src="' + url + '" style="width:100%;height:100%;"></iframe>';
    return s;
}
//在右边center区域打开菜单，新增tab  
function Open(text, url) {
    if ($("#tabs").tabs('exists', text)) {
        $('#tabs').tabs('select', text);
    } else {
        $('#tabs').tabs('add', {
            title: text,
            iconCls: "icon-file",
            closable: true,
            content: url
        });
    }
}
//创建Accrodion菜单
function createAccrodion() {

    $("#menu").accordion({ //初始化accordion
        fillSpace: true,
        fit: true,
        border: false,
        animate: false
    });
    //获取第一层初始目录 
    $.ajax({
        type: "post",
        url: "/Home/InitMenu",
        data: { "pid": "0" },
        success: function (data) {
            $.each(data, function (i, e) {
                //var id = e.id;
                if (i == 0) {
                    $('#menu').accordion('add', {
                        id: e.id,
                        title: e.text,
                        iconCls: e.iconCls,
                        selected: true,
                        content: '<ul name="' + e.text + '"></ul>'
                    });
                }
                else {
                    $('#menu').accordion('add', {
                        id: e.id,
                        title: e.text,
                        iconCls: e.iconCls,
                        selected: false,
                        content: '<ul name="' + e.text + '"></ul>'
                    });
                }
            });
        }
    });
    //异步加载子节点，即二级菜单  
    $('#menu').accordion({
        onSelect: function (title, index) {
            $("ul[name='" + title + "']").tree({
                url: '/Home/InitChildMenu',
                queryParams: {
                    menuName: title
                },
                animate: true,
                onClick: function (node) {// 在用户点击一个子节点即二级菜单时触发addTab()方法,用于添加tabs  
                    if (node.attributes) {//判断url是否存在，存在则创建tabs  
                        var tabTitle = node.text;
                        var url = node.attributes;
                        var icon = node.iconCls;
                        addTab(tabTitle, url, icon);
                    }
                }
            });
        }
    });
}
function addTab(subtitle, url, icon) {
    if (!$('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('add', {
            title: subtitle,
            content: createFrame(url),
            iconCls: icon,
            closable: true,
        });
        //$('#tabs div').removeClass("panel-body");
    } else {
        $('#tabs').tabs('select', subtitle);
    }
    tabClose();
}

function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#tabs').tabs('close', subtitle);
    })
}
//在右边center区域打开菜单
function moduleIndex(menusData) {
    var text = "";
    text += '<ul class="tree-node" style="text-align:left;padding-left:0px;list-style:none">';
    $.each(menusData, function (j, o) {
        text += '<li style="height:25px;padding:0px 0px;"><div style="height:25px"><a target="mainFrame" way="' + o.attributes.url + '"><div style="float:left;width:90%;padding:0px 0px;"><div class="" style="float:left;"></div><div>' + o.text + '</div></div></a></div></li> ';
    });
    text += '</ul>';
    return text;
}
//tabs的右键菜单
function TabsMenuRight() {
    //绑定tabs的右键菜单  
    $("#tabs").tabs({
        onContextMenu: function (e, title) {
            e.preventDefault();
            $("#tabsMenu").menu('show', {
                left: e.pageX - 2,
                top: e.pageY - 2
            }).data("tabTitle", title);
        }
    });
    //刷新
    $("#m-refresh").click(function () {
        var currTab = $('#tabs').tabs('getSelected');    //获取选中的标签项
        var url = $(currTab.panel('options').content).attr('src');    //获取该选项卡中内容标签（iframe）的 src 属性
        /* 重新设置该标签 */
        $('#tabs').tabs('update', {
            tab: currTab,
            options: {
                content: createFrame(url)
            }
        })
    });
    //关闭所有
    $("#m-closeall").click(function () {
        $(".tabs li").each(function (i, n) {
            var title = $(n).text();
            $('#tabs').tabs('close', title);
        });
    });

    //除当前之外关闭所有
    $("#m-closeother").click(function () {
        var currTab = $('#tabs').tabs('getSelected');
        currTitle = currTab.panel('options').title;

        $(".tabs li").each(function (i, n) {
            var title = $(n).text();

            if (currTitle != title) {
                $('#tabs').tabs('close', title);
            }
        });
    });

    //关闭当前
    $("#m-close").click(function () {
        var currTab = $('#tabs').tabs('getSelected');
        currTitle = currTab.panel('options').title;
        $('#tabs').tabs('close', currTitle);
    });
    //关闭当前右侧的TAB
    $('#m-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
            $.messager.alert('系统提示', '后边没有啦~~', 'warning');
            return false;
        }
        nextall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });
    //关闭当前左侧的TAB
    $('#m-tabcloseleft').click(function () {
        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
            $.messager.alert('系统提示', '到头了，前边没有啦~~', 'warning');
            return false;
        }
        prevall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });
    //退出
    $("#m-exit").click(function () {
        $('#tabsMenu').menu('hide');
    })
}
//dialog遮罩层全局
function maskInit() {
    var sWidth = document.documentElement.clientWidth;
    var sHeight = document.documentElement.clientHeight;
    //获取页面的可视区域高度和宽度  
    var wHeight = document.documentElement.clientHeight;
    var oMask = document.getElementById("mask");
    var oMaskIframe = document.getElementById("maskIframe");

    oMask.style.height = sHeight + "px";
    oMask.style.width = sWidth + "px";
    oMask.style.background = "#ccc";
    oMask.style.position = "fixed";
    oMask.style.zIndex = 5;
    oMask.style.opacity = "0.3";
    oMask.style.display = "none";
}
function globalShade() {
    //获取页面的高度和宽度  

    if (window.parent.document.getElementById('mask')) {

        window.parent.document.getElementById('mask').style.display = "block";

    }
    if (document.getElementById("maskIframe")) {
        document.getElementById("maskIframe").style.display = "block";


    }


};
function deleteGlobalShade() {
    if (window.parent.document.getElementById('mask')) {
        window.parent.document.getElementById('mask').style.display = "none";

    }
    if (document.getElementById("maskIframe")) {

        document.getElementById("maskIframe").style.display = "none";

    }

};

/*
 * 为‘文本框’列表添加‘清除’图标
 * 该实现使用了 onChange 事件，如果用户需要该事件，可传入自定义函数，会自动回调 。
 */
function addClear4TextBox(theId, onChangeFun) {
    var theObj = $(theId);

    //根据当前值，确定是否显示清除图标
    var showIcon = function () {
        var icon = theObj.textbox('getIcon', 0);
        if (theObj.textbox('getValue')) {
            icon.css('visibility', 'visible');
        } else {
            icon.css('visibility', 'hidden');
        }
    };

    theObj.textbox({
        //添加清除图标
        icons: [{
            iconCls: 'icon-clear',
            handler: function (e) {
                theObj.textbox('clear');
            }
        }],

        //值改变时，根据值，确定是否显示清除图标
        onChange: function () {
            if (onChangeFun) {
                onChangeFun();
            }
            showIcon();
        }

    });

    //根据目前值，确定是否显示清除图标
    showIcon();
}

/*
 * 为‘下拉列表框’添加‘清除’图标
 * 该实现使用了 onChange 事件，如果用户需要该事件，可传入自定义函数，会自动回调 。
 */
function addClear4Combobox(theId, onChangeFun) {
    var theObj = $(theId);

    //根据当前值，确定是否显示清除图标
    var showIcon = function () {
        var icon = theObj.combobox('getIcon', 0);
        if (theObj.combobox('getValue')) {
            icon.css('visibility', 'visible');
        } else {
            icon.css('visibility', 'hidden');
        }
    };

    theObj.combobox({
        //添加清除图标
        icons: [{
            iconCls: 'icon-clear',
            handler: function (e) {
                theObj.combobox('clear');
            }
        }],

        //值改变时，根据值，确定是否显示清除图标
        onChange: function () {
            if (onChangeFun) {
                onChangeFun();
            }
            showIcon();
        }

    });

    //初始化确认图标显示
    showIcon();
}


/*
 * 为‘数据表格下拉框’添加‘清除’图标
 * 该实现使用了 onChange 事件，如果用户需要该事件，可传入自定义函数，会自动回调 。
 */
function addClear4Combogrid(theId, onChangeFun) {
    var theObj = $(theId);

    //根据当前值，确定是否显示清除图标
    var showIcon = function () {
        var icon = theObj.combogrid('getIcon', 0);
        if (theObj.combogrid('getValue')) {
            icon.css('visibility', 'visible');
        } else {
            icon.css('visibility', 'hidden');
        }
    };

    theObj.combogrid({
        //添加清除图标
        icons: [{
            iconCls: 'icon-clear',
            handler: function (e) {
                theObj.combogrid('clear');
            }
        }],

        //值改变时，根据值，确定是否显示清除图标
        onChange: function () {
            if (onChangeFun) {
                onChangeFun();
            }
            showIcon();
        }

    });

    //初始化确认图标显示
    showIcon();
}

/*
 * 为‘数值输入框’添加‘清除’图标
 * 该实现使用了 onChange 事件，如果用户需要该事件，可传入自定义函数，会自动回调 。
 */
function addClear4Numberbox(theId, onChangeFun) {
    var theObj = $(theId);

    //根据当前值，确定是否显示清除图标
    var showIcon = function () {
        var icon = theObj.numberbox('getIcon', 0);
        if (theObj.numberbox('getValue')) {
            icon.css('visibility', 'visible');
        } else {
            icon.css('visibility', 'hidden');
        }
    };

    theObj.numberbox({
        //添加清除图标
        icons: [{
            iconCls: 'icon-clear',
            handler: function (e) {
                theObj.numberbox('clear');
            }
        }],

        //值改变时，根据值，确定是否显示清除图标
        onChange: function () {
            if (onChangeFun) {
                onChangeFun();
            }
            showIcon();
        }

    });

    //初始化确认图标显示
    showIcon();
}

/*
 * 为‘日期选择框’添加‘清除’图标
 * 该实现使用了 onChange 事件，如果用户需要该事件，可传入自定义函数，会自动回调 。
 */
function addClear4Datebox(theId, onChangeFun) {
    var theObj = $(theId);

    //根据当前值，确定是否显示清除图标
    var showIcon = function () {
        var icon = theObj.datebox('getIcon', 0);
        if (theObj.datebox('getValue')) {
            icon.css('visibility', 'visible');
        } else {
            icon.css('visibility', 'hidden');
        }
    };

    theObj.datebox({
        //添加清除图标
        icons: [{
            iconCls: 'icon-clear',
            handler: function (e) {
                theObj.datebox('clear');
            }
        }],

        //值改变时，根据值，确定是否显示清除图标
        onChange: function () {
            if (onChangeFun) {
                onChangeFun();
            }
            showIcon();
        }

    });

    //初始化确认图标显示
    showIcon();
}


/*
 * 为‘日期时间选择框’添加‘清除’图标
 * 该实现使用了 onChange 事件，如果用户需要该事件，可传入自定义函数，会自动回调 。
 */
function addClear4Datetimebox(theId, onChangeFun) {
    var theObj = $(theId);

    //根据当前值，确定是否显示清除图标
    var showIcon = function () {
        var icon = theObj.datetimebox('getIcon', 0);
        if (theObj.datetimebox('getValue')) {
            icon.css('visibility', 'visible');
        } else {
            icon.css('visibility', 'hidden');
        }
    };

    theObj.datetimebox({
        //添加清除图标
        icons: [{
            iconCls: 'icon-clear',
            handler: function (e) {
                theObj.datetimebox('clear');
            }
        }],

        //值改变时，根据值，确定是否显示清除图标
        onChange: function () {
            if (onChangeFun) {
                onChangeFun();
            }
            showIcon();
        }

    });

    //初始化确认图标显示
    showIcon();
}
//自动填加清除功能 （组件需要增加 addClear属性 ）
function autoAddClear() {
    var arr = $("input[addClear]");
    for (var i = 0; i < arr.length; i++) {
        var oneInput = $(arr[i]);
        var theId = oneInput.attr("id");
        theId = theId.replace('.', '\\.');
        var theClass = oneInput.attr("class");

        if (theClass.indexOf("easyui-textbox") != -1) {//文本框
            addClear4TextBox("#" + theId);
        }
        else if (theClass.indexOf("easyui-combobox") != -1) {//下拉列表框
            addClear4Combobox("#" + theId);
        }
        else if (theClass.indexOf("easyui-combogrid") != -1) {//数据表格下拉框
            addClear4Combogrid("#" + theId);
        }
        else if (theClass.indexOf("easyui-numberbox") != -1) {//数值输入框
            addClear4Numberbox("#" + theId);
        }
        else if (theClass.indexOf("easyui-datebox") != -1) {//日期选择框
            addClear4Datebox("#" + theId);
        }
        else if (theClass.indexOf("easyui-datetimebox") != -1) {//日期选择框
            addClear4Datetimebox("#" + theId);
        }
    }
}

//退出系统
function UserLoginOut() {
    $.messager.confirm("系统提示", "您确定要退出吗?", function (rt) {
        if (rt) {
            $.ajax({
                url: "/Login/UserLoginOut",
                type: "post",
                dataType: "json",
                success: function (result) {
                    if (result.Statu == "ok") {
                        window.location.href = result.BackUrl;
                    }
                    else {
                        $.messager.alert("系统提示", result.Msg);
                        return;
                    }
                }
            })
        }
        else {
            return;
        }
    })
}
//日期格式正确显示
function formatDatebox(value) {
    if (value == null || value == '') {
        return '';
    }
    var dt;
    if (value instanceof Date) {
        dt = value;
    }
    else {
        dt = new Date(value);
        if (isNaN(dt)) {
            value = eval(value.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"));//(/\/Date(−?\d+)\//, 'new Date($1)'); //标红的这段是关键代码，将那个长字符串的日期值转换成正常的JS日期格式  
            dt = new Date();
            dt.setTime(value);
        }
    }

    return dt.format("yyyy-MM-dd");   //这里用到一个javascript的Date类型的拓展方法，这个是自己添加的拓展方法，在后面的步骤3定义  
}
//扩展datagrid编辑日期格式正确显示
$.extend(
    $.fn.datagrid.defaults.editors, {
        datebox: {
            init: function (container, options) {
                var input = $('').appendTo(container);
                input.datebox(options);
                return input;
            },
            destroy: function (target) {
                $(target).datebox('destroy');
            },
            getValue: function (target) {
                return $(target).datebox('getValue');
            },
            setValue: function (target, value) {
                $(target).datebox('setValue', formatDatebox(value));
            },
            resize: function (target, width) {
                $(target).datebox('resize', width);
            }
        }
    });
//为原始Date类型拓展format一个方法，用于日期显示的格式化
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month   
        "d+": this.getDate(),    //day   
        "h+": this.getHours(),   //hour   
        "m+": this.getMinutes(), //minute   
        "s+": this.getSeconds(), //second   
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter   
        "S": this.getMilliseconds() //millisecond   
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
    (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
      RegExp.$1.length == 1 ? o[k] :
        ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}