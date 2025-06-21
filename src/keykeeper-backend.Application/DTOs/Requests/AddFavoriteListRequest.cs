using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.DTOs.Requests
{
    public class AddFavoriteListRequest
    {
        [Required]
        public int UserId { get; init; }

        [Required]
        public int SaleListingId { get; init; }
    }
}
