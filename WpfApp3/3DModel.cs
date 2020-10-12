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
    public abstract class _3DModel
    {
        public Vector3 position;
        public Vector3 normal;
        public List<Vector3> allTriVertex = new List<Vector3>();
        public PhongMaterial Material_Figure { get; private set; }
        //FACE
        public _3DModel(Vector3 pos, Vector3 nor, List<Vector3> allTriVertex_, PhongMaterial Material_Fig)
        {
            position = pos;
            normal = nor;
            allTriVertex = allTriVertex_;
            Material_Figure = Material_Fig;

        }
        public abstract HelixToolkit.SharpDX.Core.MeshGeometry3D DrawModel();
    }
}
