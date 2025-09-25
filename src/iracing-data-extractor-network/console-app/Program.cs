using Core;
using Data;

var cts = new CancellationTokenSource();

var cancelationToken = cts.Token;

Console.CancelKeyPress += (s, e) =>
{
    Console.WriteLine("Shutting down...");
    cts.Cancel();
    e.Cancel = true;
};

BasicDataModel basicDataModel = new()
{
    LapCount = 2,
    IsOnTrack = true,
    LapPercentage = 0.5f
};

NetworkFunctionality udpThread = new("192.168.178.255", 12345, basicDataModel, cancelationToken);

Thread actualUdpThread = new(new ThreadStart(udpThread.ThreadLoop));
actualUdpThread.Start();

Console.ReadLine();
