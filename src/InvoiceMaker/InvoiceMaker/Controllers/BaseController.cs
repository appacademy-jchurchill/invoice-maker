using InvoiceMaker.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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

        /// <summary>
        /// Behold! My proudest moment as a developer.
        /// </summary>
        /// <param name="ex"></param>
        protected void HandleDbUpdateException(DbUpdateException ex)
        {
            if (ex.InnerException?.InnerException != null)
            {
                SqlException sqlException =
                    ex.InnerException.InnerException as SqlException;
                if (sqlException != null && sqlException.Number == 2627)
                {
                    ModelState.AddModelError("Name", "That name is already taken.");
                }
            }
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