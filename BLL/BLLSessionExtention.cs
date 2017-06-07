
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBLL;
namespace BLL
{
    public partial class BLLSession:IBLL.IBLLSession
    {

        #region 01. 业务接口 ItbIconsBLL
	    ItbIconsBLL itbIconsBLL;
		public ItbIconsBLL ItbIconsBLL
		{
		    get
			{
			    if(itbIconsBLL==null)
				   itbIconsBLL=new tbIconsBLL();
				return itbIconsBLL;
			}
		    set 
			{
			    itbIconsBLL=value;
			}
		}
		#endregion

        #region 02. 业务接口 ItbMenuBLL
	    ItbMenuBLL itbMenuBLL;
		public ItbMenuBLL ItbMenuBLL
		{
		    get
			{
			    if(itbMenuBLL==null)
				   itbMenuBLL=new tbMenuBLL();
				return itbMenuBLL;
			}
		    set 
			{
			    itbMenuBLL=value;
			}
		}
		#endregion

        #region 03. 业务接口 ItbUserBLL
	    ItbUserBLL itbUserBLL;
		public ItbUserBLL ItbUserBLL
		{
		    get
			{
			    if(itbUserBLL==null)
				   itbUserBLL=new tbUserBLL();
				return itbUserBLL;
			}
		    set 
			{
			    itbUserBLL=value;
			}
		}
		#endregion
     }
}