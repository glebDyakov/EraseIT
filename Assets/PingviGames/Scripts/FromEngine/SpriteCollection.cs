
using UnityEngine;
using System;
using System.Collections.Generic;

namespace CyberCradle
{
	

    
	public class SpriteCollection : ScriptableObject
	{
        public string bundleName;
		public List<SpriteDescription> sprites;

        Dictionary<string,SpriteDescription> map = new Dictionary<string , SpriteDescription>( );

        bool isInited = false;

        void Init()
        {
            map.Clear( );
            foreach ( var v in sprites )
            {
                if ( !map.ContainsKey( v.name ) )
                {
                    map.Add( v.name , v );
                }
                else
                {
                    Debug.LogError( "ERROR : ============================= !map.ContainsKey( v.name )" + v.name );
                }
            }
        }

		public string[] GetSpriteNames ()
		{
			List<string> names = new List<string> ();

			for (int i = 0; i < sprites.Count; ++i) {
				names.Add (sprites [i].name);
			}

			return names.ToArray ();
		}

		public SpriteDescription GetDescriptionByName (string name)
		{
            if ( !isInited )
            {
                Init( );
            }

            if ( map.ContainsKey( name ) )
                 return map[ name ];
            else
            {
                Debug.LogError( "----------------------------" + name );
            }


			for (int i = 0; i < sprites.Count; ++i) {
				if (sprites[i].name == name) {
					return sprites[i];
				}
			}

            Debug.LogError( "----------------------------" + name );

			return null;
		}

        public static SpriteCollection GetSpriteCollection(CyberCradle.Sprite sprite)
        {
            string resourcePath = sprite.collection;
            resourcePath = resourcePath.Replace("Assets/Resources/", "");
            resourcePath = resourcePath.Replace(".asset", "");

            SpriteCollection spriteCollection = Resources.Load<SpriteCollection>(
                resourcePath);

            return spriteCollection;
        }

		public static SpriteDescription GetDescriptionForSprite (Sprite sprite)
		{
            SpriteCollection spriteCollection = GetSpriteCollection(sprite);
			SpriteDescription spriteDescription = spriteCollection.GetDescriptionByName(
														sprite.name);

			return spriteDescription;
		}

 		public int GetIndexByName (string name)
 		{
 			for (int i = 0; i < sprites.Count; ++i) {
 				if (sprites[i].name == name) {
 					return i;
 				}
 			}
 			
 			return -1;
 		}
	}
    
}
