using Interpreter.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Scanner
{
    class Scanner
    {
        SortedDictionary<string, BaseType> SymbolTable;
        public Dictionary<string, int> InternalSymbolsForm { get; set; }

        public Scanner()
        {
            SymbolTable = new SortedDictionary<string, BaseType>();
            //internalSymbolsForm = new Dictionary<string, int>();
            PopulateInternalSymbolForm();
        }

        private void PopulateInternalSymbolForm()
        {
            InternalSymbolsForm = new Dictionary<string, int>
            {                
                {"{", 0 },
                {"}", 1 },
                {"id", 2 },
                {";", 3 },
                {"int", 4 },
                {",", 5 },
                {"=", 6 },
                {"const", 7 }

            };
        }

        private void AnalyzeStatement(string s, out StringBuilder pif)
        {
            pif = new StringBuilder();
            pif.Clear();
            if (s.Split(' ')[0] == "int")//this is a declaration for integers
            {
                pif.Append("4");
                var varList = s.Substring(s.IndexOf(' ') + 1)
                    .Replace(" ","")
                    .Split(',').ToList(); // remove type decl and spaces between variables
                foreach (var var in varList) // parse variables from this specific declaration
                {
                    //Console.WriteLine(var);
                    pif.Append(" 2 5");
                }
                pif = pif.Remove(pif.Length - 1, 1);
                pif.Append("3");
            } 
        }

        public void ReadProgram(string filename, out StringBuilder pif, out SortedDictionary<string, BaseType> symTable)
        {
            StringBuilder prog, partialPif;
            pif = new StringBuilder();            
            try
            {
                prog = new StringBuilder(System.IO.File.ReadAllText(filename)).Replace(System.Environment.NewLine,"");               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                pif = null;
                symTable = null;
                return;
            }
            var stmtList = prog.ToString().Split(';').ToList();
            stmtList.RemoveAt(stmtList.Count - 1);
            foreach (var s in stmtList)
            {                
                AnalyzeStatement(s, out partialPif);
                pif.Append(" ").Append(partialPif);
            }                     

            symTable = null;
        }

    }
}
