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
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Template>()
                .HasOptional(s => s.Group)
                .WithMany(g => g.Templates)
                .HasForeignKey(s => s.GroupId);
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<TemplateGroup> Groups { get; set; }
        public DbSet<CheckList> CheckLists { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
