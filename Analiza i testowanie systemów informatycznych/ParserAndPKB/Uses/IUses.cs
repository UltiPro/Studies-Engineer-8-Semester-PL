using SPA.ParserAndPKB.Models;

namespace SPA.ParserAndPKB.Uses;

public interface IUses
{
    void SetUses(Statement stmt, Variable var);
    void SetUses(Procedure proc, Variable var);
    bool IsUsed(Variable var, Statement stat);
    bool IsUsed(Variable var, Procedure proc);
}