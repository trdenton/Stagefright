using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{

		//determine if we want left arm or right arm

		Transform torso = transform.Find ("torso");


		Vector2 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);        //Mouse position
		Vector3 objpos = Camera.main.WorldToViewportPoint (torso.position);        //Object position on screen
		Vector2 relobjpos = new Vector2(objpos.x - 0.5f,objpos.y - 0.5f);            //Set coordinates relative to object
		Vector2 relmousepos = new Vector2 (mouse.x - 0.5f,mouse.y - 0.5f) - relobjpos;
		float angle = Vector2.Angle (Vector2.up, relmousepos);    //Angle calculation


		Rigidbody2D arm;
		Rigidbody2D foreArm;
		if (relmousepos.x > 0) {	//if we are on the right side
			foreArm = transform.Find("torso/arm_r/forearm_r").GetComponent<Rigidbody2D>();
			arm = transform.Find("torso/arm_r").GetComponent<Rigidbody2D>();
			angle = 180 - angle;

			//Debug.Log ("Right side");
		} else {
			foreArm = transform.Find("torso/arm_l/forearm_l").GetComponent<Rigidbody2D>();
			arm = transform.Find("torso/arm_l").GetComponent<Rigidbody2D>();
			//Debug.Log ("Left Side!");
			angle -= 180;
		}
		//angle += 90;
		Debug.Log ("angle is " + angle);

		//Quaternion quat = Quaternion.identity;
		//quat.eulerAngles = new Vector3(0,0,angle); //Changing angle
		arm.rotation = angle;

		//Quaternion fQuat = Quaternion.identity;
		//fQuat.eulerAngles = new Vector3 (0, 0, 0);
		foreArm.rotation = angle;

	}
}
