using SharpDX;

namespace WpfApp3
{
    class BoxModel : ModelBase
    {
        public float Length { get; set; } = 1; // по X
        public float Width { get; set; } = 2; // по Y
        public float Height { get; set; } = 3; // по Z

        public override void Update()
        {
            //Clear
            Positions.Clear();
            Indices.Clear();
            Normals.Clear();

            AddCubeFace(Vector3.Zero, Vector3.UnitX, Vector3.UnitZ, Length, Width, Height);
            AddCubeFace(Vector3.Zero, -Vector3.UnitX, Vector3.UnitZ, Length, Width, Height);
            AddCubeFace(Vector3.Zero, -Vector3.UnitY, Vector3.UnitZ, Width, Length, Height);
            AddCubeFace(Vector3.Zero, Vector3.UnitY, Vector3.UnitZ, Width, Length, Height);
            AddCubeFace(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY, Height, Length, Width);
            AddCubeFace(Vector3.Zero, -Vector3.UnitZ, Vector3.UnitY, Height, Length, Width);
        }


        void AddCubeFace(Vector3 center, Vector3 normal, Vector3 up, float dist, float width, float height)
        {
            var right = Vector3.Cross(normal, up);
            var n = normal * dist / 2;
            up *= height / 2;
            right *= width / 2;
            var p1 = center + n - up - right;
            var p2 = center + n - up + right;
            var p3 = center + n + up + right;
            var p4 = center + n + up - right;

            int i0 = Positions.Count;
            Positions.Add(p1);
            Positions.Add(p2);
            Positions.Add(p3);
            Positions.Add(p4);
            if (Normals != null)
            {
                Normals.Add(normal);
                Normals.Add(normal);
                Normals.Add(normal);
                Normals.Add(normal);
            }

            Indices.Add(i0 + 2);
            Indices.Add(i0 + 1);
            Indices.Add(i0 + 0);
            Indices.Add(i0 + 0);
            Indices.Add(i0 + 3);
            Indices.Add(i0 + 2);
            
            // add Face
            // Face.Add (......
            // нужно добавить код

            // add edges
            // пока ребра от разных граней дублируются (нужно сделать чтобы не дублировались)
            var edge = new Edge();
            edge.Indices.Add(i0 + 0);
            edge.Indices.Add(i0 + 1);
            edge.Indices.Add(i0 + 1);
            edge.Indices.Add(i0 + 2);
            edge.Indices.Add(i0 + 2);
            edge.Indices.Add(i0 + 3);
            edge.Indices.Add(i0 + 3);
            edge.Indices.Add(i0 + 0);
            Edges.Add(edge);

        }
    }
}
