using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using HashSharpCore.Models;

namespace HashSharpCore
{
    public static class Initializer
    {
        private static Assembly proAssembly;
        public static Assembly ProjectAssembly
        {
            get { return proAssembly; }
        }

        private static SiteSettings siteSettings;
        public static SiteSettings SiteSettings
        {
            get { return siteSettings; }
            set { siteSettings = value; }
        }
        public static void Init(Assembly projectAssembly)
        {
            proAssembly = projectAssembly;
        }
    }
}
