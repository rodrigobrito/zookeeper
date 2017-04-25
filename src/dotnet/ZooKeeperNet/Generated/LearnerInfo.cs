namespace Org.Apache.Zookeeper.Server.Quorum
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class LearnerInfo : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(LearnerInfo));

        public LearnerInfo()
        {
        }

        public LearnerInfo(long serverid, int protocolVersion)
        {
            this.Serverid = serverid;
            this.ProtocolVersion = protocolVersion;
        }

        public int CompareTo(object obj)
        {
            LearnerInfo info = (LearnerInfo) obj;
            if (info == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = (this.Serverid == info.Serverid) ? 0 : ((this.Serverid < info.Serverid) ? -1 : 1);
            if (num == 0)
            {
                num = (this.ProtocolVersion == info.ProtocolVersion) ? 0 : ((this.ProtocolVersion < info.ProtocolVersion) ? -1 : 1);
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
            this.Serverid = a_.ReadLong("serverid");
            this.ProtocolVersion = a_.ReadInt("protocolVersion");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            LearnerInfo objA = (LearnerInfo) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Serverid == objA.Serverid;
            if (flag)
            {
                flag = this.ProtocolVersion == objA.ProtocolVersion;
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
            hashCode = (int) this.Serverid;
            num = (0x25 * num) + hashCode;
            hashCode = this.ProtocolVersion;
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
            a_.WriteLong(this.Serverid, "serverid");
            a_.WriteInt(this.ProtocolVersion, "protocolVersion");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LLearnerInfo(li)";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteLong(this.Serverid, "serverid");
                    archive.WriteInt(this.ProtocolVersion, "protocolVersion");
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

        public int ProtocolVersion { get; set; }

        public long Serverid { get; set; }
    }
}

