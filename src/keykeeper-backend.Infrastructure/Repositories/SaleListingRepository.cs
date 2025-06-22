using keykeeper_backend.Application.DTOs;
using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Application.Interfaces;
using keykeeper_backend.domain.Entities;
using keykeeper_backend.Domain.Entities;
using keykeeper_backend.Infrastructure.Extensions;
using keykeeper_backend.Infrastructure.KeykepperDbContext;
using Microsoft.EntityFrameworkCore;

namespace keykeeper_backend.Infrastructure.Repositories
{
    public class SaleListingRepository : ISaleListingRepository
    {
        private readonly AppDbContext _db;

        public SaleListingRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(SaleListing saleListing, CancellationToken ct)
        {
            await _db.SaleListings.AddAsync(saleListing, ct);
        }

        public async Task<IReadOnlyCollection<SaleListing>> GetSaleListingsByUserAsync(int userId, CancellationToken ct)
        {
            return await _db.SaleListings.Where(s => s.UserId == userId).ToListAsync(ct);
        }

        public async Task UpdateAsync(SaleListing saleListing, CancellationToken ct)
        {
            if(saleListing == null) throw new ArgumentNullException(nameof(saleListing));

            _db.SaleListings.Update(saleListing);
        }

        public async Task<(IReadOnlyList<SaleListingDTO> Items, int TotalCount)>
    FilterWithPagingAsync(ListingFilterRequest filter, CancellationToken ct)
        {
            var query = _db.SaleListings
                .AsNoTracking()

                .Include(l => l.PropertyType)
                .Include(l => l.Address)
                    .ThenInclude(a => a.District)
                .Include(l => l.Address)
                    .ThenInclude(a => a.Street)
                .Include(l => l.Address)
                    .ThenInclude(a => a.Settlement)
                        .ThenInclude(s => s.Municipalite)
                            .ThenInclude(m => m.Region)

                .ApplyFilters(filter)
                .ApplySorting(filter);

            var totalCount = await query.CountAsync(ct);

            var items = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(x => new SaleListingDTO
                {
                    SaleListingId = x.SaleListingId,
                    UserId = x.UserId,
                    PropertyTypeName = x.PropertyType.PropertyTypeName,

                    Address = new AddressDTO
                    {
                        DistrictName = x.Address.District != null
                                             ? x.Address.District.DistrictName
                                             : null,
                        StreetName = x.Address.Street != null
                                             ? x.Address.Street.StreetName
                                             : null,
                        SettlementName = x.Address.Settlement.SettlementName,
                        HouseNumber = x.Address.HouseNumber,
                        MunicipaliteName = x.Address.Settlement.Municipalite.MunicipalityName,
                        RegionName = x.Address.Settlement
                                               .Municipalite.Region.RegionName
                    },

                    Floor = x.Floor,
                    Area = x.Area,
                    RoomCount = x.RoomCount,
                    Price = x.Price,
                    Description = x.Description,
                    ListingDate = x.ListingDate,
                    TotalFloors = x.TotalFloors,
                    IsActive = x.IsActive
                })
                .ToListAsync(ct);

            return (items, totalCount);
        }



        public async Task<SaleListing?> GetSaleListingsByIdAsync(int saleListingId, CancellationToken ct)
        {
            return await _db.SaleListings.FirstOrDefaultAsync(s=> s.SaleListingId == saleListingId, ct);
        }

        public async Task<List<PhotoDTO>> GetPhotosByIdSaleListing(int saleListing, CancellationToken ct)
        {
            return await _db.ListingsPhotos
                .Where(p => p.SaleListingId == saleListing)
                .Select(p => new PhotoDTO()
                {
                    ListingPhotoId = p.ListingPhotoId,
                    Url = "/" + p.RelativePath
                })
                .ToListAsync(ct);
        }

        public async Task<int> DeleteAsync(int saleListingId, CancellationToken ct)
        {
            return await _db.SaleListings.Where(s => s.SaleListingId == saleListingId).ExecuteDeleteAsync(ct);
        }
    }
}
