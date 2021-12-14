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

    //public ICollection<Person> People { get; set; }
    //public List<PeopleInTeam> PeopleInTeams { get; set; }
}
