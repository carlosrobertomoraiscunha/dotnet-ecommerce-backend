using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.IO;

namespace Repository.Mapping
{
    public class ImageMapping : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .IsRequired()
                .ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.HasMany(i => i.Users)
                .WithOne(u => u.Photo)
                .HasForeignKey(u => u.PhotoId);

            builder.Property(i => i.Name)
                .IsRequired()
                .HasColumnName("nome");

            builder.Property(i => i.Content)
                .IsRequired()
                .HasColumnName("conteudo");

            builder.Ignore(i => i.ValidationResult);
            builder.Ignore(i => i.ErrorMessages);

            builder.ToTable("Imagens");
        }
    }
}
