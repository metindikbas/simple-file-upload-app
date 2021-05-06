using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleFileUpload.Application.Common.Exceptions;
using SimpleFileUpload.Application.Common.Interfaces;
using SimpleFileUpload.Domain.Entities;

namespace SimpleFileUpload.Application.SimpleFiles.Queries.GetFile
{
    public class GetFileQuery : IRequest<SimpleFileViewDto>
    {
        public Guid Id { get; set; }
    }

    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, SimpleFileViewDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetFileQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ??
                                    throw new NullReferenceException(nameof(applicationDbContext));
        }

        public async Task<SimpleFileViewDto> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null) throw new ArgumentNullException(nameof(request.Id));
            var entity = await _applicationDbContext.SimpleFiles
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null) throw new EntityNotFoundException(nameof(SimpleFile), request.Id);
            return new SimpleFileViewDto
            {
                Id = entity.Id,
                ContentData = $"data:{entity.ContentType};base64,{Convert.ToBase64String(entity.Content)}",
                FileSizeInKb = (decimal)(entity.ContentSize / 1024f),
                FileName = entity.FileName,
                UploadDate = entity.UploadDate
            };
        }
    }
}