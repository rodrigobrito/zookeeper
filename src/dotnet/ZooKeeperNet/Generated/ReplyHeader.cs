namespace Org.Apache.Zookeeper.Proto
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class ReplyHeader : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(ReplyHeader));

        public ReplyHeader()
        {
        }

        public ReplyHeader(int xid, long zxid, int err)
        {
            this.Xid = xid;
            this.Zxid = zxid;
            this.Err = err;
        }

        public int CompareTo(object obj)
        {
            ReplyHeader header = (ReplyHeader) obj;
            if (header == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = (this.Xid == header.Xid) ? 0 : ((this.Xid < header.Xid) ? -1 : 1);
            if (num == 0)
            {
                num = (this.Zxid == header.Zxid) ? 0 : ((this.Zxid < header.Zxid) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.Err == header.Err) ? 0 : ((this.Err < header.Err) ? -1 : 1);
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
            this.Zxid = a_.ReadLong("zxid");
            this.Err = a_.ReadInt("err");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            ReplyHeader objA = (ReplyHeader) obj;
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
                flag = this.Zxid == objA.Zxid;
                if (!flag)
                {
                    return flag;
                }
                flag = this.Err == objA.Err;
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
            hashCode = (int) this.Zxid;
            num = (0x25 * num) + hashCode;
            hashCode = this.Err;
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
            a_.WriteLong(this.Zxid, "zxid");
            a_.WriteInt(this.Err, "err");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LReplyHeader(ili)";

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
                    archive.WriteLong(this.Zxid, "zxid");
                    archive.WriteInt(this.Err, "err");
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

        public int Err { get; set; }

        public int Xid { get; set; }

        public long Zxid { get; set; }
    }
}

