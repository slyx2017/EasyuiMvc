/*************************************************************************
 *CLR版本：4.0.30319.42000
 *创建人:     zhiyong
 *创建时间:     2017-06-01 13:32:51
 *说明:        上海展通国际物流有限公司
 *版权所有:    Copyright (C) 2016-2017 
 **************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 业务层父类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseBLL<T> : IBLL.IBaseBLL<T> where T : class, new()
    {
        public BaseBLL()
        {
            SetDAL();
        }
        //1.数据层接口对象 等待被实例化
        protected IDAL.IBaseDAL<T> idal;

        /// <summary>
        /// 由子类实现，为业务父类 里的 数据接口对象 设置 值！
        /// </summary>
        public abstract void SetDAL();

        /// <summary>
        /// 2.数据仓储接口 （相当于数据层工厂，可以创建所有的数据子类对象）
        /// </summary>
        private IDAL.IDBSession iDbSession;

        #region 数据仓储 属性
        /// <summary>
        /// 数据仓储 属性
        /// </summary>
        public IDAL.IDBSession DBSession
        {
            get
            {
                if (iDbSession == null)
                {
                    //读取配置文件
                    //string strFactoryDLL = Common.ConfigurationHelper.AppSetting("DBSessionFactoryDLL");
                    //string strFactoryType = Common.ConfigurationHelper.AppSetting("DBSessionFactory");
                    //通过反射创建 DBSessionFactory 工厂对象
                    //Assembly dalDLL = Assembly.LoadFrom(strFactoryDLL);
                    //Type typeDBSessionFactory = dalDLL.GetType(strFactoryType);
                    //IDAL.IDBSessionFactory sessionFactory = Activator.CreateInstance(typeDBSessionFactory) as IDAL.IDBSessionFactory;
                    IDAL.IDBSessionFactory sessionFactory = DI.SpringHelper.GetObject<IDAL.IDBSessionFactory>("DBSessionFactory");
                    //通过工厂创建DBSession对象
                    iDbSession = sessionFactory.GetDBSession();
                }
                return iDbSession;
            }
        }
        #endregion

        public int DelList(List<T> model)
        {
            return idal.DelList(model);
        }
        public int Add(T model)
        {
            return idal.Add(model);
        }

        public int Del(T model)
        {
            return idal.Del(model);
        }

        public int DelBy(Expression<Func<T, bool>> delWhere)
        {
            return idal.DelBy(delWhere);
        }

        public int RemoveRange(IEnumerable<T> entities)
        {
            return idal.RemoveRange(entities);
        }

        public List<T> GetListBy(Expression<Func<T, bool>> whereLambda)
        {
            return idal.GetListBy(whereLambda);
        }

        public List<T> GetListBy<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda)
        {
            return idal.GetListBy(whereLambda, orderLambda);
        }

        public List<T> GetPagedList<TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderBy)
        {
            return idal.GetPagedList(pageIndex, pageSize, whereLambda, orderBy);
        }

        public int Modify(T model, params string[] proNames)
        {
            return idal.Modify(model, proNames);
        }

        public int ModifyBy(T model, Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames)
        {
            return idal.ModifyBy(model, whereLambda, modifiedProNames);
        }
    }
}
