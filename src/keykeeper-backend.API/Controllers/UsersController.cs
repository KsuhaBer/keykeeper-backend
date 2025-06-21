using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Application.UseCases.Commands;
using keykeeper_backend.Application.UseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace keykeeper_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserRequest request, CancellationToken ct)
        {
            var command = new RegisterUserCommand() { Data = request };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken ct)
        {
            var command = new LoginUserCommand() { data = request };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPatch("add-favorite-sale-listing")]
        public async Task<IActionResult> AddFavoriteSaleListing(AddFavoriteListRequest request, CancellationToken ct)
        {
            var command = new AddFavoriteListCommand() { data = request };
            await _mediator.Send(command);
            return Ok();
        }

    }
}
