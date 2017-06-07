
 
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
	public partial class tbIconsBLL : BaseBLL<ModelEF.tbIcons>,IBLL.ItbIconsBLL
    {
	    public override void SetDAL()
		{
			idal = DBSession.ItbIconsDAL;
		}
    }

	public partial class tbMenuBLL : BaseBLL<ModelEF.tbMenu>,IBLL.ItbMenuBLL
    {
	    public override void SetDAL()
		{
			idal = DBSession.ItbMenuDAL;
		}
    }

	public partial class tbUserBLL : BaseBLL<ModelEF.tbUser>,IBLL.ItbUserBLL
    {
	    public override void SetDAL()
		{
			idal = DBSession.ItbUserDAL;
		}
    }


}