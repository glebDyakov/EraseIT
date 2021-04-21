using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class PsdDocument : ScriptableObject
{
    public string path = null;
    public PsdLayer document = null;

    public PsdLayer FindLayerByName(string name)
    {
        return FindLayerByName(document, name);
    }

    public PsdLayer FindLayerByExactName(string name)
    {
        return FindLayerByExactName(document, name);
    }

    public PsdLayer FindLayerByName(PsdLayer parent, string name)
    {
        List<PsdLayer> foundLayers = FindLayersByName(parent, name);
        return foundLayers.Count > 0 ? foundLayers[0] : null;
    }

    public PsdLayer FindLayerByExactName(PsdLayer parent, string name)
    {
        List<PsdLayer> foundLayers = FindLayersByExactName(parent, name);
        return foundLayers.Count > 0 ? foundLayers[0] : null;
    }

    public List<PsdLayer> FindLayersByName(string name)
    {
        return FindLayersByName(name, false);
    }

    public List<PsdLayer> FindLayersByName(string name, bool includeChildren)
    {
        if (includeChildren)
        {
            List<PsdLayer> result = new List<PsdLayer>();
            FindLayersByName(result, document, name);
            return result;

        }
        else
            return FindLayersByName(document, name);
    }

    public List<PsdLayer> FindLayersByExactName(string name)
    {
        return FindLayersByExactName(document, name);
    }


    public void FindLayersByName(List<PsdLayer> foundLayers, PsdLayer parent, string name)
    {
        foreach (PsdLayer layer in parent.children)
        {
            if (StringUtils.CompareWithPattern(name, layer.name))
            {
                Debug.Log(layer.name);
                foundLayers.Add(layer);
            }
            FindLayersByName(foundLayers, layer, name);
        }
    }



    public List<PsdLayer> FindLayersByName(PsdLayer parent, string name)
    {
        List<PsdLayer> foundLayers = new List<PsdLayer>();

        string pattern = "^[a-zA-Z0-9]+$";

        Regex rg = new Regex( pattern );

        string tmpName =name.Replace("??","").Replace("_","");

        if ( !rg.IsMatch( tmpName ) )
        {
            Debug.LogError( "==============================================================================" );
            Debug.LogError( "Error : cyrillic? " + name  );
            Debug.LogError( "==============================================================================" );

            EditorUtility.DisplayDialog( "Error" , "Error : cyrillic? name=" + name , "OK" );

        }

        if ( tmpName.Contains( " " ) )
        {
            Debug.LogError( "==============================================================================" );
            Debug.LogError( "Error : SPACE!!! " + name );
            Debug.LogError( "==============================================================================" );

            EditorUtility.DisplayDialog( "Error" , "Error : SPACE!!!  name=" + name , "OK" );
        }

        if ( parent != null && parent.children != null )
        {
            foreach ( PsdLayer layer in parent.children )
            {
                if ( StringUtils.CompareWithPattern( name , layer.name ) )
                {
                    //Debug.Log(layer.name);
                    foundLayers.Add( layer );
                }
            }
        }
        else
        {
            Debug.LogError( "Error : parent != null && parent.children != null " + name );
        }

        return foundLayers;
    }

    public List<PsdLayer> FindLayersByExactName(PsdLayer parent, string name)
    {
        List<PsdLayer> layers = new List<PsdLayer>();
        List<PsdLayer> foundLayers = new List<PsdLayer>();

        foreach (PsdLayer layer in parent.children)
        {
            if (name == layer.name)
            {
                Debug.Log(layer.name);
                foundLayers.Add(layer);
            }
        }

        return foundLayers;
    }

    public List<PsdLayer> GetLayers()
    {
        List<PsdLayer> layers = new List<PsdLayer>();
        GetLayers(document, layers);

        return layers;
    }

    public void GetLayers(PsdLayer parent, List<PsdLayer> layers)
    {
        foreach (PsdLayer layer in parent.children)
        {
            layers.Add(layer);
            GetLayers(layer, layers);
        }
    }
}

