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

    public sealed class GetUserByIdHandler
    : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IUserRepository _users;

        public GetUserByIdHandler(IUserRepository users)
        {
            _users = users;
        }

        public async Task<UserDTO> Handle(
            GetUserByIdQuery request,
            CancellationToken ct)
        {
            var user = await _users.GetByIdAsync(request.UserId, ct);

            if (user is null)
                throw new KeyNotFoundException(
                    $"User with ID {request.UserId} not found.");

            return new UserDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                RegistrationDate = user.RegistrationDate,
                LastLoginDate = user.LastLoginDate,

                FavoriteListings = user.Favorites
                    .Select(f =>
                    {
                        var l = f.SaleListing;

                        return new SaleListingDTO
                        {
                            SaleListingId = l.SaleListingId,
                            UserId = l.UserId,
                            PropertyTypeName = l.PropertyType.PropertyTypeName,

                            Address = new AddressDTO
                            {
                                DistrictName = l.Address.District?.DistrictName,
                                StreetName = l.Address.Street?.StreetName,
                                SettlementName = l.Address.Settlement.SettlementName,
                                HouseNumber = l.Address.HouseNumber,
                                MunicipaliteName = l.Address.Settlement
                                                     .Municipalite.MunicipalityName,
                                RegionName = l.Address.Settlement
                                                     .Municipalite.Region.RegionName
                            },

                            Description = l.Description,
                            Price = l.Price,
                            ListingDate = l.ListingDate,
                            IsActive = l.IsActive,
                            Floor = l.Floor,
                            Area = l.Area,
                            RoomCount = l.RoomCount,
                            TotalFloors = l.TotalFloors
                        };
                    })
                    .ToList()
            };
        }
    }


}
