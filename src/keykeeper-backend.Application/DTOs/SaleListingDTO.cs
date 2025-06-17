using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.DTOs
{
    public class SaleListingDTO
    {
        public int SaleListingID { get; init; }
        public int UserID {  get; init; }
        public int PropertyTypeID { get; init; }
        public int AddressID {  get; init; }
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
