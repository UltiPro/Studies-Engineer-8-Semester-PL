using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB.Models.Common;

namespace SPA.ParserAndPKB.Models;

public class Statement : IAstChildrenModel
{
    public QueryTokenType StmtType { get; set; }
    public int LineNumber { get; set; }
    public Node? AstRoot { get; set; }
    public Dictionary<int, bool> ModifiesList { get; set; }
    public Dictionary<int, bool> UsesList { get; set; }
    public Statement(QueryTokenType stmtType, int lineNumber)
    {
        if (stmtType is not (QueryTokenType.Assign or QueryTokenType.If or QueryTokenType.While or QueryTokenType.Call))
            throw new InvalidOperationException();
        this.StmtType = stmtType;
        this.LineNumber = lineNumber;
        this.ModifiesList = new Dictionary<int, bool>();
        this.UsesList = new Dictionary<int, bool>();
    }
}