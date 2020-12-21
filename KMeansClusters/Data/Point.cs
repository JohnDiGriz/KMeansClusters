using System;
using System.Collections.Generic;
using System.Text;

namespace KMeansClusters.Data
{
    public class Point
    {
        public List<double> _coordinates;

        public int N { get => _coordinates.Count; }
        public Point(int n)
        {
            _coordinates = new List<double>();
            for (int i = 0; i < n; i++)
            {
                _coordinates.Add(0);
            }
        }
        public double this[int i]
        {
            get => _coordinates[i];
            set => _coordinates[i] = value;
        }

        public double d2(Point b)
        {
            if (N != b.N)
                throw new InvalidOperationException();
            var dSquare = 0.0;
            for (int i = 0; i < N; i++)
                dSquare += (_coordinates[i] - b[i]) * (_coordinates[i] - b[i]);
            return dSquare;
        }

        public double d(Point b)
        {
            return Math.Sqrt(d2(b));
        }


        public static Point operator *(double a, Point b)
        {
            var res = new Point(b.N);
            for (int i = 0; i < b.N; i++)
                res._coordinates[i] = a * b[i];
            return res;
        }
        public static Point operator +(Point a, Point b)
        {
            if (a.N != b.N)
                throw new InvalidOperationException();
            var res = new Point(a.N);
            for (int i = 0; i < a.N; i++)
                res._coordinates[i] = a[i] + b[i];
            return res;
        }
    }
}
