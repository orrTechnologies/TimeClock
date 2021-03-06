﻿using System.Web;
using System.Web.Optimization;

namespace Timeclock.Api
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/momentjs").Include(
             "~/Scripts/moment.js",
             "~/Scripts/moment-timezone-with-data-2010-2020.js",
             "~/Scripts/moment-timezone-utils.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/angular-resource.js",
                "~/Scripts/ui-bootstrap-tpls-1.3.3.js",
                "~/Scripts/ng-table.js",
                "~/Scripts/angular-moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/timeclock").IncludeDirectory("~/app", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/sbadmin")
                .Include("~/Scripts/sb-admin-2.js",
                "~/Scripts/metisMenu.js",
                "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css",
                "~/Content/bootstrap.css",
                "~/Content/timeclock.css"));

            bundles.Add(new StyleBundle("~/Content/sbadmin").Include(
                "~/Content/sbadmin/sb-admin-2.css",
                "~/Content/sbadmin/metisMenu.css",
                "~/Content/sbadmin/ng-table.css",
                 "~/Content/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
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
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}