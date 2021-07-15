using Domain.Enums;
using FluentValidation;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public long PhotoId { get; set; }
        public Image Photo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Role Role { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }

        public User()
        {
            Role = Role.Customer;
            Addresses = new List<Address>();
            Orders = new List<Order>();
        }

        public override bool IsValid()
        {
            ValidationResult = new UserValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UserValidation : AbstractValidator<User>
    {
        public const int MAX_LENGTH_FIELDS = 200;
        public const int MIN_LENGTH_PASSWORD = 8;
        public const int LENGTH_PHONE = 11;

        public UserValidation()
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("O nome do usuário é obrigatório")
                .MaximumLength(MAX_LENGTH_FIELDS).WithMessage($"O nome do usuário deve possuir no máximo {MAX_LENGTH_FIELDS} caracteres");

            RuleFor(u => u.Email)
                .MaximumLength(MAX_LENGTH_FIELDS).WithMessage($"O email do usuário deve possuir no máximo {MAX_LENGTH_FIELDS} caracteres")
                .EmailAddress().WithMessage("O email do usuário é inválido");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("A senha do usuário é obrigatória")
                .MinimumLength(MIN_LENGTH_PASSWORD).WithMessage($"A senha do usuário deve possuir no mínimo {MIN_LENGTH_PASSWORD} caracteres")
                .MaximumLength(MAX_LENGTH_FIELDS).WithMessage($"A senha do usuário deve possuir no máximo {MAX_LENGTH_FIELDS} caracteres");

            RuleFor(u => u.Role)
                .NotNull().WithMessage("O cargo do usuário é obrigatório")
                .IsInEnum().WithMessage("O cargo do usuário é inválido");

            RuleFor(u => u.Photo)
                .NotNull().WithMessage("A imagem do usuário é obrigatória")
                .SetValidator(new ImageValidation());

            RuleFor(u => u.Phone)
                .NotEmpty().WithMessage("O telefone do usuário é obrigatório")
                .Must(ValidatePhone).WithMessage("O telefone do usuário é inválido");
        }

        private bool ValidatePhone(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return true;

            return phone.Replace("(", string.Empty)
                        .Replace(")", string.Empty)
                        .Replace(" ", string.Empty)
                        .Replace("-", string.Empty).Length == LENGTH_PHONE;
        }
    }
}
