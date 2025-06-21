using keykeeper_backend.Application.Interfaces;
using keykeeper_backend.Domain.Entities;
using keykeeper_backend.Infrastructure.KeykepperDbContext;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace keykeeper_backend.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _db;

        public AddressRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Address> GetAddressByIdAsync(int id, CancellationToken ct)
        {
            return await _db.Addresses.FirstOrDefaultAsync(a => a.AddressId == id, ct);
        }

        public async Task<Address> AddAddressAsync(Address address, CancellationToken ct)
        {
            await _db.Addresses.AddAsync(address, ct);
            return address;
        }

        public async Task<Address> GetAddressByLocationAsync(Point location, CancellationToken ct)
        {
            return await _db.Addresses.FirstOrDefaultAsync(a => a.Location == location, ct);
        }

        public async Task<District> GetDistrictByNameAsync(string name, CancellationToken ct)
        {
            return await _db.Districts.FirstOrDefaultAsync(d => d.DistrictName == name, ct);
        }

        public async Task<District> AddDistrictAsync(District district, CancellationToken ct)
        {
            await _db.Districts.AddAsync(district, ct);
            return district;
        }

        public async Task<Settlement> GetSettlementByNameAsync(string name, CancellationToken ct)
        {
            return await _db.Settlements.FirstOrDefaultAsync(s => s.SettlementName == name, ct);
        }

        public async Task<Settlement> AddSettlementAsync(Settlement settlement, CancellationToken ct)
        {
            await _db.Settlements.AddAsync(settlement, ct);
            return settlement;
        }

        public async Task<Municipalite> GetMunicipaliteByNameAsync(string name, CancellationToken ct)
        {
            return await _db.Municipalites.FirstOrDefaultAsync(m => m.MunicipalityName == name, ct);
        }

        public async Task<Municipalite> AddMunicipaliteAsync(Municipalite municipalite, CancellationToken ct)
        {
            await _db.Municipalites.AddAsync(municipalite, ct);
            return municipalite;
        }

        public async Task<Region> GetRegionByNameAsync(string name, CancellationToken ct)
        {
            return await _db.Regions.FirstOrDefaultAsync(r => r.RegionName == name, ct);
        }

        public async Task<Region> AddRegionAsync(Region region, CancellationToken ct)
        {
            await _db.Regions.AddAsync(region, ct);
            return region;
        }

        public async Task<Street> GetStreetByNameAsync(string name, CancellationToken ct)
        {
            return await _db.Streets.FirstOrDefaultAsync(s => s.StreetName == name, ct);
        }

        public async Task<Street> AddStreetAsync(Street street, CancellationToken ct)
        {
            await _db.Streets.AddAsync(street, ct);
            return street;
        }
    }
}
