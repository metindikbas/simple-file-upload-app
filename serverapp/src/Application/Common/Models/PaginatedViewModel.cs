using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SimpleFileUpload.Application.Common.Models
{
    public class PaginatedViewModel<T>
    {
        private const int DefaultPageSize = 10;

        public List<T> Items { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public long TotalCount { get; set; }
        public long TotalPages { get; set; }

        public PaginatedViewModel(List<T> items, int pageSize, int pageNumber, long totalCount)
        {
            Items = items;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalCount = totalCount;
            TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);
        }

        public static async Task<PaginatedViewModel<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize is < 1 or > 10) pageSize = 10;
            
            var totalCount = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedViewModel<T>(items, pageSize, pageNumber, totalCount);
        }
    }
}