using FluentValidation;
using MediatR;

namespace Journal.Infrastructure.Middleware;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IValidator<TRequest>[] _validators;

    public ValidationBehavior(IValidator<TRequest>[] validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        
        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count != 0)
        {
            foreach (var failure in failures)
            {
                Console.WriteLine($"Validation error: {failure.PropertyName}, Error: {failure.ErrorMessage}");
            }

            throw new ValidationException(failures);
        }
        
        return await next();
    }
}