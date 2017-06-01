using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using System.Runtime.Remoting.Messaging;

namespace DALMSSQL
{
    public class DBSessionFactory : IDBSessionFactory
    {
        /// <summary>
        /// 此方法的作用：提高效率，在线程中 共用一个 DBSession 对象！
        /// </summary>
        /// <returns></returns>
        public IDBSession GetDBSession()
        {
            IDBSession dbSession = CallContext.GetData(typeof(DBSessionFactory).Name) as DBSession;
            if (dbSession == null)
            {
                dbSession = new DBSession();
                CallContext.SetData(typeof(DBSessionFactory).Name, dbSession);
            }
            return dbSession;
        }
    }
}
