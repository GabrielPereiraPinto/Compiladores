using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
    class Num : Token
    {
        public int value;

        public Num( int value)
        {
            _tag = (int)Tag.NUM;
            this.value = value;
        }
    }
}
