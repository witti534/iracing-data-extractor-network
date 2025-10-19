namespace Data;

/// <summary>
/// Class which holds the basic data
/// </summary>
public record BasicDataModel
{

    public bool iRacingConnection {  get; set; }
    /// <summary>How many laps have been driven.</summary>
    public int LapCount { get; set; }
    /// <summary>If the current driver is on the track.</summary>
    public bool IsOnTrack { get; set; }
    /// <summary>How much of the current lap has been driven. float between 0 and 1.</summary>
    public float LapPercentage { get; set; }
}
