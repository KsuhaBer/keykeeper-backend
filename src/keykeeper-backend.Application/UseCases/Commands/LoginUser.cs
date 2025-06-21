using BCrypt.Net;
using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Application.Interfaces;
using MediatR;

namespace keykeeper_backend.Application.UseCases.Commands
{
    public class LoginUserCommand: IRequest<LoginUserResponse>
    {
        public LoginUserRequest data { get; init; } = default!;
    }

    public class LoginUserResponse
    {
        public string Token { get; init; } = default!;
    }

    public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly IUserRepository _users;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHashers;
        private readonly IUnitOfWork _uow;

        public LoginUserHandler(IUserRepository users, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHashers)
        {
            _users = users;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHashers = passwordHashers;
        }

        public async Task<LoginUserResponse> Handle(LoginUserCommand cmd, CancellationToken ct)
        {
            var d = cmd.data;
            var user = await _users.GetByEmailAsync(d.Email, ct);
            if (user == null || !_passwordHashers.Verify(d.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials.");

            user.UpdateLastLogin();

            //await _uow.SaveChangesAsync(ct);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new LoginUserResponse
            {
                Token = token,
            };
        }
    }
}
