using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
    public class Token
    {
        public int _tag { get; set; }
        public Token ()
        {
        }
        public Token(int tag)
        {
            this._tag = tag;
        }
    }
}
