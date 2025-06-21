using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.UseCases.Queries
{
    public static class SaleListingQueryableExtensions
    {
        public static IQueryable<SaleListing> ApplyFilters(
            this IQueryable<SaleListing> query,
            ListingFilterRequest filter)
        {
            if (filter.MinPrice.HasValue)
                query = query.Where(x => x.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(x => x.Price <= filter.MaxPrice.Value);

            if (filter.RoomCounts != null && filter.RoomCounts.Count > 0)
            {
                query = query.Where(x =>
                    x.RoomCount.HasValue &&
                    filter.RoomCounts.Contains(x.RoomCount.Value)
                );
            }


            if (filter.RegionId.HasValue)
                query = query.Where(x => x.Address.Settlement.Municipalite.RegionId
                                         == filter.RegionId.Value);

            if (filter.MunicipaliteId.HasValue)
                query = query.Where(x => x.Address.Settlement.MunicipalityId
                                         == filter.MunicipaliteId.Value);

            if (filter.SettlementId.HasValue)
                query = query.Where(x => x.Address.SettlementId
                                         == filter.SettlementId.Value);

            if (filter.DistrictId.HasValue)
                query = query.Where(x => x.Address.DistrictId
                                         == filter.DistrictId.Value);

            if (filter.PropertyTypeId.HasValue)
                query = query.Where(x => x.PropertyTypeId
                                         == filter.PropertyTypeId.Value);

            return query;
        }

        public static IQueryable<SaleListing> ApplySorting(
            this IQueryable<SaleListing> query,
            ListingFilterRequest filter)
        {
            if (string.IsNullOrWhiteSpace(filter.SortBy))
            {
                // По-умолчанию — по дате
                return filter.SortDesc
                    ? query.OrderByDescending(x => x.ListingDate)
                    : query.OrderBy(x => x.ListingDate);
            }

            // Пример: сортировка по полю из DTO (названия должны совпадать)
            return filter.SortBy.ToLower() switch
            {
                "price" => filter.SortDesc
                    ? query.OrderByDescending(x => x.Price)
                    : query.OrderBy(x => x.Price),

                "roomcount" => filter.SortDesc
                    ? query.OrderByDescending(x => x.RoomCount)
                    : query.OrderBy(x => x.RoomCount),

                "area" => filter.SortDesc
                    ? query.OrderByDescending(x => x.Area)
                    : query.OrderBy(x => x.Area),

                // Добавьте другие поля по потребности

                _ => filter.SortDesc
                    ? query.OrderByDescending(x => x.ListingDate)
                    : query.OrderBy(x => x.ListingDate)
            };
        }
    }

}
