using UnityEngine;
using System;
using System.Collections.Generic;

namespace CyberCradle
{
	[Serializable]
	public class Contour
	{
		public Vector2[] points;
	}

	[Serializable]
	public class HiddenItemDesc
	{
		public string name;
		public Rect mainRect;
		public float mainLayer;
		public Sprite mainImage;
		public Contour[] contours;
	}
}

