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
    public class ClientsController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var repository = new ClientRepository(Context);
            IList<Client> clients = repository.GetClients();
            return View("Index", clients);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var formModel = new CreateClient();
            formModel.IsActivated = true;
            return View("Create", formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateClient formModel)
        {
            var repository = new ClientRepository(Context);

            try
            {
                var client = new Client(0, formModel.Name, formModel.IsActivated);
                repository.Insert(client);
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
            var repository = new ClientRepository(Context);
            Client client = repository.GetClient(id);

            var formModel = new EditClient();
            formModel.Id = client.Id;
            formModel.IsActivated = client.IsActive;
            formModel.Name = client.Name;

            return View("Edit", formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditClient formModel)
        {
            var repository = new ClientRepository(Context);

            try
            {
                var client = new Client(id, formModel.Name, formModel.IsActivated);
                repository.Update(client);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                HandleDbUpdateException(ex);
            }

            return View("Edit", formModel);
        }

        /// <summary>
        /// Behold! My proudest moment as a developer.
        /// </summary>
        /// <param name="ex"></param>
        private void HandleDbUpdateException(DbUpdateException ex)
        {
            if (ex.InnerException != null && ex.InnerException.InnerException != null)
            {
                SqlException sqlException =
                    ex.InnerException.InnerException as SqlException;
                if (sqlException != null && sqlException.Number == 2627)
                {
                    ModelState.AddModelError("Name", "That name is already taken.");
                }
            }
        }
    }
}