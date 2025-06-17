using keykeeper_backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.DTOs
{
    public class UserDTO
    {
        public int UserId { get; init; }
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string PhoneNumber { get; init; } = default!;
        public DateTime RegistrationDate {  get; init; }
        public DateTime LastLoginDate {  get; init; }

        public IReadOnlyCollection<SaleListingDTO> FavoriteListings { get; init; } = Array.Empty<SaleListingDTO>();
    }
}
