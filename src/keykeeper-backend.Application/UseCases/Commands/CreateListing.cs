using keykeeper_backend.Application.DTOs;
using keykeeper_backend.Application.Interfaces;
using keykeeper_backend.Domain.Entities;
using MediatR;

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
        private readonly ISaleListingRepository _saleListingRepositores;
        private readonly IUnitOfWork _uow;

        public AddListingHandler(ISaleListingRepository saleListingRepositores, IUnitOfWork uow)
        {
            _saleListingRepositores = saleListingRepositores;
            _uow = uow;
        }

        public async Task<AddSaleListingResponse> Handle(AddSaleListingCommand cmd, CancellationToken ct)
        {
            var d = cmd.data;

            var listing = new SaleListing(d.UserId, d.PropertyTypeId, d.AddressId, d.Price, d.Description, d.Floor, d.Area, d.RoomCount, d.TotalFloors);

            await _saleListingRepositores.AddAsync(listing, ct);
            await _uow.SaveChangesAsync(ct);

            return new AddSaleListingResponse
            {
                ListingId = listing.SaleListingId
            };
        }
    }
}
