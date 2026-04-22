using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace LR1_S2
{
    internal class Program
    {
        static uint CountDivs(uint[]primes, ulong v)
        {
            uint count = 1;
            //Спробуємо ділити на кожне просте
            for(uint i=0; primes[i]!=0; i++)
            {
                uint n = 0;
                //Рахуємо, скільки разів v діліться на просте
                while ((v % primes[i])==0)
                {
                    n++;
                    v = v / primes[i];
                }
                if (n != 0)
                {
                    //v ділиться на просте
                    //Оновлюємо кількість дільників
                    count = count * (n + 1);
                }
            }
            return count;
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; //Дозволити вивод в UTF-8
            //Створюємо таблицю простих чисел
            uint max_prime = 1000;
            uint[] primes = new uint[max_prime];
            uint max_prime_sqr = (uint)Math.Round(Math.Sqrt(max_prime));
            //Решето Ератосфена
            for (uint i =2; i< max_prime_sqr; i++)
            {
                for (uint j = i*2; j < max_prime; j += i) primes[j] = 1;
            }
            //Пакування массиву
            uint top_prime = 0;
            for (uint i = 2; i < max_prime; i++)
            {
                if (primes[i]==0)
                {
                    primes[top_prime++] = i;
                }
            }
            primes[top_prime] = 0; //Маркер кінця
            Console.WriteLine("Усього " + top_prime + " простих до " + max_prime);
            ulong value;
            uint n;
            //Рахуємо кількість дільників
            value = 12;
            n=CountDivs(primes,value);
            Console.WriteLine(value + " -> " + n);
            value = 239;
            n = CountDivs(primes, value);
            Console.WriteLine(value + " -> " + n);
        }
    }
}
