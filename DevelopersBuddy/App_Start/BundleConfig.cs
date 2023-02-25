using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace DevelopersBuddy
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap")
                .Include("~/Scripts/jquery-3.6.3.js", "~/Scripts/umd/popper.js", "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/Styles/bootstrap").Include("~/Content/bootstrap.css"));
            bundles.Add(new ScriptBundle("~/Styles/site").Include("~/Content/Style.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}