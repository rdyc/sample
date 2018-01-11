using Sample.Domain.SeedWork;
using System.Threading.Tasks;

namespace Sample.Domain.AggregatesModel.PersonAggregate
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Add(Person person);
        
        void Update(Person person);

        Task<Person> GetAsync(int personId);
    }
}