namespace Org.Apache.Zookeeper.Txn
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class CreateSessionTxn : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(CreateSessionTxn));

        public CreateSessionTxn()
        {
        }

        public CreateSessionTxn(int timeOut)
        {
            this.TimeOut = timeOut;
        }

        public int CompareTo(object obj)
        {
            CreateSessionTxn txn = (CreateSessionTxn) obj;
            if (txn == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = (this.TimeOut == txn.TimeOut) ? 0 : ((this.TimeOut < txn.TimeOut) ? -1 : 1);
            if (num != 0)
            {
                return num;
            }
            return num;
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.TimeOut = a_.ReadInt("timeOut");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            CreateSessionTxn objA = (CreateSessionTxn) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.TimeOut == objA.TimeOut;
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
            hashCode = this.TimeOut;
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
            a_.WriteInt(this.TimeOut, "timeOut");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LCreateSessionTxn(i)";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteInt(this.TimeOut, "timeOut");
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

        public int TimeOut { get; set; }
    }
}

