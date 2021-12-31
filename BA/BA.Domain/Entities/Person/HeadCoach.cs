using BA.Domain.Enums;

namespace BA.Domain.Entities.Person;

public class HeadCoach : Person
{
    public HeadCoach()
    {
        Role = PersonRole.HeadCoach;
    }

    public int TeamId { get; set; }

    public Team Team { get; set; } = default!;
}