using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class MonoExtensions
{
    public static Vector3 RandomShifted(this Vector3 v3, float offset = 50f)
	{
		return new Vector3(v3.x + Helpers.RandomFloatFromRangeInclusive(-offset, +offset), v3.y + Helpers.RandomFloatFromRangeInclusive(-offset, +offset), v3.z);
	}


	public static void TryStopCoroutine(this MonoBehaviour mono, IEnumerator coroutine)
	{
		if (coroutine != null)
		{
			mono.StopCoroutine(coroutine);
		}
	}

	// Extension.
	public static void CallAction(this MonoBehaviour mono, Action action)
	{
		callAction (mono, action);
	}
	
	static void callAction(MonoBehaviour mono, Action method)
	{
		if (mono.enabled
		    && method != null)
		{
			method();
		}
	}

	// Extension
	public static void CallActionDelayed(this MonoBehaviour mono, Action action)
	{
		mono.StartCoroutine (actionDelayed (mono, action, 0));
	}
	
	// Extension
	public static void CallActionDelayed(this MonoBehaviour mono, Action action, float delay)
	{
		mono.StartCoroutine (actionDelayed (mono, action, delay));
	}
	
	static IEnumerator actionDelayed(MonoBehaviour mono, Action method, float delay)
	{
		// Wait.
		if (delay == 0)
		{
			yield return null;
		}
		else
		{
			yield return new WaitForSeconds (delay);
		}
		
		// Call.
		callAction (mono, method);
	}
	
	public static void StopDelayedActions(this MonoBehaviour mono)
	{
		mono.StopAllCoroutines();
	}
	
	// Extension
	public static void CallActionRepeat(this MonoBehaviour mono, Action method, float delay)
	{
		mono.StartCoroutine (actionRepeat (mono, method, delay));
	}
	
	static IEnumerator actionRepeat(MonoBehaviour mono, Action method, float delay)
	{
		// Call.
		callAction(mono, method);
		
		// Wait.
		yield return new WaitForSeconds (delay);
		
		// Re-call.
		mono.StartCoroutine (actionRepeat (mono, method, delay));
	}
	
	// Extension
	public static Renderer[] GetRenderersInChildren(this MonoBehaviour mono)
	{
		return mono.GetComponentsInChildren<Renderer> (true) as Renderer[];
	}
	
	// Extension
	public static List<Material> GetMaterialsInChildren(this MonoBehaviour mono)
	{
		List<Material> mAllMaterials = new List<Material>();
		
		// Iterate renderers.
		foreach(var render in mono.GetRenderersInChildren ())
		{
			// Add materials to list.
			foreach(var mat in render.materials)
			{
				mAllMaterials.Add (mat);
			}
		}
		
		return mAllMaterials;
	}

	public static void SetLayerRecursively(this GameObject obj, int layer) {
		obj.layer = layer;
		
		foreach (Transform child in obj.transform) {
			child.gameObject.SetLayerRecursively(layer);
		}
	}


	public static void SetLayerRecursively(this GameObject obj, string layer)
	{
		int layerInt = LayerMask.NameToLayer(layer);
		obj.SetLayerRecursively(layerInt);
	}

	public static void DestroySelf(this GameObject obj)
	{
		GameObject.Destroy(obj);
	}

	public static GameObject Instantiate(GameObject prefab) 
	{
		return (GameObject.Instantiate(prefab) as GameObject);
	}

	public static void SetAlpha (this Material material, float value) 
	{ 
		Color color = material.color; color.a = value; material.color = color; 
	}
}