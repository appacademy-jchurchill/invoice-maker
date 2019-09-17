using InvoiceMaker.FormModels;
using InvoiceMaker.Models;
using InvoiceMaker.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker.Controllers
{
    public class ClientsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var repository = new ClientRepository();
            IList<Client> clients = repository.GetClients();
            return View("Index", clients);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var client = new CreateClient();
            client.IsActivated = true;
            return View("Create", client);
        }

        [HttpPost]
        public ActionResult Create(CreateClient client)
        {
            var repository = new ClientRepository();

            try
            {
                Client newClient = new Client(0, client.Name, client.IsActivated);
                repository.Insert(newClient);
                return RedirectToAction("Index");
            }
            catch (SqlException se)
            {
                if (se.Number == 2627)
                {
                    ModelState.AddModelError("Name", "That name is already taken.");
                }
            }

            return View("Create", client);
        }
    }
}