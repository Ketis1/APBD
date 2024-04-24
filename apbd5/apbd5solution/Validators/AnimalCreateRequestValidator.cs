using apbd5solution.DTOs;
using FluentValidation;

namespace apbd5solution.Validators;

public class AnimalCreateRequestValidator: AbstractValidator<CreateAnimalResponse>
{
    public AnimalCreateRequestValidator()
    {
        RuleFor(s => s.IdAnimal).NotNull();
        RuleFor(s => s.Name).MaximumLength(200).NotNull();
        RuleFor(s => s.Description).MaximumLength(200);
        RuleFor(s => s.Category).MaximumLength(200).NotNull();
        RuleFor(s => s.Area).MaximumLength(200).NotNull();
    }
}