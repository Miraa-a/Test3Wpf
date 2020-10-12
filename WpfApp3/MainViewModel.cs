﻿using SharpDX;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.SharpDX.Core;
using HelixToolkit.SharpDX.Core.Model;
using Camera = HelixToolkit.Wpf.SharpDX.Camera;
using Color = System.Windows.Media.Color;
using PerspectiveCamera = HelixToolkit.Wpf.SharpDX.PerspectiveCamera;
using SharpDX.Direct3D9;
using HelixToolkit.Wpf.SharpDX;
using SharpDX.Direct2D1;
using System.Windows.Documents;
using System.Collections.Generic;

namespace WpfApp3
{
   

    public class MainViewModel : ObservableObject
    {
        private Camera camera;
        public Camera Camera
        {
            get => camera;
            protected set => Set(ref camera, value);
        }

        private IEffectsManager effectsManager;
        public IEffectsManager EffectsManager
        {
            get => effectsManager;
            protected set => Set(ref effectsManager, value);
        }

        public LineGeometry3D Grid { get; private set; }
        public Color GridColor { get; private set; }
        public Transform3D GridTransform { get; private set; }
        public HelixToolkit.SharpDX.Core.MeshGeometry3D Box { get; private set; } 
        public PhongMaterial BoxColorMaterial { get; private set; }
        public HelixToolkit.SharpDX.Core.MeshGeometry3D Box2 { get; private set; }
        public Transform3D Box2Transform { get; private set; }
        public PhongMaterial Box2ColorMaterial { get; private set; }

        public MainViewModel()
        {
            EffectsManager = new DefaultEffectsManager();

            // camera setup
            Camera = new PerspectiveCamera
            {
                Position = new Point3D(3, 3, 5),
                LookDirection = new Vector3D(-3, -3, -5),
                UpDirection = new Vector3D(0, 1, 0),
                FarPlaneDistance = 5000000
            };

            // floor plane grid
            Grid = LineBuilder.GenerateGrid(new Vector3(0, 1, 0), -5, 5, -5, 5);
            GridColor = Colors.Black;
            GridTransform = new TranslateTransform3D(0, -3, 0);

            //---------------------Variant 1-----------------------------------
            //create box and show it
            var builder = new MeshBuilder(true, true,true);
            builder.AddBox(new Vector3(3, -2, -2), 1, 1, 1);
            Box = builder.ToMeshGeometry3D();
            BoxColorMaterial = new PhongMaterial
            {
                AmbientColor = Colors.Gray.ToColor4(),
                DiffuseColor = Colors.Gray.ToColor4(),
                EmissiveColor = Colors.DarkRed.ToColor4(),
                SpecularColor = Colors.Black.ToColor4(),
            };
            //---------------------Variant 1-----------------------------------

            //builder.AddTriangles(p);
            //    Vertex = "0,0,0 1,0,0 0,1,0 1,1,0 
            //                      0,0,1 1,0,1 0,1,1 1,1,1" 
            //Triangles = "0,2,1 1,2,3 0,4,2 2,4,6
            //                 0,1,4 1,5,4 1,7,5 1,3,7
            //                 4,5,6 7,6,5 2,6,3 3,6,7"
            //Normals = "0,1,0 0,1,0 1,0,0 1,0,0
            //         0,1,0 0,1,0 1,0,0 1,0,0"
            //-------------------------------Variant 2--------------------------------
            var p0 = new Vector3(0, 0, 0); var p3 = new Vector3(1, 1, 0);
            var p1 = new Vector3(1, 0, 0); var p4 = new Vector3(0, 0, 1);
            var p2 = new Vector3(0, 1, 0); var p5 = new Vector3(1, 0, 1);
            var p6 = new Vector3(0, 1, 1); var p7 = new Vector3(1, 1, 1);
            Vector3 positionBox = new Vector3(0, -2, -2);//position cube
            List<Vector3> vertexedge = new List<Vector3>();//add vertex to List
            vertexedge.Add(p0);
            vertexedge.Add(p1);
            vertexedge.Add(p2);
            vertexedge.Add(p3);
            vertexedge.Add(p4);
            vertexedge.Add(p5);
            vertexedge.Add(p6);
            vertexedge.Add(p7);
            Box2ColorMaterial = new PhongMaterial
            {
                //AmbientColor = Colors.Gray.ToColor4(),
                //DiffuseColor = Colors.Gray.ToColor4(),
                EmissiveColor = Colors.Blue.ToColor4(),
                //SpecularColor = Colors.Black.ToColor4(),
            };
            _3DModel box = new BoxBuilder(positionBox, p2, vertexedge, Box2ColorMaterial);
            Box2 = box.DrawModel();
            Box2Transform = new TranslateTransform3D(positionBox.ToVector3D());
            //-------------------------------Variant 2--------------------------------

        }
    }
}