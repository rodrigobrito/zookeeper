namespace Org.Apache.Zookeeper.Proto
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class GetChildrenRequest : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(GetChildrenRequest));

        public GetChildrenRequest()
        {
        }

        public GetChildrenRequest(string path, bool watch)
        {
            this.Path = path;
            this.Watch = watch;
        }

        public int CompareTo(object obj)
        {
            GetChildrenRequest request = (GetChildrenRequest) obj;
            if (request == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = this.Path.CompareTo(request.Path);
            if (num == 0)
            {
                num = (this.Watch == request.Watch) ? 0 : (this.Watch ? 1 : -1);
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
            this.Path = a_.ReadString("path");
            this.Watch = a_.ReadBool("watch");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            GetChildrenRequest objA = (GetChildrenRequest) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Path.Equals(objA.Path);
            if (flag)
            {
                flag = this.Watch == objA.Watch;
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
            hashCode = this.Path.GetHashCode();
            num = (0x25 * num) + hashCode;
            hashCode = this.Watch ? 0 : 1;
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
            a_.WriteString(this.Path, "path");
            a_.WriteBool(this.Watch, "watch");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LGetChildrenRequest(sz)";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteString(this.Path, "path");
                    archive.WriteBool(this.Watch, "watch");
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

        public string Path { get; set; }

        public bool Watch { get; set; }
    }
}

