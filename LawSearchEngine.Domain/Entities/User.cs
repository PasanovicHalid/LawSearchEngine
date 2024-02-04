using LawSearchEngine.Domain.Common.ObjectTypes;

namespace LawSearchEngine.Domain.Entities
{
    public class User : Entity<Guid>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
