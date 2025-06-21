using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Infrastructure.Extensions
{
    public static class SaleListingQuerySortingExtensions
    {
        public static IQueryable<SaleListing> ApplySorting(
            this IQueryable<SaleListing> query,
            ListingFilterRequest filter)
        {
            return filter.SortBy?.ToLower() switch
            {
                "price" => filter.SortDesc
                    ? query.OrderByDescending(x => x.Price)
                    : query.OrderBy(x => x.Price),

                "date" or "createdate" => filter.SortDesc
                    ? query.OrderByDescending(x => x.ListingDate)
                    : query.OrderBy(x => x.ListingDate),

                "roomcount" => filter.SortDesc
                    ? query.OrderByDescending(x => x.RoomCount)
                    : query.OrderBy(x => x.RoomCount),

                "settlement" => filter.SortDesc
                    ? query.OrderByDescending(x => x.Address.Settlement.SettlementName)
                    : query.OrderBy(x => x.Address.Settlement.SettlementName),

                _ => filter.SortDesc
                    ? query.OrderByDescending(x => x.SaleListingId)
                    : query.OrderBy(x => x.SaleListingId)
            };
        }
    }
}
