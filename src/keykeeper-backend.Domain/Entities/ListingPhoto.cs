using keykeeper_backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.domain.Entities
{
    public class ListingPhoto
    {
        public int ListingPhotoId { get; private set; }
        public int SaleListingId { get; private set; }
        public SaleListing SaleListing { get; private set; }
        public string RelativePath { get; private set; }

        private ListingPhoto() { }
        public ListingPhoto(int listingId, string relPath)
        => (SaleListingId, RelativePath) = (listingId, relPath);
    }
}
