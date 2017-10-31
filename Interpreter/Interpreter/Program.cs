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
            string filename = "C:/Work/LFTC/C-Like-Language-Interpreter/Interpreter/Interpreter/program.txt";
            StringBuilder pif = new StringBuilder();
            SortedDictionary<string, string> symbolTable = new SortedDictionary<string, string>();
            Scanner.Scanner s = new Scanner.Scanner();

            //s.InternalSymbolsForm.Keys.ToList().ForEach(k => Console.WriteLine(s.InternalSymbolsForm[k]));
            try
            {
                s.ReadProgram(filename);
                s.PIF.ForEach(el => Console.WriteLine(el.Item1 + " : " + el.Item2));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            
            //Console.WriteLine(s.PIF);
            Console.Read();
        }
    }
}
