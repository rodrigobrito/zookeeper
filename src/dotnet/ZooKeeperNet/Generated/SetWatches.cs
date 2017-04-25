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

    public class SetWatches : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(SetWatches));

        public SetWatches()
        {
        }

        public SetWatches(long relativeZxid, IEnumerable<string> dataWatches, IEnumerable<string> existWatches, IEnumerable<string> childWatches)
        {
            this.RelativeZxid = relativeZxid;
            this.DataWatches = dataWatches;
            this.ExistWatches = existWatches;
            this.ChildWatches = childWatches;
        }

        public int CompareTo(object obj)
        {
            throw new InvalidOperationException("comparing SetWatches is unimplemented");
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.RelativeZxid = a_.ReadLong("relativeZxid");
            IIndex index = a_.StartVector("dataWatches");
            if (index != null)
            {
                List<string> list = new List<string>();
                while (!index.Done())
                {
                    string item = a_.ReadString("e1");
                    list.Add(item);
                    index.Incr();
                }
                this.DataWatches = list;
            }
            a_.EndVector("dataWatches");
            IIndex index2 = a_.StartVector("existWatches");
            if (index2 != null)
            {
                List<string> list2 = new List<string>();
                while (!index2.Done())
                {
                    string str2 = a_.ReadString("e1");
                    list2.Add(str2);
                    index2.Incr();
                }
                this.ExistWatches = list2;
            }
            a_.EndVector("existWatches");
            IIndex index3 = a_.StartVector("childWatches");
            if (index3 != null)
            {
                List<string> list3 = new List<string>();
                while (!index3.Done())
                {
                    string str3 = a_.ReadString("e1");
                    list3.Add(str3);
                    index3.Incr();
                }
                this.ChildWatches = list3;
            }
            a_.EndVector("childWatches");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            SetWatches objA = (SetWatches) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.RelativeZxid == objA.RelativeZxid;
            if (flag)
            {
                flag = this.DataWatches.Equals(objA.DataWatches);
                if (!flag)
                {
                    return flag;
                }
                flag = this.ExistWatches.Equals(objA.ExistWatches);
                if (!flag)
                {
                    return flag;
                }
                flag = this.ChildWatches.Equals(objA.ChildWatches);
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
            hashCode = (int) this.RelativeZxid;
            num = (0x25 * num) + hashCode;
            hashCode = this.DataWatches.GetHashCode();
            num = (0x25 * num) + hashCode;
            hashCode = this.ExistWatches.GetHashCode();
            num = (0x25 * num) + hashCode;
            hashCode = this.ChildWatches.GetHashCode();
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
            a_.WriteLong(this.RelativeZxid, "relativeZxid");
            a_.StartVector<string>(this.DataWatches, "dataWatches");
            if (this.DataWatches != null)
            {
                foreach (string str in this.DataWatches)
                {
                    a_.WriteString(str, str);
                }
            }
            a_.EndVector<string>(this.DataWatches, "dataWatches");
            a_.StartVector<string>(this.ExistWatches, "existWatches");
            if (this.ExistWatches != null)
            {
                foreach (string str2 in this.ExistWatches)
                {
                    a_.WriteString(str2, str2);
                }
            }
            a_.EndVector<string>(this.ExistWatches, "existWatches");
            a_.StartVector<string>(this.ChildWatches, "childWatches");
            if (this.ChildWatches != null)
            {
                foreach (string str3 in this.ChildWatches)
                {
                    a_.WriteString(str3, str3);
                }
            }
            a_.EndVector<string>(this.ChildWatches, "childWatches");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LSetWatches(l[s][s][s])";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteLong(this.RelativeZxid, "relativeZxid");
                    archive.StartVector<string>(this.DataWatches, "dataWatches");
                    if (this.DataWatches != null)
                    {
                        foreach (string str in this.DataWatches)
                        {
                            archive.WriteString(str, str);
                        }
                    }
                    archive.EndVector<string>(this.DataWatches, "dataWatches");
                    archive.StartVector<string>(this.ExistWatches, "existWatches");
                    if (this.ExistWatches != null)
                    {
                        foreach (string str2 in this.ExistWatches)
                        {
                            archive.WriteString(str2, str2);
                        }
                    }
                    archive.EndVector<string>(this.ExistWatches, "existWatches");
                    archive.StartVector<string>(this.ChildWatches, "childWatches");
                    if (this.ChildWatches != null)
                    {
                        foreach (string str3 in this.ChildWatches)
                        {
                            archive.WriteString(str3, str3);
                        }
                    }
                    archive.EndVector<string>(this.ChildWatches, "childWatches");
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

        public IEnumerable<string> ChildWatches { get; set; }

        public IEnumerable<string> DataWatches { get; set; }

        public IEnumerable<string> ExistWatches { get; set; }

        public long RelativeZxid { get; set; }
    }
}

