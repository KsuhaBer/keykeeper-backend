using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Application.UseCases.Commands;
using keykeeper_backend.Application.UseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.Claims;

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
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken ct)
        {
            var command = new LoginUserCommand() { data = request };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> AddFavoriteSaleListing(AddFavoriteListRequest request, CancellationToken ct)
        {
            var command = new AddFavoriteListCommand() { data = request };
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize]
        [HttpDelete("favorite/{listingId}")]
        public async Task<IActionResult> RemoveFavoriteSaleListing([FromRoute] int listingId, CancellationToken ct)
        {
            var userIdString = User.FindFirstValue("sub");
            int userId = int.Parse(userIdString);
            var command = new RemoveFavoriteSaleListingCommand(userId, listingId);
            await _mediator.Send(command, ct);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken ct)
        {
            var command = new DeleteUserCommand(id);
            await _mediator.Send(command, ct); 
            return Ok();
        }
    }
}
