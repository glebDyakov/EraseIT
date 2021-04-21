
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CyberCradle
{
    [Serializable]
    public partial class Sprite
    {

        //static Dictionary<string, ObjectRef> cache = new Dictionary<string, ObjectRef>();
        static Dictionary<string, List<EventHandler>> queue = new Dictionary<string, List<EventHandler>>();
        static Texture2D emptyTexture = null;
        private string textureName = "";

        public bool foldout = false;

        public string name = null;
        public string collection = null;

        private Material material = null;
        private static int[] spriteIndices = new int[6] 
		{
			0, 2, 1, 2, 3, 1
		};
        private static Vector2[] baseUV = new Vector2[4] 
		{
			new Vector2 (0.0f, 1.0f),
			new Vector2 (1.0f, 1.0f),
			new Vector2 (1.0f, 0.0f),
			new Vector2 (0.0f, 0.0f)
		};

        public float Width
        {
            get
            {
                SpriteDescription spriteDescription =
                    SpriteCollection.GetDescriptionForSprite(this);
                return spriteDescription.sourceRect.width;
            }
        }

        public float Height
        {
            get
            {
                SpriteDescription spriteDescription =
                    SpriteCollection.GetDescriptionForSprite(this);
                return spriteDescription.sourceRect.height;
            }
        }

        protected Vector2[] GenerateUV(Mesh mesh)
        {
            Vector2[] uvs = mesh.uv.Length > 0 ? mesh.uv : new Vector2[4];

            uvs[0] = new Vector2(0.0f, 0.0f);
            uvs[1] = new Vector2(1.0f, 0.0f);
            uvs[2] = new Vector2(0.0f, 1.0f);
            uvs[3] = new Vector2(1.0f, 1.0f);

            return uvs;
        }

        protected int[] GenerateIndices(Mesh mesh)
        {
            return spriteIndices;
        }

        protected Color[] GenerateColors(Mesh mesh, Color color)
        {
            Color[] colors = mesh.colors.Length > 0 ? mesh.colors : new Color[4];

            for (int i = 0; i < colors.Length; ++i)
            {
                colors[i] = color;
            }

            return colors;
        }

        protected Vector3[] GenerateVertices(Mesh mesh, Vector2 anchor)
        {
            SpriteDescription spriteDescription = SpriteCollection.GetDescriptionForSprite(this);

            float width = spriteDescription.sourceRect.width;
            float height = spriteDescription.sourceRect.height;

            Vector3[] vertices = mesh.vertices.Length > 0 ? mesh.vertices : new Vector3[4];

            float left = -anchor.x * width;
            float right = (1.0f - anchor.x) * width;
            float bottom = -anchor.y * height;
            float top = (1.0f - anchor.y) * height;

            vertices[0] = new Vector3(left, bottom, 0);
            vertices[1] = new Vector3(right, bottom, 0);
            vertices[2] = new Vector3(left, top, 0);
            vertices[3] = new Vector3(right, top, 0);

            return vertices;
        }

        public Mesh GenerateMesh(Color color, Vector2 anchor)
        {
            Mesh mesh = new Mesh();

            Vector3[] vertices = GenerateVertices(mesh, anchor);
            Color[] colors = GenerateColors(mesh, color);
            Vector2[] uvs = GenerateUV(mesh);
            int[] indices = GenerateIndices(mesh);

            mesh.vertices = vertices;
            mesh.triangles = indices;
            mesh.colors = colors;
            mesh.uv = uvs;

            return mesh;
        }

        #region LOAD TEXTURE

        public string TextureName
        {
            get
            {
                return textureName;
            }
        }

        
            

           

        #endregion LOAD TEXTURE

        

           

          



        //         public Texture2D GenerateTexture_old()
        //         {
        //             SpriteDescription spriteDescription = SpriteCollection.GetDescriptionForSprite(this);
        // 
        // 
        // 
        // #if UNITY_EDITOR
        //             if (spriteDescription != null)
        //             {
        //                 texture = (Texture2D)AssetDatabase.LoadAssetAtPath(
        //                                     spriteDescription.sourceTexture, typeof(Texture2D));
        //             }
        // #elif UNITY_STANDALONE || UNITY_IPHONE
        // 			if (spriteDescription != null) {
        // 				texture = TextureUtils.LoadTexture (spriteDescription.sourceTexture, null);
        // 
        // 				if( texture == null )
        // 					UnityEngine.Debug.Log("*********************** extureUtils.LoadTexture " + spriteDescription.sourceTexture );
        // 			}
        // #endif
        //             if (texture != null)
        //             {
        //                 texture.name = Path.GetFileNameWithoutExtension(
        //                                         spriteDescription.sourceTexture);
        //             }
        // 
        //             return texture;
        //         }

        //         public Material GenerateMaterial_old(string shaderName)
        //         {
        //             material = new Material(Shader.Find(shaderName));
        // 
        //             material.mainTexture = GenerateTexture();
        // 
        //             if (material.mainTexture != null)
        //             {
        //                 material.name = material.mainTexture.name;
        //             }
        // 
        //             return material;
        //         }

        //         public void Destroy_old()
        //         {
        //             if (material != null)
        //             {
        // #if UNITY_EDITOR
        // 
        //                 Resources.UnloadAsset(material.mainTexture);
        //                 Material.DestroyImmediate(material);
        // 
        // #elif UNITY_STANDALONE || UNITY_IPHONE
        // 			Texture.DestroyImmediate (material.mainTexture);
        // 			Material.DestroyImmediate (material);
        // #endif
        //             }
        //         }


            

        public void UpdateShader(string shader)
        {
            material.shader = Shader.Find(shader);
        }

        public void UpdateAnchor(Mesh mesh, Vector2 anchor)
        {
            Vector3[] vertices = GenerateVertices(mesh, anchor);

            mesh.vertices = vertices;
            mesh.RecalculateBounds();
        }

        public void UpdateColor(Mesh mesh, Color color)
        {
            Color[] colors = GenerateColors(mesh, color);

            mesh.colors = colors;
        }


        


        public void UpdateInEditor( AssetBundle bundle )
        {
            Texture2D tt = bundle.LoadAsset<Texture2D>(textureName);

            material.mainTexture = tt;

            if ( material.mainTexture != null )
            {
                material.name = material.mainTexture.name;
            }

        }
    }
}
