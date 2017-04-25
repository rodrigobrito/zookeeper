namespace Org.Apache.Zookeeper.Txn
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class Txn : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(Org.Apache.Zookeeper.Txn.Txn));

        public Txn()
        {
        }

        public Txn(int type, byte[] data)
        {
            this.Type = type;
            this.Data = data;
        }

        public int CompareTo(object obj)
        {
            Org.Apache.Zookeeper.Txn.Txn txn = (Org.Apache.Zookeeper.Txn.Txn) obj;
            if (txn == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = (this.Type == txn.Type) ? 0 : ((this.Type < txn.Type) ? -1 : 1);
            if (num == 0)
            {
                num = this.Data.CompareTo(txn.Data);
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
            this.Type = a_.ReadInt("type");
            this.Data = a_.ReadBuffer("data");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            Org.Apache.Zookeeper.Txn.Txn objA = (Org.Apache.Zookeeper.Txn.Txn) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Type == objA.Type;
            if (flag)
            {
                flag = this.Data.Equals(objA.Data);
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
            hashCode = this.Type;
            num = (0x25 * num) + hashCode;
            hashCode = this.Data.GetHashCode();
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
            a_.WriteInt(this.Type, "type");
            a_.WriteBuffer(this.Data, "data");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LTxn(iB)";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteInt(this.Type, "type");
                    archive.WriteBuffer(this.Data, "data");
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

        public int Type { get; set; }
    }
}

