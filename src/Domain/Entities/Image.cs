using FluentValidation;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Image : BaseEntity
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Product> Products { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new ImageValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ImageValidation : AbstractValidator<Image>
    {
        public ImageValidation()
        {
            RuleFor(i => i.Name)
                .NotEmpty().WithMessage("O nome da imagem é obrigatório");
        }
    }
}
