using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IDAL;
using ModelEF;

namespace DALMSSQL
{
    public partial class tbUserDAL : IDAL.ItbUserDAL
    {
       
        public ModelEF.tbUser Login(string loginName)
        {
          return GetListBy(u => u.AccountName == loginName).FirstOrDefault();
        }
    }
}
