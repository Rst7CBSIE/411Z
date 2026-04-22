using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR_1_2_S2
{
    internal class Program
    {
        static void MySort(int[] a, int left, int right)
        {
            //Реалізація quicksort
            int l, r;
            l = left;
            r = right;
            //Классичний вибір опорного елементу
            //int pivot = a[left + ((right - left + 1) >> 1)];
            //Вибір опорного елементу по з обчисленням медіани трьох елементів
            int p1= a[left + ((right - left + 1) >> 1)];
            int p2 = a[left];
            int p3 = a[right];
            int temp;
            //Сортуємо елементи
            if (p1 > p2) { temp = p1; p1 = p2; p2 = temp; }
            if (p2 > p3) { temp = p2; p2 = p3; p3 = temp; }
            if (p1 > p2) { temp = p1; p1 = p2; p2 = temp; }
            //Опорний елемент дорвнює медіані
            int pivot = p2;
            while (l <= r)
            {
                while (a[l] < pivot) l++;
                while (a[r] > pivot) r--;

                if (l <= r)
                {
                    temp = a[l];
                    a[l] = a[r];
                    a[r] = temp;
                    l++;
                    r--;
                }
            }
            if (r > left)
                MySort(a, left, r);
            if (l < right)
                MySort(a, l, right);
        }
        static void Main(string[] args)
        {
            int input_sz = 100;
            int[] input = new int[input_sz];
            int[] test_array = new int[input_sz];
            int[] output = new int[input_sz];
            //Заповнюємо вхідний массив
            Console.WriteLine("Unsorted:");
            var rand = new Random();
            for (int i = 0; i < input_sz; i++)
            {
                input[i] = rand.Next(10) + 123;
                Console.Write(input[i] + " ");
                test_array[i] = input[i];
            }
            Console.WriteLine();
            //Сортуємо масив
            MySort(input, 0, input_sz-1);
            //Сортуємо тестовий масив
            Array.Sort(test_array);
            Console.WriteLine("Sorted:");
            for (int i = 0; i < input_sz; i++)
            {
                Console.Write(input[i] + " ");
                if (test_array[i] != input[i])
                    Console.WriteLine("*** ERROR ***");
            }
            Console.WriteLine();
        }
    }
}
