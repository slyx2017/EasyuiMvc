using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF
{
    public partial class tbUser
    {
        /// <summary>
        /// 生成实体对象
        /// </summary>
        /// <returns></returns>
        public tbUser ToPOCO()
        {
            tbUser poco = new tbUser()
            {
                ID = this.ID,
                AccountName = this.AccountName,
                Password = this.Password,
                RealName = this.RealName,
                MobilePhone = this.MobilePhone,
                Description = this.Description,
                Email = this.Email,
                IfChangePwd = this.IfChangePwd,
                IsAble = this.IsAble,
                UpdateBy = this.UpdateBy,
                UpdateTime = this.UpdateTime,
                CreateBy = this.CreateBy,
                CreateTime = this.CreateTime
            };
            return poco;
        }
    }
}
