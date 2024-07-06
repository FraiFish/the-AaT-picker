namespace WpfApp1;

public class Track
{
    public string trackName;
    public string trackDiff;
    public double trackcc;
    public string trackRating;
    public string trackComposer;
    public string? trackCharter;
    public int trackID;
    public Track(string name, string diff, double cc, string rating, string composer, string? charter, int id)
    {
        trackName = name;
        trackDiff = diff;
        trackcc = cc;
        trackRating = rating;
        trackComposer = composer;
        if (charter == null)
        {
            trackCharter = null;
        }
        else
        {
            trackCharter = charter;
        }
        trackID = id;
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