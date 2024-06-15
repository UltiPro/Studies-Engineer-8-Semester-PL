using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB.Models;

namespace SPA.ParserAndPKB.Tables;

public interface IStmtTable : ITable<List<Statement>>
{
    int AddStatement(QueryTokenType entityType, int codeLine);
    Statement? GetStatement(int codeLine);
    int SetAstRoot(int codeLine, Node node);
    Node? GetAstRoot(int codeLine);
}