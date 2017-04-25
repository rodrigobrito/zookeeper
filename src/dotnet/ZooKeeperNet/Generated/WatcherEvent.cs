namespace Org.Apache.Zookeeper.Proto
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class WatcherEvent : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(WatcherEvent));

        public WatcherEvent()
        {
        }

        public WatcherEvent(int type, int state, string path)
        {
            this.Type = type;
            this.State = state;
            this.Path = path;
        }

        public int CompareTo(object obj)
        {
            WatcherEvent event2 = (WatcherEvent) obj;
            if (event2 == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = (this.Type == event2.Type) ? 0 : ((this.Type < event2.Type) ? -1 : 1);
            if (num == 0)
            {
                num = (this.State == event2.State) ? 0 : ((this.State < event2.State) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = this.Path.CompareTo(event2.Path);
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
            this.State = a_.ReadInt("state");
            this.Path = a_.ReadString("path");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            WatcherEvent objA = (WatcherEvent) obj;
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
                flag = this.State == objA.State;
                if (!flag)
                {
                    return flag;
                }
                flag = this.Path.Equals(objA.Path);
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
            hashCode = this.State;
            num = (0x25 * num) + hashCode;
            hashCode = this.Path.GetHashCode();
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
            a_.WriteInt(this.State, "state");
            a_.WriteString(this.Path, "path");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LWatcherEvent(iis)";

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
                    archive.WriteInt(this.State, "state");
                    archive.WriteString(this.Path, "path");
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

        public int State { get; set; }

        public int Type { get; set; }
    }
}

