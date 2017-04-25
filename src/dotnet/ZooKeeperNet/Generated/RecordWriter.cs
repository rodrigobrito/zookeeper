namespace Org.Apache.Jute
{
    using System;
    using ZooKeeperNet.IO;

    public class RecordWriter
    {
        private readonly BinaryOutputArchive archive;

        public RecordWriter(EndianBinaryWriter writer, string format)
        {
            this.archive = new BinaryOutputArchive(writer);
        }

        public void Write(IRecord r)
        {
            r.Serialize(this.archive, "");
        }
    }
}

