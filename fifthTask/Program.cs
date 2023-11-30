using System;
using System.IO;
using System.Numerics;

namespace ConsoleApp
{
    class Program
    {
        // путь к файлам
        private const string Path = @".\Contents\";

        /* Методы */
        private static string s_encryptionMethod()
        {
            int P = 2039;
            int Q = 4001;
            BigInteger N = P * Q;  
            BigInteger phi = (P - 1) * (Q - 1);
            BigInteger E = s_chooseE(phi);
            BigInteger D = s_modInverse(E, phi);

            var inputText = File.ReadAllText($"{Path}Input.txt");

             

            return "string";
        }

        private static BigInteger s_chooseE(BigInteger phi) => 65537;
        

        private static BigInteger s_modInverse(BigInteger a, BigInteger m)
        {
            // Расширенный алгоритм Евклида для нахождения обратного по модулю
            BigInteger m0 = m, t, q;
            BigInteger x0 = 0, x1 = 1;

            if (m == 1) return 0;

            while (a > 1)
            {
                q = a / m;
                t = m;
                m = a % m;
                a = t;
                t = x0;
                x0 = x1 - q * x0;
                x1 = t;
            }

            if (x1 < 0) x1 += m0;
            return x1;
        }


        /* Главный метод */
        private static void Main()
        {
            Console.WriteLine();
        }
    }
}
