using BA.Core.Commands.Team;
using BA.Core.Functional.Tests.Fixtures;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace BA.Core.Functional.Tests.Handlers.Team;

[Collection(nameof(SliceFixture))]
public class CreateTests
{
    private readonly SliceFixture _fixture;

    public CreateTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Should_query_for_command()
    {
        var command = new CreateCommand()
        {
            Name = "123",
            City = "123",
            Code = "123",
            Founded = 1977,
            Stadium = "123",
            Conference = Domain.Enums.Conference.Eastern,
            Division = Domain.Enums.Division.NorthWest
        };

        
        var result = await _fixture.SendAsync(command);

        result.ShouldNotBe(null);
    }
}