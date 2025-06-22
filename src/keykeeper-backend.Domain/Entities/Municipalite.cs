namespace keykeeper_backend.Domain.Entities
{
    public class Municipalite
    {
        public int MunicipalityId { get; private set; }
        public string MunicipalityName { get; private set; }
        public int RegionId { get; private set; }
        public Region Region { get; private set; }

        private readonly List<Settlement> _settlements = new();
        public ICollection<Settlement> Settlements => _settlements;

        private Municipalite() { }

        public Municipalite(string name, int regionId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя муниципалитета обязательно");
            MunicipalityName = name;
            RegionId = regionId;
        }
    }

}
