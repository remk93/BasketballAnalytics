using BA.Core.Commands.File;
using BA.Core.Exceptions;
using BA.Core.Options;
using MediatR;
using Microsoft.Extensions.Options;

namespace BA.Core.Handlers.File
{
    public class UploadHandler : IRequestHandler<UploadCommand, MemoryStream>
    {
        private readonly FileStorageOptions _fileStorageOptions;

        public UploadHandler(
            IOptions<FileStorageOptions> fileStorageOptions)
        {
            _fileStorageOptions = fileStorageOptions.Value;
        }

        public async Task<MemoryStream> Handle(UploadCommand request, CancellationToken cancellationToken)
        {
            var path = Path.Combine(_fileStorageOptions.DownloadsFolder, request.Link);

            if (!System.IO.File.Exists(path))
                throw new NotFoundException($"'{request.Name}' was not found by link '{request.Link}'");

            var memory = new MemoryStream();
            await using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory, cancellationToken);
            }

            memory.Position = 0;

            return memory;
        }
    }
}
