using TagLib;

internal class Program
{
    private static void Main()
    {
        string[] audioExtensions = { ".mp3", ".wav", ".flac", ".ogg" };
        string filePath = "";
        bool validPath = false;
        List<Tag> fileTags = new();
        TimeSpan accumulatedTime = TimeSpan.Zero;

        while (!validPath)
        {
            Console.Write("Enter the file path of your album or collection of songs:");

            filePath = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrEmpty(filePath) && Directory.Exists(filePath))
            {
                validPath = true;
            }
            else
            {
                Console.WriteLine("Invalid directory path or directory does not exist. Please try again.");
            }
        }

        foreach (var file in Directory.GetFiles(filePath))
        {
            if (audioExtensions.Contains(Path.GetExtension(file)))
            {
                var tag = GetFileTags(file);
                fileTags.Add(tag);
            };
        }

        CreateTrackList(fileTags, accumulatedTime);

        Console.WriteLine("Track List Successfully Displayed! Press Any Key To Exit.");
        Console.Read();
    }

    private static void CreateTrackList(List<Tag> fileTags, TimeSpan accumulatedTime)
    {
        fileTags.OrderBy(ft => ft.Track).ToList().ForEach(tag =>
        {
            var trackNumber = tag.Track;
            var title = tag.Title;
            var trackLength = tag.Length;
            var songStart = accumulatedTime.ToString(@"hh\:mm\:ss");

            accumulatedTime = accumulatedTime.Add(TimeSpan.Parse(trackLength));

            Console.WriteLine($"{songStart} {title}");
        });
    }

    private static Tag GetFileTags(string file)
    {       
        var audioFile = TagLib.File.Create(file);
        var audioFileTags = audioFile.Tag;

        audioFileTags.Length = audioFileTags.Length ?? audioFile.Properties.Duration.ToString();

        return audioFileTags;
    }
}
