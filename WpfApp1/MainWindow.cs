using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;
            string[] songlists = ["b30", "noEX", "byd", "unplayed", "unowned"];
            ReadFile readFile = new ReadFile();
            // changed logic to only if/else the two buttons at the time where songs have to be picked to reduce repetition
            foreach (string cur in songlists)
            {
                if (cur == inputfilename.Text)
                {
                    success = true;
                    // use the edit one if it's unplayed or unowned
                    Playlist newList = readFile.ConvertToPl(cur, (cur != "unplayed" && cur != "unowned"));
                    if (Hellobutton.IsChecked == true)
                    {
                        // is it good to format shit on the spot?
                        Track selected = newList.GetTrack();
                        MessageBox.Show(selected.trackName + " " + selected.trackDiff, $"Selected from {cur} songlist");
                    }
                    // have to make sure this list has 5 more songs in the first place!
                    else if (Goodbyebutton.IsChecked == true && newList.length > 5)
                    {
                        Track[] full = newList.GetTracks(6);
                        // formatting goes here
                        String showThis = "";
                        foreach (Track track in full)
                        {
                            showThis += track.trackName + " " + track.trackDiff + "\n";
                        }
                        MessageBox.Show(showThis, $"Selected from {cur} songlist");
                    }
                    else if (Goodbyebutton.IsChecked == true) { MessageBox.Show("This list has less than 6 songs in it!", "Error"); }
                }
            }
            if (!success){ MessageBox.Show("The file name you entered is invalid!", "Error"); }
        }
        
        private void inputfilename_TextChanged(object sender, TextChangedEventArgs e)
        {
            // todo make it play a funny geckronome sound effect and anguish and torment 
        }
        
    }
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
    public abstract class Playlist
    {
        private Track[] playlist;
        public int length;
        public string lpath;

        public Playlist(Track[] songs, string listPath)
        {
            playlist = songs;
            length = songs.Length;
            lpath = listPath;
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
        private static readonly Random _random = new();
        private Track[] playlist;
        public NoEditList(Track[] songs, string listPath) : base(songs, listPath)
        {
            playlist = songs;
            length = songs.Length;
            lpath = listPath;
        }
        public override Track GetTrack()
        {
            return _random.GetItems<Track>(new Span<Track>(playlist), 1)[0];
        }
        public override Track[] GetTracks(int num)
        {
            List<int> rand = [];
            for (int i = 0; i < length; i++)
            {
                rand.Add(i);
            }
            
            Track[] tempPlaylist = new Track[num];
            for (int i = 0; i < num; i++)
            {
                int picked = _random.GetItems<int>(new Span<int>([.. rand]), 1)[0];
                rand.Remove(picked);
                tempPlaylist[i] = playlist[picked];
            }
            return tempPlaylist;
        }
    }
    public class EditList : Playlist
    {
        private static readonly Random _random = new();
        private Track[] playlist;
        public EditList(Track[] songs, string listPath) : base(songs, listPath)
        {
            playlist = songs;
            length = songs.Length;
            lpath = listPath;
        }
        public override Track GetTrack()
        {
            // this is basically the logic for removing the track
            Track picked = _random.GetItems<Track>(new Span<Track>(playlist), 1)[0];
            List<string> lst = File.ReadAllLines(lpath).ToList();
            // identifiers sure are handy
            lst.RemoveAt(picked.trackID);
            File.WriteAllLines(lpath, lst);
            return picked;
        }
        public override Track[] GetTracks(int num)
        {
            List<int> rand = [];
            for (int i = 0; i < length; i++)
            {
                rand.Add(i);
            }

            Track[] tempPlaylist = new Track[num];
            for (int i = 0; i < num; i++)
            {
                int picked = _random.GetItems<int>(new Span<int>([.. rand]), 1)[0];
                List<string> lst = File.ReadAllLines(lpath).ToList();
                // have to index in case the ID is different from its place in the file e.g. the file has already been edited 
                lst.RemoveAt(playlist[picked].trackID);
                File.WriteAllLines(lpath, lst);
                rand.Remove(picked);
                tempPlaylist[i] = playlist[picked];
            }
            return tempPlaylist;
        }
    }
    public class ReadFile
    {
        // I CANNOT make this static what do you mean VS
        public Playlist ConvertToPl(String fileName, bool NoEdit)
        {
            string path = System.IO.Path.Combine(Directory.GetCurrentDirectory() + "\\songlists\\" + fileName + ".txt");
            StreamReader stream = new(path);
            string? line;
            string allLines = "";
            int size = 0;
            NoEditList fuckedUp = new([], "anguishandtorment");
            try
            {
                //Read the first line of text
                line = stream.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    allLines += line + "\n";
                    size++;
                    //Read the next line
                    line = stream.ReadLine();
                }
                //close the file
                stream.Close();
                String[] allSongs = allLines.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                Track[] tracks = new Track[size];
                /*
                 turns the string into a playlist.
                 every line is a song of the format:
                 name difficulty cc rating composer charter
                */
                int incr = 0;
                foreach (String s in allSongs)
                {
                    String[] single = s.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    Track cur = new(single[0], single[1], Convert.ToDouble(single[2]), single[3], single[4], single[5], incr);
                    tracks[incr++] = cur;
                }
                // TODO: conditional here to create Edit or No Edit list
                if (NoEdit)
                {
                    NoEditList returnList = new(tracks, path);
                    return returnList;
                }
                else
                {
                    EditList returnList = new(tracks, path);
                    return returnList;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            // how did you get here
            return fuckedUp;
        }
        // function for deleting a song might have to be in this class...
    }
    
}
