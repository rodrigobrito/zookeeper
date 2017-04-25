namespace Org.Apache.Jute
{
    using System;
    using ZooKeeperNet.IO;

    public class RecordReader
    {
        private readonly IInputArchive archive;

        public RecordReader(EndianBinaryReader reader, string format)
        {
            this.archive = new BinaryInputArchive(reader);
        }

        public void Read(IRecord r)
        {
            r.Deserialize(this.archive, "");
        }
    }
}

