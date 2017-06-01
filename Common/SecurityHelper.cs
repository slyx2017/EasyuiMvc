using System;
using System.Web.Security;

namespace Common
{
    public class SecurityHelper
    {
        #region 1.0 使用票据对象将用户数据加密成字符串 +string EncryptUserInfo(string userInfo)
        /// <summary>
        /// 1.0 使用票据对象将用户数据加密成字符串
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static string EncryptUserInfo(string userInfo)
        {
            //1.1 将用户数据 存入 票据对象
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "哈哈", DateTime.Now, DateTime.Now, true, userInfo);
            //1.2 将票据对象 加密成字符串
            string strData = FormsAuthentication.Encrypt(ticket);
            return strData;
        }
        #endregion

        #region 2.0 加密成字符串 解密 +string DecryptUserInfo(string cryptograph)
        /// <summary>
        /// 2.0 加密成字符串 解密
        /// </summary>
        /// <param name="cryptograph">解密字符串</param>
        /// <returns></returns>
        public static string DecryptUserInfo(string cryptograph)
        {
            //1.1 将加密字符串解密成 票据对象
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cryptograph);
            //1.2 将票据里的 用户数据 返回
            return ticket.UserData;
        }
        #endregion
    }
}
