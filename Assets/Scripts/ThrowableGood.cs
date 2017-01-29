using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableGood : MonoBehaviour {

	private float startX,endX;
	private float startY,endY;
	private float startTime,flightTime,endTime;
	private float arcHeight;

	private float A,B,C;

	public float maxHeight;
	private Vector3 startScale;

	private float t;
	// Use this for initialization
	void Start () {
		startTime = Time.time;

		flightTime = Random.Range (0.5f, 1.0f);

		endTime = startTime + flightTime;

		arcHeight = maxHeight;
		startY = transform.position.y;
		startX = transform.position.x;
		endX = GameObject.Find ("Player/torso/head").transform.position.x;
		endY = GameObject.Find ("Player/torso/head").transform.position.y;
		startScale = transform.localScale;

		float y0 = startY;
		float y1 = startY+arcHeight;
		float y2 = endY;

		C = y0;
		B = -(y2 - 4f * y1 + 3f * y0);
		A = (y2 - 2f * y1 + y0) / 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		//follow arc -x^2
		float halfTime = startTime + flightTime/2f;
		//y
		//t goes from 0 to 1
		t = (Time.time - startTime)/flightTime;
		//float deltaY = arcHeight - (arcHeight*4f) * (t - 0.5f) * (t - 0.5f);


		float deltaY = A * t * t + B * t + C;


		if (t>=1.0f) {
			gameObject.DestroySelf ();
		}

		float deltaX = t * (endX - startX);
		this.transform.position = new Vector3 (startX+deltaX,deltaY,0);

		//from 0 to 1

		this.transform.localScale = startScale * (2.0f-t);
	}




}
