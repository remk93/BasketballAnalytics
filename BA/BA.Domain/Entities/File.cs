namespace BA.Domain.Entities;

public class File : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Link { get; set; }
}