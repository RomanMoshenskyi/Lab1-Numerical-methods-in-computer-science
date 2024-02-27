using System;
using System.Collections.Generic;

class Program
{
    static double FindRoot(Func<double, double> function, double x0, double x1, double epsilon)
    {
        double x = x1;
        double xPrevious = x0;
        double fx = function(x);
        double fxPrevious = function(xPrevious);

        while (Math.Abs(x - xPrevious) > epsilon)
        {
            double tempX = x;
            x = x - fx * (x - xPrevious) / (fx - fxPrevious);
            xPrevious = tempX;
            fxPrevious = fx;
            fx = function(x);
        }

        return x;
    }
    static List<double> FindRootsOnInterval(Func<double, double> function, double start, double end, int numberOfParts, double epsilon)
    {
        if (epsilon <= 0)
        {
            throw new ArgumentException("Епсилон повинен бути більшим за 0.");
        }

        if (end <= start)
        {
            throw new ArgumentException("Кінець має бути більшим за початок.");
        }

        if (numberOfParts <= 0)
        {
            throw new ArgumentException("Кількість частин має бути більшою за 0");
        }

        List<double> roots = new List<double>();
        double h = (end - start) / numberOfParts;

        for (int i = 0; i < numberOfParts; i++)
        {
            double x1 = start + i * h;
            double x2 = x1 + h;
            double y1 = function(x1);
            double y2 = function(x2);

            if (y1 * y2 <= 0)
            {
                double root = FindRoot(function, x1, x2, epsilon);
                roots.Add(root);
            }
        }

        if (roots.Count == 0)
        {
            if (function(start) == 0)
                roots.Add(start);
            if (function(end) == 0)
                roots.Add(end);
        }

        if (roots.Count == 0 && numberOfParts > 1)
        {
            List<double> roots1 = FindRootsOnInterval(function, start, (start + end) / 2, numberOfParts / 2, epsilon);
            List<double> roots2 = FindRootsOnInterval(function, (start + end) / 2, end, numberOfParts / 2, epsilon);
            roots.AddRange(roots1);
            roots.AddRange(roots2);
        }

        return roots;
    }
    static void Main(string[] args)
    {
        double startOfRange = -4;
        double endOfRange = 4;
        int maxNumberOfParts = 10;
        double epsilon = 0.001;
        Func<double, double> function = x => x * x - 2 * x - 4;

        List<double> roots = FindRootsOnInterval(function, startOfRange, endOfRange, maxNumberOfParts, epsilon);

        Console.WriteLine("Корені:");
        foreach (double root in roots)
        {
            Console.WriteLine(root);
        }
    }
}
