using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMvc.Models;

namespace WebAppMvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            try
            {
                string filepath = Server.MapPath("~/App_Data/tree_data1.json");
                ViewBag.Tree = GetFile.GetFileJson(filepath);
                filepath = Server.MapPath("~/App_Data/datagrid_data1.json");
                ViewBag.Json = GetFile.GetFileJson(filepath);
                return View();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ActionResult InitMenu(string pid)
        {
            try
            {
                int id = int.Parse(pid);
                OADBEntities db = new OADBEntities();

                var temp = from u in db.Sys_Menu
                           where u.ParentID == id
                           select u;
                MenuModel menu = new MenuModel();
                List<MenuModel> list = new List<MenuModel>();

                menu.id = int.Parse(temp.Select(u => u.ID).ToString());
                menu.text = temp.Select(u => u.MenuName).ToString();
                menu.attributes = temp.Select(u => u.MenuUrl).ToString();
                menu.iconCls = "icon-ok";
                menu.state = temp.Select(u => u.ParentID == id).Count() > 0 ? "closed" : "open";
               
                list.Add(menu);
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
