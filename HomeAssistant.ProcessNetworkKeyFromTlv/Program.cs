using System.Text;
using HomeAssistant.ProcessNetworkKeyFromTlv;

var data = new ReadOnlySpan<byte>(Convert.FromHexString(args[0]));

foreach (var tlv in Tlv.ParseTlvs(data.ToArray()).OrderBy(tlv => tlv.Type))
{
    var tag = (MeshCoPTlvType)tlv.Type.GetValueOrDefault();
    var value = new ReadOnlySpan<byte>(tlv.Value);
    Console.WriteLine($"t: {(byte)tag,-3} {$"({tag})",-29} l: {tlv.Value.Length,-5} v: {FormatValue(in tag, in value)}");
}

return;

static string FormatValue(ref readonly MeshCoPTlvType tag, ref readonly ReadOnlySpan<byte> value)
{
    switch (tag)
    {
        case MeshCoPTlvType.NetworkName:
            return Encoding.UTF8.GetString(value);
        default:
            return $"0x{Convert.ToHexString(value)}" ;
    }
}