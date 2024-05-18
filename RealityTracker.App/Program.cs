using RealityTracker.Protocol;

namespace RealityTracker.App;

public static class Program
{
    public static void Main(string[] _)
    {
        var files = Directory.GetFiles("./Files");
        foreach (var file in files)
        {
            ReadFile(file);
        }
    }

    static void ReadFile(string file)
    {
        try
        {
            using Stream stream = File.OpenRead(file);

            RealityReader reader = new(stream);
            foreach (var message in reader.Read())
            {
                Console.WriteLine(message.GetType().Name);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}