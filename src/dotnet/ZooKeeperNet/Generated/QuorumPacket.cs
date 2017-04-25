namespace Org.Apache.Zookeeper.Server.Quorum
{
    using log4net;
    using Org.Apache.Jute;
    using Org.Apache.Zookeeper.Data;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class QuorumPacket : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(QuorumPacket));

        public QuorumPacket()
        {
        }

        public QuorumPacket(int type, long zxid, byte[] data, IEnumerable<ZKId> authinfo)
        {
            this.Type = type;
            this.Zxid = zxid;
            this.Data = data;
            this.Authinfo = authinfo;
        }

        public int CompareTo(object obj)
        {
            throw new InvalidOperationException("comparing QuorumPacket is unimplemented");
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Type = a_.ReadInt("type");
            this.Zxid = a_.ReadLong("zxid");
            this.Data = a_.ReadBuffer("data");
            IIndex index = a_.StartVector("authinfo");
            if (index != null)
            {
                List<ZKId> list = new List<ZKId>();
                while (!index.Done())
                {
                    ZKId r = new ZKId();
                    a_.ReadRecord(r, "e1");
                    list.Add(r);
                    index.Incr();
                }
                this.Authinfo = list;
            }
            a_.EndVector("authinfo");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            QuorumPacket objA = (QuorumPacket) obj;
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
                flag = this.Zxid == objA.Zxid;
                if (!flag)
                {
                    return flag;
                }
                flag = this.Data.Equals(objA.Data);
                if (!flag)
                {
                    return flag;
                }
                flag = this.Authinfo.Equals(objA.Authinfo);
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
            hashCode = (int) this.Zxid;
            num = (0x25 * num) + hashCode;
            hashCode = this.Data.GetHashCode();
            num = (0x25 * num) + hashCode;
            hashCode = this.Authinfo.GetHashCode();
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
            a_.WriteLong(this.Zxid, "zxid");
            a_.WriteBuffer(this.Data, "data");
            a_.StartVector<ZKId>(this.Authinfo, "authinfo");
            if (this.Authinfo != null)
            {
                foreach (ZKId id in this.Authinfo)
                {
                    a_.WriteRecord(id, "e1");
                }
            }
            a_.EndVector<ZKId>(this.Authinfo, "authinfo");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LQuorumPacket(ilB[LId(ss)])";

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
                    archive.WriteLong(this.Zxid, "zxid");
                    archive.WriteBuffer(this.Data, "data");
                    archive.StartVector<ZKId>(this.Authinfo, "authinfo");
                    if (this.Authinfo != null)
                    {
                        foreach (ZKId id in this.Authinfo)
                        {
                            archive.WriteRecord(id, "e1");
                        }
                    }
                    archive.EndVector<ZKId>(this.Authinfo, "authinfo");
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

        public IEnumerable<ZKId> Authinfo { get; set; }

        public byte[] Data { get; set; }

        public int Type { get; set; }

        public long Zxid { get; set; }
    }
}

