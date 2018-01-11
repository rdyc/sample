using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Sample.Domain.AggregatesModel.PersonAggregate;
using Sample.Infrastructure;
using Sample.Domain.SeedWork;

namespace Sample.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly SampleContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public PersonRepository(SampleContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Person Add(Person person)
        {
            return  _context.Persons.Add(person).Entity;
        }

        public async Task<Person> GetAsync(int personId)
        {
            var person = await _context.Persons.FindAsync(personId);
            if (person != null)
            {
                // await _context.Entry(person).Collection(i => i.OrderItems).LoadAsync();
                // await _context.Entry(person).Reference(i => i.OrderStatus).LoadAsync();
                // await _context.Entry(person).Reference(i => i.Address).LoadAsync();
            }

            return person;
        }

        public void Update(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
        }
    }
}
