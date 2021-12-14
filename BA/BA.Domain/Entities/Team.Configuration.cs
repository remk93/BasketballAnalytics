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

        //builder
        //    .HasMany(x => x.People)
        //    .WithMany(x => x.Teams)
        //    .UsingEntity<PeopleInTeam>(
        //        x => x
        //            .HasOne(x => x.Person)
        //            .WithMany(x => x.PeopleInTeams)
        //            .HasForeignKey(k => new { k.PersonId, k.Role }),
        //        x => x
        //            .HasOne(x => x.Team)
        //            .WithMany(x => x.PeopleInTeams)
        //            .HasForeignKey(k => k.TeamId),
        //        x => x.HasKey(k => new { k.PersonId, k.Role, k.TeamId })
        //            );
    }
}