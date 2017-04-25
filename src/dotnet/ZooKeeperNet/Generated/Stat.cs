namespace Org.Apache.Zookeeper.Data
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class Stat : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(Stat));

        public Stat()
        {
        }

        public Stat(long czxid, long mzxid, long ctime, long mtime, int version, int cversion, int aversion, long ephemeralOwner, int dataLength, int numChildren, long pzxid)
        {
            this.Czxid = czxid;
            this.Mzxid = mzxid;
            this.Ctime = ctime;
            this.Mtime = mtime;
            this.Version = version;
            this.Cversion = cversion;
            this.Aversion = aversion;
            this.EphemeralOwner = ephemeralOwner;
            this.DataLength = dataLength;
            this.NumChildren = numChildren;
            this.Pzxid = pzxid;
        }

        public int CompareTo(object obj)
        {
            Stat stat = (Stat) obj;
            if (stat == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = (this.Czxid == stat.Czxid) ? 0 : ((this.Czxid < stat.Czxid) ? -1 : 1);
            if (num == 0)
            {
                num = (this.Mzxid == stat.Mzxid) ? 0 : ((this.Mzxid < stat.Mzxid) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.Ctime == stat.Ctime) ? 0 : ((this.Ctime < stat.Ctime) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.Mtime == stat.Mtime) ? 0 : ((this.Mtime < stat.Mtime) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.Version == stat.Version) ? 0 : ((this.Version < stat.Version) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.Cversion == stat.Cversion) ? 0 : ((this.Cversion < stat.Cversion) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.Aversion == stat.Aversion) ? 0 : ((this.Aversion < stat.Aversion) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.EphemeralOwner == stat.EphemeralOwner) ? 0 : ((this.EphemeralOwner < stat.EphemeralOwner) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.DataLength == stat.DataLength) ? 0 : ((this.DataLength < stat.DataLength) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.NumChildren == stat.NumChildren) ? 0 : ((this.NumChildren < stat.NumChildren) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.Pzxid == stat.Pzxid) ? 0 : ((this.Pzxid < stat.Pzxid) ? -1 : 1);
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
            this.Czxid = a_.ReadLong("czxid");
            this.Mzxid = a_.ReadLong("mzxid");
            this.Ctime = a_.ReadLong("ctime");
            this.Mtime = a_.ReadLong("mtime");
            this.Version = a_.ReadInt("version");
            this.Cversion = a_.ReadInt("cversion");
            this.Aversion = a_.ReadInt("aversion");
            this.EphemeralOwner = a_.ReadLong("ephemeralOwner");
            this.DataLength = a_.ReadInt("dataLength");
            this.NumChildren = a_.ReadInt("numChildren");
            this.Pzxid = a_.ReadLong("pzxid");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            Stat objA = (Stat) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Czxid == objA.Czxid;
            if (flag)
            {
                flag = this.Mzxid == objA.Mzxid;
                if (!flag)
                {
                    return flag;
                }
                flag = this.Ctime == objA.Ctime;
                if (!flag)
                {
                    return flag;
                }
                flag = this.Mtime == objA.Mtime;
                if (!flag)
                {
                    return flag;
                }
                flag = this.Version == objA.Version;
                if (!flag)
                {
                    return flag;
                }
                flag = this.Cversion == objA.Cversion;
                if (!flag)
                {
                    return flag;
                }
                flag = this.Aversion == objA.Aversion;
                if (!flag)
                {
                    return flag;
                }
                flag = this.EphemeralOwner == objA.EphemeralOwner;
                if (!flag)
                {
                    return flag;
                }
                flag = this.DataLength == objA.DataLength;
                if (!flag)
                {
                    return flag;
                }
                flag = this.NumChildren == objA.NumChildren;
                if (!flag)
                {
                    return flag;
                }
                flag = this.Pzxid == objA.Pzxid;
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
            hashCode = (int) this.Czxid;
            num = (0x25 * num) + hashCode;
            hashCode = (int) this.Mzxid;
            num = (0x25 * num) + hashCode;
            hashCode = (int) this.Ctime;
            num = (0x25 * num) + hashCode;
            hashCode = (int) this.Mtime;
            num = (0x25 * num) + hashCode;
            hashCode = this.Version;
            num = (0x25 * num) + hashCode;
            hashCode = this.Cversion;
            num = (0x25 * num) + hashCode;
            hashCode = this.Aversion;
            num = (0x25 * num) + hashCode;
            hashCode = (int) this.EphemeralOwner;
            num = (0x25 * num) + hashCode;
            hashCode = this.DataLength;
            num = (0x25 * num) + hashCode;
            hashCode = this.NumChildren;
            num = (0x25 * num) + hashCode;
            hashCode = (int) this.Pzxid;
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
            a_.WriteLong(this.Czxid, "czxid");
            a_.WriteLong(this.Mzxid, "mzxid");
            a_.WriteLong(this.Ctime, "ctime");
            a_.WriteLong(this.Mtime, "mtime");
            a_.WriteInt(this.Version, "version");
            a_.WriteInt(this.Cversion, "cversion");
            a_.WriteInt(this.Aversion, "aversion");
            a_.WriteLong(this.EphemeralOwner, "ephemeralOwner");
            a_.WriteInt(this.DataLength, "dataLength");
            a_.WriteInt(this.NumChildren, "numChildren");
            a_.WriteLong(this.Pzxid, "pzxid");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LStat(lllliiiliil)";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteLong(this.Czxid, "czxid");
                    archive.WriteLong(this.Mzxid, "mzxid");
                    archive.WriteLong(this.Ctime, "ctime");
                    archive.WriteLong(this.Mtime, "mtime");
                    archive.WriteInt(this.Version, "version");
                    archive.WriteInt(this.Cversion, "cversion");
                    archive.WriteInt(this.Aversion, "aversion");
                    archive.WriteLong(this.EphemeralOwner, "ephemeralOwner");
                    archive.WriteInt(this.DataLength, "dataLength");
                    archive.WriteInt(this.NumChildren, "numChildren");
                    archive.WriteLong(this.Pzxid, "pzxid");
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

        public int Aversion { get; set; }

        public long Ctime { get; set; }

        public int Cversion { get; set; }

        public long Czxid { get; set; }

        public int DataLength { get; set; }

        public long EphemeralOwner { get; set; }

        public long Mtime { get; set; }

        public long Mzxid { get; set; }

        public int NumChildren { get; set; }

        public long Pzxid { get; set; }

        public int Version { get; set; }
    }
}

