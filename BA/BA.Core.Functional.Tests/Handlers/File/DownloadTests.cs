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
public class DownloadTests
{
    private readonly SliceFixture _fixture;

    public DownloadTests(SliceFixture fixture) => _fixture = fixture;

    [Theory]
    [InlineData("test.png")]
    public async Task ShouldUploadFile(string fileName)
    {
        var file = System.IO.File.ReadAllBytes(Path.Combine(GetTestDataDir(), fileName)).ToArray();
        using var stream = new MemoryStream(file);

        var result = await _fixture.SendAsync(new DownloadCommand { File = new FormFile(stream, 0, stream.Length, "file", fileName) });

        result.ShouldNotBeNull();
        result.Id.ShouldBeGreaterThan(0);
        result.Link.ShouldNotBeNullOrEmpty();
        result.Name.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task Should_Get_Validation_Exception_When_File_Null()
    {
        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(new DownloadCommand()));
    }

    [Theory]
    [InlineData("test.png")]
    public async Task Should_Get_Validation_Exception_When_Length_Zero(string fileName)
    {
        var file = System.IO.File.ReadAllBytes(Path.Combine(GetTestDataDir(), fileName)).ToArray();
        using var stream = new MemoryStream(file);

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(new DownloadCommand { File = new FormFile(stream, 0, 0, "file", fileName) }));
    }

    [Theory]
    [InlineData("test.png")]
    public async Task Should_Get_BadRequest_Exception_When_Not_Allowed_Extention(string fileName)
    {
        var file = System.IO.File.ReadAllBytes(Path.Combine(GetTestDataDir(), fileName)).ToArray();
        using var stream = new MemoryStream(file);

        await Should.ThrowAsync<ValidationException>(() => _fixture.SendAsync(new DownloadCommand { File = new FormFile(stream, 0, 0, "file", "test.test") }));
    }

    private string GetTestDataDir()
    {
        var location = Assembly.GetExecutingAssembly().Location;
        var testDataDir = Path.GetDirectoryName(location) + @"\TestData";
        return testDataDir;
    }
}