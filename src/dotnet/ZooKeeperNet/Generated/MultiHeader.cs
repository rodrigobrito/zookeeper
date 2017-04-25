namespace Org.Apache.Zookeeper.Proto
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class MultiHeader : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(MultiHeader));

        public MultiHeader()
        {
        }

        public MultiHeader(int type, bool done, int err)
        {
            this.Type = type;
            this.Done = done;
            this.Err = err;
        }

        public int CompareTo(object obj)
        {
            MultiHeader header = (MultiHeader) obj;
            if (header == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = (this.Type == header.Type) ? 0 : ((this.Type < header.Type) ? -1 : 1);
            if (num == 0)
            {
                num = (this.Done == header.Done) ? 0 : (this.Done ? 1 : -1);
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
            this.Type = a_.ReadInt("type");
            this.Done = a_.ReadBool("done");
            this.Err = a_.ReadInt("err");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            MultiHeader objA = (MultiHeader) obj;
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
                flag = this.Done == objA.Done;
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
            hashCode = this.Type;
            num = (0x25 * num) + hashCode;
            hashCode = this.Done ? 0 : 1;
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
            a_.WriteInt(this.Type, "type");
            a_.WriteBool(this.Done, "done");
            a_.WriteInt(this.Err, "err");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LMultiHeader(izi)";

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
                    archive.WriteBool(this.Done, "done");
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

        public bool Done { get; set; }

        public int Err { get; set; }

        public int Type { get; set; }
    }
}

