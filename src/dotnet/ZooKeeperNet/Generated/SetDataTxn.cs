namespace Org.Apache.Zookeeper.Txn
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class SetDataTxn : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(SetDataTxn));

        public SetDataTxn()
        {
        }

        public SetDataTxn(string path, byte[] data, int version)
        {
            this.Path = path;
            this.Data = data;
            this.Version = version;
        }

        public int CompareTo(object obj)
        {
            SetDataTxn txn = (SetDataTxn) obj;
            if (txn == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = this.Path.CompareTo(txn.Path);
            if (num == 0)
            {
                num = this.Data.CompareTo(txn.Data);
                if (num != 0)
                {
                    return num;
                }
                num = (this.Version == txn.Version) ? 0 : ((this.Version < txn.Version) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
            }
            return num;
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Path = a_.ReadString("path");
            this.Data = a_.ReadBuffer("data");
            this.Version = a_.ReadInt("version");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            SetDataTxn objA = (SetDataTxn) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Path.Equals(objA.Path);
            if (flag)
            {
                flag = this.Data.Equals(objA.Data);
                if (!flag)
                {
                    return flag;
                }
                flag = this.Version == objA.Version;
                if (!flag)
                {
                    return flag;
                }
            }
            return flag;
        }

        public override int GetHashCode()
        {
            int num = 0x11;
            int hashCode = base.GetType().GetHashCode();
            num = (0x25 * num) + hashCode;
            hashCode = this.Path.GetHashCode();
            num = (0x25 * num) + hashCode;
            hashCode = this.Data.GetHashCode();
            num = (0x25 * num) + hashCode;
            hashCode = this.Version;
            return ((0x25 * num) + hashCode);
        }

        public void ReadFields(EndianBinaryReader reader)
        {
            BinaryInputArchive archive = new BinaryInputArchive(reader);
            this.Deserialize(archive, string.Empty);
        }

        public void Serialize(IOutputArchive a_, string tag)
        {
            a_.StartRecord(this, tag);
            a_.WriteString(this.Path, "path");
            a_.WriteBuffer(this.Data, "data");
            a_.WriteInt(this.Version, "version");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LSetDataTxn(sBi)";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteString(this.Path, "path");
                    archive.WriteBuffer(this.Data, "data");
                    archive.WriteInt(this.Version, "version");
                    archive.EndRecord(this, string.Empty);
                    stream.Position = 0L;
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }
            catch (Exception exception)
            {
                log.Error(exception);
            }
            return "ERROR";
        }

        public void Write(EndianBinaryWriter writer)
        {
            BinaryOutputArchive archive = new BinaryOutputArchive(writer);
            this.Serialize(archive, string.Empty);
        }

        public byte[] Data { get; set; }

        public string Path { get; set; }

        public int Version { get; set; }
    }
}

