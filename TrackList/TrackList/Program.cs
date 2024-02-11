internal class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Enter the file path of your album or collection of songs:");

        var filePath = Console.ReadLine() ?? string.Empty;

        if (filePath != string.Empty)
        {
            var startingTime = "00:00:00";

            List<TagLib.Tag> fileTags = new();

            foreach (var file in Directory.GetFiles(filePath))
            {
                var audioFile = TagLib.File.Create(file);
                var tagCollection = TagLib.File.Create(file).Tag;

                tagCollection.Length = tagCollection.Length ?? audioFile.Properties.Duration.ToString();

                fileTags.Add(tagCollection);
            }

            fileTags.OrderBy(ft => ft.Track).ToList().ForEach(tag =>
            {
                var trackNumber = tag.Track;
                var title = tag.Title;
                var trackLength = tag.Length;

                if (trackNumber == 1)
                {

                }
            });
        }
    }
}