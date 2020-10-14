using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Media.Media3D;

namespace WpfApp3
{
    class Box_Figure : Model_3D
    {
        public float length { get; set; }
        public float weight { get; set; }
        public float h { get; set; }
        public Box_Figure(Point3D pos) : base(pos)
        {
            indexs = null;
            BuildGeometry(pos);
        }
        public override void BuildGeometry(Point3D pos)
        {
            List<List<Vector3>> EdgeVertex = new List<List<Vector3>>();
            //Add vertex
            point.Add(new Point3D(pos.X + 1, pos.Y, pos.Z));
            point.Add(new Point3D(pos.X, pos.Y + 1, pos.Z));
            point.Add(new Point3D(pos.X + 1, pos.Y + 1, pos.Z));
            point.Add(new Point3D(pos.X, pos.Y, pos.Z + 1));
            point.Add(new Point3D(pos.X + 1, pos.Y, pos.Z + 1));
            point.Add(new Point3D(pos.X, pos.Y + 1, pos.Z + 1));
            point.Add(new Point3D(pos.X + 1, pos.Y + 1, pos.Z + 1));

            //Add triangles
            EdgeVertex.Add(CreateList(point[0], point[2], point[1]));
            EdgeVertex.Add(CreateList(point[1], point[2], point[3]));
            EdgeVertex.Add(CreateList(point[0], point[4], point[2]));
            EdgeVertex.Add(CreateList(point[2], point[4], point[6]));
            EdgeVertex.Add(CreateList(point[0], point[1], point[4]));
            EdgeVertex.Add(CreateList(point[1], point[5], point[4]));
            EdgeVertex.Add(CreateList(point[1], point[7], point[5]));
            EdgeVertex.Add(CreateList(point[1], point[3], point[7]));
            EdgeVertex.Add(CreateList(point[4], point[5], point[6]));
            EdgeVertex.Add(CreateList(point[7], point[6], point[5]));
            EdgeVertex.Add(CreateList(point[6], point[6], point[3]));
            EdgeVertex.Add(CreateList(point[3], point[6], point[7]));

            foreach (var v in EdgeVertex)
            {
                foreach (var j in v)
                {
                    triangles.Add(j);
                }
            }
            //Add normals
            foreach (var v in EdgeVertex)
            {

                normals.Add(CreateNormal(v[0].ToPoint3D(), v[1].ToPoint3D(), v[2].ToPoint3D()));

            }
            //Add edge

        }
        private List<Vector3> CreateList(Point3D p, Point3D p1, Point3D p2)
        {
            List<Vector3> v = new List<Vector3>() { p.ToVector3(), p1.ToVector3(), p2.ToVector3() };
            return v;
        }
        private /*KeyValuePair<Vector3, Vector3>*/ void hypoT () { 
            
            }
    }
}
