namespace Org.Apache.Jute
{
    using System;

    public interface IInputArchive
    {
        void EndMap(string tag);
        void EndRecord(string tag);
        void EndVector(string tag);
        bool ReadBool(string tag);
        byte[] ReadBuffer(string tag);
        byte ReadByte(string tag);
        double ReadDouble(string tag);
        float ReadFloat(string tag);
        int ReadInt(string tag);
        long ReadLong(string tag);
        void ReadRecord(IRecord r, string tag);
        string ReadString(string tag);
        IIndex StartMap(string tag);
        void StartRecord(string tag);
        IIndex StartVector(string tag);
    }
}

