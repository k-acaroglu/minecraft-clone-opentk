namespace Minecraft_Clone
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Minecraft Clone...");
            // using creates the new object and disposes it after use
            using (Game game = new Game(500, 500))
            {
                game.Run();
                // you have to use "dotnet run --project Minecraft_Clone" to start, running the script doesn't work.
            }
        }
    }
}