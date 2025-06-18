using keykeeper_backend.Application.DTOs;
using keykeeper_backend.Application.Interfaces;
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

        public async Task<(IReadOnlyList<SaleListingDTO> Items, int TotalCount)> FilterWithPagingAsync(
            ListingFilterRequest filter,
            CancellationToken ct)
        {
            var query = _db.SaleListings
                .Include(x => x.Address)
                    .ThenInclude(a => a.Settlement)
                    .ThenInclude(s => s.Municipalite)
                .AsQueryable()
                .ApplyFilters(filter)
                .ApplySorting(filter);


            int totalCount = await query.CountAsync(ct);

            var items = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(x => new SaleListingDTO
                {
                    SaleListingId = x.SaleListingId,
                    UserId = x.UserId,
                    PropertyTypeId = x.PropertyTypeId,
                    AddressId = x.AddressId,
                    Floor = x.Floor,
                    Area = x.Area,
                    RoomCount = x.RoomCount,
                    Price = x.Price,
                    Description = x.Description,
                    ListingDate = x.ListingDate,
                    TotalFloors = x.TotalFloors,
                    IsActive = x.IsActive
                })
                .AsNoTracking()
                .ToListAsync(ct);

            return (items, totalCount);
        }

        public async Task<SaleListing?> GetSaleListingsByIdAsync(int saleListingId, CancellationToken ct)
        {
            return await _db.SaleListings.FirstOrDefaultAsync(s=> s.SaleListingId == saleListingId, ct);
        }
    }
}
