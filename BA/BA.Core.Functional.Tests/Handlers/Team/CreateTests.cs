using BA.Core.Exceptions;
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
    public async Task Create_TeamModel()
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeFileCommand());

        var command = new Fakers.Faker().FakeCreateCommand(fileModel);
        var result = await _fixture.SendAsync(command);

        result.Id.ShouldBeGreaterThan(0);
        result.Name.ShouldBe(command.Name);
        result.Code.ShouldBe(command.Code);
        result.City.ShouldBe(command.City);
        result.Conference.ShouldBe(command.Conference);
        result.Division.ShouldBe(command.Division);
        result.Founded.ShouldBe(command.Founded);
        result.Stadium.ShouldBe(command.Stadium);

        result.Logo.Id.ShouldBe(fileModel.Id);
        result.Logo.Name.ShouldBe(fileModel.Name);
        result.Logo.Link.ShouldBe(fileModel.Link);
    }

    [Theory]
    [InlineData(1)]
    public async Task Get_ValidationExcecption_On_Not_Zero_Id(int id)
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeFileCommand());

        var command = new Fakers.Faker().FakeCreateCommand(fileModel);
        command.Id = id;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_ValidationExcecption_On_Invalid_Name(string name)
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeFileCommand());

        var command = new Fakers.Faker().FakeCreateCommand(fileModel);
        command.Name = name;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("test")]
    [InlineData("t")]
    public async Task Get_ValidationExcecption_On_Invalid_Code(string code)
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeFileCommand());

        var command = new Fakers.Faker().FakeCreateCommand(fileModel);
        command.Code = code;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_ValidationExcecption_On_Invalid_City(string city)
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeFileCommand());

        var command = new Fakers.Faker().FakeCreateCommand(fileModel);
        command.City = city;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_ValidationExcecption_On_Invalid_Stadium(string stadium)
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeFileCommand());

        var command = new Fakers.Faker().FakeCreateCommand(fileModel);
        command.Stadium = stadium;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }

    [Theory]
    [InlineData(1897)]
    public async Task Get_ValidationExcecption_On_Invalid_Founded(int founded)
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeFileCommand());

        var command = new Fakers.Faker().FakeCreateCommand(fileModel);
        command.Founded = founded;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(command));
    }
}