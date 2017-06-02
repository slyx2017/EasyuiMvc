﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class tbUser :IBLL.ItbUserBLL
    {
        /// <summary>
        /// 登录验证 Login(string strName,string strPwd)
        /// </summary>
        /// <param name="strName">用户名</param>
        /// <param name="strPwd">密码</param>
        /// <returns></returns>
        public ModelEF.tbUser Login(string strName,string strPwd)
        {
            ModelEF.tbUser usr = base.GetListBy(u => u.AccountName == strName).Select(u=>u.ToPOCO()).First();
            if (usr != null && usr.Password == Common.CommonHelper.Md5Hash(strPwd))
            {
                return usr;
            }
            return null;
        }
    }
}