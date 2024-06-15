using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA.ParserAndPKB
{
    public static class ParserUtils
    {    
        /// <summary>
        /// Lista słów kluczowych
        /// </summary>
        private static readonly List<string> _reservedWords = new List<string>()
        {
            "procedure", "while", "if", "then", "else", "call"
        };
        public static bool IsVarName(string name) => name.Length != 0 && Char.IsLetter(name[0]) && _reservedWords.IndexOf(name) < 0;
        public static bool IsConstValue(string name) => name.Length != 0 && Int64.TryParse(name, out _);
    }
}
