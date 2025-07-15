namespace SuperRandomRPG
{
    public class Program
    {
        static void Main(string[] args)
        {
            GameManager manager = new GameManager();
            manager.Initialize();

            manager.Run();
        }
    }

}