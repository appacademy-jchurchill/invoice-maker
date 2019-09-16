using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker.Controllers
{
    public class InvoicesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}