using ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppMvcHelper;

namespace WebAppMvc.Controllers
{
    public class IconsController : BaseController
    {
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
            List<tbIcons> temp = OperateContext.BLLSession.ItbIconsBLL.GetPagedList(pageIndex,pageSize,s => s.IconName.Contains(searchName),s=>s.Id);
            total = temp.Count();
            var data = new
            {
                total = total,
                rows = temp//icons.ToList()
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
                List<tbIcons> iconsObj = OperateContext.BLLSession.ItbIconsBLL.GetListBy(u => u.Id != -1);
                //var iconsObj = from u in db.tbIcons select u;
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
                
                return Json(list, JsonRequestBehavior.AllowGet);
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