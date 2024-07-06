using System.IO;

namespace WpfApp1;

public class IO
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