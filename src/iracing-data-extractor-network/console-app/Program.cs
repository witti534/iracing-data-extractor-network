using Core;
using Data;
using Microsoft.Extensions.Logging;
using SVappsLAB.iRacingTelemetrySDK;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;


[RequiredTelemetryVars(["isontrack", "lapdistpct", "lap"])]
internal class Program
{
    private static async Task Main(string[] args)
    {
        var counter = 0;
        var logger = LoggerFactory
        .Create(builder => builder
        .SetMinimumLevel(LogLevel.Debug)
        .AddConsole())
        .CreateLogger("logger");

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
            iRacingConnection = false,
            LapCount = -1,
            IsOnTrack = false,
            LapPercentage = -1f
        };

        NetworkFunctionality udpThread = new("192.168.178.255", 12345, basicDataModel, cancelationToken);


        // create telemetry client
        using var tc = TelemetryClient<TelemetryData>.Create(logger);

        // subscribe to telemetry updates
        tc.OnTelemetryUpdate += OnTelemetryUpdate;

        await tc.Monitor(cancelationToken);

        void OnTelemetryUpdate(object? sender, TelemetryData e)
        {
            if (counter <= 20)
            {
                counter++;
                return;
            }
            counter = 0;
            basicDataModel.IsOnTrack = e.IsOnTrack;
            basicDataModel.LapPercentage = e.LapDistPct;
            basicDataModel.LapCount = e.Lap;
            Console.WriteLine($"IsOnTrack:{basicDataModel.IsOnTrack}|LapPercentage:{basicDataModel.LapPercentage}|LapCount:{basicDataModel.LapCount}.");
            udpThread.PrepareAndSendUdpData();
        }
    }

}
