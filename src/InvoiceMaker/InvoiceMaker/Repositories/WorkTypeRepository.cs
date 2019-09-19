using InvoiceMaker.Data;
using InvoiceMaker.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace InvoiceMaker.Repositories
{
    public class WorkTypeRepository
    {
        private Context _context;

        public WorkTypeRepository(Context context)
        {
            _context = context;
        }

        public IList<WorkType> GetWorkTypes()
        {
            return _context.WorkTypes
                .OrderBy(wt => wt.Name)
                .ToList();
        }

        public WorkType GetWorkType(int id)
        {
            return _context.WorkTypes
                .SingleOrDefault(wt => wt.Id == id);
        }

        public void Insert(WorkType workType)
        {
            _context.WorkTypes.Add(workType);
            _context.SaveChanges();
        }

        public void Update(WorkType workType)
        {
            _context.WorkTypes.Attach(workType);
            _context.Entry(workType).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}