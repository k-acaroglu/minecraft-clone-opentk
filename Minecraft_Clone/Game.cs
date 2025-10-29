using StbImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Minecraft_Clone.Graphics;
using Minecraft_Clone.World;

namespace Minecraft_Clone
{
    internal class Game : GameWindow
    {
        // List<Vector3> vertices = new List<Vector3>()
        // {

        //     // right face
        //     new Vector3(0.5f, 0.5f, 0.5f), // topleft vert
        //     new Vector3(0.5f, 0.5f, -0.5f), // topright vert
        //     new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
        //     new Vector3(0.5f, -0.5f, 0.5f), // bottomleft vert
        //     // back face
        //     new Vector3(0.5f, 0.5f, -0.5f), // topleft vert
        //     new Vector3(-0.5f, 0.5f, -0.5f), // topright vert
        //     new Vector3(-0.5f, -0.5f, -0.5f), // bottomright vert
        //     new Vector3(0.5f, -0.5f, -0.5f), // bottomleft vert
        //     // left face
        //     new Vector3(-0.5f, 0.5f, -0.5f), // topleft vert
        //     new Vector3(-0.5f, 0.5f, 0.5f), // topright vert
        //     new Vector3(-0.5f, -0.5f, 0.5f), // bottomright vert
        //     new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
        //     // top face
        //     new Vector3(-0.5f, 0.5f, -0.5f), // topleft vert
        //     new Vector3(0.5f, 0.5f, -0.5f), // topright vert
        //     new Vector3(0.5f, 0.5f, 0.5f), // bottomright vert
        //     new Vector3(-0.5f, 0.5f, 0.5f), // bottomleft vert
        //     // bottom face
        //     new Vector3(-0.5f, -0.5f, 0.5f), // topleft vert
        //     new Vector3(0.5f, -0.5f, 0.5f), // topright vert
        //     new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
        //     new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
        // };

        // // goes from 0,0 to 1,1 because flipped
        // List<Vector2> texCoords = new List<Vector2>()
        // {
        //     new Vector2(0f, 1f),
        //     new Vector2(1f, 1f),
        //     new Vector2(1f, 0f),
        //     new Vector2(0f, 0f),

        //     new Vector2(0f, 1f),
        //     new Vector2(1f, 1f),
        //     new Vector2(1f, 0f),
        //     new Vector2(0f, 0f),

        //     new Vector2(0f, 1f),
        //     new Vector2(1f, 1f),
        //     new Vector2(1f, 0f),
        //     new Vector2(0f, 0f),

        //     new Vector2(0f, 1f),
        //     new Vector2(1f, 1f),
        //     new Vector2(1f, 0f),
        //     new Vector2(0f, 0f),

        //     new Vector2(0f, 1f),
        //     new Vector2(1f, 1f),
        //     new Vector2(1f, 0f),
        //     new Vector2(0f, 0f),

        //     new Vector2(0f, 1f),
        //     new Vector2(1f, 1f),
        //     new Vector2(1f, 0f),
        //     new Vector2(0f, 0f),
        // };

        // List<uint > indices = new List<uint>
        // {
        //     // first face
        //     // top triangle
        //     0, 1, 2,
        //     // bottom triangle
        //     2, 3, 0,

        //     4, 5, 6,
        //     6, 7, 4,

        //     8, 9, 10,
        //     10, 11, 8,

        //     12, 13, 14,
        //     14, 15, 12,

        //     16, 17, 18,
        //     18, 19, 16,

        //     20, 21, 22,
        //     22, 23, 20
        // };

        // // Render Pipeline  variables
        // VAO vao;
        // IBO ibo;
        // ShaderProgram program;
        // Texture texture;

        Chunk chunk;
        ShaderProgram program;
        Camera camera;

        // Transformation variables
        float yRot = 0f;


        int width, height;
        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.width = width;
            this.height = height;
            // center the window on monitor
            CenterWindow(new Vector2i(width, height));
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.width = e.Width;
            this.height = e.Height;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            chunk = new Chunk(new Vector3(0, 0, 0));
            program = new ShaderProgram("Default.vert", "Default.frag");
            GL.Enable(EnableCap.DepthTest);

            // GL.FrontFace(FrontFaceDirection.Cw); // Clockwise orientation!!
            // GL.Enable(EnableCap.CullFace);
            // GL.CullFace(CullFaceMode.Back);

            camera = new Camera(width, height, Vector3.Zero);
            CursorState = CursorState.Grabbed;
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f); // set color
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // apply color

            // Transformation matrices (I HAVE TO FUCKING REVIEW LINEAR ALGEBRA FOR THIS SHIT)
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();

            int modelLocation = GL.GetUniformLocation(program.ID, "model");
            int viewLocation = GL.GetUniformLocation(program.ID, "view");
            int projectionLocation = GL.GetUniformLocation(program.ID, "projection");

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);

            chunk.Render(program);

            // GL.DrawArrays(PrimitiveType.Triangles, 0, 3); // draw the triangle 

            Context.SwapBuffers(); // there are 2 windows - 
            // the one being rendered and the one being displayed, this swaps them

            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            MouseState mouse = MouseState;
            KeyboardState input = KeyboardState;

            base.OnUpdateFrame(args);
            camera.Update(input, mouse, args);
        }
    }
}