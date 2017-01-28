using UnityEngine;

public static class TransformExtensions
{
	public static Vector3 point;

	public static void SetPositionX(this Transform t, float newX)
	{
		t.position = new Vector3(newX, t.position.y, t.position.z);
	}
	
	public static void SetPositionY(this Transform t, float newY)
	{
		t.position = new Vector3(t.position.x, newY, t.position.z);
	}
	
	public static void SetPositionZ(this Transform t, float newZ)
	{
		t.position = new Vector3(t.position.x, t.position.y, newZ);
	}

	public static void SetLocalPositionX(this Transform t, float newX)
	{
		t.localPosition = new Vector3(newX, t.localPosition.y, t.localPosition.z);
	}
	
	public static void SetLocalPositionY(this Transform t, float newY)
	{
		t.localPosition = new Vector3(t.localPosition.x, newY, t.localPosition.z);
	}
	
	public static void SetLocalPositionZ(this Transform t, float newZ)
	{
		t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, newZ);
	}
	
	public static float GetPositionX(this Transform t)
	{
		return t.position.x;
	}
	
	public static float GetPositionY(this Transform t)
	{
		return t.position.y;
	}
	
	public static float GetPositionZ(this Transform t)
	{
		return t.position.z;
	}

	public static GameObject InstantiateAsChild(this Transform t, GameObject prefab)
	{
		GameObject go = (MonoBehaviour.Instantiate(prefab, t.position, Quaternion.identity) as GameObject);
		go.transform.parent = t;
		return go;
	}

	public static GameObject InstantiateAtLocation(this Transform t, GameObject prefab)
	{
		GameObject go = (MonoBehaviour.Instantiate(prefab, t.position, Quaternion.identity) as GameObject);
		return go;
	}

	public static bool IsTransformWithinCameraViewport(this Transform t, Camera camera)
	{
		point =	camera.WorldToViewportPoint(t.position);
		
		if(point.x >= 0f && point.x <= 1f && point.y >= 0f && point.y <= 1f && point.z >= 0f)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public static Transform LocateTransform(string name)
	{
		GameObject go = GameObject.Find(name);
		if(go != null)
		{
			return go.transform;
		}
		
		return null;
	}

	public static Vector3 FarVector3()
	{
		return new Vector3(100000f, 100000f, 100000f);
	}

	public static void SetBool(this Material mat, string propertyName, bool value)
	{
		mat.SetInt(propertyName, value ? 1 : 0);
	}


}