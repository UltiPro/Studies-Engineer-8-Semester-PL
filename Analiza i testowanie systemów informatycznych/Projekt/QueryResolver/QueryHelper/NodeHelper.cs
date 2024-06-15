using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB;

namespace SPA.QueryResolver;

public static class NodeHelper
{
    public static Node? GetNodeByType(QueryTokenType entityType, int idx) =>
    entityType == QueryTokenType.Procedure ? PKB.Instance._ProcTable!.GetAstRoot(idx) : PKB.Instance._StmtTable!.GetAstRoot(idx);
}