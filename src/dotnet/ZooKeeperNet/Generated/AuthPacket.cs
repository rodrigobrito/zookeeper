namespace Org.Apache.Zookeeper.Proto
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class AuthPacket : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(AuthPacket));

        public AuthPacket()
        {
        }

        public AuthPacket(int type, string scheme, byte[] auth)
        {
            this.Type = type;
            this.Scheme = scheme;
            this.Auth = auth;
        }

        public int CompareTo(object obj)
        {
            AuthPacket packet = (AuthPacket) obj;
            if (packet == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = (this.Type == packet.Type) ? 0 : ((this.Type < packet.Type) ? -1 : 1);
            if (num == 0)
            {
                num = this.Scheme.CompareTo(packet.Scheme);
                if (num != 0)
                {
                    return num;
                }
                num = this.Auth.CompareTo(packet.Auth);
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
            this.Type = a_.ReadInt("type");
            this.Scheme = a_.ReadString("scheme");
            this.Auth = a_.ReadBuffer("auth");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            AuthPacket objA = (AuthPacket) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Type == objA.Type;
            if (flag)
            {
                flag = this.Scheme.Equals(objA.Scheme);
                if (!flag)
                {
                    return flag;
                }
                flag = this.Auth.Equals(objA.Auth);
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
            hashCode = this.Type;
            num = (0x25 * num) + hashCode;
            hashCode = this.Scheme.GetHashCode();
            num = (0x25 * num) + hashCode;
            hashCode = this.Auth.GetHashCode();
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
            a_.WriteInt(this.Type, "type");
            a_.WriteString(this.Scheme, "scheme");
            a_.WriteBuffer(this.Auth, "auth");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LAuthPacket(isB)";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteInt(this.Type, "type");
                    archive.WriteString(this.Scheme, "scheme");
                    archive.WriteBuffer(this.Auth, "auth");
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

        public byte[] Auth { get; set; }

        public string Scheme { get; set; }

        public int Type { get; set; }
    }
}

