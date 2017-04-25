namespace Org.Apache.Jute
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ZooKeeperNet.IO;

    public class BinaryOutputArchive : IOutputArchive
    {
        private readonly EndianBinaryWriter writer;

        public BinaryOutputArchive(EndianBinaryWriter writer)
        {
            this.writer = writer;
        }

        public void EndMap(SortedDictionary<string, string> v, string tag)
        {
        }

        public void EndRecord(IRecord r, string tag)
        {
        }

        public void EndVector<T>(IEnumerable<T> v, string tag)
        {
        }

        public static BinaryOutputArchive getArchive(EndianBinaryWriter writer) => 
            new BinaryOutputArchive(writer);

        public void StartMap(SortedDictionary<string, string> v, string tag)
        {
            this.WriteInt(v.Count, tag);
        }

        public void StartRecord(IRecord r, string tag)
        {
        }

        public void StartVector<T>(IEnumerable<T> v, string tag)
        {
            if (v == null)
            {
                this.WriteInt(-1, tag);
            }
            else
            {
                this.WriteInt(v.Count<T>(), tag);
            }
        }

        public void WriteBool(bool b, string tag)
        {
            this.writer.Write(b);
        }

        public void WriteBuffer(byte[] barr, string tag)
        {
            if (barr == null)
            {
                this.writer.Write(-1);
            }
            else
            {
                this.writer.Write(barr.Length);
                this.writer.Write(barr);
            }
        }

        public void WriteByte(byte b, string tag)
        {
            this.writer.Write(b);
        }

        public void WriteDouble(double d, string tag)
        {
            this.writer.Write(d);
        }

        public void WriteFloat(float f, string tag)
        {
            this.writer.Write(f);
        }

        public void WriteInt(int i, string tag)
        {
            this.writer.Write(i);
        }

        public void WriteLong(long l, string tag)
        {
            this.writer.Write(l);
        }

        public void WriteRecord(IRecord r, string tag)
        {
            if (r != null)
            {
                r.Serialize(this, tag);
            }
        }

        public void WriteString(string s, string tag)
        {
            if (s == null)
            {
                this.WriteInt(-1, "len");
            }
            else
            {
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                this.WriteInt(bytes.Length, "len");
                this.writer.Write(bytes, 0, bytes.Length);
            }
        }
    }
}

