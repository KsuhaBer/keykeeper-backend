using keykeeper_backend.Domain.Entities;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> GetAddressByIdAsync(int id, CancellationToken ct);
        Task<Address> AddAddressAsync(Address address, CancellationToken ct);

        Task<Address?> GetAddressByLocationAsync(Point location, CancellationToken ct);

        Task<District?> GetDistrictByNameAsync(string name, CancellationToken ct);
        Task<District> AddDistrictAsync(District district, CancellationToken ct);

        Task<Settlement?> GetSettlementByNameAsync(string name, CancellationToken ct);
        Task<Settlement> AddSettlementAsync(Settlement settlement, CancellationToken ct);

        Task<Municipalite?> GetMunicipaliteByNameAsync(string name, CancellationToken ct);
        Task<Municipalite> AddMunicipaliteAsync(Municipalite municipalite, CancellationToken ct);

        Task<Region?> GetRegionByNameAsync(string name, CancellationToken ct);
        Task<Region> AddRegionAsync(Region region, CancellationToken ct);

        Task<Street?> GetStreetByNameAsync(string name, CancellationToken ct);
        Task<Street> AddStreetAsync(Street street, CancellationToken ct);
    }
}
