using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.SharpDX.Core;
using HelixToolkit.SharpDX.Core.Model;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using Camera = HelixToolkit.Wpf.SharpDX.Camera;
using Color = System.Windows.Media.Color;
using PerspectiveCamera = HelixToolkit.Wpf.SharpDX.PerspectiveCamera;
using MeshGeometry3D = HelixToolkit.SharpDX.Core.MeshGeometry3D;

namespace WpfApp3
{


    public class MainViewModel : ObservableObject
    {
        public Camera Camera { get; } = new PerspectiveCamera
        {
            Position = new Point3D(3, 3, 5),
            LookDirection = new Vector3D(-3, -3, -5),
            UpDirection = new Vector3D(0, 1, 0),
            FarPlaneDistance = 5000000
        };

        public IEffectsManager EffectsManager { get; } = new DefaultEffectsManager();

        public LineGeometry3D Grid { get; } = LineBuilder.GenerateGrid(new Vector3(0, 1, 0), -5, 5, -5, 5);
        public Color GridColor { get; } = Colors.Black;

        public MeshGeometry3D Model { get; private set; }
        public PhongMaterial ModelMaterial { get; } = PhongMaterials.Blue;

        public LineGeometry3D Edges { get; private set; }
        public Color EdgesColor { get; } = Colors.DimGray;


        public MainViewModel()
        {
            ModelBase box = new BoxModel();
            box.Update();

            VisualiseModel(box);
        }


        void VisualiseModel(ModelBase model)
        {
            Model  =  new MeshGeometry3D()
            {
                Positions = model.Positions,
                Indices = model.Indices,
                Normals =  model.Normals,
                TextureCoordinates = null,
                Tangents =  null,
                BiTangents =  null,
            };

            var inxs = new IntCollection();
            foreach (var edge in model.Edges)
            {
                inxs.AddAll(edge.Indices);
            }

            Edges =  new LineGeometry3D { Positions = model.Positions, Indices = inxs };
        }
    }
}
