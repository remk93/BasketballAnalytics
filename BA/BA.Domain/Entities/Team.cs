using BA.Domain.Enums;

namespace BA.Domain.Entities;

public class Team : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public Division Division { get; set; }
    public Conference Conference { get; set; }
    public string City { get; set; }
    public string Stadium { get; set; }
    public int Founded { get; set; }
    public int LogoId { get; set; }

    public File Logo { get; set; } = default!;
}
