﻿using System.Web;
using System.Web.Mvc;
using Blog.WEB.Filters;

namespace Blog.WEB
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}