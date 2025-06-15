namespace keykeeper_backend.Domain.Entities
{
    public class Street
    {
        public int StreetId { get; private set; }
        public string StreetName { get; private set; }

        private readonly List<Address> _addresses = new();
        public IReadOnlyCollection<Address> Addresses => _addresses;

        private Street() { }

        public Street(string streetName)
        {
            if (string.IsNullOrWhiteSpace(streetName))
                throw new ArgumentException("Название улицы обязательно");
            StreetName = streetName;
        }
    }

}
