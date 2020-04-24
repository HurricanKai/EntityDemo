using System;
using System.Collections.Generic;
using System.Numerics;

namespace EntityDemo
{
    public sealed class EntityResolver : IEntityResolver
    {
        private Dictionary<int, IEntityFactory> _factories;

        public EntityResolver()
        {
            _factories = new Dictionary<int, IEntityFactory>();
            _factories[1] = new TestFactory();
        }
        
        private sealed class TestFactory : IEntityFactory
        {
            public Entity Create(Guid guid) => new Entity(Vector3.Zero, Vector3.Zero,
                new IEntityMetadata[] {new EntityMetadata<int>(0, 0)});
        }

        public IEntityFactory GetFactory(int id) => _factories[id];
    }
}