using MediatR;

namespace Sample.Domain.Events
{
    /// <summary>
    /// Event used when an order is created
    /// </summary>
    public class PersonAddedDomainEvent : INotification
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        
        public PersonAddedDomainEvent(string firstName, string lastName,  string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}
