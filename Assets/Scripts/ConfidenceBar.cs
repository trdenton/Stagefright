using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfidenceBar : MonoBehaviour {

	public float confidenceMax=100.0f;
	public float confidence=50.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
