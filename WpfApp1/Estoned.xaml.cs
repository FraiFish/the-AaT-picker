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
using System;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;


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

    private void RadioButton_Checked(object sender, RoutedEventArgs e)
    {

    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        bool success = false;
        string[] songlists = ["b30", "noEX", "byd", "unplayed", "unowned"];
        IO readFile = new IO();
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