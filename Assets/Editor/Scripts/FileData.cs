using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using CyberCradle;

class FileData
{
    public bool needbuild = false;
    public FileInfo m_File = null;
    public bool IsShow=true;
    public string fileName = "";

    

    public void OnGUI( )
    {

        GUILayout.BeginHorizontal( );

        if ( IsShow )
        {
            needbuild = EditorGUILayout.Toggle( needbuild , GUILayout.Width( 50 ) );
            EditorGUILayout.LabelField( m_File.Name , GUILayout.Width( 300 ) );
           
        }

        GUILayout.EndHorizontal( );
    }
}
