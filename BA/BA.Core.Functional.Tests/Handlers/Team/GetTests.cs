using BA.Core.Commands.Team;
using BA.Core.Exceptions;
using BA.Core.Functional.Tests.Fixtures;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace BA.Core.Functional.Tests.Handlers.Team;

[Collection(nameof(SliceFixture))]
public class GetTests 
{
    private readonly SliceFixture _fixture;

    public GetTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Get_TeamModel()
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeFileCommand());

        var command = new Fakers.Faker().FakeCreateCommand(fileModel);
        var created = await _fixture.SendAsync(command);

        var result = await _fixture.SendAsync(new GetCommand { Id = created.Id });

        result.Id.ShouldBe(created.Id);
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
    [InlineData(0)]
    public async Task Get_ValidationExcecption_On_Zero_Id(int id)
    {
        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(new GetCommand { Id = id }));
    }


    [Theory]
    [InlineData(int.MaxValue)]
    public async Task Get_NotFoundExcecption_On_Zero_Id(int id)
    {
        await Should.ThrowAsync<NotFoundException>(() => _fixture.SendAsync(new GetCommand { Id = id }));
    }
}