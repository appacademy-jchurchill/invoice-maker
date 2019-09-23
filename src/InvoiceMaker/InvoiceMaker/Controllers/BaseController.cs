using InvoiceMaker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker.Controllers
{
    public abstract class BaseController : Controller
    {
        protected Context Context;

        public BaseController()
        {
            Context = new Context();
        }

        private bool disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                Context.Dispose();
            }

            disposed = true;

            base.Dispose(disposing);
        }
    }
}