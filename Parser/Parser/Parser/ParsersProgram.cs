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
            //ParserPos parsepos = new ParserPos();
            //parsepos.expr();
            //Console.Write("\n\n");
            //Console.In.Read();
            Console.WriteLine("Imprimindo S -> +SS | -SS | a \nDigitar valores desejados \nPara Teste utilizar 1-3 com saida -13 ou 4+5 com saida +45");
            ParserPre parserPre = new ParserPre();
            parserPre.expr();
            Console.Write("\n");
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
                else return;
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
        public void term() { }

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
                else return;
            }
        }
}
