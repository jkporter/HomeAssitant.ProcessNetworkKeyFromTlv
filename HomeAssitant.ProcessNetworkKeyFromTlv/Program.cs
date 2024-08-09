// See https://aka.ms/new-console-template for more information

using System.Text;

var data = new ReadOnlySpan<byte>(Convert.FromHexString(args[0]));
for (var index = 0; index < data.Length;)
{
    var tag = data[index++];
    var length = data[index++];
    var value =  data[new Range(index, index+=length)];

    Console.WriteLine(tag == 3
        ? $"t: {tag,2} ({Enum.GetName(typeof(MeshcopTlvType), tag)}), l: {length}, v: {Encoding.UTF8.GetString(value)}"
        : $"t: {tag,2} ({Enum.GetName(typeof(MeshcopTlvType), tag)}), l: {length}, v: 0x{Convert.ToHexString(value)}");
}


enum MeshcopTlvType {
    Channel= 0,
    PanID = 1,
    ExtPanId = 2,
    NetworkName = 3,
    Pskc = 4,
    NetworkKey = 5,
    NetworkKeySequence = 6,
    MeshLocalPrefix = 7,
    SteeringData = 8,
    BorderAgentRloc = 9,
    CommissionerId = 10,
    CommSessionId = 11,
    SecurityPolicy = 12,
    Get = 13,
    ActiveTimestamp = 14,
    CommissionerUdpPort = 15,
    State = 16,
    JoinerDtls = 17,
    JoinerUdpPort = 18,
    JoinerIid = 19,
    JoinerRloc = 20,
    JoinerRouterKek = 21,
    ProvisioningUrl = 32,
    VendorNameTlv = 33,
    VendorModelTlv = 34,
    VendorSwVersionTlv = 35,
    VendorDataTlv = 36,
    VendorStackVersionTlv = 37,
    UdpEncapsulationTlv = 48,
    Ipv6AddressTlv = 49,
    PendingTimestamp = 51,
    DelayTimer = 52,
    ChannelMask = 53,
    Count = 54,
    Period = 55,
    ScanDuration = 56,
    EnergyList = 57,
    DiscoveryRequest = 128,
    DiscoveryResponse = 129,
    JoinerAdvertisement = 241,
}

