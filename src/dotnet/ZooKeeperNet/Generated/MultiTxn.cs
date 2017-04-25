namespace Org.Apache.Zookeeper.Txn
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class MultiTxn : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(MultiTxn));

        public MultiTxn()
        {
        }

        public MultiTxn(IEnumerable<Org.Apache.Zookeeper.Txn.Txn> txns)
        {
            this.Txns = txns;
        }

        public int CompareTo(object obj)
        {
            throw new InvalidOperationException("comparing MultiTxn is unimplemented");
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            IIndex index = a_.StartVector("txns");
            if (index != null)
            {
                List<Org.Apache.Zookeeper.Txn.Txn> list = new List<Org.Apache.Zookeeper.Txn.Txn>();
                while (!index.Done())
                {
                    Org.Apache.Zookeeper.Txn.Txn r = new Org.Apache.Zookeeper.Txn.Txn();
                    a_.ReadRecord(r, "e1");
                    list.Add(r);
                    index.Incr();
                }
                this.Txns = list;
            }
            a_.EndVector("txns");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            MultiTxn objA = (MultiTxn) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Txns.Equals(objA.Txns);
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
            hashCode = this.Txns.GetHashCode();
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
            a_.StartVector<Org.Apache.Zookeeper.Txn.Txn>(this.Txns, "txns");
            if (this.Txns != null)
            {
                foreach (Org.Apache.Zookeeper.Txn.Txn txn in this.Txns)
                {
                    a_.WriteRecord(txn, "e1");
                }
            }
            a_.EndVector<Org.Apache.Zookeeper.Txn.Txn>(this.Txns, "txns");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LMultiTxn([LTxn(iB)])";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.StartVector<Org.Apache.Zookeeper.Txn.Txn>(this.Txns, "txns");
                    if (this.Txns != null)
                    {
                        foreach (Org.Apache.Zookeeper.Txn.Txn txn in this.Txns)
                        {
                            archive.WriteRecord(txn, "e1");
                        }
                    }
                    archive.EndVector<Org.Apache.Zookeeper.Txn.Txn>(this.Txns, "txns");
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

        public IEnumerable<Org.Apache.Zookeeper.Txn.Txn> Txns { get; set; }
    }
}

