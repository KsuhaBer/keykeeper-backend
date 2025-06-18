using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.DTOs
{
    public class PagingRequest
    {
        [Range(1, int.MaxValue)]
        public int Page { get; init; } = 1;

        [Range(1, 100)]
        public int PageSize { get; init; } = 20;
    }

    public class ListingFilterRequest : PagingRequest
    {
        public int? MinPrice { get; init; }
        public int? MaxPrice { get; init; }

        public List<int>? RoomCounts { get; init; }
        public int? RegionId{ get; init; }
        public int? MunicipaliteId { get; init; }
        public int? PropertyTypeId { get; init; }
        public int? SettlementId { get; init; }
        public int? DistrictId { get; init; }

        public string? SortBy { get; init; }
        public bool SortDesc { get; init; } = false;
    }
}
