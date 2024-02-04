using LawSearchEngine.Domain.Common.ObjectTypes;

namespace LawSearchEngine.Domain.Entities
{
    public class Permission : Entity<uint>
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
