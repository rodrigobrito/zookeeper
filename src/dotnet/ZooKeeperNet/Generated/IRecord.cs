namespace Org.Apache.Jute
{
    using System;

    public interface IRecord
    {
        void Deserialize(IInputArchive archive, string tag);
        void Serialize(IOutputArchive archive, string tag);
    }
}

