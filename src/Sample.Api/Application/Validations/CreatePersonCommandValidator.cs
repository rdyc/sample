using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Sample.Api.Application.Commands;

namespace Sample.Application.Validations
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(command => command.FirstName).NotEmpty().WithMessage("Asmane sampeyan ndak bole kosong").NotEqual("string").WithMessage("opo kui string?");
            //RuleFor(command => command.LastName);
            RuleFor(command => command.Email).NotEmpty().EmailAddress().WithMessage("lhoo piye? imel sampeyan gak bener");
            /*RuleFor(command => command.State).NotEmpty();
            RuleFor(command => command.Country).NotEmpty();
            RuleFor(command => command.ZipCode).NotEmpty();
            RuleFor(command => command.CardNumber).NotEmpty().Length(12, 19); 
            RuleFor(command => command.CardHolderName).NotEmpty();
            RuleFor(command => command.CardExpiration).NotEmpty().Must(BeValidExpirationDate).WithMessage("Please specify a valid card expiration date"); 
            RuleFor(command => command.CardSecurityNumber).NotEmpty().Length(3); 
            RuleFor(command => command.CardTypeId).NotEmpty();
            RuleFor(command => command.OrderItems).Must(ContainOrderItems).WithMessage("No order items found");*/ 
        }

        /*private bool BeValidExpirationDate(DateTime dateTime)
        {
            return dateTime >= DateTime.UtcNow;
        }

        private bool ContainOrderItems(IEnumerable<OrderItemDTO> orderItems)
        {
            return orderItems.Any();
        }*/
    }
}
