using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL.Mathematics
{
    public class Transform
    {
        #region Properties

        public Vector3 Position
        {
            get;
            set;
        }

        public Vector3 Scale
        {
            get;
            set;
        }

        public Vector3 Rotation
        {
            get;
            set;
        }

        #endregion

        #region Properties

        public Matrix4 GetTRS()
        {
            return Matrix4.CreateScaling(Scale) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z) * Matrix4.CreateTranslation(Position);
            //return Matrix4.CreateTranslation(Position) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z) * Matrix4.CreateScaling(Scale);
        }

        #endregion

    }
}
