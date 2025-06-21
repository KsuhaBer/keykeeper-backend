using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Application.Interfaces;
using MediatR;

namespace keykeeper_backend.Application.UseCases.Commands
{
    public class UpdateSaleListingCommand: IRequest<UpdateSaleListingResponse>
    {
        public UpdateSaleListingRequest Data { get; init; } = default!;
    }

    public class UpdateSaleListingResponse
    {
        public int SaleListingID { get; init; }
    }

    public class UpdateSaleListingHandler : IRequestHandler<UpdateSaleListingCommand, UpdateSaleListingResponse>
    {
        private readonly ISaleListingRepository _saleListings;
        private readonly IUnitOfWork _uow;

        public UpdateSaleListingHandler(ISaleListingRepository saleListings, IUnitOfWork uow)
        {
            _saleListings = saleListings;
            _uow = uow;
        }

        public async Task<UpdateSaleListingResponse> Handle(UpdateSaleListingCommand cmd, CancellationToken ct)
        {
            var d = cmd.Data;

            var saleListing = await _saleListings.GetSaleListingsByIdAsync(d.ListingID, ct) ?? throw new ApplicationException("Такого обьявления нету");

            saleListing.UpdateDescription(d.Description);
            saleListing.UpdatePrice(d.Price);

            await _saleListings.UpdateAsync(saleListing, ct);
            await _uow.SaveChangesAsync(ct);

            return new UpdateSaleListingResponse()
            {
                SaleListingID = saleListing.SaleListingId
            };
        }
    }
}
