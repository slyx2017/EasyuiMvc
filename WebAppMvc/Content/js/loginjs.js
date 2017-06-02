//异步实现用户的登录
function LoginUserInfo() {
    //验证用户必须输入必填项
    var validate = $("#ff").form("validate");
    if (validate == false) {
        return false;
    }
    //ajax提交
    $.ajax({
        url: "/Login/Login",
        type: "POST",
        dataType: "json",
        data: { "UserName": $("#UserName").val(), "Password": $("#Password").val(), "isAllway": $("#isAllway").val() },
        success: function (data) {
            if (data.Statu == "ok") {
                window.location.href = data.BackUrl;
            }
            else {
                $.messager.alert("系统提示", "登录失败，请重新登录！");
                return;
            }
        }
        ,
        beforeSend: function (data) {
            progressBar("");
        },
        complete: function (data) {
            progressBar("close");
        }
    });
}
function progressBar(obj) {
    if (obj=="") {
        $.messager.progress({
            //title: '登录',
            //msg: '正在登录...',
            text: '正在登录...'
        });
    }
    else {
        $.messager.progress('close');
    }
}