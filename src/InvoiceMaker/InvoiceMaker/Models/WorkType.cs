using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class WorkType
    {
        public WorkType() { }

        public WorkType(int id, string name, decimal rate)
        {
            Id = id;
            Name = name;
            Rate = rate;
        }

        public int Id { get; set; }
        [Required, Column("WorkTypeName"), MaxLength(255)]
        public string Name { get; set; }
        public decimal Rate { get; set; }
    }
}