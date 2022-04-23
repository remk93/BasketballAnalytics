using BA.Core.Exceptions;
using BA.Core.Functional.Tests.Fixtures;
using BA.Core.Handlers.Team.Commands;
using BA.Core.Models;
using BA.Core.Queries.Filter;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BA.Core.Functional.Tests.Handlers.Team;

[Collection(nameof(SliceFixture))]
public class GetAllTests
{
    private readonly SliceFixture _fixture;

    public GetAllTests(SliceFixture fixture) => _fixture = fixture;

    [Fact]
    public async Task GetAll_TeamModel()
    {
        var fileModel1 = await _fixture.SendAsync(new File.Fakers.Faker().FakeCreateCommand());
        var command1 = new Fakers.Faker().FakeCreateCommand(fileModel1);
        var created1 = await _fixture.SendAsync(command1);

        var fileModel2 = await _fixture.SendAsync(new File.Fakers.Faker().FakeCreateCommand());
        var command2 = new Fakers.Faker().FakeCreateCommand(fileModel2);
        var created2 = await _fixture.SendAsync(command2);

        var result = await _fixture.SendAsync(new GetAllCommand { FilterData = new() { PageSize = 2 } });

        result.TotalCount.ShouldBeGreaterThan(0);
        result.TotalCount.ShouldBe(2);

        result.Result.FirstOrDefault(x => x.Id == created1.Id).ShouldNotBeNull();
        result.Result.FirstOrDefault(x => x.Id == created2.Id).ShouldNotBeNull();
    }

    [Fact]
    public async Task GetAll_TeamModel_By_Search_By_Text()
    {
        var fileModel = await _fixture.SendAsync(new File.Fakers.Faker().FakeCreateCommand());
        var command = new Fakers.Faker().FakeCreateCommand(fileModel);
        var created = (await _fixture.SendAsync(command)) as TeamModel;

        var result1 = await _fixture.SendAsync(new GetAllCommand { FilterData = new() { SearchByText = $"{created.Name}" } });
        var result2 = await _fixture.SendAsync(new GetAllCommand { FilterData = new() { SearchByText = $"{created.Code}" } });
        var result3 = await _fixture.SendAsync(new GetAllCommand { FilterData = new() { SearchByText = $"{created.City}" } });
        var result4 = await _fixture.SendAsync(new GetAllCommand { FilterData = new() { SearchByText = $"{created.Stadium}" } });

        result1.TotalCount.ShouldBeGreaterThan(0);
        result1.TotalCount.ShouldBe(1);
        result1.Result.FirstOrDefault(x => x.Id == created.Id).ShouldNotBeNull();

        result2.TotalCount.ShouldBeGreaterThan(0);
        result2.TotalCount.ShouldBe(1);
        result2.Result.FirstOrDefault(x => x.Id == created.Id).ShouldNotBeNull();

        result3.TotalCount.ShouldBeGreaterThan(0);
        result3.TotalCount.ShouldBe(1);
        result3.Result.FirstOrDefault(x => x.Id == created.Id).ShouldNotBeNull();

        result4.TotalCount.ShouldBeGreaterThan(0);
        result4.TotalCount.ShouldBe(1);
        result4.Result.FirstOrDefault(x => x.Id == created.Id).ShouldNotBeNull();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Get_ValidationExcecption_On_Zero_Or_Negative_PageSize(int pageSize)
    {
        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(new GetAllCommand { FilterData = new FilterModel { PageNumber = pageSize } }));
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Get_NotFoundExcecption_On__Zero_Or_Negative_PageNamber(int pageNumber)
    {
        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(new GetAllCommand { FilterData = new FilterModel { PageNumber = pageNumber } }));
    }
}