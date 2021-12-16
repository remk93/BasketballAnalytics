using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BA.Domain.Entities;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder
            .ToTable(@"Person")
            .HasKey(x => x.Id)
            .HasName("PK_Person_Id");

        builder
            .Property(x => x.Id)
            .HasColumnName(@"Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.Name)
            .HasColumnName(@"Name")
            .HasColumnType("nvarchar")
            .HasMaxLength(128)
            .IsRequired();

        builder
            .Property(x => x.Surname)
            .HasColumnName(@"Surname")
            .HasColumnType("nvarchar")
            .HasMaxLength(128)
            .IsRequired();

        builder
           .Property(x => x.Role)
           .HasColumnName(@"Role")
           .HasColumnType("tinyint")
           .IsRequired();

        builder
           .Property(x => x.Birthday)
           .HasColumnName(@"Birthday")
           .HasColumnType("datetime")
           .IsRequired();
    }
}