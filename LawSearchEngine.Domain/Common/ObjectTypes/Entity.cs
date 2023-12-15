using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Domain.Common.ObjectTypes
{
    public abstract class Entity<KeyType> where KeyType : notnull
    {
        public KeyType Id { get; set; } = default!;
        public Guid Guid { get; set; }
        public bool Deleted { get; set; }
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

        protected Entity() { }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var entity = (Entity<KeyType>)obj;

            return Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<KeyType>? a, Entity<KeyType>? b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<KeyType>? a, Entity<KeyType>? b)
        {
            return !(a == b);
        }
    }
}
