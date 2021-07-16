using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .IsRequired();

            builder.Property(a => a.UserId)
                .HasColumnName("id_usuario");

            builder.Property(a => a.Cep)
                .IsRequired()
                .HasColumnName("cep");

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(AddressValidation.MAX_LENGTH_FIELDS)
                .HasColumnName("cidade");

            builder.Property(a => a.Complement)
                .HasMaxLength(AddressValidation.MAX_LENGTH_FIELDS)
                .HasColumnName("complemento");

            builder.Property(a => a.District)
                .IsRequired()
                .HasMaxLength(AddressValidation.MAX_LENGTH_FIELDS)
                .HasColumnName("bairro");

            builder.Property(a => a.Number)
                .IsRequired()
                .HasColumnName("numero");

            builder.Property(a => a.State)
                .IsRequired()
                .HasMaxLength(AddressValidation.MAX_LENGTH_FIELDS)
                .HasColumnName("estado");

            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(AddressValidation.MAX_LENGTH_FIELDS)
                .HasColumnName("rua");

            builder.Ignore(a => a.ValidationResult);
            builder.Ignore(a => a.ErrorMessages);

            builder.ToTable("Enderecos");
        }
    }
}
