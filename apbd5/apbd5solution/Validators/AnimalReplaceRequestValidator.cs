using apbd5solution.DTOs;
using FluentValidation;
using apbd5solution.DTOs;
namespace apbd5solution.Validators;

public class AnimalReplaceRequestValidator : AbstractValidator<ReplaceAnimalRequest>
{
    public AnimalReplaceRequestValidator()
    {
        RuleFor(s => s.IdAnimal).NotNull();
        RuleFor(s => s.Name).MaximumLength(200).NotNull();
        RuleFor(s => s.Description).MaximumLength(200);
        RuleFor(s => s.Category).MaximumLength(200).NotNull();
        RuleFor(s => s.Area).MaximumLength(200).NotNull();
    }
}
