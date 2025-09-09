using FluentValidation;
using GeminiBack.Dtos;

namespace GeminiBack.Validators;

public class BasicPromptValidator: AbstractValidator<BasicPromptDto>
{
    public BasicPromptValidator()
    {
        RuleFor(x => x.Prompt).NotNull();
    }
}