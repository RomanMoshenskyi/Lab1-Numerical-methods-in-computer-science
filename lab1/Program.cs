using System;
using System.Collections.Generic;

namespace lab1
{
    class Program
    {
        const double eps = 0.001;
        public static int numberFunction = 0;
        
        public static double fx(double x)  //рівняння
        {
            switch (Program.numberFunction - 1)
            {
                case 0:
                    return Math.Cos(Math.Sin(x * x * x)) - 0.7; ;
                case 1:
                    return x * x - 2 * x - 4;
                case 2:
                    return Math.Sin(x * x * x) / 5;
                case 3:
                    return (Math.Pow(x, 4) - 1) / Math.Pow(x, 3);
                case 4:
                    return x * x * Math.Cos(x) - 0.2;
                case 5:
                    return 1.5 - Math.Pow(x, 1 - Math.Cos(x));
                case 6:
                    return x * x * x - Math.Pow(3, x) + 1.5;
                case 7:
                    return Math.Cos(x * x - x + 1);
                case 8:
                    return Math.Exp(x) - Math.Sin(2 * x) - 1;
                case 9:
                    return (x * x * x - 7 * x + 1) / 3;
                case 10:
                    return Math.Cos(x * x) - 0.5;
                case 11:
                    return Math.Sin(x) - Math.Cos(x * x * x);
                case 12:
                    return Math.Sin(2 * x) + Math.Cos(2 * x);
                default:
                    return Math.Sin(2 * x) + Math.Cos(2 * x);
            }
        }
        public static List<double> VyzovFunction(double accuracy) //проміжки
        {
            switch (numberFunction - 1)
            {
                case 0:
                    return SecantMethodInterval(-(Math.PI / 2), Math.PI / 2);
                case 1:
                    return SecantMethodInterval(-4, 4);
                case 2:
                    return SecantMethodInterval(-2.5, 2.5);
                case 3:
                    return SecantMethodInterval(-10, 10);
                case 4:
                    return SecantMethodInterval(-(2 * Math.PI), 2 * Math.PI);
                case 5:
                    return SecantMethodInterval(0, 5 * Math.PI);
                case 6:
                    return SecantMethodInterval(-5, 5);
                case 7:
                    return SecantMethodInterval(-3, 3);
                case 8:
                    return SecantMethodInterval(-6, 6);
                case 9:
                    return SecantMethodInterval(-6, 6);
                case 10:
                    return SecantMethodInterval(-4, 4);
                case 11:
                    return SecantMethodInterval(-2, 2);
                default: return new List<double> { 0.0 };
            }
        }

        public static List<double> SecantMethodInterval(double a, double b)
        {
            List<double> result = new List<double>();
            List<double> intervals = new List<double>();
            double c = (double)(Math.Abs(a - b) / 1000); //крок

            for (double i = a; i <= b; i += c)
            {
                intervals.Add(i); //додаємо кожен знайдений Х
            }

            for (int i = 0; i < intervals.Count - 1; i++)
            {
                if (fx(intervals[i]) * fx(intervals[i + 1]) < 0) //перевіряємо, чи змінився знак
                {
                    result.Add(SecantMethod(intervals[i], intervals[i + 1], c)); 
                }
            }
            return result;
        } 
        static double SecantMethod(double x0, double x1, double h)
        {
            List<double> interval = new List<double>();
            double H = h * 0.001; //новий крок

            for (double j = x0; j <= x1; j += H)
            {
                interval.Add(j); //додаємо кожен новий знайдений Х
            }
            double x2;
            int l;
            do
            { //перевіряємо кожен Х за формулою
                for (l = 0;  l < interval.Count - 1; l++) 
                {
                    x2 = interval[l + 1] -
                        (fx(interval[l + 1]) * (interval[l + 1] - interval[l])) /
                        (fx(interval[l + 1]) - fx(interval[l]));
                    x0 = x1;
                    x1 = x2;
                    l++;
                }
            } while (Math.Abs(x1 - x0) <= eps && l < 1000); //первірка виходу

            return x1;
        }


        static void Main(string[] args)
        {
            Program.numberFunction = 2;
            double accuracy = 3000;
            var result = VyzovFunction(accuracy);
            for (int i = 0; i < result.Count; i++)
            {
                Console.Write($"X{i + 1} = ");
                Console.WriteLine($"{result[i]}");
            }
            Console.ReadKey();
        }
    }
}

