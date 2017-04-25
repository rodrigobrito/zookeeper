namespace Org.Apache.Zookeeper.Txn
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class TxnHeader : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(TxnHeader));

        public TxnHeader()
        {
        }

        public TxnHeader(long clientId, int cxid, long zxid, long time, int type)
        {
            this.ClientId = clientId;
            this.Cxid = cxid;
            this.Zxid = zxid;
            this.Time = time;
            this.Type = type;
        }

        public int CompareTo(object obj)
        {
            TxnHeader header = (TxnHeader) obj;
            if (header == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = (this.ClientId == header.ClientId) ? 0 : ((this.ClientId < header.ClientId) ? -1 : 1);
            if (num == 0)
            {
                num = (this.Cxid == header.Cxid) ? 0 : ((this.Cxid < header.Cxid) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.Zxid == header.Zxid) ? 0 : ((this.Zxid < header.Zxid) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.Time == header.Time) ? 0 : ((this.Time < header.Time) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
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
            this.ClientId = a_.ReadLong("clientId");
            this.Cxid = a_.ReadInt("cxid");
            this.Zxid = a_.ReadLong("zxid");
            this.Time = a_.ReadLong("time");
            this.Type = a_.ReadInt("type");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            TxnHeader objA = (TxnHeader) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.ClientId == objA.ClientId;
            if (flag)
            {
                flag = this.Cxid == objA.Cxid;
                if (!flag)
                {
                    return flag;
                }
                flag = this.Zxid == objA.Zxid;
                if (!flag)
                {
                    return flag;
                }
                flag = this.Time == objA.Time;
                if (!flag)
                {
                    return flag;
                }
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
            hashCode = (int) this.ClientId;
            num = (0x25 * num) + hashCode;
            hashCode = this.Cxid;
            num = (0x25 * num) + hashCode;
            hashCode = (int) this.Zxid;
            num = (0x25 * num) + hashCode;
            hashCode = (int) this.Time;
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
            a_.WriteLong(this.ClientId, "clientId");
            a_.WriteInt(this.Cxid, "cxid");
            a_.WriteLong(this.Zxid, "zxid");
            a_.WriteLong(this.Time, "time");
            a_.WriteInt(this.Type, "type");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LTxnHeader(lilli)";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteLong(this.ClientId, "clientId");
                    archive.WriteInt(this.Cxid, "cxid");
                    archive.WriteLong(this.Zxid, "zxid");
                    archive.WriteLong(this.Time, "time");
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

        public long ClientId { get; set; }

        public int Cxid { get; set; }

        public long Time { get; set; }

        public int Type { get; set; }

        public long Zxid { get; set; }
    }
}

