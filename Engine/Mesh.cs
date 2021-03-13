using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sanjigen.Engine.MathHelpers;

namespace sanjigen.Engine
{
    public struct Face
    {
        public int A;
        public int B;
        public int C;

        public Vector3 Normal;
    }
    public partial class Mesh
    {
        public string Name { get; set; }
        public Vertex[] Vertices { get; private set; }
        public Face[] Faces { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Texture Texture { get; set; }

        public Mesh(string name, int verticesCount, int facesCount)
        {
            Vertices = new Vertex[verticesCount];
            Faces = new Face[facesCount];
            Name = name;
        }

        public void ComputeFacesNormals()
        {
            for (int i = 0; i < Faces.Length; i++)
            {
                var face = Faces[i];
                var vertexA = Vertices[face.A];
                var vertexB = Vertices[face.B];
                var vertexC = Vertices[face.C];

                Faces[i].Normal = (vertexA.Normal + vertexB.Normal + vertexC.Normal) / 3.0f;
                Vector3.Normalize(Faces[i].Normal);
            }
        }
    }

    public struct Vertex
    {
        public Vector3 Normal;
        public Vector3 Coordinates;
        public Vector3 WorldCoordinates;
        public Vector2 TextureCoordinates;
    }

}
