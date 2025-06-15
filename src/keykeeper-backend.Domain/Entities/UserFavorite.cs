namespace keykeeper_backend.Domain.Entities
{
    public class UserFavorite
    {
        public int UserId { get; private set; }
        public User User { get; private set; }

        public int SaleListingId { get; private set; }
        public SaleListing SaleListing { get; private set; }

        private UserFavorite() { }

        public UserFavorite(int userId, int saleListingId)
        {
            UserId = userId;
            SaleListingId = saleListingId;
        }
    }

}
