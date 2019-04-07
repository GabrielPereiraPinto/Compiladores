using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
    public class Lexer
    {
        public int line = 1;
        private char peek = ' ';
        StringReader _string;
        private Hashtable words = new Hashtable();

        void Reserve(Word temp)
        {
            words.Add(temp.lexeme, temp);
        }

        public Lexer()
        {
        }

        public Token Scan()
        {
            _string = new StringReader(Console.ReadLine());

            while (_string.Peek() != -1)
            {
                peek = (char)_string.Read();

                if (peek == ' ' || peek == '\t')
                    continue;

                else if (peek == '\n')
                {
                    line++;
                    break;
                }

                else
                    DecideChar();
            }

            Token token = new Token(peek);
            peek = ' ';
            return token;
        }

        public void DecideChar()
        {
            IsComment();
            IsDigit();
            IsOperator();
            IsLetter();
        }

        private void IsDigit()
        {
            if (char.IsDigit(peek))
            {
                int value = 0;

                while (char.IsDigit(peek))
                {
                    value = 10 * value + Convert.ToInt32(peek.ToString(), 10);
                    peek = (char)_string.Read();
                }
                string _decimal = IsDecimal();
                Console.Write(string.Format("< {0}, {1} >", Tag.NUM.ToString(), (value + _decimal)));
            }

            string _zeroDecimal = IsDecimal();
            if (!string.IsNullOrEmpty(_zeroDecimal))
                Console.Write(string.Format("< {0}, {1} >", Tag.NUM.ToString(), (0 + _zeroDecimal)));
        }

        private string IsDecimal()
        {
            string _decimal = "";
            if (peek == '.')
            {
                peek = (char)_string.Read();
                _decimal += ".";
                while (char.IsDigit(peek))
                {
                    _decimal += peek;
                    peek = (char)_string.Read();
                }
            }

            return _decimal;
        }

        private void IsOperator()
        {
            if (peek == '+')
            {
                peek = (char)_string.Read();
                if (peek == '+')
                    Console.Write(string.Format("< {0}, ++ >", Tag.ICR.ToString()));
                else
                    Console.Write(string.Format("< {0}, + >", Tag.ADD.ToString()));
            }

            else if (peek == '-')
            {
                peek = (char)_string.Read();
                if (peek == '-')
                    Console.Write(string.Format("< {0}, -- >", Tag.DCR.ToString()));                
                else
                    Console.Write(string.Format("< {0}, - >", Tag.SUB.ToString()));
            }

            else if (peek == '*')
                Console.Write(string.Format("< {0}, * >", Tag.MLP.ToString()));

            else if (peek == '/')
                Console.Write(string.Format("< {0}, / >", Tag.DIV.ToString()));

            else if (peek == '=')
            {
                peek = (char)_string.Read();
                if (peek == '=')
                    Console.Write(string.Format("< {0}, == >", Tag.EQ.ToString()));
                else
                    Console.Write("< = >");
            }

            else if (peek == '<')
            {
                peek = (char)_string.Read();
                if (peek == '=')
                    Console.Write(string.Format("< {0}, <= >", Tag.LE.ToString()));
                else if (peek == '>')
                    Console.Write(string.Format("< {0}, <> >", Tag.NE.ToString()));
                else
                    Console.Write(string.Format("< {0}, < >", Tag.LT.ToString()));
            }

            else if (peek == '>')
            {
                peek = (char)_string.Read();
                if (peek == '=')
                    Console.Write(string.Format("< {0}, >= >", Tag.GE.ToString()));
                else
                    Console.Write(string.Format("< {0}, > >", Tag.GT.ToString()));
            }
            else if (peek == '!')
            {
                peek = (char)_string.Read();
                if (peek == '=')
                    Console.Write(string.Format("< {0}, != >", Tag.NE.ToString()));
            }

        }

        private void IsLetter()
        {
            if (char.IsLetter(peek))
            {
                string builder = "";

                while (char.IsLetterOrDigit(peek))
                {
                    builder += peek;
                    peek = (char)_string.Read();
                }

                if (!words.Contains(builder))
                {
                    var word = new Word((int)Tag.ID, builder);
                    words.Add(builder, word);
                }
                Console.Write(string.Format("< {0}, {1} >", Tag.ID.ToString(), builder));
            }
            return;
        }

        private void IsComment()
        {
            if (peek == '/')
            {
                char lookAhead = (char)_string.Peek();
                if (lookAhead == '/')
                {
                    while (_string.Peek() != -1)
                        peek = (char)_string.Read();
                    peek = (char)_string.Read();
                    return;
                }
                else if (lookAhead == '*')
                {
                    while (true)
                    {
                        peek = (char)_string.Read();
                        lookAhead = (char)_string.Peek();
                        if (peek == '*' && lookAhead == '/') {
                            peek = (char)_string.Read(); // reads the '/'
                            peek = (char)_string.Read();// next char
                            return;
                        }
                    }
                }
                else return;
            }
        }
    }
}
