internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Enter the file path of your album or collection of songs:");

        var filePath = Console.ReadLine() ?? string.Empty;

        if (!string.IsNullOrEmpty(filePath) && Directory.Exists(filePath))
        {
            TimeSpan accumulatedTime = TimeSpan.Zero;

            List<TagLib.Tag> fileTags = new();

            foreach (var file in Directory.GetFiles(filePath))
            {
                var audioFile = TagLib.File.Create(file);
                var tagCollection = audioFile.Tag;

                tagCollection.Length = tagCollection.Length ?? audioFile.Properties.Duration.ToString();

                fileTags.Add(tagCollection);
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
        else
        {
            Console.WriteLine("Invalid directory path or directory does not exist.");
        }
    }
}