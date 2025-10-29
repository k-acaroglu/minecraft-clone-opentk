using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Minecraft_Clone.World
{
    internal static class TextureData
    {
        public static Dictionary<BlockType, Dictionary<Faces, Vector2>> blockTypeUVCoord = new Dictionary<BlockType, Dictionary<Faces, Vector2>>()
        {
            {BlockType.DIRT, new Dictionary<Faces, Vector2>()
                {
                    {Faces.FRONT, new Vector2(2, 15) },
                    {Faces.LEFT, new Vector2(2, 15) },
                    {Faces.RIGHT, new Vector2(2, 15) },
                    {Faces.BACK, new Vector2(2, 15) },
                    {Faces.TOP, new Vector2(2, 15) },
                    {Faces.BOTTOM, new Vector2(2, 15) },
                }
            },
            {BlockType.GRASS, new Dictionary<Faces, Vector2>()
                {
                    {Faces.FRONT, new Vector2(3, 15) },
                    {Faces.LEFT, new Vector2(3, 15) },
                    {Faces.RIGHT, new Vector2(3, 15) },
                    {Faces.BACK, new Vector2(3, 15) },
                    {Faces.TOP, new Vector2(7, 13) },
                    {Faces.BOTTOM, new Vector2(2, 15) },
                }
            }
        };

        // this stores the texture data for each of our block types in a dictionary!!!
        public static readonly Dictionary<BlockType, Dictionary<Faces, List<Vector2>>> blockTypeUVs = new Dictionary<BlockType, Dictionary<Faces, List<Vector2>>>
        {
            // out atlas is made of 16 x 16 blocks
            
            // {BlockType.DIRT, new Dictionary<Faces, List<Vector2>>()
            // {
            //     {Faces.FRONT, new List<Vector2>() // THIS IS DIRT TEXTURE
            //         {
            //         new Vector2(2f/16f, 15f/16f),
            //         new Vector2(3f/16f, 15f/16f),
            //         new Vector2(3f/16f, 1f),
            //         new Vector2(2f/16f, 1f),
            //         }
            //     },
            //     {Faces.BACK, new List<Vector2>()
            //         {
            //         new Vector2(2f/16f, 15f/16f),
            //         new Vector2(3f/16f, 15f/16f),
            //         new Vector2(3f/16f, 1f),
            //         new Vector2(2f/16f, 1f),
            //         }
            //     },
            //     {Faces.LEFT, new List<Vector2>()
            //         {
            //         new Vector2(2f/16f, 15f/16f),
            //         new Vector2(3f/16f, 15f/16f),
            //         new Vector2(3f/16f, 1f),
            //         new Vector2(2f/16f, 1f),
            //         }
            //     },
            //     {Faces.RIGHT, new List<Vector2>()
            //         {
            //         new Vector2(2f/16f, 15f/16f),
            //         new Vector2(3f/16f, 15f/16f),
            //         new Vector2(3f/16f, 1f),
            //         new Vector2(2f/16f, 1f),
            //         }
            //     },
            //     {Faces.TOP, new List<Vector2>()
            //         {
            //         new Vector2(2f/16f, 15f/16f),
            //         new Vector2(3f/16f, 15f/16f),
            //         new Vector2(3f/16f, 1f),
            //         new Vector2(2f/16f, 1f),
            //         }
            //     },
            //     {Faces.BOTTOM, new List<Vector2>()
            //         {
            //         new Vector2(2f/16f, 15f/16f),
            //         new Vector2(3f/16f, 15f/16f),
            //         new Vector2(3f/16f, 1f),
            //         new Vector2(2f/16f, 1f),
            //         }
            //     },
            // }
            // },

            // {BlockType.GRASS, new Dictionary<Faces, List<Vector2>>()
            // {
            //     {Faces.FRONT, new List<Vector2>() // THIS IS GRASS TEXTURE
            //         {
            //         new Vector2(4f/16f, 1f),
            //         new Vector2(3f/16f, 1f),
            //         new Vector2(3f/16f, 15f/16f),
            //         new Vector2(4f/16f, 15f/16f),
            //         }
            //     },
            //     {Faces.BACK, new List<Vector2>()
            //         {
            //         new Vector2(4f/16f, 1f),
            //         new Vector2(3f/16f, 1f),
            //         new Vector2(3f/16f, 15f/16f),
            //         new Vector2(4f/16f, 15f/16f),
            //         }
            //     },
            //     {Faces.LEFT, new List<Vector2>()
            //         {
            //         new Vector2(4f/16f, 1f),
            //         new Vector2(3f/16f, 1f),
            //         new Vector2(3f/16f, 15f/16f),
            //         new Vector2(4f/16f, 15f/16f),
            //         }
            //     },
            //     {Faces.RIGHT, new List<Vector2>()
            //         {
            //         new Vector2(4f/16f, 1f),
            //         new Vector2(3f/16f, 1f),
            //         new Vector2(3f/16f, 15f/16f),
            //         new Vector2(4f/16f, 15f/16f),
            //         }
            //     },
            //     {Faces.TOP, new List<Vector2>()
            //         {
            //         new Vector2(8f/16f, 14f/16f),
            //         new Vector2(7f/16f, 14f/16f),
            //         new Vector2(7f/16f, 13f/16f), // bottom left
            //         new Vector2(8f/16f, 13f/16f), // bottom right
            //         }
            //     },
            //     {Faces.BOTTOM, new List<Vector2>()
            //         {
            //         new Vector2(2f/16f, 15f/16f),
            //         new Vector2(3f/16f, 15f/16f),
            //         new Vector2(3f/16f, 1f),
            //         new Vector2(2f/16f, 1f),
            //         }
            //     },
            // }
            // }
        };
    }
}