using keykeeper_backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.DTOs.Requests
{
    public class CreateSaleListingRequest
    {
        [Required]
        public int UserId { get; init; }

        [Required]
        public int PropertyTypeId {  get; init; }

        [Required]
        public string SettlementName { get; init; } = default!;

        [Required]
        public string MunicipaliteName { get; init; } = default!;
        [Required]
        public string RegionName { get; init; } = default!;

        public string StreetName { get; init; } = default!;

        public string DistrictName { get; init; } = default!;

        public string HouseNumber { get; init; } = default!;

        [Required]
        public double Latitude { get; init; }

        [Required]
        public double Longitude { get; init; }

        [Required, Range(1, int.MaxValue)]
        public int Price {  get; init; }

        [Required, MaxLength(1000)]
        public string Description { get; init; } = default!;

        public int? Floor {  get; init; }
        public double? Area { get; init; }
        public int? RoomCount { get; init; }
        public int? TotalFloors { get; init; }
    }
}
