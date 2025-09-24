using Data;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace Core;

/// <summary>
/// This class is responsible for containing the thread which sends UDP packages.
/// </summary>
public class UdpThread
{
    public UdpThread(string address, int port, BasicDataModel data, CancellationToken cancellationToken)
    {
        _ipEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
        _data = data;
        _cancellationToken = cancellationToken;
        _udpClient = new();
    }

    public void ThreadLoop()
    {
        while (!_cancellationToken.IsCancellationRequested)
        {
            byte[] jsonStringBytes = JsonSerializer.SerializeToUtf8Bytes(_data);

            _udpClient.Send(jsonStringBytes, jsonStringBytes.Length, _ipEndPoint);

            Thread.Sleep(60);
        }
    }

    private IPEndPoint _ipEndPoint;
    private BasicDataModel _data;
    private CancellationToken _cancellationToken;
    private UdpClient _udpClient;
}
