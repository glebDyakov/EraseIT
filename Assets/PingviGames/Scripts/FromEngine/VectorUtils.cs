using System;
using UnityEngine;

public class VectorUtils
{
    //equals  Vector3.one
	//public static Vector3 identity = new Vector3 (1.0f, 1.0f, 1.0f);

	public static Vector3 Vector3 (Vector3 vector, float layer)
	{
		return new Vector3(vector.x, vector.y, layer);
	}
	
	public static Vector3 Vector3 (Transform transform, float layer)
	{
		return new Vector3(transform.position.x, transform.position.y, layer);
	}

    public static Vector3 Vector3Local (Transform transform, float layer)
    {
        return new Vector3(transform.localPosition.x, transform.localPosition.y, layer);
    }
}
