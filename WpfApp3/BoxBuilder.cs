using HelixToolkit.SharpDX.Core;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace WpfApp3
{
   public class BoxBuilder : _3DModel
    {
        public HelixToolkit.SharpDX.Core.MeshGeometry3D Box { get; private set; }
        //FACE

        public BoxBuilder(Vector3 pos, Vector3 nor, List<Vector3> allTriVertex_, PhongMaterial Material_Fig) 
            : base (pos,nor,allTriVertex_, Material_Fig)
        {
           //position = pos;
           //normal = nor;
           //allTriVertex = allTriVertex_;
           // Material_Figure = new PhongMaterial
           //{
           //    AmbientColor = Colors.Gray.ToColor4(),
           //    DiffuseColor = Colors.Gray.ToColor4(),
           //    EmissiveColor = Colors.BlueViolet.ToColor4(),
           //    SpecularColor = Colors.Black.ToColor4(),
           //};
        }
        public override HelixToolkit.SharpDX.Core.MeshGeometry3D DrawModel()
        {
            var builder = new MeshBuilder();
            builder.AddTriangle(allTriVertex[0], allTriVertex[2], allTriVertex[1]);
            builder.AddTriangle(allTriVertex[1], allTriVertex[2], allTriVertex[3]);
            builder.AddTriangle(allTriVertex[0], allTriVertex[4], allTriVertex[2]);
            builder.AddTriangle(allTriVertex[2], allTriVertex[4], allTriVertex[6]);
            builder.AddTriangle(allTriVertex[0], allTriVertex[1], allTriVertex[4]);
            builder.AddTriangle(allTriVertex[1], allTriVertex[5], allTriVertex[4]);
            builder.AddTriangle(allTriVertex[1], allTriVertex[7], allTriVertex[5]);
            builder.AddTriangle(allTriVertex[1], allTriVertex[3], allTriVertex[7]);
            builder.AddTriangle(allTriVertex[4], allTriVertex[5], allTriVertex[6]);
            builder.AddTriangle(allTriVertex[7], allTriVertex[6], allTriVertex[5]);
            builder.AddTriangle(allTriVertex[2], allTriVertex[6], allTriVertex[3]);
            builder.AddTriangle(allTriVertex[3], allTriVertex[6], allTriVertex[7]);
            Box = builder.ToMeshGeometry3D();
            return Box;
        }

        private Vector3D CreateNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            Vector3D v0 = new Vector3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            Vector3D v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            return Vector3D.CrossProduct(v0, v1);
        }
    }

    }
////public System.Windows.Media.Media3D.Material mat;
////public Transform3D BoxTransform;
////public System.Windows.Media.Media3D.GeometryModel3D geometryModel;
//public System.Windows.Media.Media3D.Material mat { get; set; }
//public Transform3D BoxTransform { get; set; }
//public System.Windows.Media.Media3D.GeometryModel3D geometryModel { get; set; }
//public BoxBuilder(Vector3 pos, Size s /*Vector3 nor, List>Triangle> allTri*/) 
//    :base (pos, s /*nor, allTri*/)
//{
//    System.Windows.Media.Media3D.MeshGeometry3D mesh = new System.Windows.Media.Media3D.MeshGeometry3D();
//    // Проставляем вершины квадрату
//    mesh.Positions = new Point3DCollection(new List<Point3D>
//    {
//        new Point3D(-size.Width/2, -size.Height/2, 0),
//        new Point3D(size.Width/2, -size.Height/2, 0),
//        new Point3D(size.Width/2, size.Height/2, 0),
//        new Point3D(-size.Width/2, size.Height/2, 0)
//    });
//    // Указываем индексы для квадрата
//    mesh.TriangleIndices = new Int32Collection(new List<int> { 0, 1, 2, 0, 2, 3 });
//    //nor = normal;
//    //allTri = allTri;
//    PhongMaterial BoxColorMaterial = new PhongMaterial
//    {
//        AmbientColor = Colors.Gray.ToColor4(),
//        DiffuseColor = Colors.Gray.ToColor4(),
//        EmissiveColor = Colors.Red.ToColor4(),
//        SpecularColor = Colors.Black.ToColor4(),
//    };

//    /*System.Windows.Media.Media3D.Material*/ mat = new System.Windows.Media.Media3D.DiffuseMaterial(Brushes.Red);
//    /*Transform3D */ BoxTransform = new TranslateTransform3D(pos.X, pos.Y, pos.Z);  
//    /*System.Windows.Media.Media3D.GeometryModel3D*/ geometryModel = new System.Windows.Media.Media3D.GeometryModel3D(mesh, mat);
//public Vector3 position;
//public Vector3 normal;
//public List<Vector3> allTriVertex = new List<Vector3>();
//public PhongMaterial Material_Figure { get; private set; }

