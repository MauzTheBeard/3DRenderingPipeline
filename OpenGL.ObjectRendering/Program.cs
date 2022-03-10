using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenGL;
using OpenGL.Game;
using OpenGL.Mathematics;
using OpenGL.Platform;
using OpenGL.UI;
using OpenGLGame;
using static OpenGL.GenericVAO;

namespace SAE.GPR.ObjectRendering
{
    static class Program
    {
        #region Members

        private static int width = 1920;
        private static int height = 1080;

        #region Game(Objects)
        private static Game game;
        private static GameObject gameObjectRough;
        private static GameObject gameObjectInteractive;
        private static GameObject gameObjectGlossy;
        private static GameObject gameObjectGround;
        #endregion
        
        private static float transformationSpeed = 10.0f;
        private static float rotationSpeed = 2.0f;

        #endregion

        #region Methods

        static void Main()
        {
            InitializeWindow();

            game = new Game();

            InitializeGameObjects();

            //StressTestGameObjects(); //--> error message: DAFUQ?(?)
            
            #region Register Input Events
            // Hook to the escape press event using the OpenGL.UI class library
            Input.Subscribe((char)Keys.Escape, Window.OnClose);

            Event evt = new Event(OnKeyStateChanged);
            Input.Subscribe((char)Key.W, evt);
            Input.Subscribe((char)Key.A, evt);
            Input.Subscribe((char)Key.S, evt);
            Input.Subscribe((char)Key.D, evt);

            Input.Subscribe((char)Key.I, evt);
            Input.Subscribe((char)Key.J, evt);
            Input.Subscribe((char)Key.K, evt);
            Input.Subscribe((char)Key.L, evt);
            Input.Subscribe((char)Key.O, evt);
            Input.Subscribe((char)Key.P, evt);

            Input.Subscribe((char)Key.N, evt);
            Input.Subscribe((char)Key.M, evt);

            Input.Subscribe((char)Key.R, evt);
            Input.Subscribe((char)Key.F, evt);
            Input.Subscribe((char)Key.T, evt);
            Input.Subscribe((char)Key.G, evt);
            Input.Subscribe((char)Key.Z, evt);
            Input.Subscribe((char)Key.H, evt);

            Input.Subscribe((char)Key.Up, evt);
            Input.Subscribe((char)Key.Left, evt);
            Input.Subscribe((char)Key.Down, evt);
            Input.Subscribe((char)Key.Right, evt);

            // Make sure to set up mouse event handlers for the window
            Window.OnMouseCallbacks.Add(OpenGL.UI.UserInterface.OnMouseClick);
            Window.OnMouseMoveCallbacks.Add(OpenGL.UI.UserInterface.OnMouseMove);
            #endregion

            PrintApplicationInformation();

            GameLoop();
        }

        private static void InitializeWindow()
        {
            Time.Initialize();
            Window.CreateWindow("Object Rendering S1", width, height);

            // add a reshape callback to update the UI
            Window.OnReshapeCallbacks.Add(OnResize);

            // add a close callback to make sure we dispose of everythng properly
            Window.OnCloseCallbacks.Add(OnClose);

            // Enable depth testing to ensure correct z-ordering of our fragments
            Gl.Enable(EnableCap.DepthTest);
            Gl.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            Gl.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
        }

