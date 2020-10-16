using SharpDX;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Windows.Media.Media3D;
using DoubleOrSingle = System.Double;

namespace WpfApp3.Models
{
    class SphereModel : ModelBase
    {
        public float Radius { get; set; } = 1; 

        public override void Update()
        {   
            //Clear
            Positions.Clear();
            Indices.Clear();
            Normals.Clear();
            Point3D center = new Point3D(Vector3.UnitX.X, Vector3.UnitX.Y, Vector3.UnitX.Z);
            int thetaDiv = 32; int phiDiv = 32;
            int index0 = this.Positions.Count;
            var dt = 2 * (DoubleOrSingle)Math.PI / thetaDiv;
            var dp = (DoubleOrSingle)Math.PI / phiDiv;
            for (int pi = 0; pi <= phiDiv; pi++)
            {
                var phi = pi * dp;

                for (int ti = 0; ti <= thetaDiv; ti++)
                {
                    var theta = ti * dt;
                    var x = (DoubleOrSingle)Math.Cos(theta) * (DoubleOrSingle)Math.Sin(phi);
                    var y = (DoubleOrSingle)Math.Sin(theta) * (DoubleOrSingle)Math.Sin(phi);
                    var z = (DoubleOrSingle)Math.Cos(phi);

                    var p = new Point3D(center.X + (DoubleOrSingle)(Radius * x), center.Y + (DoubleOrSingle)(Radius * y), center.Z + (DoubleOrSingle)(Radius * z));
                    Positions.Add(new Vector3((float)p.X, (float)p.Y, (float)p.Z));

                    if (Normals != null)
                    {
                        var n = new Vector3((float)x, (float)y, (float)z);
                        Normals.Add(n);
                    }
                }
            }

            this.AddIndices(index0, phiDiv + 1, thetaDiv + 1, true);
        }

        public void AddIndices(int index0, int rows, int columns, bool isSpherical = false)
        {
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < columns - 1; j++)
                {
                    int ij = (i * columns) + j;
                    if (!isSpherical || i > 0)
                    {
                        Indices.Add(index0 + ij);
                        Indices.Add(index0 + ij + 1 + columns);
                        Indices.Add(index0 + ij + 1);
                    }

                    if (!isSpherical || i < rows - 2)
                    {
                        Indices.Add(index0 + ij + 1 + columns);
                        Indices.Add(index0 + ij);
                        Indices.Add(index0 + ij + columns);
                    }
                }
            }

        }
    }
}
