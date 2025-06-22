using keykeeper_backend.Application.Interfaces;
using MediatR;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.UseCases.Commands
{
    public record DeleteUserCommand(int userId) : IRequest<int>;

    public class DeleteUserHandler: IRequestHandler<DeleteUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;

        public DeleteUserHandler(IUserRepository userRepository, IUnitOfWork uow) => (_userRepository,  _uow) = (userRepository, uow);

        public async Task<int> Handle(DeleteUserCommand command, CancellationToken ct)
        {
            await _userRepository.DeleteAsync(command.userId, ct);

            return command.userId;
        } 
    }
}
