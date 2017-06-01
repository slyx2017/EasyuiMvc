using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    /// <summary>
    /// EF数据上下文 工厂
    /// </summary>
    public interface IDBContextFactory
    {
        DbContext GetDbContext();
    }
}
