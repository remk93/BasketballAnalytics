using BA.Core.Commands.File;
using BA.Core.Exceptions;
using BA.Core.Functional.Tests.Fixtures;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace BA.Core.Functional.Tests.Handlers.File;

[Collection(nameof(SliceFixture))]
public class GetTests
{
    private readonly SliceFixture _fixture;

    public GetTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task Get_FileModel()
    {
        var command = new Fakers.Faker().FakeFileCommand();
        var model = await _fixture.SendAsync(command);

        var result = await _fixture.SendAsync(new GetCommand { Id = model.Id });

        result.Id.ShouldBeGreaterThan(0);
        result.Name.ShouldBe(command.Name);
        result.Link.ShouldBe(command.Link);
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