using Microsoft.EntityFrameworkCore;
using System;
using Sample.Concrete.Repository.Entities;
using Sample.Concrete.Repository.Data;

/* namespace Sample.Concrete.Repository
{
    public class SampleDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // make sure the connection string makes sense for your machine
            optionsBuilder.UseSqlite(@"Data Source=../../../../../storage/Sample.db");
        }
    }
} */

namespace Sample.Concrete.Repository
{
    public class SampleContext : DbContext
    {
        /* public SampleContext(DbContextOptions<SampleContext> options) : base(options)
        {
        } */

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // make sure the connection string makes sense for your machine
            optionsBuilder.UseSqlite(@"Data Source=../../../../../storage/Sample.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new PersonMap(modelBuilder.Entity<PersonEntity>());
        }
    }
}