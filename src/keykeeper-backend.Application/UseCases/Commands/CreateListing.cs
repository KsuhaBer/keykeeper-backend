using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Application.Interfaces;
using keykeeper_backend.Domain.Entities;
using MediatR;
using NetTopologySuite.Geometries;
using System.Globalization;

namespace keykeeper_backend.Application.UseCases.Commands
{
    public class AddSaleListingCommand: IRequest<AddSaleListingResponse>
    {
        public CreateSaleListingRequest data { get; init; } = default!;
    }

    public class AddSaleListingResponse
    {
        public int ListingId { get; init; }
    }

    public class AddListingHandler: IRequestHandler<AddSaleListingCommand, AddSaleListingResponse>
    {
        private readonly ISaleListingRepository _saleListings;
        private readonly IUnitOfWork _uow;
        private readonly IAddressRepository _addresses;

        public AddListingHandler(ISaleListingRepository saleListings, IUnitOfWork uow, IAddressRepository addresses)
        {
            _saleListings = saleListings;
            _uow = uow;
            _addresses = addresses;
        }

        public async Task<AddSaleListingResponse> Handle(AddSaleListingCommand cmd, CancellationToken ct)
        {
            var d = cmd.data;

            var region = await _addresses.GetRegionByNameAsync(d.RegionName, ct) ?? await _addresses.AddRegionAsync(new Region(d.RegionName), ct);

            await _uow.SaveChangesAsync(ct);

            var municipalite = await _addresses.GetMunicipaliteByNameAsync(d.MunicipaliteName, ct) ?? await _addresses.AddMunicipaliteAsync(new Municipalite(d.MunicipaliteName, region.RegionId), ct);

            await _uow.SaveChangesAsync(ct);

            var settlement = await _addresses.GetSettlementByNameAsync(d.SettlementName, ct) ?? await _addresses.AddSettlementAsync(new Settlement(d.SettlementName, municipalite.MunicipalityId), ct);

            await _uow.SaveChangesAsync(ct);

            var street = d.StreetName != null ? await _addresses.GetStreetByNameAsync(d.StreetName, ct) ?? await _addresses.AddStreetAsync(new Street(d.StreetName), ct) : null;

            await _uow.SaveChangesAsync(ct);

            var district = d.DistrictName != null ? await _addresses.GetDistrictByNameAsync(d.DistrictName, ct) ?? await _addresses.AddDistrictAsync(new District(d.DistrictName), ct) : null;

            await _uow.SaveChangesAsync(ct);

            var address = await _addresses.GetAddressByLocationAsync(new Point(d.Longitude, d.Latitude), ct) ?? await _addresses.AddAddressAsync(new Address(settlement.SettlementId, new Point(d.Longitude, d.Latitude)), ct);
            address.SetHouseNumber(d.HouseNumber);
            address.SetDistrictId(district.DistrictId);
            address.SetStreetId(street.StreetId);

            await _uow.SaveChangesAsync(ct);

            var saleListing = new SaleListing(d.UserId, d.PropertyTypeId, address.AddressId, d.Price, d.Description, d.Floor, d.Area, d.RoomCount, d.TotalFloors);
            await _saleListings.AddAsync(saleListing, ct);
            await _uow.SaveChangesAsync(ct);

            return new AddSaleListingResponse() { ListingId = saleListing.SaleListingId };
        }
    }
}
