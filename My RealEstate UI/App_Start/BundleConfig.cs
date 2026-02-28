using System.Web.Optimization;

namespace My_RealEstate_UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // ── Scripts ──────────────────────────────────────────
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                "~/Scripts/site.js"));

            // ── Styles ───────────────────────────────────────────
            bundles.Add(new StyleBundle("~/Content/RealEstate").Include(
                "~/Content/site.css"));

#if !DEBUG
            // Enable minification & bundling in release mode
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
