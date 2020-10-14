using HelixToolkit.SharpDX.Core;
using SharpDX;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;

namespace WpfApp3
{
    class Model_3D
    {
        public List<Point3D> point { get; set; }
        public List<Vector3> indexs { get; set; }
        public List<Vector3> normals { get; set; }
        public List<Face> faces;
        public List<Edge> edges;
        public List<Vector3> triangles {get;set;}

        public Model_3D(Point3D pos)
        {
            this.point = new List<Point3D>();
            this.point.Add(pos);
            this.indexs = new List<Vector3>();
            this.normals = new List<Vector3>();
            this.triangles = new List<Vector3>();
        }

        public virtual void BuildGeometry(Point3D pos)
        {
           
        }
        public Vector3 CreateNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            Vector3D v0 = new Vector3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            Vector3D v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            Vector3D v3 = Vector3D.CrossProduct(v0, v1);
            Vector3 res = new Vector3((float)v3.X, (float)v3.Y, (float)v3.Z);
            return res;
        }
    }
}
