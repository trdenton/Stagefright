using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfidenceBar : MonoBehaviour {

	public float confidenceMax=100.0f;
	public float confidence=50.0f;
	private GameObject greenBar,redBar;

	private Vector3 startScale;
	// Use this for initialization
	void Start () {

		greenBar = transform.Find ("greenBar").gameObject;
		redBar = transform.Find ("redBar").gameObject;

		greenBar.GetComponent<SpriteRenderer> ().color = Color.green;
		redBar.GetComponent<SpriteRenderer> ().color = Color.red;
		startScale = greenBar.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (confidence <= 0.0f) {
			//Debug.Log ("YOU HAVE LOST!!!");
		}


		float scaleX = confidence / 100.0f * startScale.x;
		Vector3 scale = new Vector3 (scaleX,startScale.y,startScale.z);

		greenBar.transform.localScale = scale;

	}

	public void IncreaseConfidence(float c)
	{
		confidence += c;
		confidence = Mathf.Clamp (confidence, 0, confidenceMax);

	}

	public void DecreaseConfidence(float c)
	{
		IncreaseConfidence (-c);
	}

	public float GetConfidence()
	{
		return confidence;
	}


}
