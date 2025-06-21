using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.UseCases.Commands
{
    public class AddFavoriteListCommand : IRequest
    {
        public AddFavoriteListRequest data { get; init; } = default!;
    }

    public class AddFavoriteListHandler : IRequestHandler<AddFavoriteListCommand>
    {
        private readonly IUserRepository _users;
        private readonly IUnitOfWork _uow;

        public AddFavoriteListHandler(IUserRepository users, IUnitOfWork uow)
        {
            _users = users;
            _uow = uow;
        }

        public async Task Handle(AddFavoriteListCommand cmd, CancellationToken ct)
        {
            var d = cmd.data;
            await _users.AddFavoriteListAsync(d.UserId, d.SaleListingId, ct);
            await _uow.SaveChangesAsync(ct);
        }
    }
}
