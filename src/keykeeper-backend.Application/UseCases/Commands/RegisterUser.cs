using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Application.Interfaces;
using keykeeper_backend.Domain.Entities;
using MediatR;

namespace keykeeper_backend.Application.UseCases.Commands
{
    public class RegisterUserCommand : IRequest<RegisterUserResponse>
    {
        public CreateUserRequest Data { get; init; } = default!;
    }
    public class RegisterUserResponse
    {
        public int UserId { get; init; }
        public string Email { get; init; } = default!;
    }

    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHasher _hasher;
        private readonly IUnitOfWork _uow;

        public RegisterUserHandler(IUserRepository users, IPasswordHasher hasher, IUnitOfWork uow)
        {
            _users = users;
            _hasher = hasher;
            _uow = uow;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand cmd, CancellationToken ct)
        {
            var d = cmd.Data;
            if (await _users.GetByEmailAsync(d.Email, ct) != null)
                throw new ApplicationException("Email занят");

            var hash = _hasher.Hash(d.Password);
            var user = new User(d.FirstName, d.LastName, d.Email, hash, roleId: 2);

            await _users.CreateAsync(user, ct);
            await _uow.SaveChangesAsync(ct);

            return new RegisterUserResponse
            {
                UserId = user.UserId,
                Email = user.Email
            };
        }
    }
}
