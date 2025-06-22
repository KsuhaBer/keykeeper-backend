using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.DTOs.Requests
{
    public class PagingRequest
    {
        [Range(1, int.MaxValue)]
        public int Page { get; init; } = 1;

        [Range(1, 100)]
        public int PageSize { get; init; } = 20;
    }

    public sealed class ListingFilterRequest : PagingRequest
    {
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
        public IReadOnlyCollection<int>? RoomCounts { get; init; }

        public string? RegionName { get; init; }
        public string? MunicipalityName { get; init; }
        public string? SettlementName { get; init; }
        public string? DistrictName { get; init; }
        public string? PropertyTypeName { get; init; }

        public string? SortBy {  get; init; }
        public bool SortDesc { get; init; }
    }

}
