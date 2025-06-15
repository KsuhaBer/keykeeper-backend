namespace keykeeper_backend.Domain.Entities
{
    public class User
    {
        public int UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string Email { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public string PhoneNumber { get; private set; }
        public string PasswordHash { get; private set; }

        public int RoleId { get; private set; }
        public Role Role { get; private set; }

        public DateTime LastLoginDate { get; private set; }
        public bool IsDeleted { get; private set; }

        private readonly List<SaleListing> _listings = new();
        public IReadOnlyCollection<SaleListing> Listings => _listings;

        private readonly List<UserFavorite> _favorites = new();
        public IReadOnlyCollection<UserFavorite> Favorites => _favorites.AsReadOnly();

        private User() { }

        public User(string firstName, string lastName, string email, string phoneNumber, string passwordHash, int roleId)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email обязателен");
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordHash = passwordHash;
            RegistrationDate = DateTime.UtcNow;
            RoleId = roleId;
            LastLoginDate = RegistrationDate;
            IsDeleted = false;
        }

        public void UpdateLastLogin() => LastLoginDate = DateTime.UtcNow;

        public void MarkAsDeleted()
        {
            if (IsDeleted) throw new InvalidOperationException("Уже удалён");
            IsDeleted = true;
        }
    }

}
