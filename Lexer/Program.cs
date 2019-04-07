using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Exemplo 1:\nEntrada: 5. + 4.2 < 9 - 4 /* verdadeiro */ 0.5 2 <= 10 /*verdadeiro*/ 4 == 10 /* falso */ i ++ //incremento ");
            Console.WriteLine("Saida:< ID, Entrada >< NUM, 5. >< ADD, + >< NUM, 4.2 >< LT, < >< NUM, 9 >< SUB, - >< NUM, 4 >< NUM, 0.5 >< NUM, 2 >< LE, <= >< NUM, 10 >< NUM, 4 >< EQ, == >< NUM, 10 >< ID, i >< ICR, ++ >\n");
            Console.WriteLine("Exemplo 2:\nEntrada: 4. != 5 6 <> 5 .9 >= 10   0.5 * 2  <= 4. / 2 i --//4 != 6 5 <> 4");
            Console.WriteLine("Saida:< NUM, 4. >< NE, != >< NUM, 5 >< NUM, 6 >< NE, <> >< NUM, 5 >< NUM, 0.9 >< GE, >= >< NUM, 10 >< NUM, 0.5 >< MLP, * >< NUM, 2 >< LE, <= >< NUM, 4. >< DIV, / >< NUM, 2 >< ID, i >< DCR, ++ >\n");
            Console.WriteLine("Digite entrada desejado");
            Lexer lexer = new Lexer();
            lexer.Scan(); 
        }
    }
}
