using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BA.Domain.Entities;
public class TeamConfigurationpublic : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder
            .ToTable(@"Team")
            .HasKey(x => x.Id)
            .HasName("PK_Team_Id");

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
            .Property(x => x.ShortName)
            .HasColumnName(@"ShortName")
            .HasColumnType("nvarchar")
            .HasMaxLength(3)
            .IsRequired();

        builder
           .Property(x => x.Division)
           .HasColumnName(@"Division")
           .HasColumnType("tinyint")
           .IsRequired();

        builder
            .Property(x => x.City)
            .HasColumnName(@"City")
            .HasColumnType("nvarchar")
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(x => x.Stadium)
            .HasColumnName(@"Stadium")
            .HasColumnType("nvarchar")
            .HasMaxLength(256)
            .IsRequired();

        builder
          .Property(x => x.Founded)
          .HasColumnName(@"Founded")
          .HasColumnType("int")
          .IsRequired();
    }
}