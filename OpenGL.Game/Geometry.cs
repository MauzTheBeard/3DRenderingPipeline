using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenGL.Game
{
    public enum GeometryShapes
    {
        Cube,
        Plane
    }

    public abstract class Geometry
    {
        protected Vector3[] Vertices;
        protected uint[] Indices;
        protected Vector2[] UVs;
        protected Vector3[] Colors;
        protected Vector3[] Normals;

        public abstract Vector3[] GetVertices();
        public abstract uint[] GetIndices();
        public abstract Vector2[] GetUVs();
        public abstract Vector3[] GetColors();
        public abstract Vector3[] GetNormals();
    }
}