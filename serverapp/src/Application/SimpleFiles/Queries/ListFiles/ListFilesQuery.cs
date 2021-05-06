using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleFileUpload.Application.Common.Interfaces;
using SimpleFileUpload.Application.Common.Mappings;
using SimpleFileUpload.Application.Common.Models;

namespace SimpleFileUpload.Application.SimpleFiles.Queries.ListFiles
{
    public class ListFilesQuery : IRequest<PaginatedViewModel<SimpleFileDto>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string ContentTypeFilter { get; set; }
    }

    public class ListFilesQueryHandler : IRequestHandler<ListFilesQuery, PaginatedViewModel<SimpleFileDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ListFilesQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ??
                                    throw new NullReferenceException(nameof(applicationDbContext));
        }

        public async Task<PaginatedViewModel<SimpleFileDto>> Handle(ListFilesQuery request,
            CancellationToken cancellationToken)
        {
            var queryable = _applicationDbContext.SimpleFiles.AsQueryable();
            if (!string.IsNullOrEmpty(request.ContentTypeFilter))
                queryable = queryable.Where(x => x.ContentType == request.ContentTypeFilter);

            var result = await queryable
                .Select(x => new SimpleFileDto
                {
                    Id = x.Id,
                    FileName = x.FileName,
                    UploadDate = x.UploadDate,
                    FileSizeInKb = (decimal)(x.ContentSize / 1024f),
                })
                .PaginatedListAsync(request.PageNumber, request.PageSize);
            return result;
        }
    }
}