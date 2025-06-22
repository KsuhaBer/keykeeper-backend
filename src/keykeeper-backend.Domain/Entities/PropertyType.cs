namespace keykeeper_backend.Domain.Entities
{
    public class PropertyType
    {
        public int PropertyTypeId { get; private set; }
        public string PropertyTypeName { get; private set; }

        private readonly List<SaleListing> _saleListings = new();
        public ICollection<SaleListing> SaleListings => _saleListings;

        private PropertyType() { }

        public PropertyType(string propertyTypeName)
        {
            if (string.IsNullOrWhiteSpace(propertyTypeName))
                throw new ArgumentException("Тип недвижимости обязателен");
            PropertyTypeName = propertyTypeName;
        }
    }

}
