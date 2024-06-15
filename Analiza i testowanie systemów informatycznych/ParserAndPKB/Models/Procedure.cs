using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Models.Common;

namespace SPA.ParserAndPKB.Models;

public class Procedure : BaseModel<int, string>, IAstChildrenModel
{
    public Node? AstRoot { get; set; }
    public Dictionary<int, bool> ModifiesList { get; set; }
    public Dictionary<int, bool> UsesList { get; set; }
    public Procedure(string identifier)
    {
        this.Identifier = identifier;
        this.ModifiesList = new Dictionary<int, bool>();
        this.UsesList = new Dictionary<int, bool>();
    }
}