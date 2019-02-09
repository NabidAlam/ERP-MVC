using System.Web;
using System.Web.Optimization;

namespace ERP
{
    public class BundleConfig
    {
     
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/validation").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/snowtex").Include(
                        "~/Scripts/st-*"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/nano-scroller.css",
                      "~/Content/menu-light.css",
                      "~/Content/style.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/responsive.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/anytime.5.2.0.min.css",
                      "~/Content/custom.css"));
            
            BundleTable.EnableOptimizations = false;
        }
    }
}
