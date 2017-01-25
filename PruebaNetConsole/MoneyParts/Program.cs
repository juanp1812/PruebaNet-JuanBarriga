using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyParts
{
    class Program
    {

        static void Main(string[] args)
        {
            double[] lstValores = {0.05, 0.1, 0.2, 0.5, 1, 2, 5, 10, 20, 50, 100, 200 };
            double[] conteos = new double[lstValores.Length];
            Console.WriteLine("Ingrese un monto:");

            int montoEvaluar = int.Parse(Console.ReadLine());
            var lstCombinaciones = new List<List<double>>();
            lstCombinaciones  = Build(lstValores, conteos, 0, montoEvaluar, lstCombinaciones);

            for (int i = 0; i <= lstCombinaciones.Count - 1; i++) {
                int c = i + 1;
                Console.WriteLine("Combinacion " + c +": " + string.Join(", ", lstCombinaciones[i].ToArray()));
            }
            Console.ReadLine();
        }


        public static List<List<double>> Build(double[] lstValores, double[] conteos, int startIndex, double montoEvaluar, List<List<double>> lstCombinaciones)
        {
            if (startIndex >= lstValores.Length)
            {
                var lstCombinacion = new List<double>();
                for (int i = 0; i < lstValores.Length; i++)
                {
                    if (conteos[i] != 0)
                    {
                        //xResultado += conteos[i] + "*" + lstValores[i] + "+";
                        int conteo = int.Parse(conteos[i].ToString());
                        for (int c = 0; c<=conteo-1; c++) {
                            lstCombinacion.Add(lstValores[i]);
                        }
                        //Console.WriteLine("" + conteos[i] + "*" + lstValores[i] + "+");
                    }
                }
                lstCombinaciones.Add(lstCombinacion);
                //Console.WriteLine(xResultado);5
                return lstCombinaciones;
            }
            if (startIndex == lstValores.Length - 1)
            {
                if (montoEvaluar % lstValores[startIndex] == 0)
                {
                    conteos[startIndex] = montoEvaluar / lstValores[startIndex];
                    lstCombinaciones = Build(lstValores, conteos, startIndex + 1, 0, lstCombinaciones);
                }
            }
            else
            {
                for (int i = 0; i <= montoEvaluar / lstValores[startIndex]; i++)
                {
                    conteos[startIndex] = i;
                    lstCombinaciones = Build(lstValores, conteos, startIndex + 1, montoEvaluar - lstValores[startIndex] * i, lstCombinaciones);
                }
            }
            return lstCombinaciones;
        }

    }
}
