using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BA.Domain.Entities;

//public class PeopleInTeamConfiguration : IEntityTypeConfiguration<PeopleInTeam>
//{
//    public void Configure(EntityTypeBuilder<PeopleInTeam> builder)
//    {
//        builder
//            .ToTable(@"PeopleInTeam")
//            .HasKey(x => new { x.TeamId, x.PersonId, x.Role })
//            .HasName("PK_Person_Id");

//        builder
//            .Property(x => x.TeamId)
//            .HasColumnName(@"TeamId")
//            .HasColumnType("int")
//            .IsRequired();

//        builder
//            .Property(x => x.PersonId)
//            .HasColumnName(@"PersonId")
//            .HasColumnType("int")
//            .IsRequired();

//        builder
//            .Property(x => x.Role)
//            .HasColumnName(@"Role")
//            .HasColumnType("tinyint")
//            .IsRequired();
//    }
//}