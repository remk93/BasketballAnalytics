using BA.Domain.Entities.Person;
using BA.Domain.Enums;
using System.Collections.Generic;

namespace BA.Domain.Entities;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public Division Division { get; set; }
    public Conference Conference { get; set; }
    public string City { get; set; }
    public string Stadium { get; set; }
    public int Founded { get; set; }
    public int LogoId { get; set; }
    public int HeadCoachId { get; set; }

    public File Logo { get; set; } = default!;
    public HeadCoach HeadCoach { get; set;} = default!;
}
