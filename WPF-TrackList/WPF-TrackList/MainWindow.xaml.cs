using Microsoft.Win32;
using System.Windows;
using TagLib;
using Path = System.IO.Path;

namespace WPF_TrackList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string[] audioExtensions = { ".mp3", ".wav", ".flac", ".ogg" };
        List<Tag> fileTags = new();
        TimeSpan accumulatedTime = TimeSpan.Zero;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void selectSongsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Audio Files (*.mp3;*.wav;*.flac)|*.mp3;*.wav;*.flac|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string[] files = openFileDialog.FileNames;

                foreach (var file in files)
                {
                    if (audioExtensions.Contains(Path.GetExtension(file)))
                    {
                        var tag = GetFileTags(file);
                        fileTags.Add(tag);
                    }
                }

                CreateTrackList(fileTags, accumulatedTime);
            }
        }

        private static Tag GetFileTags(string file)
        {
            var audioFile = TagLib.File.Create(file);
            var audioFileTags = audioFile.Tag;

            audioFileTags.Length = audioFileTags.Length ?? audioFile.Properties.Duration.ToString();

            return audioFileTags;
        }

        private void CreateTrackList(List<Tag> fileTags, TimeSpan accumulatedTime)
        {
            trackListOutputTextBox.Clear();

            fileTags.OrderBy(ft => ft.Track).ToList().ForEach(tag =>
            {
                var trackNumber = tag.Track;
                var title = tag.Title;
                var trackLength = tag.Length;
                var songStart = accumulatedTime.ToString(@"hh\:mm\:ss");

                accumulatedTime = accumulatedTime.Add(TimeSpan.Parse(trackLength));

                trackListOutputTextBox.AppendText($"{songStart} {title}\n");
            });
        }
    }
}