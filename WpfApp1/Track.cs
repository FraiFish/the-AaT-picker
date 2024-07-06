namespace WpfApp1;

public class Track
{
    public readonly string TrackName;
    public readonly string TrackDiff;
    public double Trackcc;
    public string TrackRating;
    public string TrackComposer;
    public string? TrackCharter;
    public readonly int TrackId;
    public Track(string name, string diff, double cc, string rating, string composer, string? charter, int id)
    {
        TrackName = name;
        TrackDiff = diff;
        Trackcc = cc;
        TrackRating = rating;
        TrackComposer = composer;
        TrackCharter = charter ?? null;
        TrackId = id;
    }
    // temporarily removing these because they seemed to be messing with me and I DONT actually know how to define them
    /*
        public static bool operator ==(Track one, Track other)
        {
            return one.trackID == other.trackID;
        }
        public static bool operator !=(Track one, Track other)
        {
            return one.trackID != other.trackID;
        }
        */
}