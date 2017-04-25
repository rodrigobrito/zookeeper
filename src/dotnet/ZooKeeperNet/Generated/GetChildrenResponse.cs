namespace Org.Apache.Zookeeper.Proto
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class GetChildrenResponse : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(GetChildrenResponse));

        public GetChildrenResponse()
        {
        }

        public GetChildrenResponse(IEnumerable<string> children)
        {
            this.Children = children;
        }

        public int CompareTo(object obj)
        {
            throw new InvalidOperationException("comparing GetChildrenResponse is unimplemented");
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
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            GetChildrenResponse objA = (GetChildrenResponse) obj;
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
            if (!flag)
            {
                return flag;
            }
            return flag;
        }

        public override int GetHashCode()
        {
            int num = 0x11;
            int hashCode = base.GetType().GetHashCode();
            num = (0x25 * num) + hashCode;
            hashCode = this.Children.GetHashCode();
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
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LGetChildrenResponse([s])";

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
    }
}

