using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public static class Helpers
{
	private static StringBuilder s_stringBuilder = new StringBuilder(20);
	public static System.Random rng = new System.Random();

	public const int WHILE_LOOP_INSANITY = 10000;
	public static int whileLoopCounter = 0;

	public static Ray vRay;
	public static RaycastHit hit;
	public static Collider RaycastFromMouse(Camera c)
	{
		vRay = c.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(vRay, out hit);

		return hit.collider;
	}

	public static Color AlphaAdjustedColor(Color c, float newAlpha)
	{
		return new Color(c.r, c.g, c.b, newAlpha);
	}





	public static string LowercaseFirst(string s)
	{
		return Char.ToLowerInvariant(s[0]) + s.Substring(1);
	}

	public static Sprite GetSprite(string spriteName)
	{
		return Resources.Load<Sprite>(spriteName);
	}

	public static int mod(int x, int m)
	{
		return (x % m + m) % m;
	}

	public static Vector3 GetGroundMousePosition()
	{
		Plane ground = new Plane(Vector3.up, Vector3.zero);
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		float rayDistance;
		if (ground.Raycast(ray, out rayDistance))
		{
			return ray.GetPoint(rayDistance);
		}
		return new Vector3(0, 0, 0);
	}

	public static int AtLeast(int num, int atLeast)
	{
		return Mathf.Max(num, atLeast);
	}

	public static float AtLeast(float num, float atLeast)
	{
		return Mathf.Max(num, atLeast);
	}

	public static int AtMost(int num, int atMost)
	{
		return Mathf.Min(num, atMost);
	}

	public static float AtMost(float num, float atMost)
	{
		return Mathf.Min(num, atMost);
	}



	public static Vector3 CirclePoint(float percent, float radius, Vector3 center)
	{
		var i = percent;
		// get the angle for this step (in radians, not degrees)
		var angle = i * Mathf.PI * 2;
		// the X &amp; Y position for this angle are calculated using Sin &amp; Cos
		var x = Mathf.Sin(angle) * radius;
		var y = Mathf.Cos(angle) * radius;
		var pos = new Vector3(x, y, 0) + center;

		return pos;
	}

	public static Color Color255(int r = 255, int g = 255, int b = 255, int a = 255)
	{
		return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
	}

	public static GameObject Create(string resourceName)
	{
		return (GameObject.Instantiate(Resources.Load(resourceName))) as GameObject;
	}

	public static GameObject Create(string resourceName, Transform parent)
	{
		GameObject go = Create(resourceName);
		go.transform.parent = parent;
		return go;
	}

	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	public static void ClearAndDestroyList<T>(this IList<T> list) where T : MonoBehaviour
	{
		foreach (T go in list)
		{
			if (go != null && go.gameObject != null)
				go.gameObject.DestroySelf();
		}

		list.Clear();
	}

	public static void ShuffleNoSeed<T>(this IList<T> list)
	{
		if (list == null)
			return;

		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = UnityEngine.Random.Range(0, n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	/// <summary>
	/// Pass in an amount in pennies, and will return the value in dollars with a currency symbol.
	/// Example: pass in 325, will return $3.25
	/// </summary>
	/// <param name="amount"></param>
	/// <returns></returns>
	/// 
	static string output;
	public static string FormatCurrency(int amount)
	{
		output = string.Format("${0}.{1}", (amount / 100), (amount % 100).ToString("00")).Replace("-", "");
		if (amount < 0)
		{
			output = "-" + output;
		}
		return output;
	}

	public static float SignedAngle(Vector3 a, Vector3 b)
	{
		var angle = Vector3.Angle(a, b); // calculate angle
										 // assume the sign of the cross product's Y component:
		return angle * Mathf.Sign(Vector3.Cross(a, b).y);
	}

	public static float SignedAngle(Vector2 a, Vector2 b)
	{
		float ang = Vector2.Angle(a, b);
		Vector3 cross = Vector3.Cross(a, b);

		if (cross.z > 0)
			ang = 360 - ang;

		return ang;
	}


	public static bool Between(this float f, float min, float max)
	{
		return f >= min && f <= max;
	}

	/// <summary>
	/// Returns a random int in the range [minValue, maxValue) (ie: exclusive on the upper bound)
	/// </summary>
	public static int RandomIntFromRange(int minValue, int maxValue)
	{
		if (maxValue <= minValue)
		{
			return minValue;
		}



		return rng.Next(minValue, maxValue);
	}

	public static int RandomIntFromRangeInclusive(int minValue, int maxValue)
	{
		return RandomIntFromRange(minValue, maxValue + 1);
	}

	/// <summary>
	/// Returns a random float in the range [minValue, maxValue].
	/// </summary>
	/// 
	///
	static float next;
	public static float RandomFloatFromRangeInclusive(float minValue, float maxValue)
	{
		next = (float)rng.NextDouble();
		return (next * (maxValue - minValue)) + minValue;
	}

	/// <summary>
	/// Returns true or false based on the chance value (default 50%). For example if you wanted a player to have a 30% chance
	/// of getting a bonus, call ChanceRoll(30) - true means the chance passed, false means it failed.
	/// </summary>
	public static bool ChanceRoll(int chance = 50)
	{
		if (chance <= 0)
		{
			return false;
		}
		else if (chance >= 100)
		{
			return true;
		}
		else
		{
			if (rng.Next(0, 100) >= chance)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}

	/// <summary>
	/// Same as ChanceRoll, except takes a number from [0f, 1f] instead of [0, 100]
	/// </summary>
	public static bool ChanceRoll(float chance = 0.5f)
	{
		if (chance <= 0f)
		{
			return false;
		}
		else if (chance >= 1f)
		{
			return true;
		}
		else
		{
			if (RandomFloatFromRangeInclusive(0f, 1f) >= chance)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}

	public static T CreateInstance<T>(string prefabName) where T : MonoBehaviour
	{
		// get the prefab
		GameObject prefab = Resources.Load(prefabName) as GameObject;

		// create a new instance
		GameObject instance = MonoBehaviour.Instantiate(prefab) as GameObject;

		// get the component
		T component = instance.GetComponent<T>();

		return component;
	}

	public static T CreateInstance<T>(string prefabName, Transform parent, bool zeroLocalPosition = false) where T : MonoBehaviour
	{
		// get the prefab
		GameObject prefab = Resources.Load(prefabName) as GameObject;

		// create a new instance
		GameObject instance = MonoBehaviour.Instantiate(prefab) as GameObject;
		instance.transform.SetParent(parent);

		// get the component
		T component = instance.GetComponent<T>();

		if (zeroLocalPosition)
			instance.transform.localPosition = Vector3.zero;

		return component;
	}

	public static T CreateInstance<T>(GameObject prefab) where T : MonoBehaviour
	{
		// create a new instance
		GameObject instance = MonoBehaviour.Instantiate(prefab) as GameObject;

		// get the component
		T component = instance.GetComponent<T>();

		return component;
	}

	public static T CreateInstance<T>(GameObject prefab, Transform parent) where T : MonoBehaviour
	{
		// create a new instance
		GameObject instance = MonoBehaviour.Instantiate(prefab) as GameObject;
		instance.transform.SetParent(parent);

		// get the component
		T component = instance.GetComponent<T>();

		return component;
	}

	public static void StartWhileLoopCounter()
	{
		whileLoopCounter = 0;
	}

	public static bool WhileLoopTick(bool errorOnInsane = true)
	{
		if (Debug.isDebugBuild)
			errorOnInsane = false;

		whileLoopCounter++;
		if (whileLoopCounter >= WHILE_LOOP_INSANITY)
		{
			if (errorOnInsane)
			{
				Debug.LogError("Insanity in while loop");
			}
			else
			{
				Debug.LogWarning("Insanity in while loop");
			}
			return false;
		}
		return true;
	}

	public static void SetTimeScale(float t)
	{
		Time.timeScale = t;
	}

	//	public static void ShowPopup(string text, bool pause = false, Action callback = null)
	//	{
	//		Action modalCallback = null;
	//		if(pause)
	//		{
	//			modalCallback = TimeScaleOne;
	//		}
	//
	//		PopupDataStoryInfo data = new PopupDataStoryInfo("title", L.Get(text), callback, modalCallback);
	//
	//		//PopupStoryInfo popup = PopupManager.Instance.ShowPopupOnStack<PopupStoryInfo>(data);
	//		PopupManager.Instance.Enqueue(data);
	//
	//		//popup.transform.SetPositionX(700f); //HACK
	//
	//		//return popup.transform;
	//	}

	//	public static void ShowPopupTwoButtons(string text, string buttonOneText, string buttonTwoText, bool pause, Action callback = null, Action callbackTwo = null)
	//	{
	//		Action modalCallback = null;
	//		if (pause)
	//		{
	//			TimeScaleZero();
	//			modalCallback = TimeScaleOne;
	//		}
	//
	//		PopupDataStoryInfoTwoButtons data = new PopupDataStoryInfoTwoButtons(text, buttonOneText, buttonTwoText, callback, callbackTwo, modalCallback);
	//
	//		//PopupStoryInfoTwoButtons popup = PopupManager.Instance.ShowPopupOnStack<PopupStoryInfoTwoButtons>(data);
	//		PopupManager.Instance.Enqueue(data);
	//
	//		//popup.transform.SetPositionX(700f); //HACK
	//	}

	public static void TimeScaleOne()
	{
		Time.timeScale = 1f;
	}

	public static void TimeScaleZero()
	{
		Time.timeScale = 0f;
	}

	public static void ShuffleFisherYates<T>(List<T> list)
	{
		int n = list.Count;
		for (int i = 0; i < n; i++)
		{
			int r = i + RandomIntFromRange(0, n - i);
			T t = list[r];
			list[r] = list[i];
			list[i] = t;
		}
	}

	// NOTE: this code might seem more complicated than necessary, but using a StringBuilder is the most efficient way to handle this,
	// as it allocates very little memory compared to doing normal string concatenation and string.format calls, which are expensive and allocate a lot of memory
	public static string FormatTimeHoursMinutes(int timeInMinutes, bool regularTime = true)
	{
		// clear the StringBuilder
		s_stringBuilder.Length = 0;

		int hours = ((timeInMinutes % 1440) / 60); // keep in the range of [0,23]
		int minutes = timeInMinutes % 60;

		string postFix = "";

		if (hours == 12) // special case, if hours are exactly 12, is 12 PM
		{
			s_stringBuilder.Append("12");
			postFix = regularTime ? "PM" : "AM";
		}
		else if (hours == 0) // special case, if hours are exactly 0, is 12 AM
		{
			s_stringBuilder.Append("12");
			postFix = regularTime ? "AM" : "PM";
		}
		else if (hours > 11) // anything over 12 and we need to subtract 12 from the hours, and mark as PM
		{
			s_stringBuilder.Append((hours - 12).ToString());
			postFix = regularTime ? "PM" : "AM";
		}
		else // all other cases are AM
		{
			s_stringBuilder.Append(hours.ToString());
			postFix = regularTime ? "AM" : "PM";
		}

		s_stringBuilder.Append(":");

		if (minutes < 10)
		{
			s_stringBuilder.Append("0");
		}

		s_stringBuilder.Append(minutes.ToString());

		s_stringBuilder.Append(" ");
		s_stringBuilder.Append(postFix);

		return s_stringBuilder.ToString();
	}

}