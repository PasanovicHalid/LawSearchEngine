using LawSearchEngine.Domain.Common.ObjectTypes;

namespace LawSearchEngine.Domain.Entities
{
    public class Role : Entity<uint>
    {
        public static Role Admin = new Role { Id = 1, Name = "Admin" };
        public static Role Agency = new Role { Id = 2, Name = "Agency" };
        public string Name { get; set; } = string.Empty;
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
