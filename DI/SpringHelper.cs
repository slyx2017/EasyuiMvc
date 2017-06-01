/*************************************************************************
 *CLR版本：4.0.30319.42000
 *创建人:     zhiyong
 *创建时间:     2017-06-01 11:46:26
 *说明:        上海展通国际物流有限公司
 *版权所有:    Copyright (C) 2016-2017 
 **************************************************************************/
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public static class SpringHelper
    {
        private static IApplicationContext SpringContext
        {
            get { return ContextRegistry.GetContext(); }
        }
        /// <summary>
        /// 使用 Spring创建对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objName"></param>
        /// <returns></returns>
        public static T GetObject<T>(string objName) where T : class
        {
            return (T)SpringContext.GetObject(objName);
        }
    }
}
