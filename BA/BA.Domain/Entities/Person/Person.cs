using BA.Domain.Enums;
using System;

namespace BA.Domain.Entities.Person;

public abstract class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Surname { get; set; }
    public DateTime Birthday { get; set; }
    public PersonRole Role { get; set; }
}