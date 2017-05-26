/*************************************************************************
 *CLR版本：4.0.30319.42000
 *创建人:     zhiyong
 *创建时间:     2017-05-18 17:25:18
 *说明:        上海展通国际物流有限公司
 *版权所有:    Copyright (C) 2017-2017 
 **************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class GetFile
    {
        public static string GetFileJson(string filepath)
        {
            string json = string.Empty;
            using (FileStream fs = new FileStream(filepath, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312")))
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            return json;
        }
    }
}
