using System.Web;
using System.Web.Optimization;

namespace WebApplication
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //TEMPLATE MONSTER
            //style css
            bundles.Add(new StyleBundle("~/bundles/style").Include(
                        "~/dist/css/style.min.css"));
            //jquery
            bundles.Add(new ScriptBundle("~/bundles/alljquery").Include(
                        "~/assets/libs/jquery/dist/jquery.min.js",
                        "~/assets/extra-libs/taskboard/js/jquery.ui.touch-punch-improved.js",
                        "~/assets/extra-libs/taskboard/js/jquery-ui.min.js"));
            //bootstrap
            bundles.Add(new ScriptBundle("~/bundles/corebootstrap").Include(
                        "~/assets/libs/popper.js/dist/umd/popper.min.js",
                        "~/assets/libs/bootstrap/dist/js/bootstrap.min.js"));
            //apps
            bundles.Add(new ScriptBundle("~/bundles/apps").Include(
                       "~/dist/js/app.min.js",
                       "~/dist/js/app.init.horizontal.js",
                       "~/dist/js/app-style-switcher.horizontal.js",
                       "~/dist/js/feather.min.js"));
            //demas js para el layout
            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                       "~/assets/libs/perfect-scrollbar/dist/perfect-scrollbar.jquery.min.js",
                       "~/assets/libs/jquery-sparkline/jquery.sparkline.min.js",
                       "~/dist/js/waves.js",
                       "~/dist/js/sidebarmenu.js",
                       "~/dist/js/custom.min.js"));
            //datatable
            bundles.Add(new StyleBundle("~/bundles/datatablecss").Include(
                        "~/assets/libs/datatables.net-bs4/css/dataTables.bootstrap4.css"));            
            bundles.Add(new ScriptBundle("~/bundles/datatablejs").Include(
                        "~/assets/libs/datatables/media/js/jquery.dataTables.min.js",
                        "~/dist/js/pages/datatable/custom-datatable.js",
                        "~/dist/js/pages/datatable/datatable-basic.init.js"));
            //datepicker bootstrap
            bundles.Add(new StyleBundle("~/bundles/datepickerBootstrapcss").Include(
                        "~/assets/libs/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/datepickerBootstrapjs").Include(
                        "~/assets/libs/moment/moment.js",
                        "~/assets/libs/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"));
            //datepicker material
            bundles.Add(new StyleBundle("~/bundles/datepickerMaterialcss").Include(
                        "~/assets/libs/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css"));
            bundles.Add(new ScriptBundle("~/bundles/datepickerMaterialjs").Include(
                        "~/assets/libs/moment/moment.js",
                        "~/assets/libs/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker-custom.js"));
        }
    }
}
