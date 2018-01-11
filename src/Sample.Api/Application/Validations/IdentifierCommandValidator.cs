using FluentValidation;
using Sample.Api.Application.Commands;

namespace Sample.Api.Application.Validations
{
    public class IdentifierCommandValidator : AbstractValidator<IdentifiedCommand<CreatePersonCommand,bool>>
    {
        public IdentifierCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();    
        }
    }
}
