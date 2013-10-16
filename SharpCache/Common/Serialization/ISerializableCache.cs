namespace SharpCache.Common.Serialization
{
    #region Using Directives
    using System.Runtime.Serialization;
    #endregion

    public delegate byte[] Serializer(object value);

    public delegate ISerializableCache Deserializer(byte[] value);

    public interface ISerializableCache : ISerializable
    {
        byte[] Serialize();

        ISerializableCache Deserialize(byte[] value);
    }
}
