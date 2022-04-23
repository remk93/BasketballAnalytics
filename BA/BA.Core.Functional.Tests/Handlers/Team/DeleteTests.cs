using BA.Core.Exceptions;
using BA.Core.Functional.Tests.Fixtures;
using BA.Core.Handlers.Team.Commands;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace BA.Core.Functional.Tests.Handlers.Team;

[Collection(nameof(SliceFixture))]

public class DeleteTests
{
    private readonly SliceFixture _fixture;

    public DeleteTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Delete_TeamModel()
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeCreateCommand());

        var command = new Fakers.Faker().FakeCreateCommand(fileModel);
        var created = await _fixture.SendAsync(command);

        var result = await _fixture.SendAsync(new DeleteCommand { Id = created.Id });

        await Should.ThrowAsync<NotFoundException>(() => _fixture.SendAsync(new GetCommand { Id = created.Id }));
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