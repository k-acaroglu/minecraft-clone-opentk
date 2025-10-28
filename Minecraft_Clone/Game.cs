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

        List<Vector3> vertices = new List<Vector3>()
        {
            // front face
            new Vector3(-0.5f, 0.5f, 0.5f), // topleft vert
            new Vector3(0.5f, 0.5f, 0.5f), // topright vert
            new Vector3(0.5f, -0.5f, 0.5f), // bottomright vert
            new Vector3(-0.5f, -0.5f, 0.5f), // bottomleft vert
            // right face
            new Vector3(0.5f, 0.5f, 0.5f), // topleft vert
            new Vector3(0.5f, 0.5f, -0.5f), // topright vert
            new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
            new Vector3(0.5f, -0.5f, 0.5f), // bottomleft vert
            // back face
            new Vector3(0.5f, 0.5f, -0.5f), // topleft vert
            new Vector3(-0.5f, 0.5f, -0.5f), // topright vert
            new Vector3(-0.5f, -0.5f, -0.5f), // bottomright vert
            new Vector3(0.5f, -0.5f, -0.5f), // bottomleft vert
            // left face
            new Vector3(-0.5f, 0.5f, -0.5f), // topleft vert
            new Vector3(-0.5f, 0.5f, 0.5f), // topright vert
            new Vector3(-0.5f, -0.5f, 0.5f), // bottomright vert
            new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
            // top face
            new Vector3(-0.5f, 0.5f, -0.5f), // topleft vert
            new Vector3(0.5f, 0.5f, -0.5f), // topright vert
            new Vector3(0.5f, 0.5f, 0.5f), // bottomright vert
            new Vector3(-0.5f, 0.5f, 0.5f), // bottomleft vert
            // bottom face
            new Vector3(-0.5f, -0.5f, 0.5f), // topleft vert
            new Vector3(0.5f, -0.5f, 0.5f), // topright vert
            new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
            new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
        };

        // goes from 0,0 to 1,1 because flipped
        List<Vector2> texCoords = new List<Vector2>()
        {
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
        };

        uint[] indices =
        {
            // first face
            // top triangle
            0, 1, 2,
            // bottom triangle
            2, 3, 0,

            4, 5, 6,
            6, 7, 4,

            8, 9, 10,
            10, 11, 8,

            12, 13, 14,
            14, 15, 12,

            16, 17, 18,
            18, 19, 16,

            20, 21, 22,
            22, 23, 20
        };

        // Render Pipeline  variables
        int vao; // Vertex Array Object
        int shaderProgram;
        int vbo;
        int textureVBO;
        int ebo; // element buffer object, or ibo = index buffer object
        int textureID;

        // Transformation variables
        float yRot = 0f;


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
            GL.BindVertexArray(vao);

            // ------- VERTICES VBO -----------

            vbo = GL.GenBuffer(); // generate vertex buffer object

            // bind the vbo, all subsequent buffer operations will affect this buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector3.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);

            // Put the vertex VBO in slot 0 of our VAO
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0); // specify the layout of the vertex data (3 because we have 3 vertices)
            GL.EnableVertexArrayAttrib(vao, 0); // enable the vertex attribute at slot 0
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // --------- TEXTURE VBO ------------

            textureVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, textureVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, texCoords.Count * Vector2.SizeInBytes, texCoords.ToArray(), BufferUsageHint.StaticDraw);

            // Put the texture VBO in slot 1 of our VAO
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexArrayAttrib(vao, 1);

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

            // ------------- TEXTURES -------------
            textureID = GL.GenTexture();

            // activate the texutre in the unit
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, textureID);

            // Texture parameters is something detailed, you can research on that on your own later
            // It'll take the nearest texture and display it, instead of blurring it
            // Texture paramaters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            
            // Load image
            // STB uses inverted coordinates
            StbImage.stbi_set_flip_vertically_on_load(1); // now won't have to flip coordinates every time
            ImageResult dirtTexture = ImageResult.FromStream(File.OpenRead("Minecraft_Clone/Textures/dirt_texture.png"), ColorComponents.RedGreenBlueAlpha);  
            
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, dirtTexture.Width, dirtTexture.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, dirtTexture.Data);
            // unbind the texture
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            // cleanup
            GL.DeleteVertexArray(vao);
            GL.DeleteBuffer(vbo);
            GL.DeleteBuffer(ebo);
            GL.DeleteTexture(textureID);
            GL.DeleteProgram(shaderProgram);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f); // set color
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // apply color

            // draw our triangle
            GL.UseProgram(shaderProgram); // use the shader program
            GL.BindVertexArray(vao); // bind the VAO to our current context

            GL.BindTexture(TextureTarget.Texture2D, textureID);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);

            // Transformation matrices (I HAVE TO FUCKING REVIEW LINEAR ALGEBRA FOR THIS SHIT)
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = Matrix4.Identity;

            // I definitely gotta learn the theory for this...
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60.0f), width / height, 0.1f, 100.0f);

            model = Matrix4.CreateRotationY(yRot);
            yRot += 0.0005f;

            Matrix4 translation = Matrix4.CreateTranslation(0f, 0f, -3f);
            model *= translation;

            int modelLocation = GL.GetUniformLocation(shaderProgram, "model");
            int viewLocation = GL.GetUniformLocation(shaderProgram, "view");
            int projectionLocation = GL.GetUniformLocation(shaderProgram, "projection");

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);

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
                using (StreamReader reader = new StreamReader("Minecraft_Clone/Shaders/" + filePath))
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