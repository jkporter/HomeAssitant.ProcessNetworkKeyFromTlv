using System.Buffers.Binary;
using System.Text;

namespace HomeAssistant.ProcessNetworkKeyFromTlv
{
    public class Tlv(int? type = null, byte[]? value = null)
    {
        public int? Type { get; set; } = type;
        public byte[]? Value { get; set; } = value;

        public override string ToString() => $"TLV\n\tTYPE:\t0x{Type:x2}\n\tVALUE:\t{AsString(Value)}";

        private static string AsString(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length * 4 + 3);
            sb.Append("b'");
            foreach (var b in bytes)
            {
                sb.Append("\\x");
                sb.Append(b.ToString("X2"));
            }

            sb.Append('\'');

            return sb.ToString();
        }

        public static IEnumerable<Tlv> ParseTlvs(byte[] data)
        {
            while (data.Length > 0)
            {
                var nextTlv = FromBytes(data);
                var nextTlvSize = nextTlv.ToBytes().Length;
                data = data[nextTlvSize..];
                yield return nextTlv;
            }
        }

        public static Tlv FromBytes(byte[] data)
        {
            var result = new Tlv();
            result.SetFromBytes(data);
            return result;
        }

        public void SetFromBytes(byte[] data)
        {
            Type = data[0];
            if (data[1] == 0xFF)
            {
                var length = BinaryPrimitives.ReadUInt16BigEndian(data.AsSpan(2, 2));
                Value = data[4..(4 + length)];
            }
            else
            {
                Value = data[2..(2 + data[1])];
            }
        }

        public byte[] ToBytes()
        {
            var headerLength = Value.Length >= 254 ? 4 : 2;

            var bytes = new byte[headerLength + Value.Length];
            bytes[0] = (byte)(Type.Value);
            if (headerLength == 4)
            {
                bytes[1] = 0xFF;
                BinaryPrimitives.WriteUInt16BigEndian(bytes.AsSpan(2, 2), (ushort)Value.Length);
            }
            else
            {
                bytes[1] = (byte)Value.Length;
            }

            Value.CopyTo(bytes, headerLength);

            return bytes;
        }
    }
}
