using FluentValidation;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        public long PictureId { get; set; }
        public Image Picture { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new ProductValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ProductValidation : AbstractValidator<Product>
    {
        public const int MAX_LENGTH_FIELDS = 150;
        public const int MAX_LENGTH_DESCRIPTION = 300;

        public ProductValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O nome do produto é obrigatório")
                .MaximumLength(MAX_LENGTH_FIELDS).WithMessage($"O nome do produto deve possuir no máximo {MAX_LENGTH_FIELDS} caracteres");

            RuleFor(p => p.Price)
                .GreaterThan(decimal.Zero).WithMessage("O preço do produto é inválido");
            
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("A descrição do produto é obrigatória")
                .MaximumLength(MAX_LENGTH_DESCRIPTION).WithMessage($"A descrição do produto deve possuir no máximo {MAX_LENGTH_DESCRIPTION} caracteres");

            RuleFor(p => p.Category)
                .NotEmpty().WithMessage("O nome da categoria é obrigatório")
                .MaximumLength(MAX_LENGTH_FIELDS).WithMessage($"O nome da categoria deve possuir no máximo {MAX_LENGTH_FIELDS} caracteres");

            RuleFor(p => p.Picture)
                .NotNull().WithMessage("A imagem do produto é obrigatória")
                .SetValidator(new ImageValidation());
        }
    }
}
