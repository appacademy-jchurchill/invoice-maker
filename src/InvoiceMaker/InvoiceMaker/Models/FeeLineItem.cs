using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class FeeLineItem : ILineItem
    {
        public FeeLineItem(string description, decimal amount, DateTimeOffset when)
        {
            Amount = amount;
            Description = description;
            When = when;
        }

        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public DateTimeOffset When { get; private set; }
    }
}