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
                      "~/Content/bootstrap.css",
                      "~/Content/Restraunt/owl.carousel.css",
                      "~/Content/Restraunt/owl.theme.css",
                      "~/Content/Restraunt/animate.css",
                      "~/Content/Restraunt/jquery.datetimepicker.css",
                      "~/Content/Restraunt/prettyPhoto.css",
                      "~/Content/Restraunt/font-awesome.min.css",
                      "~/Content/Restraunt/fonts/fonts.css",
                      "~/Content/Restraunt/main.css",
                      "~/Content/Restraunt/main-responsive.css",
                      "~/Content/Restraunt/pizza.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/fonts").Include(
                      "~/Content/Restraunt/fonts/font_icons/fontawesome-webfont.eot",
                      "~/Content/Restraunt/fonts/font_icons/fontawesome-webfont.svg",
                      "~/Content/Restraunt/fonts/font_icons/fontawesome-webfont.ttf",
                      "~/Content/Restraunt/fonts/font_icons/fontawesome-webfont..woff",
                      "~/Content/Restraunt/fonts/font_icons/fontawesome-webfont.woff2",
                      "~/Content/Restraunt/fonts/font_icons/FontAwesome.otf",
                      "~/Content/Restraunt/fonts/font_icons/icomoon.eot",
                      "~/Content/Restraunt/fonts/font_icons/icomoon.svg",
                      "~/Content/Restraunt/fonts/font_icons/icomoon.ttf",
                      "~/Content/Restraunt/fonts/font_icons/icomoon.woff"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Scripts").Include(
                      "~/Scripts/html5shiv.js",
                      "~/Scripts/plugins.js",
                      "~/Scripts/main.js",
                      "~/Scripts/mbBgGallery_init.js"
            ));
        }
    }
}
