using keykeeper_backend.domain.Entities;

namespace keykeeper_backend.Domain.Entities
{
    public class SaleListing
    {
        public int SaleListingId { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }

        public int PropertyTypeId { get; private set; }
        public PropertyType PropertyType { get; private set; }

        public int AddressId { get; private set; }
        public Address Address { get; private set; }

        public int? Floor { get; private set; }
        public double? Area { get; private set; }
        public int? RoomCount { get; private set; }

        public int Price { get; private set; }
        public string Description { get; private set; }
        public DateTime ListingDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public int? TotalFloors { get; private set; }
        public bool IsActive { get; private set; } = true;

        private readonly List<UserFavorite> _favorites = new();
        public ICollection<UserFavorite> Favorites => _favorites;

        private readonly List<ListingPhoto> _photos = new();
        public ICollection<ListingPhoto> ListingPhotos => _photos;

        private SaleListing() { }

        public SaleListing(int userId, int propertyTypeId, int addressId, int price, string description, int? floor, double? area, int? roomCount, int? totalFloors)
        {
            if (price <= 0) throw new ArgumentException("Цена должна быть положительной");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Описание обязательно");

            UserId = userId;
            PropertyTypeId = propertyTypeId;
            AddressId = addressId;
            Price = price;
            Description = description;
            ListingDate = DateTime.UtcNow;
            LastUpdateDate = DateTime.UtcNow;
        }

        public void Active()
        {
            if (IsActive) throw new InvalidOperationException("Уже активно");
            IsActive = true;
        }

        public void Deactivate()
        {
            if (!IsActive) throw new InvalidOperationException("Уже не активно");
            IsActive = false;
        }

        public void UpdatePrice(int newPrice)
        {
            if (newPrice <= 0) throw new ArgumentException("Цена должна быть положительной");
            Price = newPrice;
            LastUpdateDate = DateTime.UtcNow;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
            LastUpdateDate = DateTime.UtcNow;
        }

        public void AddToFavorites(User user)
        {
            if (_favorites.Any(f => f.UserId == user.UserId))
                throw new InvalidOperationException("Уже в избранном");
            _favorites.Add(new UserFavorite(user.UserId, SaleListingId));
        }
    }

}
