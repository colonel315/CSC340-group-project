using System.Web;
using System.Web.Optimization;

namespace CSC340_ordering_sytem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Assets/favicon.ico",
                      "~/Content/bootstrap.css",
                      "~/Content/stylesheets/owl.carousel.css",
                      "~/Content/stylesheets/owl.theme.css",
                      "~/Content/stylesheets/animate.css",
                      "~/Content/stylesheets/jquery.datetimepicker.css",
                      "~/Content/stylesheets/prettyPhoto.css",
                      "~/Content/stylesheets/font-awesome.min.css",
                      "~/Content/fonts/fonts.css",
                      "~/Content/stylesheets/main.css",
                      "~/Content/stylesheets/main-responsive.css",
                      "~/Content/stylesheets/themes/pizza.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Scripts").Include(
                      "~/Scripts/html5shiv.js",
                      "~/Scripts/script.js",
                      "~/Scripts/plugins.js",
                      "~/Scripts/main.js",
                      "~/Scripts/mbBgGallery_init.js"
            ));
        }
    }
}