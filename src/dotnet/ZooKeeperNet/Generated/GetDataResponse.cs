namespace Org.Apache.Zookeeper.Proto
{
    using log4net;
    using Org.Apache.Jute;
    using Org.Apache.Zookeeper.Data;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class GetDataResponse : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(GetDataResponse));

        public GetDataResponse()
        {
        }

        public GetDataResponse(byte[] data, Org.Apache.Zookeeper.Data.Stat stat)
        {
            this.Data = data;
            this.Stat = stat;
        }

        public int CompareTo(object obj)
        {
            GetDataResponse response = (GetDataResponse) obj;
            if (response == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = this.Data.CompareTo(response.Data);
            if (num == 0)
            {
                num = this.Stat.CompareTo(response.Stat);
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
            this.Data = a_.ReadBuffer("data");
            this.Stat = new Org.Apache.Zookeeper.Data.Stat();
            a_.ReadRecord(this.Stat, "stat");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            GetDataResponse objA = (GetDataResponse) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Data.Equals(objA.Data);
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
            hashCode = this.Data.GetHashCode();
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
            a_.WriteBuffer(this.Data, "data");
            a_.WriteRecord(this.Stat, "stat");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LGetDataResponse(BLStat(lllliiiliil))";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteBuffer(this.Data, "data");
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

        public byte[] Data { get; set; }

        public Org.Apache.Zookeeper.Data.Stat Stat { get; set; }
    }
}

