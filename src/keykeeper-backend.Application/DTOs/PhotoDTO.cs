using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.DTOs
{
    public class PhotoDTO
    {
        public int ListingPhotoId { get; init; }
        public string Url { get; init; } = default!;
    }


}
