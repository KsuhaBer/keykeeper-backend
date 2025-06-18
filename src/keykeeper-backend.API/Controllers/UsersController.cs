using keykeeper_backend.Application.DTOs;
using keykeeper_backend.Application.UseCases.Commands;
using keykeeper_backend.Application.UseCases.Queries;
using MediatR;
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
            return Ok();
        }
    }
}
