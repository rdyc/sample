using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Domain.AggregatesModel.PersonAggregate;
using Sample.Domain.SeedWork;
using Sample.Infrastructure.Idempotency;

namespace Sample.Infrastructure
{
    public class SampleContext : DbContext, IUnitOfWork

    {
        const string DEFAULT_SCHEMA = "sample";

        public DbSet<Person> Persons { get; set; }

        private readonly IMediator _mediator;

        public SampleContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


            Debug.WriteLine("SampleContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientRequest>(ConfigureRequests);
            modelBuilder.Entity<Person>(ConfigurePerson);
        }

        private void ConfigureRequests(EntityTypeBuilder<ClientRequest> requestConfiguration)
        {
            requestConfiguration.ToTable("requests", DEFAULT_SCHEMA);
            requestConfiguration.HasKey(cr => cr.Id);
            requestConfiguration.Property(cr => cr.Name).IsRequired();
            requestConfiguration.Property(cr => cr.Time).IsRequired();
        }

        void ConfigurePerson(EntityTypeBuilder<Person> personConfiguration)
        {
            personConfiguration.ToTable("persons", DEFAULT_SCHEMA);
            personConfiguration.HasKey(o => o.Id);
            personConfiguration.Ignore(b => b.DomainEvents);
            personConfiguration.Property(o => o.Id).ForSqlServerUseSequenceHiLo("personseq", DEFAULT_SCHEMA);
            personConfiguration.Property<string>("FirstName").IsRequired(true);
            personConfiguration.Property<string>("LastName").IsRequired(false);
            personConfiguration.Property<string>("Email").IsRequired(true);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);


            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed throught the DbContext will be commited
            var result = await base.SaveChangesAsync();

            return true;
        }
    }
}