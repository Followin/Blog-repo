﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WEB.Controllers
{
    public class FeedbackController : Controller
    {
        //
        // GET: /Feedback/

        public ActionResult Index()
        {
            return View();
        }

    }
}