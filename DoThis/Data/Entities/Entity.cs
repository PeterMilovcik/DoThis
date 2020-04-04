using System;
using System.Collections.Generic;

namespace Beeffective.Data.Entities
{
    public class Entity : IEquatable<Entity>
    {
        public int Id { get; set; }

        public bool Equals(Entity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Entity) obj);
        }

        public override int GetHashCode() => Id;

        public static bool operator ==(Entity left, Entity right) => 
            Equals(left, right);

        public static bool operator !=(Entity left, Entity right) => 
            !Equals(left, right);

        private sealed class IdEqualityComparer : IEqualityComparer<Entity>
        {
            public bool Equals(Entity x, Entity y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id;
            }

            public int GetHashCode(Entity obj) => obj.Id;
        }

        public static IEqualityComparer<Entity> IdComparer { get; } = new IdEqualityComparer();
    }
}