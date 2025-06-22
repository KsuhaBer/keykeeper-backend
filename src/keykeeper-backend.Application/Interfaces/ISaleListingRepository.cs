using keykeeper_backend.Application.DTOs;
using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.Interfaces
{
    public interface ISaleListingRepository
    {
        Task AddAsync(SaleListing saleListing, CancellationToken ct);
        Task<IReadOnlyCollection<SaleListing>> GetSaleListingsByUserAsync(int userId, CancellationToken ct);

        Task UpdateAsync(SaleListing saleListing, CancellationToken ct);
        Task<(IReadOnlyList<SaleListingDTO> Items, int TotalCount)> FilterWithPagingAsync(ListingFilterRequest filter, CancellationToken ct);

        Task<SaleListing?> GetSaleListingsByIdAsync(int saleListingId, CancellationToken ct);
        Task<List<PhotoDTO>> GetPhotosByIdSaleListing(int saleListing, CancellationToken ct);

        Task <int> DeleteAsync(int id, CancellationToken ct);
    }
}
