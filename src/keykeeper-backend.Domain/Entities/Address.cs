using NetTopologySuite.Geometries;

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
        public Point Location { get; private set; }

        private readonly List<SaleListing> _saleListings = new();
        public IReadOnlyCollection<SaleListing> SaleListings => _saleListings;

        private Address() { }

        public Address(int settlementId, Point location)
        {
            SettlementId = settlementId;
            Location = location;
        }

        public void SetHouseNumber(string? house)
        {
            HouseNumber = house;
        }
        public void SetDistrictId(int? districtId)
        {
            DistrictId = districtId;
        }
        public void SetStreetId(int? streetId)
        {
            StreetId = streetId;
        }
    }
}
