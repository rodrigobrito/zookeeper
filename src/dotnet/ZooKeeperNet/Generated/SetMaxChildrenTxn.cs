namespace Org.Apache.Zookeeper.Txn
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class SetMaxChildrenTxn : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(SetMaxChildrenTxn));

        public SetMaxChildrenTxn()
        {
        }

        public SetMaxChildrenTxn(string path, int max)
        {
            this.Path = path;
            this.Max = max;
        }

        public int CompareTo(object obj)
        {
            SetMaxChildrenTxn txn = (SetMaxChildrenTxn) obj;
            if (txn == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = this.Path.CompareTo(txn.Path);
            if (num == 0)
            {
                num = (this.Max == txn.Max) ? 0 : ((this.Max < txn.Max) ? -1 : 1);
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
            this.Max = a_.ReadInt("max");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            SetMaxChildrenTxn objA = (SetMaxChildrenTxn) obj;
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
                flag = this.Max == objA.Max;
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
            hashCode = this.Max;
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
            a_.WriteInt(this.Max, "max");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LSetMaxChildrenTxn(si)";

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
                    archive.WriteInt(this.Max, "max");
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

        public int Max { get; set; }

        public string Path { get; set; }
    }
}

