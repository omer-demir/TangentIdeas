﻿using System.Web.Mvc;

namespace TangentIdeas.Web.Blog {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}