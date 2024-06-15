using SPA.ParserAndPKB.AST.Nodes;
using SPA.ParserAndPKB.Enums;
using SPA.ParserAndPKB.Models;
using SPA.ParserAndPKB.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.ParserAndPKB
{
    public static class ParserUpdater
    {

        public static void UpdateModifiesAndUsesTablesInProcedures()
        {
            bool wasChange = true;
            while (wasChange)
            {
                wasChange = false;
                foreach (Procedure p1 in ProcTable.Instance.procedures)
                    foreach (Procedure p2 in ProcTable.Instance.procedures)
                        if (p1 != p2 && Calls.Calls.Instance.IsCalls(p1.Identifier, p2.Identifier))
                        {
                            UpdateLists(p1.ModifiesList, p2.ModifiesList, ref wasChange);
                            UpdateLists(p1.UsesList, p2.UsesList, ref wasChange);
                        }        
            }

            foreach (Statement s in StmtTable.Instance.statements)
                if (s.StmtType == QueryTokenType.Call)
                {
                    string pname = s.AstRoot.Name;
                    Procedure p = ProcTable.Instance.GetProcedure(pname);
                    if (p != null)
                    {
                        s.ModifiesList = p.ModifiesList;
                        s.UsesList = p.UsesList;
                    }
                }
        }

        private static void UpdateLists(Dictionary<int, bool> target, Dictionary<int, bool> source, ref bool wasChange)
        {
            foreach (var variable in source)
                if (!target.ContainsKey(variable.Key))
                {
                    target[variable.Key] = true;
                    wasChange = true;
                }
        }

        public static void UpdateModifiesAndUsesTablesInWhilesAndIfs()
        {
            List<Statement> ifOrWhileStmts = StmtTable.Instance.statements
                .Where(i => i.AstRoot.EntityType == QueryTokenType.While || i.AstRoot.EntityType == QueryTokenType.If).ToList();

            foreach (var stmt in ifOrWhileStmts)
            {
                var node = stmt.AstRoot;
                List<Node> stmtLstNodes = AST.AST.Instance
                .GetLinkedNodes(node, NodeRelationType.Child)
                .Where(i => i.EntityType == QueryTokenType.Stmtlist).ToList();

                List<Procedure> procedures = new List<Procedure>();
                foreach (var stmtL in stmtLstNodes)
                    Calls.Calls.Instance.GetCalls(procedures, stmtL);

                foreach (Procedure proc in procedures)
                {
                    foreach (KeyValuePair<int, bool> variable in proc.ModifiesList)
                        if (!stmt.ModifiesList.ContainsKey(variable.Key))
                            stmt.ModifiesList[variable.Key] = true;
                    foreach (KeyValuePair<int, bool> variable in proc.UsesList)
                        if (!stmt.UsesList.ContainsKey(variable.Key))
                            stmt.UsesList[variable.Key] = true;
                }
            }
        }
    }
}


