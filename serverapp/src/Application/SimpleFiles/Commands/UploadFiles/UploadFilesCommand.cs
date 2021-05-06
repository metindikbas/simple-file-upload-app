using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleFileUpload.Application.Common.Exceptions;
using SimpleFileUpload.Application.Common.Interfaces;
using SimpleFileUpload.Domain.Entities;
using SimpleFileUpload.Domain.Enums;

namespace SimpleFileUpload.Application.SimpleFiles.Commands.UploadFiles
{
    public class UploadFilesCommand : IRequest
    {
        public List<UploadFileDto> Files { get; set; } = new List<UploadFileDto>();
    }

    public class UploadFilesCommandHandler : IRequestHandler<UploadFilesCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UploadFilesCommandHandler(IApplicationDbContext applicationDbContext, IDateTimeProvider dateTimeProvider)
        {
            _applicationDbContext = applicationDbContext;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Unit> Handle(UploadFilesCommand request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            foreach (var entity in request.Files.Select(file =>
                new SimpleFile(file.FileName, _dateTimeProvider.Now, file.Content, file.Content.Length,
                    file.ContentType)))
            {
                await _applicationDbContext.SimpleFiles.AddAsync(entity, cancellationToken);
            }

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task ValidateRequest(UploadFilesCommand request)
        {
            var supportedContentTypes = await _applicationDbContext.UploadSettings
                .SingleOrDefaultAsync(x => x.Key == UploadSettingKeys.AllowedContentTypes);
            var maxAllowedFileSizeInBytesString = await _applicationDbContext.UploadSettings
                .SingleOrDefaultAsync(x => x.Key == UploadSettingKeys.MaxAllowedSingleFileSizeInBytes);
            var maxAllowedFileSizeInBytes = maxAllowedFileSizeInBytesString == null
                ? 0
                : int.Parse(maxAllowedFileSizeInBytesString.Value);

            foreach (var file in request.Files)
            {
                if (supportedContentTypes != null && !supportedContentTypes.Value.Contains(file.ContentType))
                    throw new UnsupportedMediaTypeException(file.FileName);
                if (maxAllowedFileSizeInBytes > 0 && file.Content.Length > maxAllowedFileSizeInBytes)
                    throw new MaximumSizeExceedsException(file.FileName);
            }
        }
    }
}