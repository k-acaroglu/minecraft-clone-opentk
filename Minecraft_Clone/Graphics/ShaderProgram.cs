using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
namespace Minecraft_Clone.Graphics
{
    internal class ShaderProgram
    {
        public int ID;
        public ShaderProgram(string vertexShaderFilepath, string fragmentShaderFilepath)
        {
            // Create the shader program
            ID = GL.CreateProgram(); // create empty shader program

            int vertexShader = GL.CreateShader(ShaderType.VertexShader); // create vertex shader
            GL.ShaderSource(vertexShader, LoadShaderSource(vertexShaderFilepath)); // load shader source code
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader); // create fragment shader
            GL.ShaderSource(fragmentShader, LoadShaderSource(fragmentShaderFilepath)); // load shader source code
            GL.CompileShader(fragmentShader);

            GL.AttachShader(ID, vertexShader); // attach vertex shader to program
            GL.AttachShader(ID, fragmentShader); // attach fragment shader to program
            GL.LinkProgram(ID); // link the shader program

            // delete the shaders 
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public void Bind() { GL.UseProgram(ID); }
        public void Unbind() { GL.UseProgram(0); }
        public void Delete () { GL.DeleteShader(ID); }

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