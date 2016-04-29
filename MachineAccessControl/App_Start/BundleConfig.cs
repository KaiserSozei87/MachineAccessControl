using System.Web;
using System.Web.Optimization;

namespace MachineAccessControl
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                     "~/Scripts/jquery-ui-{version}.min.js"
                     //,
                     //"~/Scripts/jquery-ui*"
                     ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/progresstimer").Include(
                      "~/Scripts/jquery.progresstimer.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/pwstrength").Include(
                     "~/Scripts/pwstrength-bootstrap-1.2.10.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapConfirmation").Include(
                     "~/Scripts/bootstrap-confirmation.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootbox").Include(
                    "~/Scripts/bootbox.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-Flatly.css",
                      "~/Content/site.css",
                      "~/Content/simple-sidebar.css"
                      ).Include("~/Content/font-awesome-4.6.1/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Content/css/font-awesome").Include(
                 "~/Content/font-awesome-4.6.1/css/font-awesome.min.css"
                ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
