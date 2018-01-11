using Sample.Domain.Events;
using Sample.Domain.SeedWork;

namespace Sample.Domain.AggregatesModel.PersonAggregate
{
    public class Person : Entity, IAggregateRoot
    {
        //public string FirstName { get { return _firstName; }}
        private string _firstName;
        private string _lastName;
        private string _email;

        public Person(string firstName, string lastName, string email)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            
            AddDomainEvent(new PersonAddedDomainEvent(firstName, lastName, email));
        }
    }
}