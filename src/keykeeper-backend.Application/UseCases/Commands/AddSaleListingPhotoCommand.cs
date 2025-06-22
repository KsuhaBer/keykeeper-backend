using keykeeper_backend.Application.Interfaces;
using keykeeper_backend.domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace keykeeper_backend.Application.UseCases.Commands
{
    public record class AddSaleListingPhotoCommand(
        int ListingId,
        IFormFile File) : IRequest<int>;


    public sealed class AddSaleListingPhotoHandler
    : IRequestHandler<AddSaleListingPhotoCommand, int>
    {
        private readonly IFileStorage _storage;
        private readonly ISaleListingRepository _repo;
        private readonly IUnitOfWork _uow;

        public AddSaleListingPhotoHandler(IFileStorage storage,
                                          ISaleListingRepository repo,
                                          IUnitOfWork uow)
            => (_storage, _repo, _uow) = (storage, repo, uow);

        public async Task<int> Handle(AddSaleListingPhotoCommand c, CancellationToken ct)
        {
            var listing = await _repo.GetSaleListingsByIdAsync(c.ListingId, ct);

            var ext = Path.GetExtension(c.File.FileName);
            await using var s = c.File.OpenReadStream();
            var rel = await _storage.SaveAsync(s, ext, $"listings/{c.ListingId}", ct);

            var photo = new ListingPhoto(listing.SaleListingId, rel);
            
            await _storage.AddPhotoAsync(photo, ct);

            await _uow.SaveChangesAsync(ct);

            return photo.ListingPhotoId;
        }
    }
}
