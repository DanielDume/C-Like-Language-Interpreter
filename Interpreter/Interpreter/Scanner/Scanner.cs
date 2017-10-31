using Interpreter.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Interpreter.Scanner
{
    class Scanner
    {
        SortedDictionary<string, string> SymbolTable;
        public Dictionary<string, string> InternalSymbolsForm { get; set; }
        //public SortedDictionary<string, string> symTable;
        public List<Tuple<string, string>> PIF = new List<Tuple<string, string>>();
        private int symTableIndex = 1000, currentLine = 0, currentColumn = 0;

        public Scanner()
        {
            SymbolTable = new SortedDictionary<string, string>();
            //internalSymbolsForm = new Dictionary<string, int>();
            PopulateInternalSymbolForm();
        }

        private void PopulateInternalSymbolForm()
        {
            InternalSymbolsForm = new Dictionary<string, string>
            {
                {"id", "0 "},
                {"const", "1 "},
                {"{","2 "},
                {"}","3 "},
                {"int","4 "},
                {"float","5 "},
                {"struct","6 "},
                {"+","7 "},
                {"-","8 "},
                {"*","9 "},
                {"/","10 "},
                {"%","11 "},
                {"<","12 "},
                {"<=","13 "},
                {">","14 " },
                {">=","15 " },
                {"==","16 " },
                {"!=","17 " },
                {",","18 " },
                {"while","19 " },
                {"(","20 " },
                {")","21 " },
                {"if","22 " },
                {"else","23 " },
                {"cin>>","24 " },
                {"cout<<","25 " },
                {";", "26 " },
                {"=", "27 " }
            };
        }
        private void CheckWhatItIs(string el)
        {
            //identifier/constant
            //const
            if (int.TryParse(el, out var n) || float.TryParse(el, out var f)) // is a number
            {
                if (!SymbolTable.ContainsKey(el))
                {
                    SymbolTable.Add(el, symTableIndex.ToString());
                    symTableIndex++;
                }
                PIF.Add(new Tuple<string, string>("(" + el + ")" + InternalSymbolsForm["const"], SymbolTable[el]));
                return;
            }
            //id
            if (Char.IsLetter(el.FirstOrDefault()) && el.All(c => Char.IsLetterOrDigit(c)))
            {
                if (!SymbolTable.ContainsKey(el))
                {
                    SymbolTable.Add(el, symTableIndex.ToString());
                    symTableIndex++;
                }
                PIF.Add(new Tuple<string, string>("(" + el + ")" + InternalSymbolsForm["id"], SymbolTable[el]));
                return;
            }
            throw new Exception("There is an error at line " + currentLine + ", element " + currentColumn);
        }
        private string GetPif(String prog)
        {
            Console.WriteLine(prog);
            currentColumn = 0;
            var elements = prog.Split(' ').ToList();
            elements.ForEach(el => {
                if (InternalSymbolsForm.Keys.Any(k => k == el) && el != "const" && el != "id")
                {
                    PIF.Add(new Tuple<string, string>("(" + el + ")" + "  " + InternalSymbolsForm[el],null));
                }
                else
                {
                    CheckWhatItIs(el);
                }
                currentColumn++;
            });
            return null;
        }

        public void ReadProgram(string filename)
        {
            StringBuilder prog, partialPif = new StringBuilder();

            try
            {
                prog = new StringBuilder(System.IO.File.ReadAllText(filename)).Replace(";", " ; ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                PIF = null;
                SymbolTable = null;
                return;
            }
            //var text = Regex.Replace(prog.ToString(), @"\s+", " ");
            var lines = prog.ToString().Split(new[] { Environment.NewLine },
                StringSplitOptions.None).ToList();
            //lines.ForEach(l => Console.WriteLine(l));
            //var lines = Regex.Replace(prog.ToString(), @"\s+", " ").Split(';').ToList();
            lines.ForEach(l => {GetPif(Regex.Replace(l, @"\s+", " ").Trim()); currentLine++; });
            //lines.ForEach(l => Console.WriteLine(l.Trim()));
            //GetPif(Regex.Replace(prog.ToString(), @"\s+", " "));
        }

    }
}
