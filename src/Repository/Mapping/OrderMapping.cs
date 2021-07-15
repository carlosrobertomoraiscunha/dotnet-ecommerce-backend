using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .IsRequired();

            builder.Property(o => o.UserId)
                .HasColumnName("id_usuario");

            builder.Property(o => o.Amount)
                .IsRequired()
                .HasColumnType("float")
                .HasColumnName("total");

            builder.Property(o => o.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnName("status")
                .HasDefaultValue(OrderStatus.Opened);

            builder.Ignore(o => o.ValidationResult);
            builder.Ignore(o => o.ErrorMessages);

            builder.ToTable("Pedidos");
        }
    }
}
