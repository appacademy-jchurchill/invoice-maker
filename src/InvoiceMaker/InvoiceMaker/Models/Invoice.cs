using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class Invoice
    {
        public Invoice(string invoiceNumber)
        {
            InvoiceNumber = invoiceNumber;
            LineItems = new List<ILineItem>();
            Status = InvoiceStatus.Open;
        }

        public Invoice(string invoiceNumber, InvoiceStatus status)
            : this(invoiceNumber)
        {
            Status = status;
        }

        public string InvoiceNumber { get; private set; }
        public InvoiceStatus Status { get; private set; }
        public IList<ILineItem> LineItems { get; private set; }

        public void FinalizeInvoice()
        {
            if (Status == InvoiceStatus.Open)
            {
                Status = InvoiceStatus.Finalized;
            }
        }

        public void CloseInvoice()
        {
            if (Status == InvoiceStatus.Finalized)
            {
                Status = InvoiceStatus.Closed;
            }
        }

        public void AddWorkLineItem(WorkDone workDone)
        {
            WorkLineItem lineItem = new WorkLineItem(workDone);
            LineItems.Add(lineItem);
        }

        public void AddFeeLineItem(string description, decimal amount, DateTimeOffset when)
        {
            FeeLineItem lineItem = new FeeLineItem(description, amount, when);
            LineItems.Add(lineItem);
        }
    }
}