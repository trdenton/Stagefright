using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableGenerator: MonoBehaviour {


	public GameObject[] items;


	public float throwPeriod;

	// Use this for initialization
	void Start () {
//		int spriteIndex = Random.Range (0, items.Length);
//		SpriteRenderer item = items [spriteIndex].GetComponent<SpriteRenderer> ();
//		gameObject.AddCompondent (item);

		GenerateObject ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			GenerateObject ();
		}
	}


	void GenerateObject()
	{
		int spriteIndex = Random.Range (0, items.Length);
		GameObject newObject = Instantiate (items[spriteIndex]);


		float xStart = Random.Range (-8f, 8f);
		float yStart = Random.Range (-5f, -2.5f);

		//Vector3 thePoint = Camera.main.ViewportToWorldPoint (new Vector3(xStart,yStart,transform.position.z));

		Vector3 point = new Vector3 (xStart,yStart, transform.position.z);

		Debug.Log ("Generating at " + point);

		newObject.transform.position = point;
	}
}
