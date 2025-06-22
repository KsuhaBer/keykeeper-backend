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

    public sealed class GetSaleListingByIdHandler
    : IRequestHandler<GetSaleListingByIdQuery, SaleListingDTO>
    {
        private readonly ISaleListingRepository _saleListings;

        public GetSaleListingByIdHandler(ISaleListingRepository saleListings)
        {
            _saleListings = saleListings;
        }

        public async Task<SaleListingDTO> Handle(
            GetSaleListingByIdQuery request,
            CancellationToken ct)
        {
            // Репозиторий возвращает доменную сущность `SaleListing`
            // (обязательно с Include-ами PropertyType и Address-веткой)
            var listing = await _saleListings.GetSaleListingsByIdAsync(
                request.SaleListingId, ct);

            if (listing == null)
                throw new KeyNotFoundException(
                    $"Sale listing #{request.SaleListingId} not found.");

            // ↙️ Маппим прямо в новый DTO-формат
            return new SaleListingDTO
            {
                SaleListingId = listing.SaleListingId,
                UserId = listing.UserId,
                PropertyTypeName = listing.PropertyType.PropertyTypeName,

                Address = new AddressDTO
                {
                    DistrictName = listing.Address.District?.DistrictName,
                    StreetName = listing.Address.Street?.StreetName,
                    SettlementName = listing.Address.Settlement.SettlementName,
                    HouseNumber = listing.Address.HouseNumber,
                    MunicipaliteName = listing.Address.Settlement
                                              .Municipalite.MunicipalityName,
                    RegionName = listing.Address.Settlement
                                              .Municipalite.Region.RegionName
                },

                Description = listing.Description,
                Price = listing.Price,
                ListingDate = listing.ListingDate,
                IsActive = listing.IsActive,
                Floor = listing.Floor,
                Area = listing.Area,
                RoomCount = listing.RoomCount,
                TotalFloors = listing.TotalFloors
            };
        }
    }

}
