namespace Org.Apache.Zookeeper.Server.Persistence
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class FileHeader : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(FileHeader));

        public FileHeader()
        {
        }

        public FileHeader(int magic, int version, long dbid)
        {
            this.Magic = magic;
            this.Version = version;
            this.Dbid = dbid;
        }

        public int CompareTo(object obj)
        {
            FileHeader header = (FileHeader) obj;
            if (header == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = (this.Magic == header.Magic) ? 0 : ((this.Magic < header.Magic) ? -1 : 1);
            if (num == 0)
            {
                num = (this.Version == header.Version) ? 0 : ((this.Version < header.Version) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.Dbid == header.Dbid) ? 0 : ((this.Dbid < header.Dbid) ? -1 : 1);
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
            this.Magic = a_.ReadInt("magic");
            this.Version = a_.ReadInt("version");
            this.Dbid = a_.ReadLong("dbid");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            FileHeader objA = (FileHeader) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Magic == objA.Magic;
            if (flag)
            {
                flag = this.Version == objA.Version;
                if (!flag)
                {
                    return flag;
                }
                flag = this.Dbid == objA.Dbid;
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
            hashCode = this.Magic;
            num = (0x25 * num) + hashCode;
            hashCode = this.Version;
            num = (0x25 * num) + hashCode;
            hashCode = (int) this.Dbid;
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
            a_.WriteInt(this.Magic, "magic");
            a_.WriteInt(this.Version, "version");
            a_.WriteLong(this.Dbid, "dbid");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LFileHeader(iil)";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteInt(this.Magic, "magic");
                    archive.WriteInt(this.Version, "version");
                    archive.WriteLong(this.Dbid, "dbid");
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

        public long Dbid { get; set; }

        public int Magic { get; set; }

        public int Version { get; set; }
    }
}

