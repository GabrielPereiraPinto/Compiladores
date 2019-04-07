using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
    class Word : Token
    {
        public string lexeme;
        public Word (int tag, string name)
        {
            _tag = tag;
            lexeme = name;
        }
    }
}
