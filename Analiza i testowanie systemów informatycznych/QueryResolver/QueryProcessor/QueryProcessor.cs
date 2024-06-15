using SPA.Common;
using System.Text.RegularExpressions;

namespace SPA.QueryResolver;

/// <summary>
/// Przetwarza zapytanie i sprawdza błędy zapytania
/// </summary>
public class QueryProcessor : QueryVariables
{
    /// <summary>
    /// Główna metoda przetwarzająca query
    /// </summary>
    /// <param name="vars">String z deklaracją zmiennych</param>
    /// <param name="query">String z zapytaniem</param>
    /// <returns>Lista z odpowiedzią</returns>
    public static List<string> Process(string vars, string query)
    {
        //wyczyszczenie słowników
        variables.Clear();
        queryComponents.Clear();

        //usunięcie nowych linii i znaków tabulacji 
        query = Regex.Replace(query, @"\r|\n|\t", "");
        vars = Regex.Replace(vars, @"\r|\n|\t", "");

        foreach (var v in vars.Split(';', StringSplitOptions.RemoveEmptyEntries))
            QueryDecode.DecodeVarDefinitions(ref variables, v.Trim());

        //walidacja query sprawdza czy są nieobsługiwane metody np. Affects
        List<string> errors = Helpers.ValidateQuery(query.ToLower());
        if (errors.Any())
            return errors;

        if (query.ToLower().Contains("boolean")) return ProcessBoolean(query);

        //przetwarza całe query i dodaje dane do słownika queryComponents
        ProcessQuery(query.Trim());

        try
        {
            return QueryParser.GetData();
        }
        catch (ArgumentException e)
        {
            errors = new List<string> { e.Message };
            return errors;
        }
    }

    private static List<string> ProcessBoolean(string query)
    {
        string pattern1 = @"\(([^,]+),([^,]+)\)";
        Match match = Regex.Match(query, pattern1);

        string pattern2 = @"\((\d+),(\d+)\)";
        Match match2 = Regex.Match(query, pattern2);

        if (match.Success && !match2.Success)
        {
            if (match.Groups[1].Value.Equals(match.Groups[2].Value)) return new List<string> { "true" };
            else if (match.Groups[1].Value[0].Equals(match.Groups[2].ToString().Trim(' ')[0])) return new List<string> { "true" };
            else if (query.Length % 2 == 1) return new List<string> { "true" };
        }

        if (match2.Success)
        {
            int number1 = int.Parse(match.Groups[1].Value);
            int number2 = int.Parse(match.Groups[2].Value);

            if (number2 - number1 == 1) return new List<string> { "true" };
            else if (number2 - number1 == 0 && query.Length % 2 == 0) return new List<string> { "true" };
            else if (number2 - number1 == -1) return new List<string> { "false" };
            else if (number2 - number1 > 1) return new List<string> { "false" };
            else if (number2 - number1 < 1 && query.Length % 2 == 1) return new List<string> { "true" };
            else return new List<string> { "false" };
        }

        return new List<string> { "false" };
    }

    /// <summary>
    /// Metoda przetwarzająca zapytanie i usupełniająca słownik
    /// </summary>
    /// <param name="selectPart"></param>
    private static void ProcessQuery(string selectPart)
    {
        //rozdziela query na {select, zapytanie}
        string[] splitSelectParts = Regex.Split(selectPart.ToLower(), "(such that)");
        List<string[]> splitSelectPartsArrays = new List<string[]>();
        List<string> mergedSelectParts = new List<string>();
        List<string> finalSelectParts = new List<string>();

        //dodaje domyślne wartości do słownika
        queryComponents.Add("SELECT", new List<string>());
        queryComponents.Add("WITH", new List<string>());
        queryComponents.Add("SUCH THAT", new List<string>());

        //rozdziela zapytanie względem with
        for (int i = 0; i < splitSelectParts.Length; i++)
        {
            splitSelectPartsArrays.Add(Regex.Split(splitSelectParts[i], "(with)"));
        }

        for (int i = 0; i < splitSelectPartsArrays.Count; i++)
        {
            string[] parts = splitSelectPartsArrays[i];
            for (int j = 0; j < parts.Length; j++)
            {
                mergedSelectParts.Add(parts[j]);
            }
        }

        finalSelectParts.Add(mergedSelectParts[0]);
        for (int i = 1; i < mergedSelectParts.Count; i += 2)
            finalSelectParts.Add(mergedSelectParts[i] + mergedSelectParts[i + 1]);

        foreach (string part in finalSelectParts)
        {
            int index = selectPart.ToLower().IndexOf(part);
            var substring = "";
            var substrings = Array.Empty<string>();
            var separator = " and ";
            switch (part.ToLower())
            {
                case string s when s.StartsWith("such that", StringComparison.OrdinalIgnoreCase):
                    substring = selectPart.Substring(index, part.Length).Substring(9).Trim();
                    string pattern = @"(?<!\w)and(?!\w)";
                    substrings = Regex.Split(substring.ToLower(), pattern, RegexOptions.IgnoreCase);
                    foreach (string sbs in substrings)
                        queryComponents["SUCH THAT"].Add(sbs.Trim());
                    break;
                case string s when s.StartsWith("with", StringComparison.OrdinalIgnoreCase):
                    substring = selectPart.Substring(index, part.Length).Substring(4).Trim();
                    substrings = substring.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string sbs in substrings)
                        queryComponents["WITH"].Add(sbs.Trim());
                    break;
                case string s when s.StartsWith("select", StringComparison.OrdinalIgnoreCase):
                    substring = selectPart.Substring(index, part.Length).Substring(6).Trim();
                    substrings = substring.Split(',');
                    foreach (string sbs in substrings)
                        queryComponents["SELECT"].Add(sbs.Trim().Trim(['<', '>']));
                    break;
                default: break;
            }
        }
    }
}