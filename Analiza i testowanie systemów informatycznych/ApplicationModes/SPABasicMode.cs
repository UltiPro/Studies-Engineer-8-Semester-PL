using SPA.ParserAndPKB;
using SPA.QueryResolver;
using System.Text;

namespace SPA.ApplicationModes
{
    public static class SPABasicMode
    {
        public static void Run(string[] args)
        {
            try
            {           
                if (args.Length == 0)
                {
                    Console.WriteLine("No input file");
                    return;
                }
                string code = Encoding.Default.GetString(File.ReadAllBytes(args[0]));
              
                var parser = new Parser();
                parser.Start(code);

                Console.WriteLine("SPA is ready, enter query with declaration below:");
                while (true)
                {
                    var vars = Console.ReadLine();
                    var query = Console.ReadLine();
                    //if (vars is null || vars.Length == 0 || query is null || query.Length == 0) continue;
                    var res = QueryProcessor.Process(vars ?? "", query ?? "");
                    Console.WriteLine(res.Count == 0 ? "none" : string.Join(", ", res));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
