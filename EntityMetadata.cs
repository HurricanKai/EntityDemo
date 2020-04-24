namespace EntityDemo
{
    public struct EntityMetadata<T>
        : IEntityMetadata
    {
        public T Value { get; set; }

        object IEntityMetadata.Value
        {
            get => Value;
            set => Value = (T)value;
        }
        
        public int Type { get; }

        public EntityMetadata(T value, int type)
        {
            Value = value;
            Type = type;
        }
    }
}