using System;
using KMeansClusters.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
namespace KMeansClusters
{
    class Program
    {
        static void Main(string[] args)
        {
            var points = new List<Point>();
            Console.WriteLine("Enter path:");
            var text = File.ReadAllLines(Console.ReadLine());
            int pointN = 0;
            foreach(var line in text)
            {
                var coords = line.Split(",");
                var point = new Point(coords.Count());
                for (int i = 0; i < coords.Count(); i++)
                {
                    point[i] = double.Parse(coords[i], CultureInfo.InvariantCulture);
                }
                point.Number = pointN;
                points.Add(point);
                pointN++;
            }
            var clusters5 = KMeansHandler.Clusterize(points, 5);
            var clusters7 = KMeansHandler.Clusterize(points, 7);

            File.WriteAllText("Res5.txt", DisplayRes(clusters5));
            File.WriteAllText("Res7.txt", DisplayRes(clusters7));
        }

        static string DisplayRes(HashSet<Cluster> clusters)
        {
            var str = "";
            int i = 1;
            foreach(var cluster in clusters)
            {
                str += $"Cluster {i}\n";
                foreach (var point in cluster.Points)
                    str += $"{point.Number}, ";
                str.Remove(str.Length - 2);
                str += "\n";
                i++;
            }
            return str;
        }
    }
}
