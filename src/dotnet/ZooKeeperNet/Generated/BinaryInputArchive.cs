namespace Org.Apache.Jute
{
    using System;
    using System.IO;
    using System.Text;
    using ZooKeeperNet;
    using ZooKeeperNet.IO;

    public class BinaryInputArchive : IInputArchive
    {
        private readonly EndianBinaryReader reader;

        public BinaryInputArchive(EndianBinaryReader reader)
        {
            this.reader = reader;
        }

        public void EndMap(string tag)
        {
        }

        public void EndRecord(string tag)
        {
        }

        public void EndVector(string tag)
        {
        }

        public static BinaryInputArchive GetArchive(EndianBinaryReader reader) => 
            new BinaryInputArchive(reader);

        public bool ReadBool(string tag) => 
            this.reader.ReadBoolean();

        public byte[] ReadBuffer(string tag)
        {
            int num = this.ReadInt(tag);
            if (num == -1)
            {
                return null;
            }
            if ((num < 0) || (num > ClientConnection.MaximumPacketLength))
            {
                throw new IOException(new StringBuilder("Unreasonable length = ").Append(num).ToString());
            }
            return this.reader.ReadBytesOrThrow(num);
        }

        public byte ReadByte(string tag) => 
            this.reader.ReadByte();

        public double ReadDouble(string tag) => 
            this.reader.ReadDouble();

        public float ReadFloat(string tag) => 
            this.reader.ReadSingle();

        public int ReadInt(string tag) => 
            this.reader.ReadInt32();

        public long ReadLong(string tag) => 
            this.reader.ReadInt64();

        public void ReadRecord(IRecord r, string tag)
        {
            r.Deserialize(this, tag);
        }

        public string ReadString(string tag)
        {
            int count = this.reader.ReadInt32();
            if (count == -1)
            {
                return null;
            }
            byte[] bytes = this.reader.ReadBytesOrThrow(count);
            return Encoding.UTF8.GetString(bytes);
        }

        public IIndex StartMap(string tag) => 
            new BinaryIndex(this.ReadInt(tag));

        public void StartRecord(string tag)
        {
        }

        public IIndex StartVector(string tag)
        {
            int nelems = this.ReadInt(tag);
            if (nelems == -1)
            {
                return null;
            }
            return new BinaryIndex(nelems);
        }

        private class BinaryIndex : IIndex
        {
            private int nelems;

            internal BinaryIndex(int nelems)
            {
                this.nelems = nelems;
            }

            public bool Done() => 
                (this.nelems <= 0);

            public void Incr()
            {
                this.nelems--;
            }
        }
    }
}

