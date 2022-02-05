using BA.Core.Commands.File;
using BA.Core.Exceptions;
using BA.Core.Functional.Tests.Fixtures;
using Microsoft.AspNetCore.Http;
using Shouldly;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace BA.Core.Functional.Tests.Handlers.File;

[Collection(nameof(SliceFixture))]
public class DeleteTests
{
    private readonly SliceFixture _fixture;

    public DeleteTests(SliceFixture fixture) => _fixture = fixture;

    [Theory]
    [InlineData("test.png")]
    public async Task Delete_Image_And_Model(string fileName)
    {
        var file = System.IO.File.ReadAllBytes(Path.Combine(GetTestDataDir(), fileName)).ToArray();
        using var stream = new MemoryStream(file);

        var created = await _fixture.SendAsync(new DownloadCommand { File = new FormFile(stream, 0, stream.Length, "file", fileName) });

        await _fixture.SendAsync(new DeleteCommand { Id = created.Id, Link = created.Link });
    }

    [Theory]
    [InlineData("test.png", 0)]
    public async Task Get_ValidationExcecption_On_Zero_Id(string fileName, int id)
    {
        var file = System.IO.File.ReadAllBytes(Path.Combine(GetTestDataDir(), fileName)).ToArray();
        using var stream = new MemoryStream(file);

        var created = await _fixture.SendAsync(new DownloadCommand { File = new FormFile(stream, 0, stream.Length, "file", fileName) });

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(new DeleteCommand { Id = id, Link = created.Link }));
    }

    [Theory]
    [InlineData("test.png", int.MaxValue)]
    public async Task Get_NotFoundExcecption_On_Nonexistent_File(string fileName, int id)
    {
        var file = System.IO.File.ReadAllBytes(Path.Combine(GetTestDataDir(), fileName)).ToArray();
        using var stream = new MemoryStream(file);

        var created = await _fixture.SendAsync(new DownloadCommand { File = new FormFile(stream, 0, stream.Length, "file", fileName) });

        await Should.ThrowAsync<NotFoundException>(() => _fixture.SendAsync(new DeleteCommand { Id = id, Link = created.Link }));
    }


    [Theory]
    [InlineData("test.png", "")]
    [InlineData("test.png", null)]
    public async Task Get_NotFoundExcecption_On_Zero_Id(string fileName, string link)
    {
        var file = System.IO.File.ReadAllBytes(Path.Combine(GetTestDataDir(), fileName)).ToArray();
        using var stream = new MemoryStream(file);

        var created = await _fixture.SendAsync(new DownloadCommand { File = new FormFile(stream, 0, stream.Length, "file", fileName) });

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(new DeleteCommand { Id = created.Id, Link = link }));
    }

    private string GetTestDataDir()
    {
        var location = Assembly.GetExecutingAssembly().Location;
        var testDataDir = Path.GetDirectoryName(location) + @"\TestData";
        return testDataDir;
    }
}