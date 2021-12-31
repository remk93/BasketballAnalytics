using BA.Domain.Entities.Person;
using BA.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BA.Domain.Configurations.Person;

public class PersonConfiguration : IEntityTypeConfiguration<BA.Domain.Entities.Person.Person>
{
    public void Configure(EntityTypeBuilder<BA.Domain.Entities.Person.Person> builder)
    {
        builder
            .ToTable(@"Person")
            .HasKey(k => k.Id)
            .HasName("PK_Person_Id");

        builder
            .Property(p => p.Id)
            .HasColumnName(@"Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder
            .Property(p => p.Name)
            .HasColumnName(@"Name")
            .HasColumnType("nvarchar")
            .HasMaxLength(128)
            .IsRequired();

        builder
            .Property(p => p.Surname)
            .HasColumnName(@"Surname")
            .HasColumnType("nvarchar")
            .HasMaxLength(128)
            .IsRequired();

        builder
            .Property(p => p.Birthday)
            .HasColumnName(@"Birthday")
            .HasColumnType("datetime")
            .IsRequired();

        builder.HasDiscriminator<PersonRole>(p => p.Role)
            .HasValue<HeadCoach>(PersonRole.HeadCoach)
            .IsComplete(false);
    }
}