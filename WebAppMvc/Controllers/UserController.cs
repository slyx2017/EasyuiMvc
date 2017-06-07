using Common;
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
        /// <summary>
        /// 用户列表展示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllUserInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : int.Parse(Request["rows"]);
            string searchName = Request["username"] == null ? "" : Request["username"];
            int total = 0;
            List<tbUser> users = OperateContext.BLLSession.ItbUserBLL.GetPagedList(pageIndex, pageSize, s => s.AccountName.Contains(searchName) && s.IsAble==true, s => s.ID);
            total = users.Count();
            var data = new
            {
                total = total,
                rows = users
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增页面展示
        /// </summary>
        /// <returns></returns>
        public ActionResult UserAdd()
        {
            return View();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUser()
        {
            ModelEF.FormatModel.AjaxMsgModel ajaxM = new ModelEF.FormatModel.AjaxMsgModel() { Statu = "err", Msg = "添加失败！" };
            tbUser uInfo = Session["ainfo"] as tbUser;

            string userid = Request["UserID"];
            string username = Request["UserName"];
            bool isable = bool.Parse(Request["Isable"]);
            bool ifchangepwd = bool.Parse(Request["IfChangepwd"]);
            string description = Request["Description"];

            tbUser userAdd = new tbUser();
            userAdd.AccountName = userid.Trim();
            userAdd.RealName = username.Trim();
            userAdd.Password = CommonHelper.Md5Hash("q123456");   //md5加密
            userAdd.IsAble = isable;
            userAdd.IfChangePwd = ifchangepwd;
            userAdd.Description = description.Trim();
            userAdd.MobilePhone = Request["MobilePhone"];
            userAdd.Email = Request["Email"];
            userAdd.CreateTime = DateTime.Now;
            userAdd.CreateBy = uInfo.AccountName;
            userAdd.UpdateTime = DateTime.Now;
            userAdd.UpdateBy = uInfo.AccountName;
            int userId = OperateContext.BLLSession.ItbUserBLL.Add(userAdd);
            if (userId > 0)
            {
                ajaxM.Statu = "ok";
                ajaxM.Msg = "添加成功！";
                return Json(ajaxM);
            }
            else
            {
                return Json(ajaxM);
            }
        }
        /// <summary>
        /// 修改页面展示
        /// </summary>
        /// <returns></returns>
        public ActionResult UserEdit()
        {
            return View();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public ActionResult EditUser()
        {
            tbUser uInfo = Session["ainfo"] as tbUser;
            ModelEF.FormatModel.AjaxMsgModel ajaxM = new ModelEF.FormatModel.AjaxMsgModel() { Statu = "err", Msg = "修改失败！" };
            int id = Convert.ToInt32(Request["id"]);
            string originalName = Request["originalName"];
            string userid = Request["UserID"];
            string username = Request["UserName"];
            bool isable = bool.Parse(Request["Isable"]);
            bool ifchangepwd = bool.Parse(Request["IfChangepwd"]);
            string description = Request["Description"];

            tbUser userEdit = OperateContext.BLLSession.ItbUserBLL.GetListBy(u => u.ID == id).Select(u => u.ToPOCO()).First();
            userEdit.ID = id;
            userEdit.AccountName = userid.Trim();
            userEdit.RealName = username.Trim();
            userEdit.IsAble = isable;
            userEdit.IfChangePwd = ifchangepwd;
            userEdit.Description = description.Trim();
            userEdit.MobilePhone = Request["MobilePhone"];
            userEdit.Email = Request["Email"];
            userEdit.UpdateTime = DateTime.Now;
            userEdit.UpdateBy = uInfo.AccountName;
            string proName = "AccountName,RealName,IsAble,IfChangePwd,Description,MobilePhone,Email,UpdateBy,UpdateTime";
            string[] proNameArr = proName.Split(',');
            int userId = OperateContext.BLLSession.ItbUserBLL.ModifyBy(userEdit,u=>u.ID==id, proNameArr);
            if (userId > 0)
            {
                ajaxM.Statu = "ok";
                ajaxM.Msg = "修改成功！";
                return Json(ajaxM);
            }
            else
            {
                return Json(ajaxM);
            }
        }
        //伪删除
        public ActionResult DelUserList()//DelUserByIDs()
        {
            ModelEF.FormatModel.AjaxMsgModel ajaxM = new ModelEF.FormatModel.AjaxMsgModel() { Statu = "err", Msg = "删除失败！" };
            try
            {
                tbUser uInfo = Session["ainfo"] as tbUser;
                var userId = uInfo.ID;
                string Ids = Request["IDs"] == null ? "" : Request["IDs"];
                var uIds = Ids.Split(',').ToList();
                if (uIds.Contains(userId.ToString()))
                {
                    ajaxM.Msg = "含有正在使用的用户，禁止删除";
                    return Json(ajaxM);
                }
                tbUser delInfo = new tbUser();
                if (!string.IsNullOrEmpty(Ids))
                {
                    int flag = 0;
                   foreach (var id in uIds)
                    {
                        int ID = int.Parse(id);
                        delInfo.ID = ID;
                        delInfo.IsAble = false;
                        flag = OperateContext.BLLSession.ItbUserBLL.ModifyBy(delInfo,u=> u.ID== ID, "IsAble");
                    }
                    if (flag>0)
                    {
                        ajaxM.Statu = "ok";
                        ajaxM.Msg = "删除成功！";
                        return Json(ajaxM);
                    }
                    else
                    {
                        return Json(ajaxM);
                    }
                }
                else
                {
                    return Json(ajaxM);
                }
            }
            catch (Exception ex)
            {
                ajaxM.Msg = ajaxM.Msg + "," + ex.Message;
                return Json(ajaxM);
            }
        }
        //真删除
        public ActionResult DelUserByIDs()//DelUserList()
        {
            ModelEF.FormatModel.AjaxMsgModel ajaxM = new ModelEF.FormatModel.AjaxMsgModel() { Statu = "err", Msg = "删除失败！" };
            try
            {
                tbUser uInfo = Session["ainfo"] as tbUser;
                var userId = uInfo.ID;
                string Ids = Request["IDs"] == null ? "" : Request["IDs"];
                var uIds = Ids.Split(',').ToList();
                if (uIds.Contains(userId.ToString()))
                {
                    ajaxM.Msg = "含有正在使用的用户，禁止删除";
                    return Json(ajaxM);
                }

                if (!string.IsNullOrEmpty(Ids))
                {
                    int flag = 0;
                    List<tbUser> uModels = new List<tbUser>();
                    foreach (var id in uIds)
                    {
                        int ID = int.Parse(id);
                        tbUser model = OperateContext.BLLSession.ItbUserBLL.GetListBy(u=>u.ID==ID).FirstOrDefault();
                        uModels.Add(model);
                    }
                    flag = OperateContext.BLLSession.ItbUserBLL.RemoveRange(uModels);
                    if (flag > 0)
                    {
                        ajaxM.Statu = "ok";
                        ajaxM.Msg = "删除成功！";
                        return Json(ajaxM);
                    }
                    else
                    {
                        return Json(ajaxM);
                    }
                }
                else
                {
                    return Json(ajaxM);
                }
            }
            catch (Exception ex)
            {
                ajaxM.Msg = ajaxM.Msg + "," + ex.Message;
                return Json(ajaxM);
            }
        }
    }
}