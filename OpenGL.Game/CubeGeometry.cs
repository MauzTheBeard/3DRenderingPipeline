using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game
{
    public class CubeGeometry : Geometry
    {
        public override Vector3[] GetVertices()
        {
            return new Vector3[]
            {
                //Front Face
                new Vector3 (-0.5f, -0.5f, -0.5f),  //P0    0
                new Vector3 (0.5f, -0.5f, -0.5f),   //P1    1
                new Vector3 (0.5f, 0.5f, -0.5f),    //P2    2
                new Vector3 (-0.5f, 0.5f, -0.5f),   //P3    3

                //Top face                
                new Vector3 (0.5f, 0.5f, -0.5f),    //P2    4
                new Vector3 (-0.5f, 0.5f, -0.5f),   //P3    5
                new Vector3 (-0.5f, 0.5f, 0.5f),    //P4    6
                new Vector3 (0.5f, 0.5f, 0.5f),     //P5    7
                
                //Back Face
                new Vector3 (-0.5f, 0.5f, 0.5f),    //P4    8
                new Vector3 (0.5f, 0.5f, 0.5f),     //P5    9
                new Vector3 (0.5f, -0.5f, 0.5f),    //P6    10
                new Vector3 (-0.5f, -0.5f, 0.5f),   //P7    11

                //Bottom Face
                new Vector3 (-0.5f, -0.5f, -0.5f),  //P0    12
                new Vector3 (0.5f, -0.5f, -0.5f),   //P1    13
                new Vector3 (0.5f, -0.5f, 0.5f),    //P6    14
                new Vector3 (-0.5f, -0.5f, 0.5f),   //P7    15

                //Right Side Face
                new Vector3 (0.5f, -0.5f, -0.5f),   //P1    16
                new Vector3 (0.5f, 0.5f, -0.5f),    //P2    17
                new Vector3 (0.5f, 0.5f, 0.5f),     //P5    18
                new Vector3 (0.5f, -0.5f, 0.5f),    //P6    19

                //Left Side Face
                new Vector3 (-0.5f, -0.5f, -0.5f),  //P0    20
                new Vector3 (-0.5f, 0.5f, -0.5f),   //P3    21
                new Vector3 (-0.5f, 0.5f, 0.5f),    //P4    22
                new Vector3 (-0.5f, -0.5f, 0.5f),   //P7    23
            };
        }

        public override uint[] GetIndices()
        {
            return new uint[]
            {
                //Front Face
                0, 2, 1,
                0, 3, 2,

                //Top Face
                4, 6, 5,
                4, 7, 6,

                //Back Face
                8, 10, 9,
                8, 11, 10,

                //Bottom Face
                12, 14, 13,
                12, 15, 14,

                //Right Side Face
                16, 18, 17,
                16, 19, 18,

                //Left side Face
                20, 22, 21,
                20, 23, 22
            };
        }

        public override Vector2[] GetUVs()
        {
            return new Vector2[]
            {
                //x 0 - 0.5 so use first texture tile from texture atlas
                new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f), new Vector2(0.0f, 1.0f),
                new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f), new Vector2(0.0f, 1.0f),
                new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f), new Vector2(0.0f, 1.0f),
                new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f), new Vector2(0.0f, 1.0f),
                new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f), new Vector2(0.0f, 1.0f),
                new Vector2(0.0f, 0.0f), new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f), new Vector2(0.0f, 1.0f),
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
                //Front Face
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, 0.0f, -1.0f),
                new Vector3(0.0f, 0.0f, -1.0f),

                //Top Face
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),

                //Back Face
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 1.0f),

                //Bottom Face
                new Vector3(0.0f, -1.0f, 0.0f),
                new Vector3(0.0f, -1.0f, 0.0f),
                new Vector3(0.0f, -1.0f, 0.0f),
                new Vector3(0.0f, -1.0f, 0.0f),

                //Right Side Face
                new Vector3(1.0f, 0.0f, 0.0f),
                new Vector3(1.0f, 0.0f, 0.0f),
                new Vector3(1.0f, 0.0f, 0.0f),
                new Vector3(1.0f, 0.0f, 0.0f),

                //Left Side Face
                new Vector3(-1.0f, 0.0f, 0.0f),
                new Vector3(-1.0f, 0.0f, 0.0f),
                new Vector3(-1.0f, 0.0f, 0.0f),
                new Vector3(-1.0f, 0.0f, 0.0f)
            };
        }        
    }
}
