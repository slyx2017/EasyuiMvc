/*************************************************************************
 *CLR版本：4.0.30319.42000
 *创建人:     zhiyong
 *创建时间:     2017-06-01 11:28:31
 *说明:        上海展通国际物流有限公司
 *版权所有:    Copyright (C) 2016-2017 
 **************************************************************************/
using ModelEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppMvcHelper
{
    /// <summary>
    /// 网站操作上下文
    /// </summary>
    public class OperateContext
    {
        public static IBLL.IBLLSession BLLSession = DI.SpringHelper.GetObject<IBLL.IBLLSession>("BLLSession");
        #region 1.0 根据用户id 查询权限
        /*public static List<tbUser> GetUserPermission(int usrId)
        {
            //-------A 根据用户角色查询
            //1.0 根据 用户id 查询 该用户的 角色ID
            List<int> listRoleId = BLLSession.IOu_UserRoleBLL.GetListBy(ur => ur.urUId == usrId).Select(ur => ur.urRId).ToList();
            //2.0 根据 角色ID 查询角色权限ID
            List<int> listPerIds = BLLSession.IOu_RolePermissionBLL.GetListBy(rp => listRoleId.Contains(rp.rpRId)).Select(rp => rp.rpPId).ToList();
            //3.0 查询所有角色数据
            List<MODEL.Ou_Permission> listPermission = BLLSession.IOu_PermissionBLL.GetListBy(p => listPerIds.Contains(p.pid)).Select(p => p.ToPOCO()).ToList();
            //-------B根据特权表查询
            //b.1 查询 用户特权
            List<int> VipIds = BLLSession.IOu_UserVipPermissionBLL.GetListBy(vip => vip.vipUserId == usrId).Select(vip => vip.vipPermission).ToList();
            //b.2 查询 特权数据
            List<MODEL.Ou_Permission> listPermission2 = BLLSession.IOu_PermissionBLL.GetListBy(p => VipIds.Contains(p.pid)).Select(p => p.ToPOCO()).ToList();
            //-------C将两个权限表合并
            listPermission2.ForEach(p =>
            {
                listPermission.Add(p);
            });
            return listPermission;
        }*/
        #endregion
    }
}
