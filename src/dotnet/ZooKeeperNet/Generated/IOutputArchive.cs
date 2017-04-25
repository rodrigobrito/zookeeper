namespace Org.Apache.Jute
{
    using System;
    using System.Collections.Generic;

    public interface IOutputArchive
    {
        void EndMap(SortedDictionary<string, string> v, string tag);
        void EndRecord(IRecord r, string tag);
        void EndVector<T>(IEnumerable<T> v, string tag);
        void StartMap(SortedDictionary<string, string> v, string tag);
        void StartRecord(IRecord r, string tag);
        void StartVector<T>(IEnumerable<T> v, string tag);
        void WriteBool(bool b, string tag);
        void WriteBuffer(byte[] buf, string tag);
        void WriteByte(byte b, string tag);
        void WriteDouble(double d, string tag);
        void WriteFloat(float f, string tag);
        void WriteInt(int i, string tag);
        void WriteLong(long l, string tag);
        void WriteRecord(IRecord r, string tag);
        void WriteString(string s, string tag);
    }
}

