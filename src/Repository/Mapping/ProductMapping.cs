using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.HasOne(p => p.Picture)
                .WithMany(i => i.Products)
                .IsRequired()
                .HasForeignKey(p => p.PictureId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Property(p => p.PictureId)
                .HasColumnName("id_imagem");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(ProductValidation.MAX_LENGTH_FIELDS)
                .HasColumnName("nome");

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("float")
                .HasColumnName("preco");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(ProductValidation.MAX_LENGTH_DESCRIPTION)
                .HasColumnName("descricao");

            builder.Ignore(p => p.ValidationResult);
            builder.Ignore(p => p.ErrorMessages);

            builder.ToTable("Produtos");
        }
    }
}
