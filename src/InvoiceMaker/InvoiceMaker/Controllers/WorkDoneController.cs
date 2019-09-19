using InvoiceMaker.FormModels;
using InvoiceMaker.Models;
using InvoiceMaker.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker.Controllers
{
    public class WorkDoneController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var repository = new WorkDoneRepository(Context);
            IList<WorkDone> workDone = repository.GetWorkDone();
            return View("Index", workDone);
        }

        [HttpGet]
        public ActionResult Create()
        {
            CreateWorkDoneView viewModel = new CreateWorkDoneView();
            viewModel.Clients = new ClientRepository(Context).GetClients();
            viewModel.WorkTypes = new WorkTypeRepository(Context).GetWorkTypes();
            return View("Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateWorkDone formModel)
        {
            try
            {
                //// Get the client and work type based on values submitted from
                //// the form
                //Client client = new ClientRepository(Context).GetClient(formModel.ClientId);
                //WorkType workType = new WorkTypeRepository(Context).GetWorkType(formModel.WorkTypeId);

                // Create an instance of the work done with the client and work
                // type
                WorkDone workDone = new WorkDone(0, formModel.ClientId, formModel.WorkTypeId);
                //WorkDone workDone = new WorkDone(0, client, workType);
                new WorkDoneRepository(Context).Insert(workDone);
                return RedirectToAction("Index");
            }
            catch { }

            // Create a view model
            CreateWorkDoneView viewModel = new CreateWorkDoneView();

            // Copy over the values from the values submitted
            viewModel.ClientId = formModel.ClientId;
            viewModel.WorkTypeId = formModel.WorkTypeId;

            // Go get the value for the drop-downs, again.
            viewModel.Clients = new ClientRepository(Context).GetClients();
            viewModel.WorkTypes = new WorkTypeRepository(Context).GetWorkTypes();

            return View("Create", viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var repository = new WorkDoneRepository(Context);
            WorkDone workDone = repository.GetWorkDone(id);

            var viewModel = new EditWorkDoneView();
            viewModel.Id = workDone.Id;
            viewModel.ClientId = workDone.ClientId;
            viewModel.WorkTypeId = workDone.WorkTypeId;
            viewModel.StartedOn = workDone.StartedOn;

            viewModel.Clients = new ClientRepository(Context).GetClients();
            viewModel.WorkTypes = new WorkTypeRepository(Context).GetWorkTypes();

            return View("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditWorkDone formModel)
        {
            try
            {
                //// Get the client and work type based on values submitted from
                //// the form
                //Client client = new ClientRepository(Context).GetClient(formModel.ClientId);
                //WorkType workType = new WorkTypeRepository(Context).GetWorkType(formModel.WorkTypeId);

                // Create an instance of the work done with the client and work
                // type
                WorkDone workDone = new WorkDone(id, formModel.ClientId, formModel.WorkTypeId, formModel.StartedOn);
                new WorkDoneRepository(Context).Update(workDone);
                return RedirectToAction("Index");
            }
            catch { }

            // Create a view model
            EditWorkDoneView viewModel = new EditWorkDoneView();

            // Copy over the values from the values submitted
            viewModel.ClientId = formModel.ClientId;
            viewModel.WorkTypeId = formModel.WorkTypeId;
            viewModel.StartedOn = formModel.StartedOn;

            // Go get the value for the drop-downs, again.
            viewModel.Clients = new ClientRepository(Context).GetClients();
            viewModel.WorkTypes = new WorkTypeRepository(Context).GetWorkTypes();

            return View("Edit", viewModel);
        }
    }
}