using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR1_1_S2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int input_sz = 100;
            int[] input = new int[input_sz];
            int[] output = new int[input_sz];
            //Заповнюємо вхідний массив
            var rand = new Random();
            for (int i = 0; i < input_sz; i++)
            {
                input[i] = rand.Next(10)+123;
                Console.Write(input[i] + " ");
            }
            Console.WriteLine();
            //Шукаємо мінімальний та максимальний елемент
            int val_min = int.MaxValue;
            int val_max = int.MinValue;
            for (int i = 0; i < input_sz; i++)
            {
                if (input[i] < val_min) val_min = input[i];
                if (input[i] > val_max) val_max = input[i];
            }
            Console.WriteLine("Max=" + val_max + " Min" + val_min);
            //Підрахунок
            for (int i = 0; i < input_sz; i++)
            {
                output[input[i] - val_min]++;
            }
            //Результат
            val_max -= val_min;
            for (int i = 0; i <= val_max; i++)
            {
                for (int j = 0; j < output[i]; j++)
                {
                    Console.Write((i + val_min) + " ");
                }
            }
            Console.WriteLine();
        }
    }
}
