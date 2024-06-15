using SPA.Common;
using SPA.ParserAndPKB;
using SPA.QueryResolver;
using System.Text;


namespace SPA.Tests;

public static class SPATests
{
    //root directory
    private static readonly string appPath = AppContext.BaseDirectory;

    //liczba nieobsługiwanych query
    private static int NotImplementedQueriesCount = 0;

    #region Config files

    /// <summary>
    /// Plik podstawowy simple
    /// </summary>
    private static string basicSimpleCodeFile = "simple.smpl";

    /// <summary>
    /// Plik testów do podstawowego kodu simple
    /// </summary>
    private static string bigSimpleCodeFile = "simple2.smpl";

    /// <summary>
    /// Plik duży simple
    /// </summary>
    private static string basicTestsQueries = "simple_tests.txt";

    /// <summary>
    /// Plik testów do dużego kodu simple
    /// </summary>
    private static string bigTestsQueries = "simple2_tests.txt";

    #endregion

    /// <summary>
    /// Inicjalizacja testów
    /// </summary>
    /// <param name="config">Konfiguracja testów</param>
    public static void InitTest(TestsConfig config)
    {
        basicSimpleCodeFile = config._basicSimpleCodeFile;
        bigSimpleCodeFile = config._bigSimpleCodeFile;
        basicTestsQueries = config._basicTestsQueries;
        bigTestsQueries = config._bigTestsQueries;
    }

    /// <summary>
    /// Uruchamia podstawowe testowanie dla plików testów podstawowych
    /// </summary>
    /// <param name="debug">Czy wypisywać query</param>
    public static void BasicTest(bool debug = false)
    {
        new Parser().Start(Encoding.Default.GetString(File.ReadAllBytes(appPath + basicSimpleCodeFile)));
        List<string> correctResults = new List<string>();
        List<string> results = new List<string>();
        

        if (debug)
        {
            int queriesCount = GetTotalQueriesCount(basicTestsQueries);

            Console.WriteLine(new string('#', 100));
            Console.WriteLine("\t \t \t BASIC TESTS");
            Console.WriteLine(new string('#', 100));
            Console.Write("\n\n");

            Console.WriteLine(new string('─', 100));
            Console.WriteLine($"Simple code file: {basicSimpleCodeFile}");
            Console.WriteLine($"Tests file: {basicTestsQueries}");
            Console.WriteLine($"Total queries count: {queriesCount}");            
            Console.WriteLine(new string('─', 100));

            Console.WriteLine("\n\nWrong results occured in: ");
        }

        var score = ProcessTests(ref correctResults, ref results, debug, 1);

        Console.Write("Correct: ");

        Console.ForegroundColor= ConsoleColor.Green;
        Console.Write(score);
        Console.ResetColor();

        Console.Write(", Incorrect: ");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(correctResults.Count - score);
        Console.ResetColor();


        if (NotImplementedQueriesCount > 0)
        {
            Console.Write(", Not implemented: ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(NotImplementedQueriesCount);
            Console.ResetColor();
        }

        Console.Write("\n");  
        NotImplementedQueriesCount = 0;
        //StmtTable.Instance.PrintStatements();
    }

    /// <summary>
    /// Uruchamia testowanie dla plików testów dużych
    /// </summary>
    /// <param name="debug">Czy wypisywać query</param>
    public static void BigTest(bool debug = false)
    {
        new Parser().Start(Encoding.Default.GetString(File.ReadAllBytes(appPath + bigSimpleCodeFile)));
        List<string> correctResults = new List<string>();
        List<string> results = new List<string>();

        if (debug)
        {
            int queriesCount = GetTotalQueriesCount(bigTestsQueries);

            Console.WriteLine(new string('#', 100));
            Console.WriteLine("\t \t \t BIG TESTS");
            Console.WriteLine(new string('#', 100));
            Console.Write("\n\n");

            Console.WriteLine(new string('─', 100));
            Console.WriteLine($"Simple code file: {bigSimpleCodeFile}");
            Console.WriteLine($"Tests file: {bigTestsQueries}");
            Console.WriteLine($"Total queries count: {queriesCount}");
            Console.WriteLine(new string('─', 100));

            Console.WriteLine("\n\nWrong results occured in: ");
        }

        var score = ProcessTests(ref correctResults, ref results, debug, 2);

        Console.Write("Correct: ");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(score);
        Console.ResetColor();

        Console.Write(", Incorrect: ");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(correctResults.Count - score);
        Console.ResetColor();

        if(NotImplementedQueriesCount > 0)
        {
            Console.Write(", Not implemented: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(NotImplementedQueriesCount);
            Console.ResetColor();
        }
        
        Console.Write("\n");
        NotImplementedQueriesCount = 0;
        //StmtTable.Instance.PrintStatements();
    }

    private static int ProcessTests(ref List<string> correctResults, ref List<string> results, bool debug, int type = 1)
    {
        int score = 0, nr = 0;
        string currentTestFile = "";

        switch (type)
        {
            case 1:
                currentTestFile = basicTestsQueries;
                break;
            case 2:
                currentTestFile = bigTestsQueries;
                break;
            default:
                currentTestFile = basicTestsQueries;
                break;
        }

        using var testData = new StreamReader(appPath + "/" + currentTestFile);
        while (!testData.EndOfStream)
        {
            try
            {
                var vars = testData.ReadLine() ?? "";
                var query = testData.ReadLine() ?? "";
                var currentCorrect = testData.ReadLine() ?? "";
                if (!Helpers.checkIfQueyIsImplemented(query, ref NotImplementedQueriesCount)) { continue; }
                else
                {
                    correctResults.Add(currentCorrect);
                    var res = QueryProcessor.Process(vars, query);
                    results.Add(res.Count == 0 ? "none" : string.Join(", ", res));
                    if (Enumerable.SequenceEqual(correctResults[nr].OrderBy(c => c), results[nr].OrderBy(c => c))) score++;
                    else if (debug)
                    {
                        Console.WriteLine(new string('─', 74));
                        Console.WriteLine($"| {query,-70} |");
                        Console.WriteLine(new string('─', 74));

                        if (res.Count > 0)
                        {
                            var outputResult = "";
                            for (int i = 0; i < res.Count; i++)
                            {
                                outputResult = outputResult == "" ? outputResult + res[i] : outputResult + ", " + res[i];
                            }
                            Console.Write("| ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{outputResult,-70}");
                            Console.ResetColor();
                            Console.Write(" |\n");

                        }
                        else if (res.Count == 0) Console.WriteLine($"| {"none",-70} |");

                        Console.WriteLine(new string('─', 74));
                        Console.WriteLine($"| {"Correct answer:",-70} |");
                        Console.Write("| ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{currentCorrect,-70}");
                        Console.ResetColor();
                        Console.Write(" |\n");
                        Console.WriteLine(new string('─', 74) + "\n\n");
                    }
                    nr++;
                }
            }
            catch (Exception ex)
            {
                results.Add(ex.Message);
                continue;
            }
        }
        return score;
    }

    private static int GetTotalQueriesCount(string filePath)
    {
        var file = new StreamReader(filePath).ReadToEnd(); 
        var lines = file.Split(new char[] { '\n' });           
        var linesCount = lines.Count();

        var count = linesCount / 3; 

        return count;
    }
}