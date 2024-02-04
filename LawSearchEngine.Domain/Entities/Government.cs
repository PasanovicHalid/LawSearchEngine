using LawSearchEngine.Domain.Common.ObjectTypes;

namespace LawSearchEngine.Domain.Entities
{
    public class Government : Entity<Guid>
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
    }
}
