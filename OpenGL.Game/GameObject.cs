using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;
using OpenGL.Mathematics;

namespace OpenGLGame
{
    public class GameObject
    {
        public string Name;
        public Transform Transform;
        public MeshRenderer MeshRenderer;
        public Matrix4 LightData;

        public GameObject(string name, MeshRenderer meshRenderer, Matrix4 lightData)
        {
            Transform = new Transform();
            Name = name;
            MeshRenderer = meshRenderer;
            MeshRenderer.Parent = this;
            LightData = lightData;
        }

        public void Update()
        {
            SetTransform();
        }

        private void SetTransform()
        {
            Matrix4 view = GetViewMatrix();
            Matrix4 projection = GetProjectionMatrix();
            Matrix4 model = Transform.GetTRS();
            //Matrix4 tangentToWorld = model.Inverse().Transpose();

            // Data passing to shader
            Material material = this.MeshRenderer.Material;

            material["projection"].SetValue(projection);
            material["view"]?.SetValue(view);
            material["model"]?.SetValue(Transform.GetTRS());

            material["light"]?.SetValue(LightData);
        }

        private static Matrix4 GetProjectionMatrix()
        {
            float fov = 65;

            float aspectRatio = 1600.0f / 800.0f;
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(Mathf.ToRad(fov), aspectRatio, 0.1f, 1000f);
            //projection = Matrix4.CreateOrthographic0.0f, (float)screenWidth, 0.0f, (float)screenHeight, 0.1f, 100.0f);

            return projection;
        }

        private static Matrix4 GetViewMatrix()
        {
            Matrix4 viewTranslation = Matrix4.Identity;
            Matrix4 viewRotation = Matrix4.Identity;
            Matrix4 viewScale = Matrix4.Identity;

            viewTranslation = Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, 0.0f));
            viewRotation = Matrix4.CreateRotation(new Vector3(0.0f, 0.0f, 1.0f), 0.0f);
            viewScale = Matrix4.CreateScaling(new Vector3(1.0f, 1.0f, 1.0f));

            //Matrix4 view = viewTranslation * viewRotation * viewScale;// TRS matrix -> scale, rotate then translate -> All applied in WORLD Coordinates
            Matrix4 view = viewRotation * viewTranslation * viewScale;// RTS matrix -> scale, rotate then translate -> All applied in LOCAL Coordinates

            return view;
        }
    }
}