using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BA.Domain.Entities;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder
            .ToTable(@"File")
            .HasKey(x => x.Id)
            .HasName("PK_File_Id");

        builder
            .Property(x => x.Id)
            .HasColumnName(@"Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.Name)
            .HasColumnName(@"Name")
            .HasColumnType("nvarchar")
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(x => x.Link)
            .HasColumnName(@"Link")
            .HasColumnType("nvarchar")
            .HasMaxLength(256)
            .IsRequired();
    }
}