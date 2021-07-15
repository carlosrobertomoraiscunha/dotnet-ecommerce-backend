using FluentValidation;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public decimal Amount { get; set; }
        public OrderStatus Status { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }

        public override bool IsValid()
        {
            throw new System.NotImplementedException();
        }
    }

    public class OrderValidation : AbstractValidator<Order>
    {
        public OrderValidation()
        {
            RuleFor(o => o.Amount)
                .GreaterThan(decimal.Zero).WithMessage("O preço total do pedido é inválido");

            RuleFor(o => o.Status)
                .NotNull().WithMessage("O status do pedido é obrigatório")
                .IsInEnum().WithMessage("O status do pedido é inválido");

            RuleFor(o => o.User)
                .NotNull().WithMessage("O usuário que realizou o pedido é obrigatório")
                .SetValidator(new UserValidation());
        }
    }
}
