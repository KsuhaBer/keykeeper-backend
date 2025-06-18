using keykeeper_backend.Application.DTOs;
using keykeeper_backend.Application.Interfaces;
using MediatR;

namespace keykeeper_backend.Application.UseCases.Queries
{
    public class GetSaleListingsQuery : IRequest<PagedResponse<SaleListingDTO>>
    {
        public ListingFilterRequest Filter { get; init; } = default!;
    }

    public class GetSaleListingsHandler : IRequestHandler<GetSaleListingsQuery, PagedResponse<SaleListingDTO>>
    {
        private readonly ISaleListingRepository _listings;

        public GetSaleListingsHandler(ISaleListingRepository listings)
        {
            _listings = listings;
        }

        public async Task<PagedResponse<SaleListingDTO>> Handle(GetSaleListingsQuery query, CancellationToken ct)
        {
            var filter = query.Filter;

            var (items, totalCount) = await _listings.FilterWithPagingAsync(filter, ct);

            var result = new PagedResponse<SaleListingDTO>(
                items.Select(x => new SaleListingDTO
                {
                    SaleListingId = x.SaleListingId,
                    UserId = x.UserId,
                    PropertyTypeId = x.PropertyTypeId,
                    AddressId = x.AddressId,
                    Description = x.Description,
                    Price = x.Price,
                    ListingDate = x.ListingDate,
                    IsActive = x.IsActive,
                    Floor = x.Floor,
                    Area = x.Area,
                    RoomCount = x.RoomCount,
                    TotalFloors = x.TotalFloors
                }),
                totalCount,
                filter.Page,
                filter.PageSize
            );

            return result;
        }
    }


}
