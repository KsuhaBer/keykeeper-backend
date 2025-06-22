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

            #region Диапазон цены и количество комнат
            if (filter.MinPrice.HasValue)
                query = query.Where(x => x.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(x => x.Price <= filter.MaxPrice.Value);

            if (filter.RoomCounts?.Any() == true)
                query = query.Where(x =>
                    x.RoomCount.HasValue &&
                    filter.RoomCounts!.Contains(x.RoomCount.Value));
            #endregion


            if (!string.IsNullOrWhiteSpace(filter.RegionName))
                query = query.Where(x =>
                    x.Address.Settlement.Municipalite.Region.RegionName
                        .ToLower() == filter.RegionName!.Trim().ToLower());

            if (!string.IsNullOrWhiteSpace(filter.MunicipalityName))
                query = query.Where(x =>
                    x.Address.Settlement.Municipalite.MunicipalityName
                        .ToLower() == filter.MunicipalityName!.Trim().ToLower());

            if (!string.IsNullOrWhiteSpace(filter.SettlementName))
                query = query.Where(x =>
                    x.Address.Settlement.SettlementName
                        .ToLower() == filter.SettlementName!.Trim().ToLower());

            if (!string.IsNullOrWhiteSpace(filter.DistrictName))
                query = query.Where(x =>
                    x.Address.District != null && 
                    x.Address.District.DistrictName
                        .ToLower() == filter.DistrictName!.Trim().ToLower());

            if (!string.IsNullOrWhiteSpace(filter.PropertyTypeName))
                query = query.Where(x =>
                    x.PropertyType.PropertyTypeName
                        .ToLower() == filter.PropertyTypeName!.Trim().ToLower());

            return query;
        }


    }
}