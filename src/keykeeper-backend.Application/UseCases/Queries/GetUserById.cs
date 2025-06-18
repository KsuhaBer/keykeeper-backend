using keykeeper_backend.Application.DTOs;
using keykeeper_backend.Application.Interfaces;
using MediatR;

namespace keykeeper_backend.Application.UseCases.Queries
{
    public class GetUserByIdQuery : IRequest<UserDTO>
    {
        public int UserId { get; init; }
        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
        }
    }

    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IUserRepository _users;

        public GetUserByIdHandler(IUserRepository users)
        {
            _users = users;
        }

        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken ct)
        {
            var user = await _users.GetByIdAsync(request.UserId, ct);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {request.UserId} not found.");
            }

            return new UserDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber ?? "",
                RegistrationDate = user.RegistrationDate,
                LastLoginDate = user.LastLoginDate,
                FavoriteListings = user.Favorites
                    .Select(f => new SaleListingDTO {
                        SaleListingId = f.SaleListing.SaleListingId,
                        UserId = f.SaleListing.UserId,
                        AddressId = f.SaleListing.AddressId,
                        PropertyTypeId = f.SaleListing.PropertyTypeId,
                        Description = f.SaleListing.Description,
                        Price = f.SaleListing.Price,
                        ListingDate = f.SaleListing.ListingDate,
                        IsActive = f.SaleListing.IsActive,
                        Floor = f.SaleListing.Floor,
                        Area = f.SaleListing.Area,
                        RoomCount = f.SaleListing.RoomCount,
                        TotalFloors = f.SaleListing.TotalFloors
                    })
                    .ToList()
            };
        }
    }

}
