namespace Org.Apache.Zookeeper.Proto
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class ConnectRequest : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(ConnectRequest));

        public ConnectRequest()
        {
        }

        public ConnectRequest(int protocolVersion, long lastZxidSeen, int timeOut, long sessionId, byte[] passwd)
        {
            this.ProtocolVersion = protocolVersion;
            this.LastZxidSeen = lastZxidSeen;
            this.TimeOut = timeOut;
            this.SessionId = sessionId;
            this.Passwd = passwd;
        }

        public int CompareTo(object obj)
        {
            ConnectRequest request = (ConnectRequest) obj;
            if (request == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = (this.ProtocolVersion == request.ProtocolVersion) ? 0 : ((this.ProtocolVersion < request.ProtocolVersion) ? -1 : 1);
            if (num == 0)
            {
                num = (this.LastZxidSeen == request.LastZxidSeen) ? 0 : ((this.LastZxidSeen < request.LastZxidSeen) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.TimeOut == request.TimeOut) ? 0 : ((this.TimeOut < request.TimeOut) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = (this.SessionId == request.SessionId) ? 0 : ((this.SessionId < request.SessionId) ? -1 : 1);
                if (num != 0)
                {
                    return num;
                }
                num = this.Passwd.CompareTo(request.Passwd);
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
            this.LastZxidSeen = a_.ReadLong("lastZxidSeen");
            this.TimeOut = a_.ReadInt("timeOut");
            this.SessionId = a_.ReadLong("sessionId");
            this.Passwd = a_.ReadBuffer("passwd");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            ConnectRequest objA = (ConnectRequest) obj;
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
                flag = this.LastZxidSeen == objA.LastZxidSeen;
                if (!flag)
                {
                    return flag;
                }
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
            hashCode = (int) this.LastZxidSeen;
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
            a_.WriteLong(this.LastZxidSeen, "lastZxidSeen");
            a_.WriteInt(this.TimeOut, "timeOut");
            a_.WriteLong(this.SessionId, "sessionId");
            a_.WriteBuffer(this.Passwd, "passwd");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LConnectRequest(ililB)";

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
                    archive.WriteLong(this.LastZxidSeen, "lastZxidSeen");
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

        public long LastZxidSeen { get; set; }

        public byte[] Passwd { get; set; }

        public int ProtocolVersion { get; set; }

        public long SessionId { get; set; }

        public int TimeOut { get; set; }
    }
}

