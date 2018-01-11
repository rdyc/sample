using System.Runtime.Serialization;
using MediatR;

namespace Sample.Api.Application.Commands
{
    [DataContract]
    public class CreatePersonCommand : IRequest<bool>
    {
        [DataMember]
        public string FirstName { get; private set; }
        
        [DataMember]
        public string LastName { get; private set; }
        
        [DataMember]
        public string Email { get; private set; }

        public CreatePersonCommand(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

    }
}