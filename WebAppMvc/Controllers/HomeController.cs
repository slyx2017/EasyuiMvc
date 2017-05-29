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
        OADBEntities db = new OADBEntities();
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
                var temp = from u in db.Sys_Menu
                           where u.ParentID == id && u.IsInUse==1
                           select u;
                MenuModel menu = null;
                List<MenuModel> list = new List<MenuModel>();
                foreach (var item in temp)
                {
                    menu = new MenuModel();
                    menu.id = item.ID;
                    menu.text = item.MenuName;
                    menu.attributes = item.MenuUrl;
                    menu.iconCls= "icon-search";
                    menu.state= temp.Select(u => u.ParentID == item.ID).Count() > 0 ? "open" : "closed";
                    list.Add(menu);
                }
               
               
                return Json(list);
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
                //OADBEntities db = new OADBEntities();

                var rows= from s in db.Sys_Menu
                          where s.MenuName == menuName
                          select new { s.ID };
                int id = rows.ToList()[0].ID;
                var temp = from u in db.Sys_Menu
                           where u.ParentID == id && u.IsInUse == 1
                           select u;
                MenuModel menu = null;
                List<MenuModel> list = new List<MenuModel>();
                foreach (var item in temp)
                {
                    menu = new MenuModel();
                    menu.id = item.ID;
                    menu.text = item.MenuName;
                    menu.attributes = item.MenuUrl;
                    menu.iconCls = "tree-file";
                    menu.state = temp.Select(u => u.ParentID == item.ID).Count() > 0 ? "open" : "closed";
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
