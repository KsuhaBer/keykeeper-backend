using keykeeper_backend.Application.DTOs.Requests;
using keykeeper_backend.Application.UseCases.Commands;
using keykeeper_backend.Application.UseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keykeeper_backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SaleListingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SaleListingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("create-sale-listing")]
        public async Task<IActionResult> CreateSaleListing(CreateSaleListingRequest request, CancellationToken ct)
        {
            var command = new AddSaleListingCommand() { data = request };
            var result = await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("get-with-filter")]
        public async Task<IActionResult> GetSaleListingWithFilter(
            [FromQuery] ListingFilterRequest filter,
            CancellationToken ct)
        {
            var command = new GetSaleListingsQuery { Filter = filter };
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

    }
}
