using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.DTOs
{
    public class PagedResponse<T>
    {
        public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();
        public int TotalCount { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }

        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> items, int totalCount, int page, int pageSize)
        {
            Items = items.ToList();
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
        }
    }

}
