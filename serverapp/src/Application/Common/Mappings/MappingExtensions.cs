using System.Linq;
using System.Threading.Tasks;
using SimpleFileUpload.Application.Common.Models;

namespace SimpleFileUpload.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedViewModel<TDestination>> PaginatedListAsync<TDestination>(
            this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedViewModel<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
    }
}