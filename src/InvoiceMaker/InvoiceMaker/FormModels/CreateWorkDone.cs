using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker.FormModels
{
    public class CreateWorkDone
    {
        public int ClientId { get; set; }
        public int WorkTypeId { get; set; }
    }

    public class CreateWorkDoneView : CreateWorkDone
    {
        public IList<Client> Clients { get; set; }
        public IList<WorkType> WorkTypes { get; set; }

        public SelectList ClientSelectList
        {
            get
            {
                return new SelectList(Clients, "Id", "Name");
            }
        }

        public SelectList WorkTypeSelectList
        {
            get
            {
                return new SelectList(WorkTypes, "Id", "Name");
            }
        }
    }
}