        /// <summary>
        /// Create and add three cubes and a ground plane to SceneGraph & set different light data
        /// </summary>
        private static void InitializeGameObjects()
        {
            gameObjectRough = CreateGameObject(GeometryShapes.Cube, "Fancy Rough Crate", new Vector3(16.0f, 0.0f, -20.0f), new Vector3(0.0f, 0.0f, 10.0f), new Vector3(10.0f, 10.0f, 10.0f));
            gameObjectRough.LightData[1] = new Vector4(v: gameObjectRough.LightData[1].Xyz, w: 0.0f); //DiffuseIntensity
            gameObjectRough.LightData[2] = new Vector4(v: gameObjectRough.LightData[2].Xyz, w: 0.0f); //SpecularIntensity
            gameObjectRough.LightData[3] = new Vector4(v: gameObjectRough.LightData[3].Xyz, w: 0.0f); //Hardness

            gameObjectInteractive = CreateGameObject(GeometryShapes.Cube, "Fancy Interactive Crate", new Vector3(0.0f, 0.0f, -22.5f), Vector3.Zero, new Vector3(10.0f, 10.0f, 10.0f));

            gameObjectGlossy = CreateGameObject(GeometryShapes.Cube, "Fancy Glossy Crate", new Vector3(-16.0f, 0.0f, -30.0f), new Vector3(20.0f, 50.0f, 12.5f), new Vector3(10.0f, 10.0f, 10.0f));
            gameObjectGlossy.LightData[1] = new Vector4(v: gameObjectGlossy.LightData[1].Xyz, w: 0.0f); //DiffuseIntensity
            gameObjectGlossy.LightData[2] = new Vector4(v: gameObjectGlossy.LightData[2].Xyz, w: 1.0f); //SpecularIntensity
            gameObjectGlossy.LightData[3] = new Vector4(v: gameObjectGlossy.LightData[3].Xyz, w: 64.0f); //Hardness

            gameObjectGround = CreateGameObject(GeometryShapes.Plane, "Ground", new Vector3(0, -20, -75), new Vector3(0, 0, 0), new Vector3(100, 1, 100));
            gameObjectGround.LightData[1] = new Vector4(v: gameObjectRough.LightData[1].Xyz, w: 0.0f); //DiffuseIntensity
            gameObjectGround.LightData[2] = new Vector4(v: gameObjectRough.LightData[2].Xyz, w: 0.0f); //SpecularIntensity
            gameObjectGround.LightData[3] = new Vector4(v: gameObjectRough.LightData[3].Xyz, w: 0.0f); //Hardness

            game.SceneGraph.Add(gameObjectRough);
            game.SceneGraph.Add(gameObjectInteractive);
            game.SceneGraph.Add(gameObjectGlossy);
            game.SceneGraph.Add(gameObjectGround);
        }

        private static void StressTestGameObjects()
        {
            Random random = new Random();

            for (int i = 0; i < 80; i++)
            {
                float value = random.Next(0, 10);

                GameObject gameObject = CreateGameObject(GeometryShapes.Cube, "GameObject_" + i.ToString(), new Vector3(value, value, -value), Vector3.Zero, new Vector3(value, value, value));

                game.SceneGraph.Add(gameObject);
            }
        }

        /// <summary>
        /// Main Game Loop
        /// </summary>
        private static void GameLoop()
        {
            while (Window.Open)
            {
                Window.HandleInput();
                OnPreRenderFrame();

                game.Update();
                game.Render();
                
                OnPostRenderFrame();
                Time.Update();
                //System.Console.WriteLine(Time.DeltaTime);
            }
        }

        #region GameObject Helper Methods

        /// <summary>
        /// Creates a new GameObject
        /// </summary>
        /// <param name="name">Name of the GO</param>
        /// <param name="position">Initial position</param>
        /// <param name="rotation">Initial rotation</param>
        /// <param name="scale">Initial scale</param>
        /// <returns>New gameObject</returns>
        private static GameObject CreateGameObject(GeometryShapes shape, string name, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Material material = CreateMaterial();

            GameObject gameObject = new GameObject(name, new MeshRenderer(material, CreateGeometry(material, shape)), LightData.GetData());
            gameObject.Transform.Position = position;
            gameObject.Transform.Rotation = rotation;
            gameObject.Transform.Scale = scale;

            gameObject.Update();

            return gameObject;
        }

        /// <summary>
        /// Creates new material based on texture and shaders
        /// </summary>
        /// <returns></returns>
        private static Material CreateMaterial()
        {
            Texture crateTexture = new Texture("textures/texture_atlas.jpg");
            // Bind texture #0
            Gl.ActiveTexture(0);
            Gl.BindTexture(crateTexture);

            // Load shader files
            Material material = Material.Create("shaders\\vert.vs", "shaders\\frag.fs");
            material["color"]?.SetValue(new Vector3(1, 1, 1));

            return material;
        }

