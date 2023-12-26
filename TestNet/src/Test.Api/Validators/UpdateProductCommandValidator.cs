using FluentValidation;
using TestNet.Api.Models.Request;

namespace TestNet.Api.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductRequestModel>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotNull()
                .WithMessage("ProductId is null or empty.");

            RuleFor(v => v.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name is null or empty.");

            RuleFor(v => v.Stock)
            .NotNull().GreaterThanOrEqualTo(0)
            .WithMessage("Stock must be greater than zero.");

            RuleFor(v => v.Description)
                .NotEmpty()
                .WithMessage("Description is empty.");

            RuleFor(v => v.Price)
            .NotNull().GreaterThan(0)
            .WithMessage("Price must be greater than zero.");
        }
    }
}