
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IDAL
{
    public partial interface IDBSession
    {
	    ItbIconsDAL ItbIconsDAL{get;set;}
	    ItbMenuDAL ItbMenuDAL{get;set;}
	    ItbUserDAL ItbUserDAL{get;set;}
     }
}