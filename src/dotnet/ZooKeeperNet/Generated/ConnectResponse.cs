namespace Org.Apache.Zookeeper.Proto
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class ConnectResponse : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(ConnectResponse));

        public ConnectResponse()
        {
        }

        public ConnectResponse(int protocolVersion, int timeOut, long sessionId, byte[] passwd)
        {
            this.ProtocolVersion = protocolVersion;
            this.TimeOut = timeOut;
            this.SessionId = sessionId;
            this.Passwd = passwd;
        }

        public int CompareTo(object obj)
        {
            ConnectResponse response = (ConnectResponse) obj;
            if (response == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = (this.ProtocolVersion == response.ProtocolVersion) ? 0 : ((this.ProtocolVersion < response.ProtocolVersion) ? -1 : 1);
            if (num == 0)
            {
                num = (this.TimeOut == response.TimeOut) ? 0 : ((this.TimeOut < response.TimeOut) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.SessionId == response.SessionId) ? 0 : ((this.SessionId < response.SessionId) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = this.Passwd.CompareTo(response.Passwd);
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
            this.ProtocolVersion = a_.ReadInt("protocolVersion");
            this.TimeOut = a_.ReadInt("timeOut");
            this.SessionId = a_.ReadLong("sessionId");
            this.Passwd = a_.ReadBuffer("passwd");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            ConnectResponse objA = (ConnectResponse) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.ProtocolVersion == objA.ProtocolVersion;
            if (flag)
            {
                flag = this.TimeOut == objA.TimeOut;
                if (!flag)
                {
                    return flag;
                }
                flag = this.SessionId == objA.SessionId;
                if (!flag)
                {
                    return flag;
                }
                flag = this.Passwd.Equals(objA.Passwd);
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
            hashCode = this.ProtocolVersion;
            num = (0x25 * num) + hashCode;
            hashCode = this.TimeOut;
            num = (0x25 * num) + hashCode;
            hashCode = (int) this.SessionId;
            num = (0x25 * num) + hashCode;
            hashCode = this.Passwd.GetHashCode();
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
            a_.WriteInt(this.ProtocolVersion, "protocolVersion");
            a_.WriteInt(this.TimeOut, "timeOut");
            a_.WriteLong(this.SessionId, "sessionId");
            a_.WriteBuffer(this.Passwd, "passwd");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LConnectResponse(iilB)";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteInt(this.ProtocolVersion, "protocolVersion");
                    archive.WriteInt(this.TimeOut, "timeOut");
                    archive.WriteLong(this.SessionId, "sessionId");
                    archive.WriteBuffer(this.Passwd, "passwd");
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

        public byte[] Passwd { get; set; }

        public int ProtocolVersion { get; set; }

        public long SessionId { get; set; }

        public int TimeOut { get; set; }
    }
}

