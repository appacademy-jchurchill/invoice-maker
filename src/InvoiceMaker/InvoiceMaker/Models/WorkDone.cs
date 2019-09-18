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

        public WorkDone(int id, Client client, WorkType workType)
        {
            Id = id;
            _client = client;
            _workType = workType;
            StartedOn = DateTimeOffset.Now;
        }

        public WorkDone(int id, Client client, WorkType workType, 
            DateTimeOffset startedOn)
            : this(id, client, workType)
        {
            StartedOn = startedOn;
        }

        public WorkDone(int id, Client client, WorkType workType, 
            DateTimeOffset startedOn, DateTimeOffset endedOn)
            : this(id, client, workType, startedOn)
        {
            EndedOn = endedOn;
        }

        public int Id { get; private set; }

        public int ClientId
        {
            get
            {
                return _client.Id;
            }
        }

        public string ClientName
        {
            get
            {
                return _client.Name;
            }
        }

        public int WorkTypeId
        {
            get
            {
                return _workType.Id;
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