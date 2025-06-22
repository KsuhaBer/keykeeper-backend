using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.DTOs
{
    public class SaleListingDTO
    {
        public int SaleListingId { get; init; }
        public int UserId {  get; init; }
        public string PropertyTypeName { get; init; } = default!;
        public AddressDTO Address {  get; init; } = new AddressDTO();
        public int? Floor {  get; init; }
        public double? Area { get; init; }
        public int? RoomCount { get; init; }
        public int Price {  get; init; }
        public string Description { get; init; } = default!;
        public DateTime ListingDate { get; init; }
        public int? TotalFloors { get; init; }
        public bool IsActive { get; init; }
    }
}
