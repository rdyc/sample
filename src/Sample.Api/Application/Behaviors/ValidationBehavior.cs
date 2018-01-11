using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Sample.Api.Exceptions;

namespace Sample.Api.Application.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest>[] _validators;
        public ValidatorBehavior(IValidator<TRequest>[] validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                //throw new SampleDomainException($"Command Validation Errors for type {typeof(TRequest).FirstName}", new ValidationException("Validation exception", failures));
                throw new SampleDomainException($"An error occured: {string.Join(", ", failures.Select(f=>f.ErrorMessage).ToList<string>())}", new ValidationException("Validation exception", failures));
            }

            var response = await next();
            return response;
        }
    }
}