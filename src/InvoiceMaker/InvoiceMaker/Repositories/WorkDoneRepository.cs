using InvoiceMaker.Data;
using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace InvoiceMaker.Repositories
{
    public class WorkDoneRepository
    {
        private Context _context;

        public WorkDoneRepository(Context context)
        {
            _context = context;
        }

        public IList<WorkDone> GetWorkDone()
        {
            return _context.WorkDone
                .Include(wd => wd.Client)
                .Include(wd => wd.WorkType)
                .OrderByDescending(wd => wd.StartedOn)
                .ThenBy(wd => wd.Client.Name)
                .ToList();
        }

        public WorkDone GetWorkDone(int id)
        {
            return _context.WorkDone
                .Include(wd => wd.Client)
                .Include(wd => wd.WorkType)
                .SingleOrDefault(wd => wd.Id == id);
        }

        public void Insert(WorkDone workDone)
        {
            _context.WorkDone.Add(workDone);
            _context.SaveChanges();
        }

        public void Update(WorkDone workDone)
        {
            _context.WorkDone.Attach(workDone);
            _context.Entry(workDone).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}