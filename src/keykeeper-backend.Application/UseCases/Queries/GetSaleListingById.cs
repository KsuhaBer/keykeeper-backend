using keykeeper_backend.Application.DTOs;
using keykeeper_backend.Application.Interfaces;
using MediatR;

namespace keykeeper_backend.Application.UseCases.Queries
{
    public class GetSaleListingByIdQuery: IRequest<SaleListingDTO>
    {
        public int SaleListingId {  get; init; }
        public GetSaleListingByIdQuery(int saleListingId)
        {
            SaleListingId = saleListingId;
        }
    }

    public class GetSaleListingByIdHandler: IRequestHandler<GetSaleListingByIdQuery, SaleListingDTO>
    {
        private readonly ISaleListingRepository _saleListings;

        public GetSaleListingByIdHandler(ISaleListingRepository saleListings)
        {
            _saleListings = saleListings;
        }

        public async Task<SaleListingDTO> Handle(GetSaleListingByIdQuery request, CancellationToken ct)
        {
            var SaleListingDTO = await _saleListings.GetSaleListingsByIdAsync(request.SaleListingId, ct);
            if (SaleListingDTO == null)
            {
                throw new KeyNotFoundException($"Sale Listing with ID {request.SaleListingId} not found");
            }

            return new SaleListingDTO
            {
                SaleListingID = SaleListingDTO.SaleListingId,
                UserID = SaleListingDTO.UserId,
                PropertyTypeID = SaleListingDTO.PropertyTypeId,
                AddressID = SaleListingDTO.AddressId,
                Floor = SaleListingDTO.Floor,
                Area = SaleListingDTO.Area,
                RoomCount = SaleListingDTO.RoomCount,
                Price = SaleListingDTO.Price,
                Description = SaleListingDTO.Description,
                ListingDate = SaleListingDTO.ListingDate,
                TotalFloors = SaleListingDTO.TotalFloors,
                IsActive = SaleListingDTO.IsActive
            };
        }
    }
}
