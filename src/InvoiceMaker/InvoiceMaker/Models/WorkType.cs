using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class WorkType
    {
        public WorkType(string name, decimal rate)
        {
            Name = name;
            Rate = rate;
        }

        public string Name { get; private set; }
        public decimal Rate { get; private set; }
    }
}