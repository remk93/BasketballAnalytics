using BA.Core.Commands.Team;
using BA.Core.Models;
using BA.Domain.Enums;
using Bogus;
using System;

namespace BA.Core.Functional.Tests.Handlers.Team.Fakers;

public class Faker
{
    public CreateCommand FakeCreateCommand(FileModel file)
    {
        return new Faker<CreateCommand>()
            .RuleFor(c => c.Id, f => 0)
            .RuleFor(c => c.Name, (f, c) => f.Lorem.Word())
            .RuleFor(c => c.Code, (f, c) => f.Lorem.Letter(3))
            .RuleFor(c => c.City, (f, c) => f.Address.City())
            .RuleFor(c => c.Stadium, (f, c) => f.Lorem.Word())
            .RuleFor(c => c.Founded, (f, c) => f.Random.Number(1940, 2500))
            .RuleFor(c => c.Conference, (f, c) => f.PickRandom<Conference>())
            .RuleFor(c => c.Division, (f, c) => f.PickRandom<Division>())
            .RuleFor(c => c.Logo, (f, c) => file)
            .Generate();
    }



    public UpdateCommand FacUpdateCommand(TeamModel model)
    {
        return new Faker<UpdateCommand>()
            .RuleFor(c => c.Id, f => model.Id)
            .RuleFor(c => c.Name, (f, c) => f.Lorem.Word())
            .RuleFor(c => c.Code, (f, c) => f.Lorem.Letter(3))
            .RuleFor(c => c.City, (f, c) => f.Address.City())
            .RuleFor(c => c.Stadium, (f, c) => f.Lorem.Word())
            .RuleFor(c => c.Founded, (f, c) => f.Random.Number(1940, 2500))
            .RuleFor(c => c.Conference, (f, c) => f.PickRandom<Conference>())
            .RuleFor(c => c.Division, (f, c) => f.PickRandom<Division>())
            .RuleFor(c => c.Logo, (f, c) => model.Logo)
            .Generate();
    }
}