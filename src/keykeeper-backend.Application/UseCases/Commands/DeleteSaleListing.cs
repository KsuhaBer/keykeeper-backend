using keykeeper_backend.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.UseCases.Commands
{
    public record class DeleteSaleListingCommand(int saleListingId): IRequest<int>;

    public class DeleteSaleListingHandler : IRequestHandler<DeleteSaleListingCommand, int>
    {
        private readonly ISaleListingRepository _saleListings;
        private readonly IUnitOfWork _uow;

        public DeleteSaleListingHandler(ISaleListingRepository saleListingRepository, IUnitOfWork uow)
        {
            _saleListings = saleListingRepository;
            _uow = uow;
        }

        public async Task<int> Handle(DeleteSaleListingCommand cmd, CancellationToken ct)
        {
            int id = await _saleListings.DeleteAsync(cmd.saleListingId, ct);
            await _uow.SaveChangesAsync(ct);
            return id;
        }
    }
}
