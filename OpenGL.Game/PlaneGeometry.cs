using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game
{
    public class PlaneGeometry : Geometry
    {
        public override Vector3[] GetVertices()
        {
            return new Vector3[]
            {
                new Vector3 (-1.0f, 0.0f, -1.0f),  //P0    0
                new Vector3 (1.0f, 0.0f, -1.0f),   //P1    1
                new Vector3 (1.0f, 0.0f, 1.0f),    //P2    2
                new Vector3 (-1.0f, 0.0f, 1.0f),   //P3    3
            };
        }

        public override uint[] GetIndices()
        {
            return new uint[]
            {
                0, 1, 2,
                0, 3, 2
            };
        }

        public override Vector2[] GetUVs()
        {
            return new Vector2[]
            {
                //x 0.5 - 1 so use second texture tile from texture atlas
                new Vector2(0.5f, 0.0f), new Vector2(1.0f, 0.0f), new Vector2(1.0f, 1.0f), new Vector2(0.5f, 1.0f)
            };
        }

        public override Vector3[] GetColors()
        {
            return new Vector3[]
            {
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f)
            };
        }

        public override Vector3[] GetNormals()
        {
            return new Vector3[]
            {
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f)
            };
        }
    }
}