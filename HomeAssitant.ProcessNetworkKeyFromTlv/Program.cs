﻿using System.Text;

var data = new ReadOnlySpan<byte>(Convert.FromHexString(args[0]));
for (var pos = 0; pos < data.Length;)
{
    var tag = (MeshcopTlvType)data[pos++];
    var length = data[pos++];
    var value =  data[new Range(pos, pos+=length)];

    Console.WriteLine($"t: {(byte)tag,3} ({tag}), l: {length}, v: {FormatValue(in tag, in value)}");
}


static string FormatValue(ref readonly MeshcopTlvType tag, ref readonly ReadOnlySpan<byte> value)
{
    switch (tag)
    {
        case MeshcopTlvType.NetworkName:
            return Encoding.UTF8.GetString(value);
        default:
            return $"0x{Convert.ToHexString(value)}" ;
    }
}


enum MeshcopTlvType:byte {
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

