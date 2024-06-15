namespace SPA.ParserAndPKB.Models.Common;

public abstract class BaseModel<TKey, TKeyValue>
{
    public TKey Id { get; set; }
    public TKeyValue Identifier { get; set; }
}