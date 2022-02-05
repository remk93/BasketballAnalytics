using BA.Core.Exceptions;
using BA.Core.Functional.Tests.Fixtures;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace BA.Core.Functional.Tests.Handlers.Team;

[Collection(nameof(SliceFixture))]
public class UpdateTests
{
    private readonly SliceFixture _fixture;

    public UpdateTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Update_TeamModel()
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeCreateCommand());

        var faker = new Fakers.Faker();

        var createCommand = faker.FakeCreateCommand(fileModel);
        var created = await _fixture.SendAsync(createCommand);

        var updateCommand = faker.FacUpdateCommand(created);
        var result = await _fixture.SendAsync(updateCommand);


        result.Id.ShouldBe(created.Id);
        result.Name.ShouldBe(updateCommand.Name);
        result.Code.ShouldBe(updateCommand.Code);
        result.City.ShouldBe(updateCommand.City);
        result.Conference.ShouldBe(updateCommand.Conference);
        result.Division.ShouldBe(updateCommand.Division);
        result.Founded.ShouldBe(updateCommand.Founded);
        result.Stadium.ShouldBe(updateCommand.Stadium);

        result.Logo.Id.ShouldBe(fileModel.Id);
        result.Logo.Name.ShouldBe(fileModel.Name);
        result.Logo.Link.ShouldBe(fileModel.Link);
    }

    [Theory]
    [InlineData(0)]
    public async Task Get_ValidationExcecption_On_Zero_Id(int id)
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeCreateCommand());

        var faker = new Fakers.Faker();

        var createCommand = faker.FakeCreateCommand(fileModel);
        var created = await _fixture.SendAsync(createCommand);

        var updateCommand = faker.FacUpdateCommand(created);
        updateCommand.Id = id;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(updateCommand));
    }

    [Theory]
    [InlineData(int.MaxValue)]
    public async Task Get_NotFoundExcecption_On_Nonexistent_Team(int id)
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeCreateCommand());

        var faker = new Fakers.Faker();

        var createCommand = faker.FakeCreateCommand(fileModel);
        var created = await _fixture.SendAsync(createCommand);

        var updateCommand = faker.FacUpdateCommand(created);
        updateCommand.Id = id;

        await Should.ThrowAsync<NotFoundException>(() => _fixture.SendAsync(updateCommand));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_ValidationExcecption_On_Invalid_Name(string name)
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeCreateCommand());

        var faker = new Fakers.Faker();

        var createCommand = faker.FakeCreateCommand(fileModel);
        var created = await _fixture.SendAsync(createCommand);

        var updateCommand = faker.FacUpdateCommand(created);
        updateCommand.Name = name;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(updateCommand));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("test")]
    [InlineData("t")]
    public async Task Get_ValidationExcecption_On_Invalid_Code(string code)
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeCreateCommand());

        var faker = new Fakers.Faker();

        var createCommand = faker.FakeCreateCommand(fileModel);
        var created = await _fixture.SendAsync(createCommand);

        var updateCommand = faker.FacUpdateCommand(created);
        updateCommand.Code = code;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(updateCommand));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_ValidationExcecption_On_Invalid_City(string city)
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeCreateCommand());

        var faker = new Fakers.Faker();

        var createCommand = faker.FakeCreateCommand(fileModel);
        var created = await _fixture.SendAsync(createCommand);

        var updateCommand = faker.FacUpdateCommand(created);
        updateCommand.City = city;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(updateCommand));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Get_ValidationExcecption_On_Invalid_Stadium(string stadium)
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeCreateCommand());

        var faker = new Fakers.Faker();

        var createCommand = faker.FakeCreateCommand(fileModel);
        var created = await _fixture.SendAsync(createCommand);

        var updateCommand = faker.FacUpdateCommand(created);
        updateCommand.Stadium = stadium;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(updateCommand));
    }

    [Theory]
    [InlineData(1897)]
    public async Task Get_ValidationExcecption_On_Invalid_Founded(int founded)
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeCreateCommand());

        var faker = new Fakers.Faker();

        var createCommand = faker.FakeCreateCommand(fileModel);
        var created = await _fixture.SendAsync(createCommand);

        var updateCommand = faker.FacUpdateCommand(created);
        updateCommand.Founded = founded;

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(updateCommand));
    }
}