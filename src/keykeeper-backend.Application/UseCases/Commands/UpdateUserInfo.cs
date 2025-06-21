using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Application.Interfaces;
using keykeeper_backend.Domain.Entities;
using MediatR;

namespace keykeeper_backend.Application.UseCases.Commands
{
    public class UpdateUserInfoCommand: IRequest<UpdateUserInfoResponse>
    {
        public UpdateUserInfoRequest Data { get; init; } = default!;
    }

    public class UpdateUserInfoResponse
    {
        public int UserId { get; init; } = default!;
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string? PhoneNumber { get; init; } = default!;
    }

    public class UpdateUserInfoHandler : IRequestHandler<UpdateUserInfoCommand, UpdateUserInfoResponse>
    {
        private readonly IUserRepository _users;
        private readonly IUnitOfWork _uow;

        public UpdateUserInfoHandler(IUserRepository users, IUnitOfWork uow)
        {
            _users = users;
            _uow = uow;
        }

        public async Task<UpdateUserInfoResponse> Handle(UpdateUserInfoCommand cmd, CancellationToken ct)
        {
            var d = cmd.Data;

            var user = await _users.GetByIdAsync(d.UserId, ct)
                   ?? throw new ApplicationException("Пользователь не найден");

            user.UpdateName(d.FirstName, d.LastName);
            user.UpdateEmail(d.Email);
            user.UpdatePhoneNumber(d.PhoneNumber);

            await _users.UpdateAsync(user, ct);
            await _uow.SaveChangesAsync(ct);

            return new UpdateUserInfoResponse()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
        }
    }
}
