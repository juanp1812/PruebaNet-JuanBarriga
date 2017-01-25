using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaNetConsole
{
    class CompleteRange
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese numeros separador por coma:");
            var cadena = Console.ReadLine();

            List<int> listNumerosRequest = new List<int>();
            List<int> listNumerosResponse = new List<int>();

            var lstCadena = cadena.Split(',');
            var iNumero = 0;
            for (int i = 0; i <= lstCadena.Length - 1; i++) {
                if (int.TryParse(lstCadena[i], out iNumero)) {
                    listNumerosRequest.Add(iNumero);
                }
            }
            listNumerosResponse = Build(listNumerosRequest);

            Console.WriteLine(string.Join(",", listNumerosResponse.ToArray()));
            Console.ReadLine();
        }

        public static List<int> Build(List<int> lstRequest)
        {
            List<int> lstResponse = new List<int>();
            var dictI = 0;
            //var dictNumeros = lstRequest.ToDictionary(x => dictI++, x => string.Format("{0}", x));
            //var iMaximoValor = int.Parse(dictNumeros.Max(x => x.Value));

            var dictNumeros = lstRequest.ToDictionary(x => dictI++, x => x);
            var iMaximoValor = dictNumeros.Max(x => x.Value);

            for (int i = 1; i <= iMaximoValor; i++) {
                lstResponse.Add(i);
            }
            return lstResponse;
        }
    }
}
