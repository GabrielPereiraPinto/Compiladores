using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class ParsersProgram
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Imprimindo S -> SS+ | SS- | a \nDigitar valores desejados \nPara Teste utilizar 5-4 com saida 54- ou 7+8 com saida 78+");
            //var parsepos = new ParserPos();
            //parsepos.expr();
            //Console.Write("\n\n");
            //Console.In.Read();
            //Console.WriteLine("Imprimindo S -> +SS | -SS | a \nDigitar valores desejados \nPara Teste utilizar 1-3 com saida -13 ou 4+5 com saida +45");
            //var parserPre = new ParserPre();
            //Console.Write("\n\n");
            //Console.In.Read();
            Console.WriteLine("Imprimindo S ->S(S)S | E\nDigitar valores desejados");
            var parseParenthesis = new ParserParenthesis();
            parseParenthesis.expr();
        }
    }

    public abstract class Parser
    {
        public int lookAhead;
        public abstract void expr();
        public void match(int value)
        {
            if (lookAhead == value)
                lookAhead = Console.In.Read();
            else
                throw new Exception("Syntax Error - match");
        }

    }

    public class ParserPos : Parser
    {
        public ParserPos()
        {
            lookAhead = Console.In.Read();
        }
        public void term()
        {
            if (char.IsDigit((char)lookAhead))
            {
                Console.Write((char)lookAhead);
                match(lookAhead);
            }
            else
                throw new Exception("Syntax Error - term");
        }
        public override void expr()
        {
            term();
            while (true)
            {
                if (lookAhead == '+')
                {
                    match('+');
                    term();
                    Console.Write('-');
                }
                else if (lookAhead == '-')
                {
                    match('-');
                    term();
                    Console.Write('-');
                }
            }
        }
    }

    public class ParserPre : Parser
    {
        public ParserPre()
        {
            lookAhead = Console.In.Read();
        }

        public int term()
        {
            int digit;
            if (char.IsDigit((char)lookAhead))
            {
                digit = lookAhead;
                match(lookAhead);
                Console.Write((char)lookAhead);
                
            }
            else
                throw new Exception("Syntax Error - term");
            return digit;
        }

        public override void expr()
        {   
            int digit = term();
            while (true)
            {
                if (lookAhead == '+')
                {
                    match('+');
                    Console.Write((char)digit);
                    Console.Write((char)lookAhead);
                }
                else if (lookAhead == '-')
                {
                    match('-');
                    Console.Write((char)digit);
                    Console.Write((char)lookAhead);
                }
                else return;
            }
        }   
    }

    public class ParserParenthesis : Parser
    {
        public int counter;
        public ParserParenthesis()
        {
            lookAhead = Console.In.Read();
        }

        public void term()
        {
            if ('(' == (char)lookAhead)
            {
                counter++;
                Console.Write((char)lookAhead);
                match(lookAhead);
            }
            else if ((char)lookAhead == ')' && counter > 0)
            {
                counter--;
                Console.Write((char)lookAhead);
                match(lookAhead);
            }
            else
                throw new Exception("Syntax Error");
        }

        public override void expr()
        {
            while (true)
            {
                switch (lookAhead)
                {
                    case '(':
                    case ')':
                        term();
                        break;
                    default:
                        if ((lookAhead == 13 && counter == 0) || lookAhead == ' ' && counter == 0) // 13 é o valor de enter em ASCII
                        {
                            Console.Write('\n');
                        }
                        else
                            throw new Exception("Syntax Error");

                        return;
                }
            }
        }
    }
}
