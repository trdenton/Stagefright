using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Singleton.
/// 
/// This class is only meant to have one instance. A static property is provided for fast and easy access from anywhere.
/// </summary>

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	static bool isShuttingDown = false;

	private static T mInstance;
	public static T Instance
	{
		get
		{
			/* NOTE: for debugging when a Singleton is being referenced, can uncomment this and specify the proper class type, then add a breakpoint to "i++"
			if (typeof(T).Name.Equals("MatchManager"))
			{
				int i = 0;

				i++;
			}
			*/

			EnsureExists();
			
			return mInstance;
		}
	}

	public virtual void Awake()
	{
		isShuttingDown = false;
	}

	public virtual void OnApplicationQuit()
	{
		isShuttingDown = true;
	}

	public static T GetPrivateInstance()
	{
		return mInstance;
	}

	public static bool HasPrivateInstance()
	{
		return (mInstance != null);
	}
	
	public static void Unload(bool isImmediate = true)
	{
		if (mInstance != null)
		{
			if (isImmediate)
			{
				GameObject.DestroyImmediate (mInstance.gameObject);
			}
			else
			{
				Destroy (mInstance.gameObject);
			}
		}
		mInstance = null;
	}

	public static void SetInstance(T instance)
	{
		mInstance = instance;
	}

	public static void EnsureExists(GameObject parent = null)
	{
		if (Application.isPlaying
		    && isShuttingDown)
		{

			return;
		}

		if (mInstance == null)
		{
			// Search Scene and attempt to locate an existing instance.
			var objs = (T[])GameObject.FindObjectsOfType(typeof(T));
			if (objs.Length > 1)
			{
				Debug.Log ("Two objects of same type");
				Debug.Break();
			}
			else if (objs.Length == 1)
			{
				mInstance = objs[0];
			}

			if (mInstance == null)
			{
				// Create a new GameObject.
				GameObject newObj = new GameObject(typeof(T).ToString());
				
				// Add our MonoBehaviour as a Component and assign it to our instance.
				mInstance = newObj.AddComponent<T>();

				if (parent != null)
				{
					mInstance.transform.SetParent(parent.transform);
				}
			}
		}
	}
}
