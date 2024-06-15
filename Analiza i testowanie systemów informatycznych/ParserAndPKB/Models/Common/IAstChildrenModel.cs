using SPA.ParserAndPKB.AST.Nodes;

namespace SPA.ParserAndPKB.Models.Common;

public interface IAstChildrenModel
{
    public Node? AstRoot { get; set; }
}