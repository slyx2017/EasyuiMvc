using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppMvc.Controllers
{
    public class BaseController : Controller
    {
        protected string hostUrl = "";
        /// <summary>
        /// Action执行前判断
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!this.checkLogin())// 判断是否登录
            {
                filterContext.Result = RedirectToRoute("Default", new { Controller = "Login", Action = "Login" });
            }

            base.OnActionExecuting(filterContext);

        }


        /// <summary>
        /// 判断是否登录
        /// </summary>
        protected bool checkLogin()
        {
            if (this.Session["ainfo"] == null || CookiesHelper.GetCookie("ainfo")==null)
            {
                return false;
            }
            return true;
        }
    }
}