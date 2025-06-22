using keykeeper_backend.Application.Interfaces;
using keykeeper_backend.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.UseCases.Commands
{
    public record class RemoveFavoriteSaleListingCommand(int userId,int saleListingId): IRequest<int>;

    public class RemoveFavoriteSaleListingHandler : IRequestHandler<RemoveFavoriteSaleListingCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;
        public RemoveFavoriteSaleListingHandler(IUserRepository userRepository, IUnitOfWork uow) => (_userRepository, _uow) = (userRepository, uow);

        public async Task<int> Handle(RemoveFavoriteSaleListingCommand command, CancellationToken ct)
        {
            await _userRepository.RemoveFavoriteListAsync(command.userId, command.saleListingId, ct);
            return command.userId;
        }
    }
}
