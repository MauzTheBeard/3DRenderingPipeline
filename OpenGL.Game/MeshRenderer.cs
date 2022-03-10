using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;

namespace OpenGLGame
{
    public class MeshRenderer
    {
        public Material Material;
        public VAO Geometry;

        public GameObject Parent
        {
            get;
            internal set;
        }

        public MeshRenderer(Material material, VAO geometry)
        {
            Material = material;
            Geometry = geometry;
        }

        public void Render()
        {
            Geometry.Program.Use();
            Parent.Update();
            Geometry.Draw();
        }

        public void SetMaterialColor(Vector3 color)
        {
            Material["color"].SetValue(color);
        }
    }
}
