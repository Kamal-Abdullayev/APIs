using FluentValidation;
using Microsoft.AspNetCore.Http;
using ProjectX.Extension;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectX.Dto
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        [NotMapped]
        public virtual IFormFile ImageUrl { get; set; }
    }

    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(p => p.Name).MaximumLength(100).WithMessage(ErrorMessage.ProductCreateDtoNameMaxLength);
            RuleFor(p => p.Price).GreaterThan(0).WithMessage(ErrorMessage.PriceMoreThanZero);
            RuleFor(p => p.IsActive).NotNull();
            RuleFor(p => p.CategoryId).NotNull().WithMessage(ErrorMessage.NotNull("Catefory Id"));
        }
    }
}
