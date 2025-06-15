namespace keykeeper_backend.Domain.Entities
{
    public class Region
    {
        public int RegionId { get; private set; }
        public string RegionName { get; private set; }

        private readonly List<Municipalite> _municipalites = new();
        public IReadOnlyCollection<Municipalite> Municipalites => _municipalites;

        private Region() { }

        public Region(string regionName)
        {
            if (string.IsNullOrWhiteSpace(regionName))
                throw new ArgumentException("Название региона обязательно");
            RegionName = regionName;
        }
    }

}
