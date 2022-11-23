using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    internal class Program
    {
        const int n = 7, m = 5;
        static int[,] arr = new int[n, m];
        //Метод с садовник из верхнего левого угла
        static void Gardner1()
        {
            int i = 0;
            for (int j = 0; j < m; j++)
            {
                if (arr[i, j] < 0)
                {
                    return;
                }
                else if (i==0)
                {
                    for ( i = 0; i < n && arr[i, j] >= 0; i++)
                    {
                        Delay(i, j, ref arr, -1);
                    }
                    i = n - 1;
                }
                else
                {
                    for (i = n - 1; 0 <= i && arr[i, j] >= 0; i--)
                    {
                        Delay(i, j, ref arr, -1);
                    }
                    i = 0;
                }
            }
        }
        //Метод с садовником из нижнего правого угла
        static void Gardner2()
        {
            int i = n-1;
            for (int j = m-1; 0 <= j; j--)
            {
                if (arr[i, j] < 0)
                {
                    return;
                }
                else if (i == 0)
                {
                    for (i = 0; i < n && arr[i, j] >= 0; i++)
                    {
                        Delay(i, j, ref arr, -2);
                    }
                    i = n-1;
                }
                else
                {
                    for (i = n - 1; 0 <= i && arr[i, j] >= 0; i--)
                    {
                        Delay(i, j, ref arr, -2);
                    }
                    i = 0;
                }
            }
        }
        //Метод с задержкой
        public static int[,] Delay(int i,int j,ref int[,] arr, int a)
        {
            int delay = arr[i, j];
            arr[i, j] = a;
            Thread.Sleep(delay);
            return arr;
        }
        static void Main(string[] args)
        {
            /*Имеется пустой участок земли (двумерный массив) и план сада, который необходимо реализовать. Эту задачу выполняют два садовника, которые не хотят 
             * встречаться друг с другом. Первый садовник начинает работу с верхнего левого угла сада и перемещается слева направо, сделав ряд, 
             * он спускается вниз. Второй садовник начинает работу с нижнего правого угла сада и перемещается снизу вверх, сделав ряд, он перемещается влево. 
             * Если садовник видит, что участок сада уже выполнен другим садовником, он идет дальше. Садовники должны работать параллельно. 
             * Создать многопоточное приложение, моделирующее работу садовников.*/

            //Генерация матрицы(участка)
            Random rnd = new Random();
            
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    arr[i,j]= rnd.Next(0, 20);
                    Console.Write($" {arr[i,j],2} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            //Задание потока первого фермера
            ThreadStart threadStart = new ThreadStart(Gardner1);
            Thread thread = new Thread(threadStart);
            thread.Start();

            //Метод второго фермера
            Gardner2();

            //Вывод итогов
            for (int j = 0; j < m ; j++)
            {
                for (int i = 0; i < n ; i++)
                {
                    Console.Write($" {arr[i, j],2} ");
                }
                Console.WriteLine();
            }


            Console.ReadKey();

        }
    }
}
