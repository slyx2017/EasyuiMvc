


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelEF;

namespace DALMSSQL
{
	public partial class tbIconsDAL : BaseDAL<ModelEF.tbIcons>,IDAL.ItbIconsDAL
    {
    }

	public partial class tbMenuDAL : BaseDAL<ModelEF.tbMenu>,IDAL.ItbMenuDAL
    {
    }

    public partial class tbUserDAL : BaseDAL<ModelEF.tbUser>, IDAL.ItbUserDAL
    { }
}