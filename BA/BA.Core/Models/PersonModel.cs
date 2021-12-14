using BA.Domain.Enums;

namespace BA.Core.Models;

public class PersonModel
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public PersonRole Role { get; set; }
    public DateTime Birthday { get; set; }
}