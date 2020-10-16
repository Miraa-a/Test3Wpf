using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using DoubleOrSingle = System.Double;
using Point = System.Windows.Point;

namespace WpfApp3.Models
{
    internal class СylinderModel : ModelBase
    {
        public float Radius { get; set; } = 1;

        public override void Update()
        {
            Point3D p1 = new Point3D(0, 0, 0);
            Point3D p2 = new Point3D(0, 2, 0);
            Vector3D n = p2 - p1;
            var l = (DoubleOrSingle)Math.Sqrt(n.X * n.X + n.Y * n.Y + n.Z * n.Z);
            n.Normalize();
            int thetaDiv = 32;
            var pc = new PointCollection();
            pc.Add(new Point(0, 0));
            pc.Add(new Point(0, (DoubleOrSingle)Radius));
            pc.Add(new Point((DoubleOrSingle)l, (DoubleOrSingle)Radius));
            pc.Add(new Point((DoubleOrSingle)l, 0));
            BuildGeometry(pc, p1, n, thetaDiv);
        }

        public IList<Point> AddCircle(int thetaDiv, bool closed = false)
        {
            ThreadLocal<Dictionary<int, IList<Point>>> CircleCache = new ThreadLocal<Dictionary<int, IList<Point>>>(() => new Dictionary<int, IList<Point>>());

            ThreadLocal<Dictionary<int, IList<Point>>> ClosedCircleCache = new ThreadLocal<Dictionary<int, IList<Point>>>(() => new Dictionary<int, IList<Point>>());

            ThreadLocal<Dictionary<int, MeshGeometry3D>> UnitSphereCache = new ThreadLocal<Dictionary<int, MeshGeometry3D>>(() => new Dictionary<int, MeshGeometry3D>());
            IList<Point> circle = null;
            if ((!closed && !CircleCache.Value.TryGetValue(thetaDiv, out circle)) ||
                (closed && !ClosedCircleCache.Value.TryGetValue(thetaDiv, out circle)))
            {
                circle = new PointCollection();
                if (!closed)
                {
                    CircleCache.Value.Add(thetaDiv, circle);
                }
                else
                {
                    ClosedCircleCache.Value.Add(thetaDiv, circle);
                }
                var num = closed ? thetaDiv : thetaDiv - 1;
                for (int i = 0; i < thetaDiv; i++)
                {
                    var theta = (DoubleOrSingle)Math.PI * 2 * ((DoubleOrSingle)i / num);
                    circle.Add(new Point((DoubleOrSingle)Math.Cos(theta), -(DoubleOrSingle)Math.Sin(theta)));
                }
            }
            IList<Point> result = new List<Point>();
            foreach (var point in circle)
            {
                result.Add(new Point(point.X, point.Y));
            }
            return result;
        }
        public void BuildGeometry(IList<Point> points, Point3D origin, Vector3D direction, int thetaDiv)
        {
            direction.Normalize();
            Vector3D u = Vector3D.CrossProduct(new Vector3D(0, 1, 0), direction);
            if (u.LengthSquared < 1e-3)
            {
                u = Vector3D.CrossProduct(new Vector3D(1, 0, 0), direction);
            }
            var v = Vector3D.CrossProduct(direction, u);
            u.Normalize();
            v.Normalize();

            var circle = AddCircle(thetaDiv);

            int index0 = Positions.Count;
            int n = points.Count;

            int totalNodes = (points.Count - 1) * 2 * thetaDiv;
            int rowNodes = (points.Count - 1) * 2;

            for (int i = 0; i < thetaDiv; i++)
            {
                var w = (v * circle[i].X) + (u * circle[i].Y);

                for (int j = 0; j + 1 < n; j++)
                {
                    var q1 = origin + (direction * points[j].X) + (w * points[j].Y);
                    var q2 = origin + (direction * points[j + 1].X) + (w * points[j + 1].Y);
                    Positions.Add(new Vector3((float)q1.X, (float)q1.Y, (float)q1.Z));
                    Positions.Add(new Vector3((float)q2.X, (float)q2.Y, (float)q2.Z));

                    if (Normals != null)
                    {
                        var tx = points[j + 1].X - points[j].X;
                        var ty = points[j + 1].Y - points[j].Y;
                        var normal = (-direction * ty) + (w * tx);
                        normal.Normalize();
                        Normals.Add(normal.ToVector3());
                        Normals.Add(normal.ToVector3());
                    }


                    int i0 = index0 + (i * rowNodes) + (j * 2);
                    int i1 = i0 + 1;
                    int i2 = index0 + ((((i + 1) * rowNodes) + (j * 2)) % totalNodes);
                    int i3 = i2 + 1;

                    Indices.Add(i1);
                    Indices.Add(i0);
                    Indices.Add(i2);

                    Indices.Add(i1);
                    Indices.Add(i2);
                    Indices.Add(i3);
                }
            }
        }
    }
}
