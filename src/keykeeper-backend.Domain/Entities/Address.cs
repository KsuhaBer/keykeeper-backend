namespace keykeeper_backend.Domain.Entities
{
    public class Address
    {
        public int AddressId { get; private set; }
        public int? DistrictId { get; private set; }
        public District? District { get; private set; }
        public int? StreetId { get; private set; }
        public Street? Street { get; private set; }
        public int SettlementId { get; private set; }
        public Settlement Settlement { get; private set; }
        public string? HouseNumber { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        private readonly List<SaleListing> _saleListings = new();
        public IReadOnlyCollection<SaleListing> SaleListings => _saleListings;

        private Address() { }

        public Address(int settlementId, double latitude, double longitude)
        {
            SettlementId = settlementId;
            Latitude = latitude;
            Longitude = longitude;
        }

        public void SetHouseNumber(string house)
        {
            if (string.IsNullOrWhiteSpace(house))
                throw new ArgumentException("Номер дома не может быть пустым");
            HouseNumber = house;
        }
    }

}
