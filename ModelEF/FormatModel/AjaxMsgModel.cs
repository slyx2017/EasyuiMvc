using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.FormatModel
{
    /// <summary>
    /// 统一的Ajax格式类
    /// </summary>
    public class AjaxMsgModel
    {
        public string Msg { get; set; }
        public string Statu { get; set; }
        public string BackUrl { get; set; }
        public object Data { get; set; }
    }
}
