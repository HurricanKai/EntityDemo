using System;
using System.Collections.Generic;
using System.Numerics;

namespace EntityDemo
{
    public sealed class LocalEntityManager : IEntityManager
    {
        private readonly IEntityResolver _entityResolver;
        private readonly Dictionary<Guid, Entity> _entities;
        private readonly IEntityMetadataSerializer _metadataSerializer;
        public LocalEntityManager(IEntityResolver entityResolver, IEntityMetadataSerializer metadataSerializer)
        {
            _entityResolver = entityResolver;
            _metadataSerializer = metadataSerializer;
            _entities = new Dictionary<Guid, Entity>();
        }

        public Guid Create(int typeId)
        {
            var id = Guid.NewGuid();
            var factory = _entityResolver.GetFactory(typeId);
            var entity = factory.Create(id);
            _entities[id] = entity;
            return id;
        }

        public void UpdateMetadata(Guid entityId, int index, int type, byte[] data)
        {
            var entity = _entities[entityId];
            var value = _metadataSerializer.Deserialize(type, data);

            if (entity.Metadata[index].Type != type)
                throw new ArgumentOutOfRangeException(nameof(type), "Mismatched Type");

            entity.Metadata[index].Value = value;
        }

        public byte[] GetMetadata(Guid entityId, int index, int type)
        {
            var entity = _entities[entityId];
            var metadata = entity.Metadata[index];
            if (metadata.Type != type)
                throw new ArgumentOutOfRangeException(nameof(type), "Mismatched Type");

            return _metadataSerializer.Serialize(metadata.Type, metadata.Value);
        }

        public (Vector3, Vector3) GetTransform(Guid entityId)
        {
            var entity = _entities[entityId];
            return (entity.Position, entity.Velocity);
        }

        public void UpdateTransform(Guid entityId, Vector3 position, Vector3 velocity)
        {
            _entities[entityId].Position = position;
            _entities[entityId].Velocity = velocity;
        }
    }
}