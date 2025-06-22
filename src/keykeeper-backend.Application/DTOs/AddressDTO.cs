using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.DTOs
{
    public class AddressDTO
    {
        public string? DistrictName {  get; init; }
        public string? StreetName {  get; init; }
        public string SettlementName { get; init; } = default!;
        public string? HouseNumber { get; init; }
        public string MunicipaliteName { get; init; } = default!;
        public string RegionName {  get; init; } = default!;
    }
}
