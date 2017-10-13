using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter.Model.Expressions
{
    public abstract class Expression
    {
        public abstract int Eval();
    }
}
