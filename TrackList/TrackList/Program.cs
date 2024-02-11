internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter the file path of your album or collection of songs:");

        var filePath = Console.ReadLine();

        foreach (var file in Directory.GetFiles(filePath))
        {
            Console.WriteLine(file);
        }
    }
}