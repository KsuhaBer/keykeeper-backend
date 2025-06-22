using keykeeper_backend.Application.Interfaces;
using keykeeper_backend.Domain.Entities;
using keykeeper_backend.Infrastructure.KeykepperDbContext;
using Microsoft.EntityFrameworkCore;

namespace keykeeper_backend.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByIdAsync(int userId, CancellationToken ct)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId, ct);
        }
        public async Task CreateAsync(User user, CancellationToken ct)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            await _db.Users.AddAsync(user, ct);
        }

        public async Task<List<SaleListing>> GetFavoritesListings(int userId, CancellationToken ct)
        {
            return await _db.UserFavorites
                .Where(u => u.UserId == userId)
                .Select(u => u.SaleListing)
                .ToListAsync(ct);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
        {
            return await _db.Users.FirstOrDefaultAsync(u =>u.Email == email, ct);
        }

        public async Task UpdateAsync(User user, CancellationToken ct)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _db.Users.Update(user);
        }

        public async Task AddFavoriteListAsync(int userId, int saleListingId, CancellationToken ct)
        {
            await _db.UserFavorites.AddAsync(new UserFavorite(userId, saleListingId));
        }

        public async Task RemoveFavoriteListAsync(int userId, int saleListingId, CancellationToken ct)
        {
            await _db.UserFavorites.Where(uf => uf.UserId == userId && uf.SaleListingId == saleListingId).ExecuteDeleteAsync(ct);
        }

        public async Task DeleteAsync(int userId, CancellationToken ct)
        {
            await _db.Users.Where(u => u.UserId == userId).ExecuteDeleteAsync(ct);
        }
    }
}
