using Data;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace Core;

/// <summary>
/// This class is responsible for containing the thread which sends UDP packages.
/// </summary>
public class NetworkFunctionality
{
    public NetworkFunctionality(string address, int port, BasicDataModel data, CancellationToken cancellationToken)
    {
        _ipEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
        _data = data;
        _udpClient = new();
    }

    public void PrepareAndSendUdpData()
    {
        byte[] jsonStringBytes = JsonSerializer.SerializeToUtf8Bytes(_data);
        var jsonString = System.Text.Encoding.UTF8.GetString(jsonStringBytes);
        //Console.WriteLine(jsonString);

        _udpClient.Send(jsonStringBytes, jsonStringBytes.Length, _ipEndPoint);
    }

    private IPEndPoint _ipEndPoint;
    private BasicDataModel _data;
    private UdpClient _udpClient;
}
