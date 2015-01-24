using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		int speed;
		speed = 60;
		Vector3 movement = new Vector3 ( 0,0,(Input.acceleration.y * speed));
		rigidbody.AddForce (movement);
	}
}
