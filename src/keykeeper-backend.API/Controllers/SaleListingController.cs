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
        [HttpPost]
        public async Task<IActionResult> CreateSaleListing(CreateSaleListingRequest request, CancellationToken ct)
        {
            var command = new AddSaleListingCommand() { data = request };
            var result = await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetSaleListingWithFilter(
            [FromQuery] ListingFilterRequest filter,
            CancellationToken ct)
        {
            var command = new GetSaleListingsQuery { Filter = filter };
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("{id:int}/photos")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddPhoto(
        int id, IFormFile file, CancellationToken ct)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл пуст");

            var photoId = await _mediator.Send(
                new AddSaleListingPhotoCommand(id, file), ct);

            return CreatedAtAction(nameof(GetPhotos), new { id, photoId }, photoId);
        }
        

        [HttpGet("{id:int}/photos")]
        public async Task<IActionResult> GetPhotos(int id, CancellationToken ct)
        {
            var photos = await _mediator.Send(
                new GetSaleListingPhotosQuery(id), ct);
            return Ok(photos);
        }
    }
}
