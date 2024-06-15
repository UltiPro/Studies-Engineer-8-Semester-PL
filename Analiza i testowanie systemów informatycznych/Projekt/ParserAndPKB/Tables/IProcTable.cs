using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Models;

namespace SPA.ParserAndPKB.Tables;

public interface IProcTable : ITable<List<Procedure>>
{
    int AddProcedure(string procName);
    Procedure? GetProcedure(int index);
    Procedure? GetProcedure(string procName);
    int GetProcIndex(string procName);
    int SetAstRootNode(string procName, Node node);
    Node? GetAstRoot(string procName);
    Node? GetAstRoot(int index);
}