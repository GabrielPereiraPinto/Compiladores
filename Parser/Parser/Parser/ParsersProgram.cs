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
            Console.WriteLine("Digite o valor para o Parser desejado\n1- Para o Parser Posfixado\n2- Para o Parser Prefixado\n3- Para o Parser Parenthesis\n4- Para o Parser 01\n5- Para fechar o programa\n\n");
            string menu = Console.ReadLine();
            switch (menu)
            {
                case "1":
                    Console.WriteLine("Imprimindo S -> SS+ | SS- | a \nDigitar valores desejados \nPara Teste utilizar 5-4 com saida 54- ou 7+8 com saida 78+");
                    var parsepos = new ParserPos();
                    parsepos.expr();
                    Console.Write("\n\n");
                    break;

                case "2":
                    Console.WriteLine("Imprimindo S -> +SS | -SS | a \nDigitar valores desejados \nPara Teste utilizar 1-3 com saida -13 ou 4+5 com saida +45");
                    var parserPre = new ParserPre();
                    parserPre.expr();
                    Console.Write("\n\n");
                    break;

                case "3":
                    Console.WriteLine("Imprimindo S ->S(S)S | E\nDigitar valores desejados");
                    var parseParenthesis = new ParserParenthesis();
                    parseParenthesis.expr();
                    break;

                case "4":
                    Console.WriteLine("Imprimindo S ->0S1 | E\nDigitar valores desejados");
                    var parse01 = new Parser01();
                    parse01.expr();
                    break;
                
                case "5":
                    return;
            }
            Console.WriteLine("Aperte Enter para sair");
            Console.ReadLine();
            Console.ReadLine();
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
                    Console.Write('+');
                }
                else if (lookAhead == '-')
                {
                    match('-');
                    term();
                    Console.Write('-');
                }
                else
                    return;
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
                            Console.Write("\nLinguagem valida\n");
                        }
                        else
                            throw new Exception("Syntax Error");

                        return;
                }
            }
        }
    }

    public class Parser01 : Parser
    {
        public int counter;
        public bool flag = false;
        public Parser01()
        {
            lookAhead = Console.In.Read();
        }

        public void term()
        {
            if ('0' == (char)lookAhead && flag == false)
            {
                counter++;
                Console.Write((char)lookAhead);
                match(lookAhead);
            }
            else if ((char)lookAhead == '1' && counter > 0)
            {
                flag = true;
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
                    case '0':
                    case '1':
                        term();
                        break;
                    default:
                        if ((lookAhead == 13 && counter == 0 && flag == true) || lookAhead == ' ' && counter == 0 && flag == true) // 13 é o valor de enter em ASCII
                        {
                            Console.Write("\nLinguagem valida\n");
                        }
                        else
                            throw new Exception("Syntax Error");

                        return;
                }
            }
        }
    }
}
