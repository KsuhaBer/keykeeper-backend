using keykeeper_backend.Application.DTOs;
using keykeeper_backend.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.UseCases.Queries
{
    public record GetSaleListingPhotosQuery(int ListingId) : IRequest<IReadOnlyList<PhotoDTO>>;

    public sealed class GetSaleListingPhotosHandler
    : IRequestHandler<GetSaleListingPhotosQuery,
                      IReadOnlyList<PhotoDTO>>
    {
        private readonly ISaleListingRepository _repo;
        public GetSaleListingPhotosHandler(ISaleListingRepository repo) => _repo = repo;

        public async Task<IReadOnlyList<PhotoDTO>> Handle(GetSaleListingPhotosQuery q, CancellationToken ct)
        {
            var listing = await _repo.GetSaleListingsByIdAsync(q.ListingId, ct);

            return await _repo.GetPhotosByIdSaleListing(q.ListingId, ct);
        }
    }

}
