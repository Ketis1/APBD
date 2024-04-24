using FluentValidation;

namespace apbd5solution.Validators;

public static class Validators
{
    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AnimalReplaceRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<AnimalCreateRequestValidator>();
    }
}