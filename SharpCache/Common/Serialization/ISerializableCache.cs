namespace SharpCache.Common.Serialization
{
    #region Using Directives
    using System.Runtime.Serialization;
    #endregion

    public interface ISerializableCache : ISerializable
    {
        byte[] Serialize();

        ISerializableCache Deserialize(byte[] value);
    }
}
