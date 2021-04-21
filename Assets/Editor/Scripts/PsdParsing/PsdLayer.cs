using System;
using System.Collections.Generic;
using System.Xml.Serialization;
#if UNITY_EDITOR
using UnityEngine;
#endif

#if !UNITY_EDITOR
public class HideInInspectorAttribute : Attribute
{
}

public class Rect
{
    public float x;
    public float y;
    public float width;
    public float height;
}

public class Color32
{ 
}

public class Color
{
    public float r = 1.0f;
    public float g = 1.0f;
    public float b = 1.0f;
    public float a = 1.0f;

    public Color()
    { 
    }

    public Color(float r, float g, float b, float a)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }
}

public class NGUIText
{
	public enum Alignment
	{
		Automatic,
		Left,
		Center,
		Right,
		Justified
	}
}

public enum FontStyle
{
	Normal,
	Bold,
	Italic,
	BoldAndItalic
}
#endif

[Serializable]
public class Font
{
    public string name;
    public float size;
	//public NGUIText.Alignment alignment;
	public FontStyle style;
}

public enum LayerType
{
	None,
	Document,
	Image,
    Text,
	Group
};

[Serializable]
public class PsdLayer
{
	public const string Separator = "/";
	public string name = null;
	public string fullname = null;
    public string text = null;
    public Font font = null;
	public bool visible = true;
	[HideInInspector]
	public LayerType type = LayerType.None;
    public Rect rect = new Rect ();
	public int layer;
    public Color color = new Color ();
	[HideInInspector]
	public Color32[] data = null;
	public List<PsdLayer> children = new List<PsdLayer> ();

    [XmlIgnore]
    public Color Color
    {
        get { 
            return color; 
        }
        set { 
            color = value; 
        }
    }
	[XmlIgnore]
	public int Opacity {
		get {
            return (int)(255.0f * color.a);
		}
		set {
            color.a = value / 255.0f;
		}
	}
    [XmlIgnore]
	public List<PsdLayer> Children {
		get {
			return this.children;
		}
		set {
			children = value;
		}
	}
    [XmlIgnore]
	public Rect Rect {
		get {
			return this.rect;
		}
		set {
			rect = value;
		}
	}
    [XmlIgnore]
	public Color32[] Data {
		get {
			return this.data;
		}
		set {
			data = value;
		}
	}
    [XmlIgnore]
	public string Name {
		get {
			return this.name;
		}
		set {
			name = value;
		}
	}
    [XmlIgnore]
	public string Fullname {
		get {
			return this.fullname;
		}
		set {
			fullname = value;
		}
	}
    [XmlIgnore]
    public string Text
    {
        get
        {
            return this.text;
        }
        set
        {
            text = value;
        }
    }
    [XmlIgnore]
    public Font Font
    {
        get
        {
            return this.font;
        }
        set
        {
            font = value;
        }
    }
    [XmlIgnore]
	public LayerType Type {
		get {
			return this.type;
		}
		set {
			type = value;
		}
	}
    [XmlIgnore]
	public bool Visible {
		get {
			return this.visible;
		}
		set {
			visible = value;
		}
	}
    [XmlIgnore]
	public int Width {
		get {
			return (int)this.rect.width;
		}
		set {
			rect.width = (float)value;
		}
	}
    [XmlIgnore]
	public int Height {
		get {
			return (int)this.rect.height;
		}
		set {
			rect.height = (float)value;
		}
	}
    [XmlIgnore]
	public int X {
		get {
			return (int)this.rect.x;
		}
		set {
			rect.x = (float)value;
		}
	}
    [XmlIgnore]
	public int Y {
		get {
			return (int)this.rect.y;
		}
		set {
			rect.y = (float)value;
		}
	}
    [XmlIgnore]
	public int Layer {
		get {
			return layer;
		}
		set {
			layer = value;
		}
	}
	
	public PsdLayer ()
	{
		Layer = -1;
	}
	
	public PsdLayer (string name, string parentFullname)
	{
		Name = name;
		Fullname = parentFullname + Separator + name;
		X = 0;
		Y = 0;
		Width = 0;
		Height = 0;
		Layer = -1;
	}
	
	public PsdLayer (string name, string parentFullname, int x, int y, int width, int height, int layer)
	{
		Name = name;
		Fullname = parentFullname + Separator + name;
		X = x;
		Y = y;
		Width = width;
		Height = height;
		Layer = layer;
	}
		
	public void AddChild (PsdLayer child)
	{
		children.Add (child);
	}

	public void AddChildToFront (PsdLayer child)
	{
		children.Insert (0, child);
	}
	
	public int GetChildCount ()
	{
		return children.Count;
	}
	
	public PsdLayer GetChild (int childIndex)
	{
		return children [childIndex];
	}
}

