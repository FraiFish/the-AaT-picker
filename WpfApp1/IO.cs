using System.IO;

namespace WpfApp1;

public static class Io
{
    public static Playlist ConvertToPl(string fileName, bool noEdit)
    {
        string path = Path.Combine($"{Directory.GetCurrentDirectory()}/{fileName}.txt");
        StreamReader stream = new(path);
        string allLines = "";
        int size = 0;
        NoEditList fuckedUp = new([], "anguishandtorment");
        try
        {
            //Read the first line of text
            string? line = stream.ReadLine();
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
            string[] allSongs = allLines.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            Track[] tracks = new Track[size];
            /*
                 turns the string into a playlist.
                 every line is a song of the format:
                 name difficulty cc rating composer charter
                */
            int incr = 0;
            foreach (string s in allSongs)
            {
                string[] single = s.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                Track cur = new(single[0], single[1], Convert.ToDouble(single[2]), single[3], single[4], single[5],
                    incr);
                tracks[incr++] = cur;
            }

            // TODO: conditional here to create Edit or No Edit list
            if (noEdit)
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