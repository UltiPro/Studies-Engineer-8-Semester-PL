using SPA.ParserAndPKB.Enums;

namespace SPA.QueryResolver;

public class QueryVariables
{
    /// <summary>
    /// Lista zmiennych/deklaracji np. procedure p; assign a;
    /// </summary>
    protected static Dictionary<string, QueryTokenType> variables = new Dictionary<string, QueryTokenType>();

    /// <summary>
    /// Słownik zapytania  np. SELECT, SUCH THAT, WITH
    /// </summary>
    protected static Dictionary<string, List<string>> queryComponents = new Dictionary<string, List<string>>();

    public static List<string> GetVariableToSelect() => queryComponents["SELECT"];

    public static QueryTokenType GetVariableEnumType(string var)
    {
        try { return variables[var]; }
        catch (Exception) { throw new ArgumentException(string.Format("# Wrong argument: \"{0}\"", var)); }
    }

    public static Dictionary<string, QueryTokenType> GetQueryVariables() => variables;

    public static Dictionary<string, List<string>> GetVariableAttributes()
    {
        Dictionary<string, List<string>> variableAttributes = new Dictionary<string, List<string>>();
        if (queryComponents.ContainsKey("WITH"))
        {
            foreach (string attribute in queryComponents["WITH"])
            {
                string[] attribtueWithValue = attribute.Split('=');
                if (!variableAttributes.ContainsKey(attribtueWithValue[0].Trim()))
                    variableAttributes[attribtueWithValue[0].Trim()] = new List<string>();
                variableAttributes[attribtueWithValue[0].Trim()].Add(attribtueWithValue[1].Trim());
            }
        }
        return variableAttributes;
    }

    public static Dictionary<string, List<string>> GetQueryDetails() => queryComponents;
}