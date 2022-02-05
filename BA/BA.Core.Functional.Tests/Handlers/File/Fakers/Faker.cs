using BA.Core.Commands.File;
using Bogus;
using System;

namespace BA.Core.Functional.Tests.Handlers.File.Fakers;

public class Faker
{
    public CreateCommand FakeCreateCommand()
    {
        return new Faker<CreateCommand>()
            .RuleFor(o => o.Id, (f, o) => 0)
            .RuleFor(o => o.Name, (f, o) => $"{f.Lorem.Word()}.png")
            .RuleFor(o => o.Link, (f, o) => $"{Guid.NewGuid():N}.png")
            .Generate();
    }
}