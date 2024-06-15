using SPA.ParserAndPKB.Models;

namespace SPA.ParserAndPKB.Modifies;

public class Modifies : IModifies
{
    private static Modifies? _Modifies;
    public static Modifies Instance
    {
        get
        {
            _Modifies ??= new Modifies();//if is null
            return _Modifies;
        }
    }

    public bool IsModified(Variable var, Statement stat) => var == null ? false
        : stat.ModifiesList.TryGetValue(var.Id, out var value) && value;

    public bool IsModified(Variable var, Procedure proc) => var == null || proc == null ? false
        : proc.ModifiesList.TryGetValue(var.Id, out var value) && value;

    public void SetModifies(Statement stmt, Variable var)
    {
        if (stmt.ModifiesList.TryGetValue(var.Id, out var value)) stmt.ModifiesList[var.Id] = true;
        else stmt.ModifiesList.Add(var.Id, true);
    }

    public void SetModifies(Procedure proc, Variable var)
    {
        if (proc.ModifiesList.TryGetValue(var.Id, out var value)) proc.ModifiesList[var.Id] = true;
        else proc.ModifiesList.Add(var.Id, true);
    }
}