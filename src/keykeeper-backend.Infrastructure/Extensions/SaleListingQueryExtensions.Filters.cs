using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Domain.Entities;

namespace keykeeper_backend.Infrastructure.Extensions
{
    public static class SaleListingQueryFilterExtensions
    {
        public static IQueryable<SaleListing> ApplyFilters(
            this IQueryable<SaleListing> query,
            ListingFilterRequest filter)
        {
            query = query.Where(x => x.IsActive);

            if (filter.MinPrice.HasValue)
                query = query.Where(x => x.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(x => x.Price <= filter.MaxPrice.Value);

            if (filter.RoomCounts?.Any() == true)
                query = query.Where(x => x.RoomCount.HasValue && filter.RoomCounts.Contains(x.RoomCount.Value));

            if (filter.RegionId.HasValue)
                query = query.Where(x => x.Address.Settlement.Municipalite.RegionId == filter.RegionId.Value);

            if (filter.MunicipaliteId.HasValue)
                query = query.Where(x => x.Address.Settlement.MunicipalityId == filter.MunicipaliteId.Value);

            if (filter.SettlementId.HasValue)
                query = query.Where(x => x.Address.SettlementId == filter.SettlementId.Value);

            if (filter.DistrictId.HasValue)
                query = query.Where(x => x.Address.DistrictId == filter.DistrictId.Value);

            if (filter.PropertyTypeId.HasValue)
                query = query.Where(x => x.PropertyTypeId == filter.PropertyTypeId.Value);

            return query;
        }
    }
}