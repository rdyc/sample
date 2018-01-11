using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Sample.Concrete.Repository;

namespace Sample.Concrete.Repository.Migrations
{
    [DbContext(typeof(SampleContext))]
    [Migration("20171004081944_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "1.1.3");

            modelBuilder.Entity("Sample.Concrete.Repository.Person", b =>
                {
                    b.Property<int>("Id").ValueGeneratedOnAdd();
                    b.Property<string>("FirstName");
                    b.Property<string>("LastName");
                    b.Property<string>("Email");
                    b.HasKey("Id");
                    b.ToTable("Persons");
                });
        }
    }
}
