using Medicine.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DbConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
