using SPA.ParserAndPKB.Models.Common;

namespace SPA.ParserAndPKB.Models;

public class Variable : BaseModel<int, string>
{
    public Variable(string identifier) => Identifier = identifier;
}