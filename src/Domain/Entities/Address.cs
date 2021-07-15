using FluentValidation;
using System;

namespace Domain.Entities
{
    public class Address : BaseEntity
    {
        public string Cep { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new AddressValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddressValidation : AbstractValidator<Address>
    {
        public const int LENGTH_CEP = 8;
        public const int MAX_LENGTH_FIELDS = 150;
        private const int MAX_LENGTH_NUMBER = 6;

        public AddressValidation()
        {
            RuleFor(a => a.Cep)
                .NotEmpty().WithMessage("O cep é obrigatório")
                .Must(ValidateCep).WithMessage("O cep é inválido");

            RuleFor(a => a.City)
                .NotEmpty().WithMessage("A cidade é obrigatória.")
                .MaximumLength(MAX_LENGTH_FIELDS).WithMessage($"A cidade deve possuir no máximo {MAX_LENGTH_FIELDS} caracteres");

            RuleFor(a => a.Complement)
                .MaximumLength(MAX_LENGTH_FIELDS).WithMessage($"O complemento deve possuir no máximo {MAX_LENGTH_FIELDS} caracteres");;

            RuleFor(a => a.District)
                .NotEmpty().WithMessage("O bairro é obrigatório")
                .MaximumLength(MAX_LENGTH_FIELDS).WithMessage($"O bairro deve possuir no máximo {MAX_LENGTH_FIELDS} caracteres");
            
            RuleFor(a => a.Number)
                .NotEmpty().WithMessage("O número é obrigatório")
                .MaximumLength(MAX_LENGTH_NUMBER).WithMessage($"O número deve possuir no máximo {MAX_LENGTH_NUMBER} caracteres");

            RuleFor(a => a.State)
                .NotEmpty().WithMessage("O estado é obrigatório")
                .MaximumLength(MAX_LENGTH_FIELDS).WithMessage($"O estado deve possuir no máximo {MAX_LENGTH_FIELDS} caracteres");

            RuleFor(a => a.Street)
                .NotEmpty().WithMessage("A rua é obrigatória")
                .MaximumLength(MAX_LENGTH_FIELDS).WithMessage($"A rua deve possuir no máximo {MAX_LENGTH_FIELDS} caracteres");

            RuleFor(a => a.User)
                .NotNull().WithMessage("O usuário é obrigatório")
                .SetValidator(new UserValidation());
        }

        private bool ValidateCep(string cep)
        {
            if (string.IsNullOrEmpty(cep)) return true;

            return cep.Replace(".", string.Empty)
                      .Replace("-", string.Empty)
                      .Replace(" ", string.Empty).Length == 8;
        }
    }
}
