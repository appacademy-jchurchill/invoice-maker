using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceMaker.FormModels
{
    public class CreateWorkType
    {
        [Required]
        public string Name { get; set; }
        public decimal Rate { get; set; }
    }
}