        /// <summary>
        /// Creates vertex array object based on cude data and material
        /// </summary>
        /// <param name="material">Material with texture and shaders binded</param>
        /// <returns>Cube geometry (VAO)</returns>
        private static VAO CreateGeometry(Material material, GeometryShapes shape)
        {
            OpenGL.Game.Geometry geometry = null;

            switch (shape)
            {
                case GeometryShapes.Cube:
                    geometry = new CubeGeometry();
                    break;
                case GeometryShapes.Plane:
                    geometry = new PlaneGeometry();
                    break;
                default:
                    break;
            }

            List<IGenericVBO> vbos = new List<IGenericVBO>();
            vbos.Add(new GenericVBO<Vector3>(new VBO<Vector3>(geometry.GetVertices()), "in_position"));
            vbos.Add(new GenericVBO<Vector3>(new VBO<Vector3>(geometry.GetColors()), "in_color"));
            vbos.Add(new GenericVBO<Vector2>(new VBO<Vector2>(geometry.GetUVs()), "in_texcoords"));
            vbos.Add(new GenericVBO<Vector3>(new VBO<Vector3>(geometry.GetNormals()), "in_normals"));
            vbos.Add(new GenericVBO<uint>(new VBO<uint>(geometry.GetIndices(), BufferTarget.ElementArrayBuffer, BufferUsageHint.DynamicDraw)));

            return new VAO(material, vbos.ToArray());
        }        

        #endregion

        #region Callbacks

        private static void OnResize()
        {
            width = Window.Width;
            height = Window.Height;

            OpenGL.UI.UserInterface.OnResize(Window.Width, Window.Height);
        }

        private static void OnClose()
        {
            // make sure to dispose of everything
            OpenGL.UI.UserInterface.Dispose();
            OpenGL.UI.BMFont.Dispose();
        }

