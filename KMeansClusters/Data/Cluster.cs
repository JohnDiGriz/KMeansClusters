using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KMeansClusters.Data
{
    public class Cluster
    {
        public HashSet<Point> Points = new HashSet<Point>();
        public Point Centroid;
        public int N { get; set; }

        public void UpdateCentroid()
        {
            var pointSum = new Point(Points.FirstOrDefault().N);

            foreach (var p in Points)
                pointSum += p;
            Centroid = (1.0 / Points.Count) * pointSum;
        }
        public void AddPoint(Point p)
        {
            Points.Add(p);
        }

        public void RemovePoint(Point p)
        {
            Points.Remove(p);
        }
    }
}
