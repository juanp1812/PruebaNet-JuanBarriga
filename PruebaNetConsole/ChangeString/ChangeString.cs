using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaNetConsole
{
    class ChangeString
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese texto:");
            var cadena= Console.ReadLine();
            Console.WriteLine(Build(cadena));
            Console.ReadLine();
        }

        public static string Build(string cadena)
        {
            string xResultado = "";
            bool esTexto;
            char letra;
            foreach (char c in cadena)
            {
                letra = c;
                if (letra == 'z')
                {
                    letra = 'a';
                    xResultado = xResultado + letra.ToString();
                    continue;
                }
                else if (letra == 'Z')
                {
                    letra = 'A';
                    xResultado = xResultado + letra.ToString();
                    continue;
                }

                esTexto = letra.ToString().Any(x => char.IsLetter(x));
                if (esTexto)
                {
                    letra++;
                    if (letra.ToString().Any(char.IsUpper))
                    {
                        xResultado = xResultado + (letra++).ToString().ToUpper();
                    }
                    else
                    {
                        xResultado = xResultado + (letra++).ToString();
                    }
                }
                else {
                    xResultado = xResultado + c.ToString();
                }
            }
            return xResultado;
        }

    }
}
