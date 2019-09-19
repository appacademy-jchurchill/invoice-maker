using InvoiceMaker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace InvoiceMaker.Data
{
    public class Context : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }
        public DbSet<WorkDone> WorkDone { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var workType = modelBuilder.Entity<WorkType>();
            workType.Property(wt => wt.Rate).HasPrecision(5, 2);
        }
    }
}