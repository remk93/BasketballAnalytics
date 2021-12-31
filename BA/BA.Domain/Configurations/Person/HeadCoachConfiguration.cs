using BA.Domain.Entities;
using BA.Domain.Entities.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BA.Domain.Configurations.Person;

public class HeadCoachConfiguration : IEntityTypeConfiguration<HeadCoach>
{
    public void Configure(EntityTypeBuilder<HeadCoach> builder)
    {
        builder
            .HasOne(p => p.Team)
            .WithOne(p => p.HeadCoach)
            .HasForeignKey<Team>(k => k.HeadCoachId)
            .IsRequired();
    }
}