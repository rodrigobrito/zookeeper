namespace Org.Apache.Zookeeper.Data
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class ACL : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(ACL));

        public ACL()
        {
        }

        public ACL(int perms, ZKId id)
        {
            this.Perms = perms;
            this.Id = id;
        }

        public int CompareTo(object obj)
        {
            ACL acl = (ACL) obj;
            if (acl == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = (this.Perms == acl.Perms) ? 0 : ((this.Perms < acl.Perms) ? -1 : 1);
            if (num == 0)
            {
                num = this.Id.CompareTo(acl.Id);
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
            this.Perms = a_.ReadInt("perms");
            this.Id = new ZKId();
            a_.ReadRecord(this.Id, "id");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            ACL objA = (ACL) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Perms == objA.Perms;
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
            hashCode = this.Perms;
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
            a_.WriteInt(this.Perms, "perms");
            a_.WriteRecord(this.Id, "id");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LACL(iLId(ss))";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteInt(this.Perms, "perms");
                    archive.WriteRecord(this.Id, "id");
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

        public ZKId Id { get; set; }

        public int Perms { get; set; }
    }
}

