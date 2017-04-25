namespace Org.Apache.Zookeeper.Txn
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

    public class CreateTxn : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(CreateTxn));

        public CreateTxn()
        {
        }

        public CreateTxn(string path, byte[] data, IEnumerable<ACL> acl, bool ephemeral, int parentCVersion)
        {
            this.Path = path;
            this.Data = data;
            this.Acl = acl;
            this.Ephemeral = ephemeral;
            this.ParentCVersion = parentCVersion;
        }

        public int CompareTo(object obj)
        {
            throw new InvalidOperationException("comparing CreateTxn is unimplemented");
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Path = a_.ReadString("path");
            this.Data = a_.ReadBuffer("data");
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
            this.Ephemeral = a_.ReadBool("ephemeral");
            this.ParentCVersion = a_.ReadInt("parentCVersion");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            CreateTxn objA = (CreateTxn) obj;
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
                flag = this.Data.Equals(objA.Data);
                if (!flag)
                {
                    return flag;
                }
                flag = this.Acl.Equals(objA.Acl);
                if (!flag)
                {
                    return flag;
                }
                flag = this.Ephemeral == objA.Ephemeral;
                if (!flag)
                {
                    return flag;
                }
                flag = this.ParentCVersion == objA.ParentCVersion;
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
            hashCode = this.Data.GetHashCode();
            num = (0x25 * num) + hashCode;
            hashCode = this.Acl.GetHashCode();
            num = (0x25 * num) + hashCode;
            hashCode = this.Ephemeral ? 0 : 1;
            num = (0x25 * num) + hashCode;
            hashCode = this.ParentCVersion;
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
            a_.WriteBuffer(this.Data, "data");
            a_.StartVector<ACL>(this.Acl, "acl");
            if (this.Acl != null)
            {
                foreach (ACL acl in this.Acl)
                {
                    a_.WriteRecord(acl, "e1");
                }
            }
            a_.EndVector<ACL>(this.Acl, "acl");
            a_.WriteBool(this.Ephemeral, "ephemeral");
            a_.WriteInt(this.ParentCVersion, "parentCVersion");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LCreateTxn(sB[LACL(iLId(ss))]zi)";

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
                    archive.WriteBuffer(this.Data, "data");
                    archive.StartVector<ACL>(this.Acl, "acl");
                    if (this.Acl != null)
                    {
                        foreach (ACL acl in this.Acl)
                        {
                            archive.WriteRecord(acl, "e1");
                        }
                    }
                    archive.EndVector<ACL>(this.Acl, "acl");
                    archive.WriteBool(this.Ephemeral, "ephemeral");
                    archive.WriteInt(this.ParentCVersion, "parentCVersion");
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

        public byte[] Data { get; set; }

        public bool Ephemeral { get; set; }

        public int ParentCVersion { get; set; }

        public string Path { get; set; }
    }
}

