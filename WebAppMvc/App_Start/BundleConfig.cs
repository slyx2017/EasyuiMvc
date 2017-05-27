using System.Web;
using System.Web.Optimization;

namespace WebAppMvc
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/easyuijs").Include(
                      "~/Content/js/jquery.min.js",
                      "~/Content/js/jquery.easyui.min.js",
                      "~/Content/js/easyui-lang-zh_CN.js",
                      "~/Content/js/commjs.js"));
            bundles.Add(new StyleBundle("~/Content/themes").Include(
                      "~/Content/themes/default/easyui.css",
                      "~/Content/themes/icon.css"));
        }
    }
}
