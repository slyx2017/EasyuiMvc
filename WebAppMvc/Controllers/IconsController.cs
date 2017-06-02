using ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppMvc.Controllers
{
    public class IconsController : BaseController
    {
        AchieveDBEntities db = new AchieveDBEntities();
        // GET: Icons
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllIconsInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            string searchName = Request["IconName"] == null ? "" : Request["IconName"];
            int total = 0;
            var temp = from u in db.tbIcons select u;
            //根据查询条件检索
            if (!string.IsNullOrEmpty(searchName))
            {
                //根据姓名模糊查询
                temp = temp.Where(s => s.IconName.Contains(searchName));
            }

            total = temp.Count();
            var icons = temp.OrderByDescending(s => s.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var data = new
            {
                total = total,
                rows = icons.ToList()
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取按钮树
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllIconsTree()
        {
            try
            {
                var iconsObj = from u in db.tbIcons select u;
                IconModel icon = null;
                List<IconModel> list = new List<IconModel>();
                foreach (var item in iconsObj)
                {
                    icon = new IconModel();
                    icon.id = item.Id;
                    icon.text = item.IconName;
                    icon.iconCls = item.IconCssInfo;
                    list.Add(icon);
                }
                
                return Json(list);
            }
            catch (Exception ex)
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
    }
    public class IconModel
    {
        public int id { get; set; }
        public string text { get; set; }
        public string attributes { get; set; }
        public string iconCls { get; set; }
        public string state { get; set; }
    }
}