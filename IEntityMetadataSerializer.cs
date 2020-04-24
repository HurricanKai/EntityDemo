namespace EntityDemo
{
    public interface IEntityMetadataSerializer
    {
        object Deserialize(int type, byte[] data);
        byte[] Serialize(int type, object value);
    }
}