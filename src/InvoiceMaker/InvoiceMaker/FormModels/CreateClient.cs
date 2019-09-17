using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceMaker.FormModels
{
    public class CreateClient
    {
        [Required]
        public string Name { get; set; }
        public bool IsActivated { get; set; }
    }
}