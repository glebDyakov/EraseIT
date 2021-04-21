using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace CyberCradle
{
    public class TextureAlphaCannelTest 
    {

        [MenuItem( "CheckTextures/Check" , false , 1 )]
        static void DoBuildExternal( )
        {
            TextureAlphaCannelTest.ProcessTextureFormat( );
        }
        

        internal static bool  IsExistAlpha(  Texture2D texture )
        {
            if( texture == null )
            {
                return true;
            }

           Color[] pixels = texture.GetPixels( );
            for( int i = 0; i < pixels.Length;++i )
            {
                //transparent - black
                if ( pixels[ i ].a < 0.8f ) return true; 
            }

            return false;
        }

       internal  static void ProcessTextureFormat()
        {

            DirectoryInfo di = new DirectoryInfo( Application.dataPath + "/StreamingAssets/Images/Levels/" );

            List<FileInfo> arr  = new List<FileInfo>();
            Tools.GetFiles( arr, di );
            for( int i = 0; i< arr.Count;++i )
            {
                string path = arr[i].FullName.Substring(arr[i].FullName.IndexOf("Assets") );
                Texture2D texture = ( Texture2D )AssetDatabase.LoadAssetAtPath( path , typeof( Texture2D ) );
                if ( !IsExistAlpha( texture ) )
                {
                    string pathTxt = AssetDatabase.GetAssetPath( texture );
                    TextureImporter textureImporter = AssetImporter.GetAtPath( pathTxt ) as TextureImporter;
                    textureImporter.textureFormat = TextureImporterFormat.RGB24;
                    AssetDatabase.ImportAsset( path );
                }
            }

            AssetDatabase.SaveAssets( );
           
        }

    }
}