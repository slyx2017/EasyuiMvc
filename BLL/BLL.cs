
 
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
	public partial class tbIcons : BaseBLL<ModelEF.tbIcons>,IBLL.ItbIconsBLL
    {
	    public override void SetDAL()
		{
			idal = DBSession.ItbIconsDAL;
		}
    }

	public partial class tbMenu : BaseBLL<ModelEF.tbMenu>,IBLL.ItbMenuBLL
    {
	    public override void SetDAL()
		{
			idal = DBSession.ItbMenuDAL;
		}
    }

	public partial class tbUser : BaseBLL<ModelEF.tbUser>,IBLL.ItbUserBLL
    {
	    public override void SetDAL()
		{
			idal = DBSession.ItbUserDAL;
		}
    }


}