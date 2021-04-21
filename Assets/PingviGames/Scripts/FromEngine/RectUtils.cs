using UnityEngine;
using System;

public class RectUtils
{
	public static Rect zero = new Rect(0.0f, 0.0f, 0.0f, 0.0f);

	[Serializable]
	public class RectOffsets
	{
		public RectOffsets(float left, float right, float top, float bottom)
		{
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}
			
		public float left;
		public float top;
		public float right;
		public float bottom;
	}
	
	public static Rect InflateRect (Rect rect, float value)
	{
		return new Rect(rect.xMin + value, 
						rect.yMin + value, 
						rect.width - 2.0f * value,
						rect.height - 2.0f * value);
	}
	
	public static Rect InflateRect (Rect rect, RectOffsets offset)
	{
		return new Rect(rect.xMin + offset.left, 
						rect.yMin + offset.bottom, 
						rect.width - offset.right - offset.left,
						rect.height - offset.top - offset.bottom);
	}
	
	public static bool IsEmpty(Rect rect)
	{
		return rect.width == 0.0f && rect.height == 0.0f;
	}
	
	public static bool Intersect(Rect a, Rect b)
	{
	    float tw = a.width;
	    float th = a.height;
	    float rw = b.width;
	    float rh = b.height;
		
	    if (rw <= 0 || rh <= 0 || tw <= 0 || th <= 0) {
	        return false;
	    }
		
	    float tx = a.x;
	    float ty = a.y;
	    float rx = b.x;
	    float ry = b.y;
	    rw += rx;
	    rh += ry;
	    tw += tx;
	    th += ty;

	    return ((rw < rx || rw > tx) &&
	            (rh < ry || rh > ty) &&
	            (tw < tx || tw > rx) &&
	            (th < ty || th > ry));
	}
	
	public static Rect ToScreenSpace(Bounds bounds, Camera camera)
    {
        var min = camera.WorldToScreenPoint(new Vector3(bounds.min.x, bounds.min.y, 0.0f));
        var max = camera.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.max.y, 0.0f));
 
        return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
    }
	
	public static Rect MoveRect (Rect sourceRect, float x, float y)
	{
		Rect destinationRect = sourceRect;
			
		destinationRect.x += x;
		destinationRect.y += y;
		
		return destinationRect;
	}

	public static Rect TransformByMatrix (Rect sourceRect, Matrix4x4 matrix)
	{
		Vector4 srcMin = new Vector4 (sourceRect.xMin, sourceRect.yMin, 0.0f, 1.0f);
		Vector4 srcMax = new Vector4 (sourceRect.xMax, sourceRect.yMax, 0.0f, 1.0f);

		Vector4 dstMin = matrix * srcMin;
		Vector4 dstMax = matrix * srcMax;

		Rect dstRect = new Rect(dstMin.x, dstMin.y, dstMax.x - dstMin.x, dstMax.y - dstMin.y);
		return dstRect;
	}
}

