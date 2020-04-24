
using System.Numerics;

namespace EntityDemo
{
    public sealed class Entity
    {
        public Vector3 Position { get; }
        public Vector3 Velocity { get; }
        public IEntityMetadata[] Metadata { get; }

        public Entity(Vector3 position, Vector3 velocity, IEntityMetadata[] metadata)
        {
            Position = position;
            Velocity = velocity;
            Metadata = metadata;
        }
    }
}