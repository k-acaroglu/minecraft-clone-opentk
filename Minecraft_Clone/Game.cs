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

namespace Minecraft_Clone
{
    internal class Game : GameWindow
    {

        float[] vertices =
        {
            -0.5f, 0.5f, 0f, // top left vertex - 0
            0.5f, 0.5f, 0f, // top right vertex - 1
            0.5f, -0.5f, 0f, // bottom right vertex - 2
            -0.5f, -0.5f, 0f // bottom left vertex - 3
        };

        uint[] indices =
        {
            // top triangle
            0, 1, 2,
            // bottom triangle
            2, 3, 0
        };

        // Render Pipeline  variables
        int vao; // Vertex Array Object
        int shaderProgram;
        int vbo;
        int ebo; // element buffer object, or ibo = index buffer object


        int width, height;
        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.width = width;
            this.height = height;
            // center the window on monitor
            this.CenterWindow(new Vector2i(width, height));
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

            vao = (GL.GenVertexArray()); // generate vertex array object, which is empty when created
            vbo = GL.GenBuffer(); // generate vertex buffer object

            // bind the vbo, all subsequent buffer operations will affect this buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // bind the vao, all subsequent vertex attribute calls will be stored in this VAO
            GL.BindVertexArray(vao);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0); // specify the layout of the vertex data (3 because we have 3 vertices)
            GL.EnableVertexArrayAttrib(vao, 0); // enable the vertex attribute at slot 0

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // unbind the VBO
            GL.BindVertexArray(0); // unbind the VAO

            // bind the ebo
            ebo = GL.GenBuffer(); // generate the element buffer object
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw); // uint cuz we want the size of data in memory
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            // Create the shader program
            shaderProgram = GL.CreateProgram(); // create empty shader program

            int vertexShader = GL.CreateShader(ShaderType.VertexShader); // create vertex shader
            GL.ShaderSource(vertexShader, LoadShaderSource("Default.vert")); // load shader source code
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader); // create fragment shader
            GL.ShaderSource(fragmentShader, LoadShaderSource("Default.frag")); // load shader source code
            GL.CompileShader(fragmentShader);

            GL.AttachShader(shaderProgram, vertexShader); // attach vertex shader to program
            GL.AttachShader(shaderProgram, fragmentShader); // attach fragment shader to program
            GL.LinkProgram(shaderProgram); // link the shader program

            // delete the shaders 
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            // cleanup
            GL.DeleteVertexArray(vao);
            GL.DeleteBuffer(vbo);
            GL.DeleteBuffer(ebo);
            GL.DeleteProgram(shaderProgram);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f); // set color
            GL.Clear(ClearBufferMask.ColorBufferBit); // apply color

            // draw our triangle
            GL.UseProgram(shaderProgram); // use the shader program
            GL.BindVertexArray(vao); // bind the VAO to our current context
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            // GL.DrawArrays(PrimitiveType.Triangles, 0, 3); // draw the triangle

            Context.SwapBuffers(); // there are 2 windows - 
            // the one being rendered and the one being displayed, this swaps them

            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

        // Shader loading utility function, WHAT THE FUCK DOES THIS DO???
        public static string LoadShaderSource(string filePath)
        {
            string shaderSource = "";

            try
            {
                using (StreamReader reader = new StreamReader("../../../Shaders/" + filePath))
                {
                    shaderSource = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load shader source file: " + e.Message);
            }

            return shaderSource;
        }
    }
}