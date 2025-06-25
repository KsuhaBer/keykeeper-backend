namespace keykeeper_backend.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; private set; }
        public string RoleName { get; private set; }

        private readonly List<User> _users = new();
        public ICollection<User> Users => _users;

        private Role() { }

        public Role(int roleId, string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("Имя роли обязательно");
            RoleName = roleName;
            RoleId = roleId;
        }
    }

}
