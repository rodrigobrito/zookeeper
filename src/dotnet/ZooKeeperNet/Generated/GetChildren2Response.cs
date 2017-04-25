namespace Org.Apache.Zookeeper.Proto
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

    public class GetChildren2Response : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(GetChildren2Response));

        public GetChildren2Response()
        {
        }

        public GetChildren2Response(IEnumerable<string> children, Org.Apache.Zookeeper.Data.Stat stat)
        {
            this.Children = children;
            this.Stat = stat;
        }

        public int CompareTo(object obj)
        {
            throw new InvalidOperationException("comparing GetChildren2Response is unimplemented");
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            IIndex index = a_.StartVector("children");
            if (index != null)
            {
                List<string> list = new List<string>();
                while (!index.Done())
                {
                    string item = a_.ReadString("e1");
                    list.Add(item);
                    index.Incr();
                }
                this.Children = list;
            }
            a_.EndVector("children");
            this.Stat = new Org.Apache.Zookeeper.Data.Stat();
            a_.ReadRecord(this.Stat, "stat");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            GetChildren2Response objA = (GetChildren2Response) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Children.Equals(objA.Children);
            if (flag)
            {
                flag = this.Stat.Equals(objA.Stat);
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
            hashCode = this.Children.GetHashCode();
            num = (0x25 * num) + hashCode;
            hashCode = this.Stat.GetHashCode();
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
            a_.StartVector<string>(this.Children, "children");
            if (this.Children != null)
            {
                foreach (string str in this.Children)
                {
                    a_.WriteString(str, str);
                }
            }
            a_.EndVector<string>(this.Children, "children");
            a_.WriteRecord(this.Stat, "stat");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LGetChildren2Response([s]LStat(lllliiiliil))";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.StartVector<string>(this.Children, "children");
                    if (this.Children != null)
                    {
                        foreach (string str in this.Children)
                        {
                            archive.WriteString(str, str);
                        }
                    }
                    archive.EndVector<string>(this.Children, "children");
                    archive.WriteRecord(this.Stat, "stat");
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

        public IEnumerable<string> Children { get; set; }

        public Org.Apache.Zookeeper.Data.Stat Stat { get; set; }
    }
}

