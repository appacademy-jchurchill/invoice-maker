using System.Collections.Generic;
using InvoiceMaker.Models;
using InvoiceMaker.Data;
using System.Linq;
using System.Data.Entity;

namespace InvoiceMaker.Repositories
{
    public class ClientRepository
    {
        private Context _context;

        public ClientRepository(Context context)
        {
            _context = context;
        }

        public IList<Client> GetClients()
        {
            return _context.Clients.ToList();
        }

        public Client GetClient(int id)
        {
            return _context.Clients.SingleOrDefault(c => c.Id == id);
        }

        public void Insert(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public void Update(Client client)
        {
            _context.Clients.Attach(client);
            _context.Entry(client).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}