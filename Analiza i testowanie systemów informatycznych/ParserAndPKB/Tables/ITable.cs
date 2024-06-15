namespace SPA.ParserAndPKB.Tables;

public interface ITable<T> where T : class
{
    T GetTable();
}