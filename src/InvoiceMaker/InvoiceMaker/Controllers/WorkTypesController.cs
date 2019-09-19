using InvoiceMaker.FormModels;
using InvoiceMaker.Models;
using InvoiceMaker.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker.Controllers
{
    public class WorkTypesController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var repository = new WorkTypeRepository(Context);
            IList<WorkType> workTypes = repository.GetWorkTypes();
            return View("Index", workTypes);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var formModel = new CreateWorkType();
            return View("Create", formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateWorkType formModel)
        {
            var repository = new WorkTypeRepository(Context);

            try
            {
                var workType = new WorkType(0, formModel.Name, formModel.Rate);
                repository.Insert(workType);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                HandleDbUpdateException(ex);
            }

            return View("Create", formModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var repository = new WorkTypeRepository(Context);
            WorkType workType = repository.GetWorkType(id);

            var formModel = new EditWorkType();
            formModel.Id = workType.Id;
            formModel.Rate = workType.Rate;
            formModel.Name = workType.Name;

            return View("Edit", formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditWorkType formModel)
        {
            var repository = new WorkTypeRepository(Context);

            try
            {
                var workType = new WorkType(id, formModel.Name, formModel.Rate);
                repository.Update(workType);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                HandleDbUpdateException(ex);
            }

            return View("Edit", formModel);
        }
    }
}