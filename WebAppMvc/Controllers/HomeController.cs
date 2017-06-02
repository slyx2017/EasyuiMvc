using Common;
using ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMvc.Models;
using WebAppMvcHelper;

namespace WebAppMvc.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            tbUser usr = Session["ainfo"] as tbUser;
            ViewBag.AccountName = usr.AccountName;
            return View();
        }
        public ActionResult InitMenu(string pid="0")
        {
            try
            {
                
                int id = int.Parse(pid);
                List<tbMenu> temp = OperateContext.BLLSession.ItbMenuBLL.GetListBy(u => u.ParentId == id);
                MenuModel menu = null;
                List<MenuModel> list = new List<MenuModel>();
                foreach (var item in temp)
                {
                    menu = new MenuModel();
                    menu.id = item.Id;
                    menu.text = item.Name;
                    menu.attributes = item.LinkAddress;
                    menu.iconCls= item.Icon;
                    menu.state= temp.Select(u => u.ParentId == item.Id).Count() > 0 ? "open" : "closed";
                    list.Add(menu);
                }
               
               
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult InitChildMenu(string menuName)
        {
            try
            {
                List<tbMenu> list_tb = OperateContext.BLLSession.ItbMenuBLL.GetListBy(u => u.Name == menuName);
                int id = list_tb[0].Id;
                List<tbMenu> temp = OperateContext.BLLSession.ItbMenuBLL.GetListBy(u => u.ParentId == id,u=>u.Sort);
                //temp = temp.OrderBy(s => s.Sort);
                MenuModel menu = null;
                List<MenuModel> list = new List<MenuModel>();
                foreach (var item in temp)
                {
                    menu = new MenuModel();
                    menu.id = item.Id;
                    menu.text = item.Name;
                    menu.attributes = item.LinkAddress;
                    menu.iconCls = item.Icon;
                    menu.state = temp.Select(u => u.ParentId == item.Id).Count() > 0 ? "open" : "closed";
                    list.Add(menu);
                }
                return Json(list);
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
    }

    public class MenuModel
    {
        public int id { get; set; }
        public string text { get; set; }
        public string attributes { get; set; }
        public string iconCls { get; set; }
        public string state { get; set; }
    }
}
