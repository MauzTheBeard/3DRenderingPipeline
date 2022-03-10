using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLGame
{
    public class Game
    {
        public List<GameObject> SceneGraph;

        public Game()
        {
            SceneGraph = new List<GameObject>();
        }

        public void Render()
        {
            foreach (GameObject obj in SceneGraph)
            {
                obj.MeshRenderer.Render();
            }
        }

        public void Update()
        {
            foreach (GameObject obj in SceneGraph)
            {
                obj.Update();
            }
        }
    }
}
