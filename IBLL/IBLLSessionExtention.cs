
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IBLL
{
    public partial interface IBLLSession
    {
	    ItbIconsBLL ItbIconsBLL{get;set;}
	    ItbMenuBLL ItbMenuBLL{get;set;}
	    ItbUserBLL ItbUserBLL{get;set;}
     }
}