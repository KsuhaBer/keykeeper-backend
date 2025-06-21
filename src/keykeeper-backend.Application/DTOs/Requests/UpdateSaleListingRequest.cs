using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.DTOs.Requests
{
    public class UpdateSaleListingRequest
    {
        [Required]
        public int ListingID { get; init; }

        [Range(1, int.MaxValue)]
        public int Price { get; init; }

        [MaxLength(1000)]
        public string Description { get; init; } = default!;

        public bool? IsActive {  get; init; }
    }
}
