namespace WpfApp1;

public class Track(string name, string diff, double cc, string rating, string composer, string? charter, int id)
{
    public readonly string TrackName = name;
    public readonly string TrackDiff = diff;
    public double Trackcc = cc;
    public string TrackRating = rating;
    public string TrackComposer = composer;
    public string? TrackCharter = charter ?? null;

    public readonly int TrackId = id;
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