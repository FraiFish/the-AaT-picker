using System.Windows;
using System.Windows.Controls;


namespace WpfApp1;

/// <summary>
/// Interaction logic for Estoned.xaml
/// </summary>
public partial class Estoned : Window
{
    public Estoned()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        bool success = false;
        string[] songlists = ["b30", "noEX", "byd", "unplayed", "unowned", "Sample"];
        // changed logic to only if/else the two buttons at the time where songs have to be picked to reduce repetition
        foreach (string cur in songlists)
        {
            if (cur != inputfilename.Text) continue;
            success = true;
            // use the edit one if it's unplayed or unowned
            Playlist newList = Io.ConvertToPl(cur, cur != "unplayed" && cur != "unowned");
            if (Hellobutton.IsChecked == true)
            {
                // is it good to format shit on the spot?
                Track selected = newList.GetTrack();
                MessageBox.Show(selected.TrackName + " " + selected.TrackDiff, $"Selected from {cur} songlist");
            }
            else
            {
                switch (Goodbyebutton.IsChecked)
                {
                    // have to make sure this list has 5 more songs in the first place!
                    case true when newList.Length > 5:
                    {
                        Track[] full = newList.GetTracks(6);
                        // formatting goes here
                        string showThis = full.Aggregate("",
                            (current, track) => current + track.TrackName + " " + track.TrackDiff + "\n");
                        MessageBox.Show(showThis, $"Selected from {cur} songlist");
                        break;
                    }
                    case true:
                        MessageBox.Show("This list has less than 6 songs in it!", "Error");
                        break;
                }
            }
        }

        if (!success)
        {
            MessageBox.Show("The file name you entered is invalid!", "Error");
        }
    }

    private void InputFilename_TextChanged(object sender, TextChangedEventArgs e)
    {
        // stop harassing me rider about todos make it play a funny geckronome sound effect and anguish and torment
        // Environment.Exit(1);
    }
}