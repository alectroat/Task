using API_Project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace API_Project.DAL
{
    public class APPContext:DbContext
    {
        public APPContext() : base("Connection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }

    public class MyDbContextInitializer : DropCreateDatabaseIfModelChanges<APPContext>
    {
        protected override void Seed(APPContext dbContext)
        {
            base.Seed(dbContext);
        }
    }
}