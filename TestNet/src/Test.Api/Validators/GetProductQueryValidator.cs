using FluentValidation;
using TestNet.Api.Models.Request;

namespace TestNet.Api.Validators;

public class GetProductQueryValidator : AbstractValidator<GetByIdProductRequestModel>
{
    public GetProductQueryValidator()
    {
        RuleFor(v => v.ProductId)
            .NotNull()
            .WithMessage("ProductId is null or empty.");
    }
}