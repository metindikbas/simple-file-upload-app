using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleFileUpload.Application.Common.Interfaces;
using SimpleFileUpload.Domain.Enums;

namespace SimpleFileUpload.Application.UploadSettings.Queries.ListUploadSettings
{
    public class ListUploadSettingsQuery : IRequest<List<UploadSettingDto>>
    {
    }

    public class ListUploadSettingsQueryHandler : IRequestHandler<ListUploadSettingsQuery, List<UploadSettingDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ListUploadSettingsQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ??
                                    throw new NullReferenceException(nameof(applicationDbContext));
        }

        public async Task<List<UploadSettingDto>> Handle(ListUploadSettingsQuery request,
            CancellationToken cancellationToken)
        {
            return await _applicationDbContext.UploadSettings
                .AsNoTracking()
                .Select(x => new UploadSettingDto
                {
                    Key = x.Key.ToString(),
                    Value = x.Value
                }).ToListAsync(cancellationToken);
        }
    }
}