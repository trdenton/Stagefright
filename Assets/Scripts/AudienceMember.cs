using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceMember : MonoBehaviour {

	public float bobFrequency = 1.0f;	//number of seconds between bobs, roughly
	public float bobAmplitude = 1.0f;	//how hard they bob
	public float settleTime = 2.0f;

	private float bobTime;
	private bool bobbing=false;
	private Vector3 startPosition;
	// Use this for initialization

	void SetNextBobTime()
	{
		bobTime = Time.time + Random.Range(1.0f,settleTime);
	}

	void Start () {
		bobbing = false;
		SetNextBobTime ();
		startPosition = transform.position;
		transform.localScale = new Vector3(Random.Range (0.8f, 1.2f),Random.Range (0.8f, 1.2f),Random.Range (0.8f, 1.2f));
	}
	
	// Update is called once per frame
	void Update () {
		HandleBobbing ();
		HandleMeandering ();
	}

	void HandleMeandering()
	{

	}

	void HandleBobbing()
	{
		if (bobbing) {

			float dt = Time.time - bobTime;
			float dY = bobAmplitude*Mathf.Sin (2.0f * bobFrequency * Mathf.PI * dt);

			if (dY <= 0f) {
				bobbing = false;	//we are done bobbing
				transform.position=startPosition;
				SetNextBobTime ();

			} else {
				transform.position = startPosition + new Vector3 (0.0f, dY, 0.0f);
			}
		}
		else
		{
			if (Time.time > bobTime) {
				bobbing = true;
			}
		}
	}
}
