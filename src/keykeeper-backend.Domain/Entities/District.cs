namespace keykeeper_backend.Domain.Entities
{
    public class District
    {
        public int DistrictId { get; private set; }
        public string DistrictName { get; private set; }

        private readonly List<Address> _addresses = new();
        public IReadOnlyCollection<Address> Addresses => _addresses;

        private District() { }

        public District(string districtName)
        {
            if (string.IsNullOrWhiteSpace(districtName))
                throw new ArgumentException("Имя района обязательно");
            DistrictName = districtName;
        }
    }

}
