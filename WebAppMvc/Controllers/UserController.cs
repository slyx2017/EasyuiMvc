using ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMvcHelper;

namespace WebAppMvc.Controllers
{
    public class UserController : BaseController
    {
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllUserInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            string searchName = Request["username"] == null ? "" : Request["username"];
            int total = 0;
            List<tbUser> users = OperateContext.BLLSession.ItbUserBLL.GetPagedList(pageIndex, pageSize, s => s.AccountName.Contains(searchName), s => s.ID);
            total = users.Count();
            var data = new
            {
                total = total,
                rows = users
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}