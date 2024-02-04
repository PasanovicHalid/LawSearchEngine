namespace LawSearchEngine.Domain.Relationships
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public uint RoleId { get; set; }
    }
}
