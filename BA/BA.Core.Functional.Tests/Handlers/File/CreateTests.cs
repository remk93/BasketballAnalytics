using BA.Core.Exceptions;
using BA.Core.Functional.Tests.Fixtures;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace BA.Core.Functional.Tests.Handlers.File;

[Collection(nameof(SliceFixture))]
public class CreateTests
{
    private readonly SliceFixture _fixture;

    public CreateTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Should_Create_File_Model()
    {
        var command = new Fakers.Faker().FakeCreateCommand();

        var result = await _fixture.SendAsync(command);

        result.Id.ShouldBeGreaterThan(0);
        result.Name.ShouldBe(command.Name);
        result.Link.ShouldBe(command.Link);
    }

    [Theory]
    [InlineData(1)]
    public async Task Should_Get_ValidationExcecption_On_Not_Zero_Id(int id)
    {
        var command = new Fakers.Faker().FakeCreateCommand();
        command.Id = id;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Should_Get_ValidationExcecption_On_Empty_Name(string name)
    {
        var command = new Fakers.Faker().FakeCreateCommand();
        command.Name = name;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Should_Get_ValidationExcecption_On_Empty_Link(string link)
    {
        var command = new Fakers.Faker().FakeCreateCommand();
        command.Link = link;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }
}