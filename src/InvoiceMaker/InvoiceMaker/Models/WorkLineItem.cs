using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class WorkLineItem : ILineItem
    {
        private WorkDone _workDone;

        public WorkLineItem(WorkDone workDone)
        {
            _workDone = workDone;
        }

        public decimal Amount
        {
            get
            {
                decimal? amount = _workDone.GetTotal();
                return amount ?? 0m;
            }
        }

        public string Description
        {
            get
            {
                return _workDone.WorkTypeName;
            }
        }

        public DateTimeOffset When
        {
            get
            {
                return _workDone.StartedOn;
            }
        }
    }
}