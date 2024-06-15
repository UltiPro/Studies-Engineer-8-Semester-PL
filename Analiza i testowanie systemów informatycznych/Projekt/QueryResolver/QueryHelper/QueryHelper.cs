using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB.Models;

namespace SPA.QueryResolver
{
    public class QueryHelper
    {
        public static void IsModifiesOrUses(string _1Arg, string _2Arg,
                           Func<Variable, Procedure, bool> methodForProc,
                           Func<Variable, Statement, bool> methodForStmt, bool and)
        {
            var _1ArgType = DetermineEntityType(_1Arg);

            if (_1ArgType != QueryTokenType.Procedure) CheckHelpers.IsStatementModifiesOrUses(_1Arg, _2Arg, methodForStmt, and);

            else CheckHelpers.IsProcedureModifiesOrUses(_1Arg, _2Arg, methodForProc, and);
        }

        private static QueryTokenType DetermineEntityType(string arg) =>
            arg[0] == '\"' && arg[^1] == '\"' ? QueryTokenType.Procedure :
            int.TryParse(arg, out _) || arg == "_" ? QueryTokenType.Statement :
            QueryProcessor.GetVariableEnumType(arg);
    }
}