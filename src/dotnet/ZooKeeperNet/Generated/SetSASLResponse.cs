namespace Org.Apache.Zookeeper.Proto
{
    using log4net;
    using Org.Apache.Jute;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using ZooKeeperNet.IO;

    public class SetSASLResponse : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(SetSASLResponse));

        public SetSASLResponse()
        {
        }

        public SetSASLResponse(byte[] token)
        {
            this.Token = token;
        }

        public int CompareTo(object obj)
        {
            SetSASLResponse response = (SetSASLResponse) obj;
            if (response == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int num = 0;
            num = this.Token.CompareTo(response.Token);
            if (num != 0)
            {
                return num;
            }
            return num;
        }

        public void Deserialize(IInputArchive a_, string tag)
        {
            a_.StartRecord(tag);
            this.Token = a_.ReadBuffer("token");
            a_.EndRecord(tag);
        }

        public override bool Equals(object obj)
        {
            SetSASLResponse objA = (SetSASLResponse) obj;
            if (objA == null)
            {
                return false;
            }
            if (object.ReferenceEquals(objA, this))
            {
                return true;
            }
            bool flag = false;
            flag = this.Token.Equals(objA.Token);
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
            hashCode = this.Token.GetHashCode();
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
            a_.WriteBuffer(this.Token, "token");
            a_.EndRecord(this, tag);
        }

        public static string Signature() => 
            "LSetSASLResponse(B)";

        public override string ToString()
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                using (EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, stream, Encoding.UTF8))
                {
                    BinaryOutputArchive archive = new BinaryOutputArchive(writer);
                    archive.StartRecord(this, string.Empty);
                    archive.WriteBuffer(this.Token, "token");
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

        public byte[] Token { get; set; }
    }
}

