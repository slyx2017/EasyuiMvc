using ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMvcHelper;

namespace WebAppMvc.Controllers
{
    public class LoginController : Controller
    {
        //AchieveDBEntities db = new AchieveDBEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        // GET: Login
        public ActionResult Login()
        {
            return View("Login");
        }
        #region 1.0 管理员登录页面 +ActionResult Login()
        /// <summary>
        /// 1.0 管理员登录页面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            ModelEF.FormatModel.AjaxMsgModel ajaxM = new ModelEF.FormatModel.AjaxMsgModel() { Statu = "err", Msg = "失登录败！" };

            //1.1 获取数据
            string strName = Request.Params["Name"];
            string strPwd = Request.Params["Password"];
            //1.2 验证

            // 1.3 通过操作上下文获取 用户业务接口对象 ，调用里面的登录方法!
            //Ou_UserInfo usr = OperateContext.BLLSession.IOu_UserInfoBLL.Login(strName, strPwd);
            tbUser usr = OperateContext.BLLSession.ItbUserBLL.Login(strName, strPwd);
            if (usr != null)
            {
                //2.1 保存 用户数据（session or cookie）
                Session["ainfo"] = usr;

                //如果选择了复选框 则要使用cookie 保存数据
                if (!string.IsNullOrEmpty(Request.Params["isAllway"]))
                {
                    //2.1.2 将用户id加密成字符串
                    string strCookieValue = Common.SecurityHelper.EncryptUserInfo(usr.ID.ToString());
                    //2.1.3 创建cookie
                    HttpCookie cookie = new HttpCookie("ainfo", strCookieValue);
                    cookie.Expires = DateTime.Now.AddDays(1);
                    cookie.Path = "/admin";
                    Response.Cookies.Add(cookie);
                }
                //2.2 查询当前用户的权限 ， 并将权限存入Session 中
                //List<MODEL.Ou_Permission> listPers = OperateContext.GetUserPermission(usr.uId);
                //Session["uPermission"] = listPers;

                ajaxM.Statu = "ok";
                ajaxM.Msg = "登录成功！";
                ajaxM.BackUrl = "/Home/Index";
            }
            return Json(ajaxM);
        }
        #endregion
    }
}