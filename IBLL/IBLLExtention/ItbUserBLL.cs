using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public partial interface ItbUserBLL
    {
        /// <summary>
        /// 登录验证 Login(string strName,string strPwd)
        /// </summary>
        /// <param name="strName">用户名</param>
        /// <param name="strPwd">密码</param>
        /// <returns></returns>
        ModelEF.tbUser Login(string strName,string strPwd);
    }
}
