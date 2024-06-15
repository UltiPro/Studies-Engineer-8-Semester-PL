using SPA.ParserAndPKB.Models;

namespace SPA.ParserAndPKB.Modifies;

public interface IModifies
{
    void SetModifies(Statement stmt, Variable var);
    void SetModifies(Procedure proc, Variable var);
    bool IsModified(Variable var, Statement stat);
    bool IsModified(Variable var, Procedure proc);
}