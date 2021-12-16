using BA.Domain.Enums;
using System;
using System.Collections.Generic;

namespace BA.Domain.Entities;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public PersonRole Role { get; set; }
    public DateTime Birthday { get; set; }
}