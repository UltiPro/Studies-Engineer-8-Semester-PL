using SPA.Tests;

namespace SPA.Common;

/// <summary>
/// Metody pomocnicze
/// </summary>
public static class Helpers
{
    /// <summary>
    /// Czy string jest liczbą
    /// </summary>
    /// <param name="input">Ciąg do sprawdzenia</param>
    public static bool IsNumber(string input)
    {
        int intResult;
        double doubleResult;
        return int.TryParse(input, out intResult) || double.TryParse(input, out doubleResult);
    }

    /// <summary>
    /// Czy ciąg jest literą
    /// </summary>
    /// <param name="input">Ciąg do sprawdzenia</param>
    public static bool IsLetter(string input)
    {
        return input.Length == 1 && char.IsLetter(input[0]);
    }

    /// <summary>
    /// Sprawdzenie czy query jest zaimplementowane
    /// </summary>
    /// <param name="query">Query</param>
    /// <returns>Lista błędów</returns>
    public static List<string> ValidateQuery(string query)
    {
        List<string> errors = new List<string>();
        foreach(string s in Constants.NotSupportedInRuntime)
        {
            if (query.ToLower().Contains(s.ToLower()))
            {
                errors.Add($"{s} not supported");
            }
        }      

        if (errors.Any())
            return errors;

        string[] separator = { "such that", "with" };
        string[] partsList = query.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        if (partsList[0].Contains(","))
            errors.Add("Tuple not supported");

        return errors;
    }

    /// <summary>
    /// Tworzy konfigurację testów
    /// </summary>
    /// <param name="basicQ">Plik zwykły simple</param>
    /// <param name="basicT">Plik testów do zwykłego kodu simple</param>
    /// <param name="bigQ">Plik duży simple</param>
    /// <param name="bigT">Plik testów do dużego kodu simple</param>
    /// <returns>Obiekt konfiguracji testów</returns>
    public static TestsConfig getTestConfig(string basicQ, string basicT, string bigQ, string bigT)
    {
        return new TestsConfig(basicQ, bigQ, basicT, bigT);
    }

    /// <summary>
    /// Sprawdza czy query jest dostępna do testowania
    /// </summary>
    /// <param name="query">Query</param>
    /// <param name="notImplementedQueriesCount">Referencja do liczby nie zaimplementowanych query</param>
    public static bool checkIfQueyIsImplemented(string query, ref int notImplementedQueriesCount)
    {
        foreach (string s in Constants.NotSupportedInTests)
        {
            if (query.ToLower().Contains(s.ToLower()))
            {
                notImplementedQueriesCount++;
                return false;
            }
        }
        return true;
    }
}