using keykeeper_backend.Domain.Entities;

namespace keykeeper_backend.Application.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(User user, CancellationToken ct);

        Task<User?> GetByIdAsync(int userId, CancellationToken ct);

        Task<List<SaleListing>> GetFavoritesListings(int userId, CancellationToken ct);

        Task<User?> GetByEmailAsync(string email, CancellationToken ct);

        Task UpdateAsync(User user, CancellationToken ct);

        Task AddFavoriteListAsync(int userId, int saleListingId, CancellationToken ct);
    }
}
