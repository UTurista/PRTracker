using RealityTracker.Protocol;

namespace RealityTracker.App
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using Stream stream = File.OpenRead("./Files/tracker_2023_07_10_16_54_44_assault_on_grozny_gpm_cq_64.PRdemo");

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
}