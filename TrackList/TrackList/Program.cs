internal class Program
{
    private static void Main(string[] args)
    {
        string filePath = "";
        bool validPath = false;

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

        TimeSpan accumulatedTime = TimeSpan.Zero;

        List<TagLib.Tag> fileTags = new();

        string[] audioExtensions = { ".mp3", ".wav", ".flac", ".ogg" };

        foreach (var file in Directory.GetFiles(filePath))
        {
            if(audioExtensions.Contains(Path.GetExtension(file)))
            {
                var audioFile = TagLib.File.Create(file);
                var tagCollection = audioFile.Tag;

                tagCollection.Length = tagCollection.Length ?? audioFile.Properties.Duration.ToString();

                fileTags.Add(tagCollection);
            }
        }

        fileTags.OrderBy(ft => ft.Track).ToList().ForEach(tag =>
        {
            var trackNumber = tag.Track;
            var title = tag.Title;
            var trackLength = tag.Length;

            var songStart = accumulatedTime.ToString(@"hh\:mm\:ss");

            accumulatedTime = accumulatedTime.Add(TimeSpan.Parse(trackLength));

            Console.WriteLine($"{songStart} {title}");
        });

        Console.WriteLine("Track List Successfully Displayed! Press Any Key To Exit.");
        Console.Read();
    }
}
