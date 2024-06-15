using SPA.ParserAndPKB.Models;

namespace SPA.ParserAndPKB.Uses;

public class Uses : IUses
{
    private static Uses? _Uses;
    public static Uses Instance
    {
        get
        {
            _Uses ??= new Uses();//if is nulll
            return _Uses;
        }
    }

    public bool IsUsed(Variable var, Statement stat)
    {
        if (var != null && stat != null) return stat.UsesList.TryGetValue(var.Id, out bool value) && value;
        return false;
    }

    public bool IsUsed(Variable var, Procedure proc)
    {
        if (var != null && proc != null) return proc.UsesList.TryGetValue(var.Id, out var value) && value;
        return false;
    }

    public void SetUses(Statement stmt, Variable var)
    {
        if (stmt.UsesList.ContainsKey(var.Id)) stmt.UsesList[var.Id] = true;
        else stmt.UsesList.Add(var.Id, true);
    }

    public void SetUses(Procedure proc, Variable var)
    {
        if (proc.UsesList.ContainsKey(var.Id)) proc.UsesList[var.Id] = true;
        else proc.UsesList.Add(var.Id, true);
    }
}