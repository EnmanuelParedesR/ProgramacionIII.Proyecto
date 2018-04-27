﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Collections.ObjectModel;
using System.Web.Optimization;

namespace MiPrimerProyectoMVC.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            //Create bundel for jQueryUI   
            //js   
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
    "~/Scripts/jquery-ui-{version}.js"));
            //css   
            bundles.Add(new StyleBundle("~/Content/cssjqryUi").Include(
            "~/Content/jquery-ui.css"));

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