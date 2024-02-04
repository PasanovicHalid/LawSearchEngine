namespace LawSearchEngine.Domain.Common.ObjectTypes
{
    public abstract class Value
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var valueObject = (Value)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 193 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        public static bool operator ==(Value a, Value b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Value a, Value b)
        {
            return !(a == b);
        }
    }
}
