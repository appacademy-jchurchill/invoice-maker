using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Models
{
    public class WorkDone
    {
        public WorkDone() { }

        public WorkDone(int id, int clientId, int workTypeId)
        {
            Id = id;
            ClientId = clientId;
            WorkTypeId = workTypeId;
            StartedOn = DateTimeOffset.Now;
        }

        public WorkDone(int id, int clientId, int workTypeId,
            DateTimeOffset startedOn)
            : this(id, clientId, workTypeId)
        {
            StartedOn = startedOn;
        }

        //public WorkDone(int id, Client client, WorkType workType)
        //{
        //    Id = id;
        //    Client = client;
        //    WorkType = workType;
        //    StartedOn = DateTimeOffset.Now;
        //}

        //public WorkDone(int id, Client client, WorkType workType, 
        //    DateTimeOffset startedOn)
        //    : this(id, client, workType)
        //{
        //    StartedOn = startedOn;
        //}

        //public WorkDone(int id, Client client, WorkType workType, 
        //    DateTimeOffset startedOn, DateTimeOffset endedOn)
        //    : this(id, client, workType, startedOn)
        //{
        //    EndedOn = endedOn;
        //}

        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int WorkTypeId { get; set; }
        public WorkType WorkType { get; set; }

        //public int ClientId
        //{
        //    get
        //    {
        //        return _client.Id;
        //    }
        //}

        //public string ClientName
        //{
        //    get
        //    {
        //        return _client.Name;
        //    }
        //}

        //public int WorkTypeId
        //{
        //    get
        //    {
        //        return _workType.Id;
        //    }
        //}

        //public string WorkTypeName
        //{
        //    get
        //    {
        //        return _workType.Name;
        //    }
        //}

        public DateTimeOffset StartedOn { get; set; }
        public DateTimeOffset? EndedOn { get; set; }

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
                total = WorkType.Rate * (decimal)(EndedOn.Value - StartedOn).TotalHours;
            }

            return total;
        }
    }
}