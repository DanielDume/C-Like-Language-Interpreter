using Interpreter.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "D:/Projects/Interpreter/C-Like-Language-Interpreter/Interpreter/Interpreter/program.txt";
            StringBuilder pif = new StringBuilder();
            SortedDictionary<string, BaseType> symbolTable = new SortedDictionary<string, BaseType>();
            Scanner.Scanner s = new Scanner.Scanner();

            //s.InternalSymbolsForm.Keys.ToList().ForEach(k => Console.WriteLine(s.InternalSymbolsForm[k]));

            s.ReadProgram(filename, out pif, out symbolTable);
            Console.WriteLine(pif);
            Console.Read();
        }
    }
}
