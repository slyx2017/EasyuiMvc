
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
namespace DALMSSQL
{
    public partial class DBSession:IDAL.IDBSession
    {

        #region 01. 数据接口 ItbIconsDAL
	    ItbIconsDAL itbIconsDAL;
		public ItbIconsDAL ItbIconsDAL
		{
		    get
			{
			    if(itbIconsDAL==null)
				   itbIconsDAL=new tbIconsDAL();
				return itbIconsDAL;
			}
		    set 
			{
			    itbIconsDAL=value;
			}
		}
		#endregion

        #region 02. 数据接口 ItbMenuDAL
	    ItbMenuDAL itbMenuDAL;
		public ItbMenuDAL ItbMenuDAL
		{
		    get
			{
			    if(itbMenuDAL==null)
				   itbMenuDAL=new tbMenuDAL();
				return itbMenuDAL;
			}
		    set 
			{
			    itbMenuDAL=value;
			}
		}
		#endregion

        #region 03. 数据接口 ItbUserDAL
	    ItbUserDAL itbUserDAL;
		public ItbUserDAL ItbUserDAL
		{
		    get
			{
			    if(itbUserDAL==null)
				   itbUserDAL=new tbUserDAL();
				return itbUserDAL;
			}
		    set 
			{
			    itbUserDAL=value;
			}
		}
		#endregion
     }
}