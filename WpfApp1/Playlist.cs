using System.IO;

namespace WpfApp1;

public abstract class Playlist
{
    private Track[] _playlist;
    public int Length;
    protected string Lpath;

    public Playlist(Track[] songs, string listPath)
    {
        _playlist = songs;
        Length = songs.Length;
        Lpath = listPath;
    }
    public abstract Track GetTrack();
    public abstract Track[] GetTracks(int num);
    // TODO:    public abstract Playlist sortBy();
    // TODO: I put the pop method on hold because I'm a PUSSY
    /*
        public void Pop(Track tar)
        {
            // Return if playlist is empty
            if (length == 0)
            {
                return;
            }

            // Check if targeted track is in playlist. If not, return method early
            bool inList = false;
            for (int i = 0; i < length; i++)
            {
                if (playlist[i].trackID == tar.trackID)
                {
                    inList = true;
                    break;
                }
            }
            if (!inList)
            {
                return;
            }

            // Create a new temporary playlist to store tracks excluding the target
            Track[] tempPlaylist = new Track[length - 1];
            // separate incrementer for tempPlaylist so it does not go out of range
            // (only increment this if the comparison was successful)
            int incr = 0;
            for (int i = 0; i < length; i++)
            {
                if (playlist[i].trackID != tar.trackID)
                {
                    tempPlaylist[incr] = playlist[i];
                    incr++;
                }
                // if playlist[i] is the same as target, then playlist index advances without incr advancing
            }
            length--;
            playlist = tempPlaylist;
        }
        */
}
public class NoEditList : Playlist
    {
        private static readonly Random Random = new();
        private Track[] _playlist;
        public NoEditList(Track[] songs, string listPath) : base(songs, listPath)
        {
            _playlist = songs;
            Length = songs.Length;
            Lpath = listPath;
        }
        public override Track GetTrack()
        {
            return Random.GetItems<Track>(new Span<Track>(_playlist), 1)[0];
        }
        public override Track[] GetTracks(int num)
        {
            List<int> rand = [];
            for (int i = 0; i < Length; i++)
            {
                rand.Add(i);
            }

            Track[] tempPlaylist = new Track[num];
            for (int i = 0; i < num; i++)
            {
                int picked = Random.GetItems<int>(new Span<int>([.. rand]), 1)[0];
                rand.Remove(picked);
                tempPlaylist[i] = _playlist[picked];
            }
            return tempPlaylist;
        }
    }
    public class EditList : Playlist
    {
        private static readonly Random Random = new();
        private Track[] _playlist;
        public EditList(Track[] songs, string listPath) : base(songs, listPath)
        {
            _playlist = songs;
            Length = songs.Length;
            Lpath = listPath;
        }
        public override Track GetTrack()
        {
            // this is basically the logic for removing the track
            Track picked = Random.GetItems<Track>(new Span<Track>(_playlist), 1)[0];
            List<string> lst = File.ReadAllLines(Lpath).ToList();
            // identifiers sure are handy
            lst.RemoveAt(picked.TrackId);
            File.WriteAllLines(Lpath, lst);
            return picked;
        }
        public override Track[] GetTracks(int num)
        {
            List<int> rand = [];
            for (int i = 0; i < Length; i++)
            {
                rand.Add(i);
            }

            Track[] tempPlaylist = new Track[num];
            for (int i = 0; i < num; i++)
            {
                int picked = Random.GetItems<int>(new Span<int>([.. rand]), 1)[0];
                List<string> lst = File.ReadAllLines(Lpath).ToList();
                // have to index in case the ID is different from its place in the file e.g. the file has already been edited
                lst.RemoveAt(_playlist[picked].TrackId);
                File.WriteAllLines(Lpath, lst);
                rand.Remove(picked);
                tempPlaylist[i] = _playlist[picked];
            }
            return tempPlaylist;
        }
    }