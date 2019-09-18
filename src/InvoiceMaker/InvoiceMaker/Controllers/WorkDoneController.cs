using InvoiceMaker.Models;
using InvoiceMaker.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker.Controllers
{
    public class WorkDoneController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var repository = new WorkDoneRepository();
            IList<WorkDone> workDone = repository.GetWorkDone();
            return View("Index", workDone);
        }
    }
}