using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class WorkDone
    {
        private Client _client;
        private WorkType _workType;

        public WorkDone(Client client, WorkType workType)
        {
            _client = client;
            _workType = workType;
            StartedOn = DateTimeOffset.Now;
        }

        public WorkDone(Client client, WorkType workType, 
            DateTimeOffset startedOn)
            : this(client, workType)
        {
            StartedOn = startedOn;
        }

        public WorkDone(Client client, WorkType workType, 
            DateTimeOffset startedOn, DateTimeOffset endedOn)
            : this(client, workType, startedOn)
        {
            EndedOn = endedOn;
        }

        public string ClientName
        {
            get
            {
                return _client.Name;
            }
        }

        public string WorkTypeName
        {
            get
            {
                return _workType.Name;
            }
        }

        public DateTimeOffset StartedOn { get; private set; }
        public DateTimeOffset? EndedOn { get; private set; }

        public void Finished()
        {
            if (EndedOn == null)
            {
                EndedOn = DateTimeOffset.Now;
            }
        }

        public decimal? GetTotal()
        {
            decimal? total = null;

            if (EndedOn != null)
            {
                total = _workType.Rate * (decimal)(EndedOn.Value - StartedOn).TotalHours;
            }

            return total;
        }
    }
}