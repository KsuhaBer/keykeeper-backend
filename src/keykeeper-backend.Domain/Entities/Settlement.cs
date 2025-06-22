namespace keykeeper_backend.Domain.Entities
{
    public class Settlement
    {
        public int SettlementId { get; private set; }
        public string SettlementName { get; private set; }

        public int MunicipalityId { get; private set; }
        public Municipalite Municipalite { get; private set; }

        private readonly List<Address> _addresses = new();
        public ICollection<Address> Addresses => _addresses;

        private Settlement() { }

        public Settlement(string settlementName, int municipalityId)
        {
            if (string.IsNullOrWhiteSpace(settlementName))
                throw new ArgumentException("Название населённого пункта обязательно");
            SettlementName = settlementName;
            MunicipalityId = municipalityId;
        }
    }

}
