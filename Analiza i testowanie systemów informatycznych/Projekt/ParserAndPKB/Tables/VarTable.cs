using SPA.ParserAndPKB.Models;

namespace SPA.ParserAndPKB.Tables;

public class VarTable : IVarTable
{
    private static VarTable? _VarTable;
    public static VarTable Instance
    {
        get
        {
            _VarTable ??= new VarTable();//if is null
            return _VarTable;
        }
    }

    public List<Variable> variables { get; set; }

    public VarTable() => variables = new List<Variable>();

    public int AddVariable(string varName)
    {
        if (variables.Any(v => v.Identifier == varName)) return -1;
        else
        {
            variables.Add(new Variable(varName) { Id = variables.Count });
            return GetVarIndex(varName);
        }
    }

    public Variable? GetVar(int index) => variables.FirstOrDefault(v => v.Id == index);

    public Variable? GetVar(string varName) => variables.FirstOrDefault(v => v.Identifier.ToLower() == varName.ToLower());

    public int GetVarIndex(string varName)
    {
        var variable = GetVar(varName);
        return variable == null ? -1 : variable.Id;
    }

    public List<Variable> GetTable() => variables;
}