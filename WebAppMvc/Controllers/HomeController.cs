using Common;
using ModelEF;
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
        AchieveDBEntities db = new AchieveDBEntities();
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
        public ActionResult InitMenu(string pid="0")
        {
            try
            {
                int id = int.Parse(pid);
                var temp = from u in db.tbMenu
                           where u.ParentId == id 
                           select u;
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

                var rows= from s in db.tbMenu
                          where s.Name == menuName
                          select new { s.Id };
                int id = rows.ToList()[0].Id;
                var temp = from u in db.tbMenu
                           where u.ParentId == id
                           select u;
                temp = temp.OrderBy(s => s.Sort);
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
