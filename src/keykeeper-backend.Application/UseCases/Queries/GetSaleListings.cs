using keykeeper_backend.Application.DTOs;
using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Application.Interfaces;
using MediatR;

namespace keykeeper_backend.Application.UseCases.Queries
{
    public class GetSaleListingsQuery : IRequest<PagedResponse<SaleListingDTO>>
    {
        public ListingFilterRequest Filter { get; init; } = default!;
    }



    public class GetSaleListingsHandler
    : IRequestHandler<GetSaleListingsQuery, PagedResponse<SaleListingDTO>>
    {
        private readonly ISaleListingRepository _listings;

        public GetSaleListingsHandler(ISaleListingRepository listings) =>
            _listings = listings;

        public async Task<PagedResponse<SaleListingDTO>> Handle(
            GetSaleListingsQuery request, CancellationToken ct)
        {
            var (items, total) = await _listings
                .FilterWithPagingAsync(request.Filter, ct);

            return new PagedResponse<SaleListingDTO>(
                items, total, request.Filter.Page, request.Filter.PageSize);
        }
    }

}
