namespace EntityDemo
{
    public interface IEntityMetadata
    {
        object Value { get; set; }
        int Type { get; }
    }
}