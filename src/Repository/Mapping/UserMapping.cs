using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.IO;

namespace Repository.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.HasOne(u => u.Photo)
                .WithMany(i => i.Users)
                .IsRequired()
                .HasForeignKey(u => u.PhotoId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Property(u => u.PhotoId)
                .HasColumnName("id_imagem");

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(UserValidation.MAX_LENGTH_FIELDS)
                .HasColumnName("nome");

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(UserValidation.MAX_LENGTH_FIELDS)
                .HasColumnName("email");

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(UserValidation.MAX_LENGTH_FIELDS)
                .HasColumnName("senha");

            builder.Property(u => u.Role)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnName("funcao")
                .HasDefaultValue(Role.Customer);
            
            builder.Ignore(u => u.ValidationResult);
            builder.Ignore(u => u.ErrorMessages);

            builder.ToTable("Usuarios");
        }
    }
}
