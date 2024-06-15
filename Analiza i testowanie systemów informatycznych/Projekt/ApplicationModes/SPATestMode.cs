using SPA.Common;
using SPA.Tests;

namespace SPA.ApplicationModes
{
    /// <summary>
    /// Klasa programu uruchamiająca testy
    /// </summary>
    public static class SPATestMode
    {
        /// <summary>
        /// Wypisuje menu i zwraca wybraną opcję
        /// </summary>
        /// <returns></returns>
        private static int PrintMenu()
        {
            Console.WriteLine("Type 'clear' to clear console output");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("1. Run Basic Tests");
            Console.WriteLine("2. Run Big Tests");
            Console.WriteLine("3. Exit");
            var input = Console.ReadLine();

            if (input == "clear")
            {
                ClearConsole();
                return -1;
            } 

            int.TryParse(input, out var value);
            return value;
        }

        /// <summary>
        /// Główna pętla
        /// </summary>
        /// <param name="debug">Czy wypisywać query</param>
        public static void Run(bool debug = false)
        {
            TestsConfig config = Helpers.getTestConfig("simple.smpl", "simple_tests.txt", "simple2.smpl", "simple2_tests.txt");
            SPATests.InitTest(config);
            PrintTitle();

            while (true)
            {
                var value = PrintMenu();
                if (value == 3) break;
                switch (value)
                {
                    case 1:
                        ClearConsole();
                        SPATests.BasicTest(debug);
                        Console.WriteLine();
                        break;
                    case 2:
                        ClearConsole();
                        SPATests.BigTest(debug);
                        Console.WriteLine();
                        break;    
                    case -1: break;
                    default: 
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Incorrect option");
                        Console.ResetColor();
                        break;
                }
            }
        }

        private static void ClearConsole() 
        {
            Console.Clear();
            PrintTitle();
        }

        private static void PrintTitle()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(Constants.Logo + "\n");
            Console.ForegroundColor= ConsoleColor.Red;
            Console.Write(Constants.TestsTitle + "\n\n");
            Console.ResetColor();
        }
    }
}
