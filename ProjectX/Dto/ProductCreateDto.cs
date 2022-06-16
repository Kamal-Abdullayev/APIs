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

    public class ProductCreateDtoValidator:AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).WithMessage(ErrorMessage.ProductCreateDtoNameMaxLength);
        }
    }

}
