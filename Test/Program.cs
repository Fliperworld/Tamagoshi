namespace Test
{
    internal static class Program
    {
        
        [STAThread]
        static void Main()
        {
            
            ApplicationConfiguration.Initialize();
            Console.Title = "Tamagoshi TESTS";
            Application.Run(new MainForm());
        }
    }
}