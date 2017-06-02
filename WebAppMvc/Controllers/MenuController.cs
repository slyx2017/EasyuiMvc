using ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMvcHelper;

namespace WebAppMvc.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InitMenu(string pid = "0")
        {
            try
            {
                int id = int.Parse(pid);
                List<tbMenu> temp = OperateContext.BLLSession.ItbMenuBLL.GetListBy(u => u.ParentId == id);
                //var temp = from u in db.tbMenu
                //           where u.ParentId == id
                //           select u;
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
        public ActionResult GetJson()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            string searchName = Request["MenuName"] == null ? "" : Request["MenuName"];
            int total = 0;
            List<tbMenu> temp = OperateContext.BLLSession.ItbMenuBLL.GetPagedList(pageIndex, pageSize, s => s.Name.Contains(searchName), s => s.ParentId);
            total = temp.Count();
            var menus = temp.OrderBy(s=>s.ParentId).ThenBy(s=>s.Sort);
            var data = new
            {
                total = total,
                rows = menus.ToList()
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // GET: Menu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Menu/Create
        public ActionResult MenuAdd()
        {
            return View();
        }

        // POST: Menu/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Menu/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Menu/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
