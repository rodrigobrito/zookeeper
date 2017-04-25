namespace Org.Apache.Zookeeper.Data
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class ZKId : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(ZKId));

        public ZKId()
        {
        }

        public ZKId(string scheme, string id)
        {
            this.Scheme = scheme;
            this.Id = id;
        }

        public int CompareTo(object obj)
        {
            ZKId id = (ZKId) obj;
            if (id == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = this.Scheme.CompareTo(id.Scheme);
            if (num == 0)
            {
                num = this.Id.CompareTo(id.Id);
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
            this.Scheme = a_.ReadString("scheme");
            this.Id = a_.ReadString("id");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            ZKId objA = (ZKId) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Scheme.Equals(objA.Scheme);
            if (flag)
            {
                flag = this.Id.Equals(objA.Id);
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
            hashCode = this.Scheme.GetHashCode();
            num = (0x25 * num) + hashCode;
            hashCode = this.Id.GetHashCode();
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
            a_.WriteString(this.Scheme, "scheme");
            a_.WriteString(this.Id, "id");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LId(ss)";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteString(this.Scheme, "scheme");
                    archive.WriteString(this.Id, "id");
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

        public string Id { get; set; }

        public string Scheme { get; set; }
    }
}

