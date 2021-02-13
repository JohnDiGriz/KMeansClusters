using System;
using System.Collections.Generic;
using System.Text;
using KMeansClusters.Data;
using System.Linq;

namespace KMeansClusters
{
    public static class KMeansHandler
    {
        public static HashSet<Cluster> Clusterize(List<Point> list, int k)
        {
            var clusters = Init(list, k);
            HashSet<PointMove> additions;
            do
            {
                additions = new HashSet<PointMove>();
                foreach (var cluster in clusters)
                    cluster.UpdateCentroid();
                foreach (var cluster in clusters)
                    foreach (var point in cluster.Points)
                    {
                        var newCluster = FindMinDist(point, clusters);
                        if (newCluster != cluster)
                            additions.Add(new PointMove() { Point = point, From = cluster, To = newCluster });
                    }
                foreach (var addition in additions)
                {
                    addition.To.AddPoint(addition.Point);
                    addition.From.RemovePoint(addition.Point);
                }
            } while (additions.Count > 0);
            return clusters;
        }

        private static HashSet<Cluster> Init(List<Point> list, int k)
        {
            HashSet<Cluster> clusters = new HashSet<Cluster>();
            HashSet<int> chosenPoint = new HashSet<int>();
            Random rand = new Random();
            int newPoint = 0;
            for (int i = 0; i < k; i++)
            {
                do
                {
                    newPoint = rand.Next(0, list.Count);
                } while (chosenPoint.Contains(newPoint));
                chosenPoint.Add(newPoint);
                var newCluster = new Cluster();
                newCluster.AddPoint(list[newPoint]);
                newCluster.UpdateCentroid();
                clusters.Add(newCluster);
            }
            int n = 0;
            foreach (var point in list)
            {
                if (point == clusters.FirstOrDefault().Points.FirstOrDefault())
                    n = 0;
                FindMinDist(point, clusters).AddPoint(point);
                n++;
            }
          
            return clusters;
        }
        private static Cluster FindMinDist(Point point, HashSet<Cluster> clusters)
        {
            double minDist = double.MaxValue;
            Cluster minCluster = null;
            foreach (var cluster in clusters)
            {
                if (cluster.Centroid.d2(point) < minDist)
                {
                    minDist = cluster.Centroid.d2(point);
                    minCluster = cluster;
                }
            }
            return minCluster;
        }

        private class PointMove
        {
            public Point Point { get; set; }
            public Cluster From { get; set; }
            public Cluster To { get; set; }
        }
    }
}
