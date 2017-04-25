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

    public class GetACLResponse : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(GetACLResponse));

        public GetACLResponse()
        {
        }

        public GetACLResponse(IEnumerable<ACL> acl, Org.Apache.Zookeeper.Data.Stat stat)
        {
            this.Acl = acl;
            this.Stat = stat;
        }

        public int CompareTo(object obj)
        {
            throw new InvalidOperationException("comparing GetACLResponse is unimplemented");
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            IIndex index = a_.StartVector("acl");
            if (index != null)
            {
                List<ACL> list = new List<ACL>();
                while (!index.Done())
                {
                    ACL r = new ACL();
                    a_.ReadRecord(r, "e1");
                    list.Add(r);
                    index.Incr();
                }
                this.Acl = list;
            }
            a_.EndVector("acl");
            this.Stat = new Org.Apache.Zookeeper.Data.Stat();
            a_.ReadRecord(this.Stat, "stat");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            GetACLResponse objA = (GetACLResponse) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Acl.Equals(objA.Acl);
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
            hashCode = this.Acl.GetHashCode();
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
            a_.StartVector<ACL>(this.Acl, "acl");
            if (this.Acl != null)
            {
                foreach (ACL acl in this.Acl)
                {
                    a_.WriteRecord(acl, "e1");
                }
            }
            a_.EndVector<ACL>(this.Acl, "acl");
            a_.WriteRecord(this.Stat, "stat");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LGetACLResponse([LACL(iLId(ss))]LStat(lllliiiliil))";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.StartVector<ACL>(this.Acl, "acl");
                    if (this.Acl != null)
                    {
                        foreach (ACL acl in this.Acl)
                        {
                            archive.WriteRecord(acl, "e1");
                        }
                    }
                    archive.EndVector<ACL>(this.Acl, "acl");
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

        public IEnumerable<ACL> Acl { get; set; }

        public Org.Apache.Zookeeper.Data.Stat Stat { get; set; }
    }
}

