namespace Org.Apache.Zookeeper.Proto
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class RequestHeader : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestHeader));

        public RequestHeader()
        {
        }

        public RequestHeader(int xid, int type)
        {
            this.Xid = xid;
            this.Type = type;
        }

        public int CompareTo(object obj)
        {
            RequestHeader header = (RequestHeader) obj;
            if (header == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = (this.Xid == header.Xid) ? 0 : ((this.Xid < header.Xid) ? -1 : 1);
            if (num == 0)
            {
                num = (this.Type == header.Type) ? 0 : ((this.Type < header.Type) ? -1 : 1);
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
            this.Xid = a_.ReadInt("xid");
            this.Type = a_.ReadInt("type");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            RequestHeader objA = (RequestHeader) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Xid == objA.Xid;
            if (flag)
            {
                flag = this.Type == objA.Type;
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
            hashCode = this.Xid;
            num = (0x25 * num) + hashCode;
            hashCode = this.Type;
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
            a_.WriteInt(this.Xid, "xid");
            a_.WriteInt(this.Type, "type");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LRequestHeader(ii)";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteInt(this.Xid, "xid");
                    archive.WriteInt(this.Type, "type");
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

        public int Type { get; set; }

        public int Xid { get; set; }
    }
}

