using System;
using System.Buffers.Binary;

namespace EntityDemo
{
    public sealed class EntityMetadataSerializer : IEntityMetadataSerializer
    {
        public object Deserialize(int type, byte[] data) =>
            type switch
            {
                0 => DeserializeInt32(data),
                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Invalid Type {type}")
            };

        private Int32 DeserializeInt32(byte[] data) => BinaryPrimitives.ReadInt32BigEndian(data);

        public byte[] Serialize(int type, object value) =>
            type switch
            {
                0 => SerializeInt32((Int32)value),
                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Invalid Type {type}")
            };

        private byte[] SerializeInt32(Int32 value)
        {
            var data = new byte[sizeof(Int32)];
            BinaryPrimitives.WriteInt32BigEndian(data, value);
            return data;
        }
    }
}