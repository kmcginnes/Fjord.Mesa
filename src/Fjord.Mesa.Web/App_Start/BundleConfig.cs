using System;
using System.Web.Optimization;
using BundleTransformer.Core.Transformers;

namespace Fjord.Mesa
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.AddScriptBundle("~/bundles/scripts/common")
                .Include(
                    "~/Scripts/jquery-1*",
                    "~/Scripts/jquery.pjax.js",
                    "~/Scripts/jquery.unobtrusive*",
                    "~/Scripts/jquery.validate*");

            bundles.AddScriptBundle("~/bundles/scripts/jqueryui")
                .Include("~/Scripts/jquery-ui*");
            
            bundles.AddScriptBundle("~/bundles/scripts/modernizr")
                .Include("~/Scripts/modernizr-*");

            bundles.AddScriptBundle("~/bundles/scripts/application")
                .IncludeDirectory("~/Scripts/Application", "*.js");

            bundles.AddStyleBundle("~/bundles/styles/common")
                .Include("~/Content/site.css")
                .Include("~/Content/less/bootstrap.less");

            bundles.AddStyleBundle("~/bundles/styles/jqueryui")
                .Include(
                    "~/Content/themes/base/jquery.ui.core.css",
                    "~/Content/themes/base/jquery.ui.resizable.css",
                    "~/Content/themes/base/jquery.ui.selectable.css",
                    "~/Content/themes/base/jquery.ui.accordion.css",
                    "~/Content/themes/base/jquery.ui.autocomplete.css",
                    "~/Content/themes/base/jquery.ui.button.css",
                    "~/Content/themes/base/jquery.ui.dialog.css",
                    "~/Content/themes/base/jquery.ui.slider.css",
                    "~/Content/themes/base/jquery.ui.tabs.css",
                    "~/Content/themes/base/jquery.ui.datepicker.css",
                    "~/Content/themes/base/jquery.ui.progressbar.css",
                    "~/Content/themes/base/jquery.ui.theme.css");
        }
    }

    public static class BundleExtensions
    {
        private static readonly Lazy<JsTransformer> JsTransformer =
            new Lazy<JsTransformer>(() => new JsTransformer());

        private static readonly Lazy<CssTransformer> CssTransformer =
            new Lazy<CssTransformer>(() => new CssTransformer());

        public static Bundle AddBundle(this BundleCollection bundles, string virtualPath)
        {
            if(virtualPath.IsNullOrWhitespace())
            {
                throw new ArgumentNullException(virtualPath);
            }

            var bundle = new Bundle(virtualPath);
            bundles.Add(bundle);
            return bundle;
        }

        public static Bundle AddScriptBundle(this BundleCollection bundles, string virtualPath)
        {
            if (virtualPath.IsNullOrWhitespace())
            {
                throw new ArgumentNullException(virtualPath);
            }

            var bundle = new Bundle(virtualPath)
                .AddTransform(JsTransformer.Value);
            bundles.Add(bundle);
            return bundle;
        }

        public static Bundle AddStyleBundle(this BundleCollection bundles, string virtualPath)
        {
            if (virtualPath.IsNullOrWhitespace())
            {
                throw new ArgumentNullException(virtualPath);
            }

            var bundle = new Bundle(virtualPath)
                .AddTransform(CssTransformer.Value);
            bundles.Add(bundle);
            return bundle;
        }

        public static Bundle AddTransform(this Bundle bundle, params IBundleTransform[] transforms)
        {
            if(transforms == null)
            {
                throw new ArgumentNullException("transforms");
            }

            foreach (var transform in transforms)
            {
                bundle.Transforms.Add(transform);
            }
            return bundle;
        }

        public static void AddTo(this Bundle bundle, BundleCollection bundleCollection)
        {
            if(bundleCollection == null)
            {
                throw new ArgumentNullException("bundleCollection");
            }

            bundleCollection.Add(bundle);
        }
    }
}