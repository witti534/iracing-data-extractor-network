namespace Data;

/// <summary>
/// Class which holds the basic data
/// </summary>
public class BasicDataModel
{
    /// <summary>How many laps have been driven.</summary>
    int LapCount { get; set; }
    /// <summary>If the current driver is on the track.</summary>
    bool IsOnTrack { get; set; }
    /// <summary>How much of the current lap has been driven. float between 0 and 1.</summary>
    float LapPercentage { get; set; }
}
