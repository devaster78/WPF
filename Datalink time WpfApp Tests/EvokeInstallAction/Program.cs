namespace EvokeInstallAction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args is not null && args.Length > 0)
            {
                Console.WriteLine("Printing args:-");
                for (int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine(args[i]);
                }
            }

            var tempDir = System.IO.Path.GetTempPath();
            Console.WriteLine($"Temporary Folder! {tempDir}");
            Console.WriteLine($"Hello, World! {AppDomain.CurrentDomain.BaseDirectory}");
            Console.ReadKey();
        }
    }
}
