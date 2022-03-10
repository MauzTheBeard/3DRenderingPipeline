using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Game
{
    public static class LightData
    {
        public static float AmbientIntensity = 0.5f;
        public static float DiffuseIntensity = 1.0f;
        public static float SpecularIntensity = 1.0f;
        public static float Hardness = 32;

        public static Vector3 LightPosition = new Vector3(0, 10, -20);
        public static Vector3 AmbientLightColor = new Vector3(1, 1, 1);
        public static Vector3 Color = new Vector3(1, 1, 1);
        public static Vector3 ViewPosition = new Vector3(0, 0, 0);

        /// <summary>
        /// Get initial light data matrix
        /// </summary>
        /// <returns>Initial light data</returns>
        public static Matrix4 GetData()
        {
            return new Matrix4
            (
                new Vector4(LightPosition, AmbientIntensity),
                new Vector4(AmbientLightColor, DiffuseIntensity),
                new Vector4(Color, SpecularIntensity),
                new Vector4(ViewPosition, Hardness)
            );
        }
    }
}