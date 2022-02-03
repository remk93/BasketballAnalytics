using BA.Domain.Enums;

namespace BA.Core.Models;

public class TeamModel
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public Division Division { get; set; }
    public Conference Conference { get; set; }
    public string City { get; set; } = default!;
    public string Stadium { get; set; } = default!;
    public int Founded { get; set; }
    public FileModel Logo { get; set; } = default!;
}