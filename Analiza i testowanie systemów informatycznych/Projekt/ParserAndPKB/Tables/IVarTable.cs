using SPA.ParserAndPKB.Models;

namespace SPA.ParserAndPKB.Tables;

public interface IVarTable : ITable<List<Variable>>
{
    static VarTable? _VarTable;
    int AddVariable(string varName);
    Variable? GetVar(int index);
    Variable? GetVar(string varName);
    int GetVarIndex(string varName);
}