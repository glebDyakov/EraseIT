using UnityEngine;
using System.Collections;
using System.IO;
using CyberCradle;

public class FileUtils
{
    static public string ReadAllText( string path )
    {
        using( FileStream fs = new FileStream(path , FileMode.Open) )
        {
            using( StreamReader sr = new StreamReader(fs) )
            {
                return sr.ReadToEnd( );
            }
        }
    }

    public static string GetExternalEditorPath( )
    {
        return Application.dataPath + "/Editor/external/";
    }

    public static string GetDataPath( )
    {
        switch( Application.platform )
        {
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.WindowsEditor:
                return Application.dataPath.Replace("Assets" , "app_data");
        }
        Debug.LogError("========================================" + Application.persistentDataPath);
        return Application.persistentDataPath;
    }


    /*
    public static string GetBundlePathForLocation( string bundleName , string resourceName )
    {

        string bundlePath ="";
        bool isExternal = TextureManager.IsExternal(resourceName);
        if( !Application.isPlaying )
        {

#if UNITY_EDITOR

            if( isExternal )
            {
                bundlePath = FileUtils.GetLocationDataPath( ) + resourceName + "/" + bundleName + ".u3d";
            }
            else
            {
                string platform = RuntimePlatform.IPhonePlayer.ToString( );//TODO cheat
                bundlePath = Application.streamingAssetsPath + "/InternalLocations/" + platform + "/1024x768/" + resourceName + "/" + bundleName + ".u3d";

            }


            if( bundlePath.Contains("Assets") )
                bundlePath = bundlePath.Substring(bundlePath.IndexOf("Assets"));

            return bundlePath;

#endif
        }



        if( isExternal )
        {
            bundlePath = FileUtils.GetLocationDataPath( ) + resourceName + "/" + bundleName + ".u3d";
        }
        else
        {
            string platform = RuntimePlatform.IPhonePlayer.ToString( );//TODO cheat
            bundlePath = FileUtils.GetStreamingAssetPath( ) + "/InternalLocations/" + platform + "/1024x768/" + resourceName + "/" + bundleName + ".u3d";
        }


        if( !bundlePath.Contains("file://") )
        {
            bundlePath = "file://" + bundlePath;
        }


        return bundlePath;


    }
    */

    public static void RemoveFiles( string directory )
    {

        DirectoryInfo di = new DirectoryInfo(directory);
        FileInfo[] files = di.GetFiles( );


        foreach( var v in files )
        {

            if( ( v.Attributes & FileAttributes.System ) == 0 )
            {
                File.Delete(v.FullName);
            }
        }

    }

    public static string GetLocationDataPath( )
    {
        return GetDataPath( ) + "/resources/locations/";
    }

    public static string GetExternalBundlePath( )
    {
        return Application.dataPath.Replace("Assets" , "") + "/external_bundles/";
    }

    public static string GetPingvigamesPath()
    {
        return Application.dataPath + "/PingviGames/";
    }

    public static string GetHogBundlesPath( )
    {
        return "file://" + GetDataPath( ) + "/bundles/";
    }

    public static string GetUnpackPath( )
    {
        return GetDataPath( ) + "/unpack/";
    }


    public static string GetStreamingAssetPath( )
    {

#if UNITY_STANDALONE
        return "file://" + Application.streamingAssetsPath;
#endif

#if UNITY_IPHONE && !UNITY_EDITOR
		string path = Application.dataPath + "/Raw";
		if( !path.Contains("file://"))
		{
			path = "file://" + path;
		}
		return path;
#endif

#if UNITY_EDITOR && !(UNITY_ANDROID && UNITY_IPHONE)
        return "file://" + Application.streamingAssetsPath;
#else
        return Application.streamingAssetsPath;
#endif
    }





}