        private static void OnPreRenderFrame()
        {
            // set up the OpenGL viewport and clear both the color and depth bits
            Gl.Viewport(0, 0, Window.Width, Window.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        private static void OnPostRenderFrame()
        {
            // Draw the user interface after everything else
            OpenGL.UI.UserInterface.Draw();

            // Swap the back buffer to the front so that the screen displays
            Window.SwapBuffers();
        }

        private static void OnMouseClick(int button, int state, int x, int y)
        {
            // take care of mapping the Glut buttons to the UI enums
            if (!OpenGL.UI.UserInterface.OnMouseClick(button + 1, (state == 0 ? 1 : 0), x, y))
            {
                // do other picking code here if necessary
            }
        }

        private static void OnMouseMove(int x, int y)
        {
            if (!OpenGL.UI.UserInterface.OnMouseMove(x, y))
            {
                // do other picking code here if necessary
            }
        }

        #endregion

        #region Input Events

        private static void OnKeyStateChanged(char c, bool isPressed)
        {
            Key key = (Key)c;

            if (isPressed)
            {
                switch(key)
                {
                    #region Rotation
                    case Key.W:
                        gameObjectInteractive.Transform.Rotation += new Vector3(rotationSpeed, 0, 0) * Time.DeltaTime;
                        break;

                    case Key.S:
                        gameObjectInteractive.Transform.Rotation -= new Vector3(rotationSpeed, 0, 0) * Time.DeltaTime;
                        break;

                    case Key.A:
                        gameObjectInteractive.Transform.Rotation += new Vector3(0, rotationSpeed, 0) * Time.DeltaTime;
                        break;

                    case Key.D:
                        gameObjectInteractive.Transform.Rotation -= new Vector3(0, rotationSpeed, 0) * Time.DeltaTime;
                        break;
                    #endregion
                    
                    #region Material Surface
                    case Key.R:
                        DiffuseManipulation(Time.DeltaTime);
                        break;

                    case Key.F:
                        DiffuseManipulation(-Time.DeltaTime);
                        break;

                    case Key.T:
                        SpecularManipulation(Time.DeltaTime);
                        break;

                    case Key.G:
                        SpecularManipulation(-Time.DeltaTime);
                        break;

                    case Key.Z:
                        HardnessManipulation(1.0f);
                        break;

                    case Key.H:
                        HardnessManipulation(-1.0f);
                        break;
                    #endregion
                    
                    #region Light
                    case Key.I:
                        LightMovement(new Vector3(0, transformationSpeed, 0));
                        break;

                    case Key.K:
                        LightMovement(new Vector3(0, -transformationSpeed, 0));
                        break;

                    case Key.J:
                        LightMovement(new Vector3(-transformationSpeed, 0, 0));
                        break;

                    case Key.L:
                        LightMovement(new Vector3(transformationSpeed, 0, 0));
                        break;

                    case Key.O:
                        LightMovement(new Vector3(0, 0, transformationSpeed));
                        break;

                    case Key.P:
                        LightMovement(new Vector3(0, 0, -transformationSpeed));
                        break;

                    case Key.N:
                        AmbientLightManipulation(Time.DeltaTime);
                        break;

                    case Key.M:
                        AmbientLightManipulation(-Time.DeltaTime);
                        break;
                    #endregion
                }
            }
        }

        private static void DiffuseManipulation(float value)
        {
            Vector4 newVec = new Vector4(gameObjectInteractive.LightData[1].Xyz, Mathf.Clamp(gameObjectInteractive.LightData[1].W + value, 0.0f, 2.0f));
            gameObjectInteractive.LightData[1] = newVec;
        }

        private static void SpecularManipulation(float value)
        {
            Vector4 newVec = new Vector4(gameObjectInteractive.LightData[2].Xyz, Mathf.Clamp(gameObjectInteractive.LightData[2].W + value, 0.0f, 2.0f));
            gameObjectInteractive.LightData[2] = newVec;
        }

        private static void HardnessManipulation(float value)
        {
            float newValue = Mathf.Clamp(gameObjectInteractive.LightData[3].W + value, 1.0f, 100.0f);
            gameObjectInteractive.LightData[3] = new Vector4(0, 0, 0, newValue);
        }

        private static void LightMovement(Vector3 direction)
        {
            foreach (GameObject gameObject in game.SceneGraph)
            {
                Vector4 newVec = new Vector4(gameObject.LightData[0].Xyz + direction * Time.DeltaTime, gameObject.LightData[0].W);
                gameObject.LightData[0] = newVec;
            }
        }

        private static void AmbientLightManipulation(float value)
        {
            foreach (GameObject gameObject in game.SceneGraph)
            {
                float newIntensity = Mathf.Clamp01(gameObject.LightData[0].W + value);
                gameObject.LightData[0] = new Vector4(v: gameObject.LightData[0].Xyz, w: newIntensity);
            }
        }

        #endregion

        #region Print Methods

        private static void PrintApplicationInformation()
        {
            System.Console.Clear();

            PrintLineBreaker();

            PrintInfos();

            PrintLineBreaker();

            PrintCrateControls();
            
            PrintLineBreaker();

            PrintLightControls();

            PrintLineBreaker();
        }

        private static void PrintInfos()
        {
            System.Console.WriteLine("INFOS:");
            System.Console.WriteLine("Left Crate: Specular Surface");
            System.Console.WriteLine("Middle Crate: Interactive (see Controls below)");
            System.Console.WriteLine("Right Crate: Matte Surface\n");
        }

        private static void PrintCrateControls()
        {
            System.Console.WriteLine("INTERACTIVE CRATE CONTROLS:");
            System.Console.WriteLine($"[RF] = Diffuse manipulation \t Initial: {gameObjectInteractive.LightData[1].W}");
            System.Console.WriteLine($"[TG] = Specular manipulation \t Initial: {gameObjectInteractive.LightData[2].W}");
            System.Console.WriteLine($"[ZH] = Hardness manipulation \t Initial: {gameObjectInteractive.LightData[3].W}");
            System.Console.WriteLine($"[WASD] = Rotation manipulation \t Initial: {gameObjectInteractive.Transform.Rotation}\n");
        }

        private static void PrintLightControls()
        {
            System.Console.WriteLine("POINT LIGHT CONTROLS:");
            System.Console.WriteLine($"[IK] = Y translation manipulation \t Initial: {gameObjectInteractive.LightData[0].Y}");
            System.Console.WriteLine($"[JL] = X translation manipulation \t Initial: {gameObjectInteractive.LightData[0].X}");
            System.Console.WriteLine($"[OP] = Z translation manipulation \t Initial: {gameObjectInteractive.LightData[0].Z}");
            System.Console.WriteLine($"[NM] = Ambient Intensity manipulation \t Initial: {gameObjectInteractive.LightData[0].W}\n");
        }

        private static void PrintLineBreaker()
        {
            System.Console.WriteLine("==================================================\n");
        }

        #endregion

        #endregion
    }
}